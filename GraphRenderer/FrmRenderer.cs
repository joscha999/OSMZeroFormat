using OSMData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using System.Numerics;
using SkiaSharp;
using SpatialTrees;
using RBush;

namespace GraphRenderer {
	public partial class FrmRenderer : Form {
		private class TreeNode : IGenericNode<Node> {
			public Node Data { get; set; }
			public Vector2 Position { get; set; }
			public QuadTree<Node, IGenericNode<Node>> QuadTree { get; set; }
		}

		private class RTreeNode : ISpatialData {
			private readonly Envelope _envelope;
			public ref readonly Envelope Envelope => ref _envelope;

			public Node Node { get; }

			public RTreeNode(Node n, float lat, float lon) {
				_envelope = new Envelope(lat, lon, lat, lon);
				Node = n;
			}
		}

		//private QuadTree<Node, IGenericNode<Node>> QuadTree;
		private RBush<RTreeNode> RTree;

		private OSM OSM = new OSM {
			Nodes = new(),
			Ways = new(),
			Relations = new(),
			RequiredNodes = new(),
			NodeCache = new(),
			WayCache = new()
		};

		private bool IsPanning;
		private Point PanStart;

		private double LastRenderTime;
		private double LastFrameTime;

		private double CurrMouseLatitude;
		private double CurrMouseLongitude;

		public FrmRenderer() {
			InitializeComponent();

			LoadData();

			GLCanvas.MouseWheel += (_, e) => {
				double scaleFactor = 1;

				if (e.Delta > 0)
					scaleFactor = 2;
				if (e.Delta < 0)
					scaleFactor = 0.5d;

				var dX = SkiaCanvas.Width / 2;
				var dY = SkiaCanvas.Height / 2;

				var pX = (double)e.X / SkiaCanvas.Width;
				var pY = (double)e.Y / SkiaCanvas.Height;

				var dLeft = pX * dX;
				var dUp = pY * dY;

				if (scaleFactor > 1) {
					var llDiff = PointToLatLonScaled(new Point((int)dLeft, (int)(dY - dUp)), MapScale);
					MapOffset += new Vector2((float)llDiff.lat, (float)llDiff.lon);
				}

				if (scaleFactor < 1) {
					var llDiff = PointToLatLonScaled(new Point((int)(dLeft), (int)(dY - dUp)), MapScale * scaleFactor);
					MapOffset -= new Vector2((float)llDiff.lat, (float)llDiff.lon);
				}

				MapScale *= scaleFactor;
				Refresh();
			};

			GLCanvas.MouseDown += (_, e) => {
				if (e.Button != MouseButtons.Left)
					return;

				IsPanning = true;
				PanStart = e.Location;
			};

			GLCanvas.MouseMove += (_, e) => {
				var (lat, lon) = PointToLatLonMap(new Point(e.X, -e.Y + SkiaCanvas.Height));
				CurrMouseLatitude = lat;
				CurrMouseLongitude = lon;

				UpdateDebugLabel();

				if (IsPanning && e.Button == MouseButtons.Left) {
					var diffX = (PanStart.X - e.Location.X) / MapScale;
					var diffY = (PanStart.Y - e.Location.Y) / MapScale;

					var ll = PointToLatLon(new Point((int)diffX, (int)diffY));
					MapOffset -= new Vector2((float)ll.lat, -(float)ll.lon);
					PanStart = e.Location;

					Refresh();
				}
			};

			GLCanvas.MouseUp += (_, e) => {
				if (e.Button == MouseButtons.Left) {
					IsPanning = false;
					PanStart = Point.Empty;
				}
			};

			GLCanvas.PaintSurface += (_, e) => RenderSK(e.Surface.Canvas);
		}

		private void LoadData() {
			var file = "SaltLakeCity.osm";
			//var file = "luxembourg-latest.osm";

			var nodeSerializer = new XmlSerializer(typeof(Node));
			var waySerializer = new XmlSerializer(typeof(Way));
			var relationSerializer = new XmlSerializer(typeof(Relation));

			using var sr = new StreamReader(file);
			using var reader = XmlReader.Create(sr);

			reader.Read();
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
				reader.Read();

			do {
				switch (reader.Name) {
					case "bounds":
						var boundsSerializer = new XmlSerializer(typeof(Bounds));
						OSM.Bounds = (Bounds)boundsSerializer.Deserialize(reader.ReadSubtree());
						break;
					case "node":
						var n = (Node)nodeSerializer.Deserialize(reader);
						break;
					case "way":
						var w = (Way)waySerializer.Deserialize(reader);
						if (ShouldInclude(w)) {
							OSM.Ways.Add(w);
							OSM.WayCache.Add(w.Id, w);

							foreach (var nRef in w.NodeReferences)
								OSM.RequiredNodes.Add(nRef.NodeId);
						}

						break;
					case "relation":
						var r = (Relation)relationSerializer.Deserialize(reader);
						break;
					default:
						if (string.IsNullOrWhiteSpace(reader.Name) || reader.Name == "osm")
							break;

						Console.WriteLine("Could not deserialize node of type " + reader.Name);
						break;
				}
			} while (reader.Read());

			reader.Close();
			sr.Close();

			using var sr2 = new StreamReader(file);
			using var reader2 = XmlReader.Create(sr2);

			reader2.Read();
			if (reader2.NodeType == XmlNodeType.XmlDeclaration)
				reader2.Read();

			do {
				switch (reader2.Name) {
					case "bounds":
						var boundsSerializer = new XmlSerializer(typeof(Bounds));
						break;
					case "node":
						var n = (Node)nodeSerializer.Deserialize(reader2);
						if (OSM.RequiredNodes.Contains(n.Id)) {
							OSM.Nodes.Add(n);
							OSM.NodeCache.Add(n.Id, n);
						}

						break;
					case "way":
						var w = (Way)waySerializer.Deserialize(reader2);
						break;
					case "relation":
						var r = (Relation)relationSerializer.Deserialize(reader2);
						break;
					default:
						if (string.IsNullOrWhiteSpace(reader2.Name) || reader2.Name == "osm")
							break;

						Console.WriteLine("Could not deserialize node of type " + reader2.Name);
						break;
				}
			} while (reader2.Read());

			//build node => way lookup
			foreach (var w in OSM.Ways) {
				foreach (var n in w.NodeReferences) {
					if (OSM.NodeCache.TryGetValue(n.NodeId, out var node)
						&& !node.Ways.ContainsKey(w.Id)) {
						node.Ways.Add(w.Id, w);
					}
				}
			}

			var visibleMin = PointToLatLonMap(new Point(0, 0));
			var visibleMax = PointToLatLonMap(new Point(SkiaCanvas.Width, SkiaCanvas.Height));
			float visibleLat = (float)(visibleMax.lat - visibleMin.lat);
			float visibleLon = (float)(visibleMax.lon - visibleMin.lon);

			MapOffset = new Vector2(
				OSM.Bounds.MinLatitude + ((OSM.Bounds.MaxLatitude - OSM.Bounds.MinLatitude) / 2) - (visibleLat / 2),
				OSM.Bounds.MinLongitude + ((OSM.Bounds.MaxLongitude - OSM.Bounds.MinLongitude) / 2) - (visibleLon / 2));

			var osmLat = OSM.Bounds.MaxLatitude - OSM.Bounds.MinLatitude;
			var osmLon = OSM.Bounds.MaxLongitude - OSM.Bounds.MinLongitude;

			//QuadTree = new QuadTree<Node, IGenericNode<Node>>(4, 20, new BoundingBox(
			//	(float)(osmLat / 2 + OSM.Bounds.MinLatitude),
			//	(float)(osmLon / 2 + OSM.Bounds.MinLongitude),
			//	Math.Max(osmLat / 2, osmLon / 2)));

			//QuadTree = new QuadTree<Node, IGenericNode<Node>>(4, 100, new BoundingBox(0, 0, 180));
			RTree = new RBush<RTreeNode>();

			//foreach (var n in OSM.Nodes) {
			//	QuadTree.Insert(new TreeNode {
			//		Data = n,
			//		Position = new Vector2(n.Latitude, n.Longitude)
			//	});
			//}

			RTree.BulkLoad(OSM.Nodes.Select(n => new RTreeNode(n, n.Latitude, n.Longitude)));
		}

		private Dictionary<(SKColor, int), SKPaint> PaintCache = new();

		private void RenderSK(SKCanvas canvas) {
			var sw = Stopwatch.StartNew();
			canvas.Clear(SKColors.White);

			var pMin = LatLonMapToPoint(OSM.Bounds.MinLatitude, OSM.Bounds.MinLongitude);
			var pMax = LatLonMapToPoint(OSM.Bounds.MaxLatitude, OSM.Bounds.MaxLongitude);

			var exportedRect = new SKRect(pMin.X, -pMin.Y + SkiaCanvas.Height - (pMax.Y - pMin.Y),
				pMax.X, -pMax.Y + SkiaCanvas.Height + (pMax.Y - pMin.Y));

			canvas.DrawRect(exportedRect, new SKPaint { Color = SKColors.Red, StrokeWidth = 2, IsStroke = true });

			//DrawQT(QuadTree, canvas);

			var visibleMin = PointToLatLonMap(new Point(0, 0));
			var visibleMax = PointToLatLonMap(new Point(SkiaCanvas.Width, SkiaCanvas.Height));
			var visibleWidth = visibleMax.lat - visibleMin.lat;
			var visibleHeight = visibleMax.lon - visibleMin.lon;

			//var nodes = QuadTree.Query(new BoundingBox(
			//	(float)(visibleWidth / 2 + visibleMin.lat),
			//	(float)(visibleHeight / 2 + visibleMin.lon),
			//	(float)Math.Max(visibleWidth / 2, visibleHeight / 2)));

			var nodes = RTree.Search(new Envelope(visibleMin.lat, visibleMin.lon, visibleMax.lat, visibleMax.lon)).ToList();

			var alreadyRendered = new HashSet<ulong>();
			var totalRenderTime = TimeSpan.Zero;

			foreach (var n in nodes) {
				foreach (var kvp in n.Node.Ways) {
					if (alreadyRendered.Contains(kvp.Key))
						continue;
					alreadyRendered.Add(kvp.Key);

					if (!ShouldRender(kvp.Value))
						continue;

					totalRenderTime += RenderWay(canvas, kvp.Value);
				}
			}

			sw.Stop();
			Debug.WriteLine($"Total: {sw.ElapsedMilliseconds}ms, Render: {totalRenderTime.TotalMilliseconds}ms");
			LastRenderTime = totalRenderTime.TotalMilliseconds;
			LastFrameTime = sw.ElapsedMilliseconds;

			UpdateDebugLabel();
		}

		private void RenderSKCached(SKCanvas canvas) {
			var visibleMin = PointToLatLonMap(new Point(0, 0));
			var visibleMax = PointToLatLonMap(new Point(SkiaCanvas.Width, SkiaCanvas.Height));
			var visibleWidth = visibleMax.lat - visibleMin.lat;
			var visibleHeight = visibleMax.lon - visibleMin.lon;

			canvas.Clear(SKColors.White);

			for (int x = 0; x < SkiaCanvas.Width; x += 100) {
				for (int y = 0; y < SkiaCanvas.Height; y += 100) {
					var rect = new SKRect(x, y, x + 100, y + 100);

					canvas.DrawRect(rect, new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsStroke = true });
				}
			}
		}

		private bool ShouldRender(Way w) {
			if (MapScale <= 0.25d && w.Is.Highway.Residential)
				return false;

			return true;
		}

		private List<Point> Points = new List<Point>();
		private Stopwatch SWRender = new Stopwatch();

		private TimeSpan RenderWay(SKCanvas canvas, Way way) {
			Points.Clear();
			SWRender.Reset();

			var totalRenderTime = TimeSpan.Zero;

			foreach (var nRef in way.NodeReferences) {
				if (!OSM.NodeCache.TryGetValue(nRef.NodeId, out var node))
					continue;

				Points.Add(LatLonMapToPoint(node.Latitude, node.Longitude));
			}

			for (int i = 0; i < Points.Count - 1; i++) {
				var c = SKColorForWay(way);
				var w = (int)(WidthForWay(way) * MapScale);

				if (!PaintCache.TryGetValue((c, w), out var paint)) {
					paint = new SKPaint { Color = c, StrokeWidth = w, IsStroke = true };
					PaintCache.Add((c, w), paint);
				}

				paint.PathEffect = SKPathEffect.CreateDash(Array.Empty<float>(), 20);

				SWRender.Reset();
				SWRender.Start();
				canvas.DrawLine(Points[i].X, -Points[i].Y + SkiaCanvas.Height,
					Points[i + 1].X, -Points[i + 1].Y + SkiaCanvas.Height, paint);
				SWRender.Stop();
				totalRenderTime += SWRender.Elapsed;
			}

			return totalRenderTime;
		}

		private SKPaint QTPaint = new SKPaint { Color = SKColors.Blue, StrokeWidth = 2, IsStroke = true };

		private void DrawQT(QuadTree<Node, IGenericNode<Node>> qt, SKCanvas canvas) {
			var pMinQT = LatLonMapToPoint(qt.Boundary.Left, qt.Boundary.Top);
			var pMaxQT = LatLonMapToPoint(qt.Boundary.Right, qt.Boundary.Bottom);

			var QTRect = new SKRect(pMinQT.X, -pMinQT.Y + SkiaCanvas.Height - (pMaxQT.Y - pMinQT.Y),
				pMaxQT.X, -pMaxQT.Y + SkiaCanvas.Height + (pMaxQT.Y - pMinQT.Y));

			canvas.DrawRect(QTRect, QTPaint);

			if (qt.NorthWest != null)
				DrawQT(qt.NorthWest, canvas);
			if (qt.NorthEast != null)
				DrawQT(qt.NorthEast, canvas);
			if (qt.SouthWest != null)
				DrawQT(qt.SouthWest, canvas);
			if (qt.SouthEast != null)
				DrawQT(qt.SouthEast, canvas);
		}

		private bool ShouldInclude(Way w) {
			if (!w.Is.Highway)
				return false;
			if (w.Is.Highway.Proposed)
				return false;
			if (w.Is.Highway.Path || w.Is.Highway.Cycleway || w.Is.Highway.Footway)
				return false;

			return true;
		}

		private const double SmA = 6378137.0;
		private double MapScale = 1d;
		private Vector2 MapOffset = Vector2.Zero;

		private Point LatLonMapToPoint(double lat, double lon) {
			lat = (lat - MapOffset.X) / 180 * Math.PI * MapScale;
			lon = (lon - MapOffset.Y) / 180 * Math.PI * MapScale;

			return new Point(
				(int)(SmA * lon),
				(int)(SmA * Math.Log((Math.Sin(lat) + 1) / Math.Cos(lat)))
			);
		}

		private (double lat, double lon) PointToLatLonMap(Point p) {
			return (
				((2 * Math.Atan(Math.Exp(p.Y / MapScale / SmA))) - (Math.PI / 2)) * (180 / Math.PI) + MapOffset.X,
				(p.X / MapScale / SmA) * (180 / Math.PI) + MapOffset.Y
			);
		}

		private (double lat, double lon) PointToLatLonScaled(Point p, double scale) {
			return (
				((2 * Math.Atan(Math.Exp(p.Y / scale / SmA))) - (Math.PI / 2)) * (180 / Math.PI),
				(p.X / scale / SmA) * (180 / Math.PI)
			);
		}

		private (double lat, double lon) PointToLatLon(Point p) {
			return (
				((2 * Math.Atan(Math.Exp(p.Y / SmA))) - (Math.PI / 2)) * (180 / Math.PI),
				(p.X / SmA) * (180 / Math.PI)
			);
		}

		private static Color MotorwayColor = Color.FromArgb(226, 122, 143);
		private static Color TrunkColor = Color.FromArgb(249, 178, 156);
		private static Color PrimaryColor = Color.FromArgb(252, 206, 144);
		private static Color SecondaryColor = Color.FromArgb(244, 251, 173);

		private Color ColorForWay(Way w) {
			if (w.Is.Highway.Motorway || w.Is.Highway.MotorwayLink)
				return MotorwayColor;
			if (w.Is.Highway.Trunk || w.Is.Highway.TrunkLink)
				return TrunkColor;
			if (w.Is.Highway.Primary || w.Is.Highway.PrimaryLink)
				return PrimaryColor;
			if (w.Is.Highway.Secondary || w.Is.Highway.SecondaryLink)
				return SecondaryColor;

			return Color.Black;
		}

		private static SKColor SKMotorwayColor = new SKColor(226, 122, 143);
		private static SKColor SKTrunkColor = new SKColor(249, 178, 156);
		private static SKColor SKPrimaryColor = new SKColor(252, 206, 144);
		private static SKColor SKSecondaryColor = new SKColor(244, 251, 173);

		private SKColor SKColorForWay(Way w) {
			if (w.Is.Highway.Motorway || w.Is.Highway.MotorwayLink)
				return SKMotorwayColor;
			if (w.Is.Highway.Trunk || w.Is.Highway.TrunkLink)
				return SKTrunkColor;
			if (w.Is.Highway.Primary || w.Is.Highway.PrimaryLink)
				return SKPrimaryColor;
			if (w.Is.Highway.Secondary || w.Is.Highway.SecondaryLink)
				return SKSecondaryColor;
			if (w.Is.Highway.Pedestrian)
				return SKColors.LightGray;

			return SKColors.Black;
		}

		private double WidthForWay(Way w) {
			if (w.Is.Highway.Motorway || w.Is.Highway.MotorwayLink)
				return 5;
			if (w.Is.Highway.Trunk || w.Is.Highway.TrunkLink)
				return 5;
			if (w.Is.Highway.Primary || w.Is.Highway.PrimaryLink)
				return 3;
			if (w.Is.Highway.Secondary || w.Is.Highway.SecondaryLink)
				return 3;

			return 1.5;
		}

		private void UpdateDebugLabel() {
			LblInfo.Text = $"Latitude: {CurrMouseLatitude}, Longitude: {CurrMouseLongitude};{Environment.NewLine}" +
				$"FrameTime: {LastFrameTime}ms, RenderTime: {LastRenderTime}ms;{Environment.NewLine}" +
				$"MapScale: {MapScale};";
		}
	}
}