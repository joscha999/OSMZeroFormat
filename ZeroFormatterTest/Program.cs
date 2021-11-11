using OSMData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ZeroFormatOSM.TagConvertion;

namespace ZeroFormatterTest {
	public class Program {
		private static readonly HashSet<string> UnknownElements = new HashSet<string>();
		private static readonly HashSet<string> UnknownAttributes = new HashSet<string>();

		private static readonly HashSet<string> IgnoredUnknownAttributes = new HashSet<string> {
			"version", "generator", "copyright", "attribution", "license", "visible",
			"changeset", "timestamp", "user", "uid", "meta", "note"
		};

		public static void Main(string[] args) {
			var file = "SaltLakeCity.osm";
			var fileOut = file + ".zero";

			var serializer = new XmlSerializer(typeof(OSM));

			var nodeSerializer = new XmlSerializer(typeof(Node));
			var waySerializer = new XmlSerializer(typeof(Way));
			var relationSerializer = new XmlSerializer(typeof(Relation));

			serializer.UnknownElement += SerializerUnknownElement;
			serializer.UnknownAttribute += SerializerUnknownAttribute;
			var osm = new OSM {
				Nodes = new(),
				Ways = new(),
				Relations = new(),
				RequiredNodes = new()
			};

			using var sr = new StreamReader(file);
			using var reader = XmlReader.Create(sr);

			reader.Read();
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
				reader.Read();

			var sw = Stopwatch.StartNew();
			//osm = (OSM)serializer.Deserialize(sr);

			do {
				switch (reader.Name) {
					case "bounds":
						var boundsSerializer = new XmlSerializer(typeof(Bounds));
						osm.Bounds = (Bounds)boundsSerializer.Deserialize(reader.ReadSubtree());
						break;
					case "node":
						var n = (Node)nodeSerializer.Deserialize(reader);
						break;
					case "way":
						var w = (Way)waySerializer.Deserialize(reader);
						if (w.Is.Highway && (!w.Is.Highway.Proposed || !w.Is.Highway.UnderConstruction)) {
							osm.Ways.Add(w);

							foreach (var nRef in w.NodeReferences)
								osm.RequiredNodes.Add(nRef.NodeId);
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
						if (osm.RequiredNodes.Contains(n.Id))
							osm.Nodes.Add(n);

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

			sw.Stop();

			Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");
			Console.WriteLine("Writing TagTypes");
			File.WriteAllText(file + ".tagTypes", GenerateTagTypeCode(osm));

			Console.WriteLine("Converting ...");

			sw.Reset();
			sw.Start();
			var zosm = osm.ToZero();
			sw.Stop();
			Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");
			osm = null;

			Console.WriteLine("Saving ...");

			sw.Reset();
			sw.Start();
			File.WriteAllBytes(fileOut, ZeroFormatter.ZeroFormatterSerializer.Serialize(zosm));
			sw.Stop();
			zosm = null;

			Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");
			Console.WriteLine("Reading ...");

			sw.Reset();
			sw.Start();
			var zosmr = ZeroFormatter.ZeroFormatterSerializer.Deserialize<ZeroFormatOSM.ZeroOSM>(File.ReadAllBytes(fileOut));
			sw.Stop();
			zosmr = null;

			Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");
			Console.WriteLine("Done!");

			Console.ReadLine();
		}

		private static void SerializerUnknownAttribute(object sender, XmlAttributeEventArgs e) {
			if (IgnoredUnknownAttributes.Contains(e.Attr.Name))
				return;

			UnknownAttributes.Add(e.Attr.Name);
		}

		private static void SerializerUnknownElement(object sender, XmlElementEventArgs e)
			=> UnknownElements.Add(e.Element.ParentNode.Name + "." + e.Element.Name);

		private static void PrintUnknown() {
			Console.WriteLine("Unknown Elements:");
			foreach (var ue in UnknownElements)
				Console.WriteLine("\t" + ue);

			Console.WriteLine("Unknown Attributes:");
			foreach (var ua in UnknownAttributes)
				Console.WriteLine("\t" + ua);
		}

		private static void PrintTagKeys(OSM osm, bool unknownOnly = true) {
			Console.WriteLine("Tag Keys:");
			foreach (var tag in osm.Nodes.SelectMany(n => n.Tags)
				.Concat(osm.Relations.SelectMany(r => r.Tags))
				.Concat(osm.Ways.SelectMany(w => w.AppliedTags))
				.Select(t => t.Key)
				.Where(k => !unknownOnly || ZeroOSMTagTypeConverter.FromString(k) == ZeroFormatOSM.TagType.ZeroUnavailable)
				.Distinct()) {
				Console.WriteLine(tag);
			}
		}

		private static void PrintMemberTypes(OSM osm) {
			Console.WriteLine("Member Types:");
			foreach (var type in osm.Relations.SelectMany(r => r.Members).Select(m => m.Type)
				.Distinct()) {
				Console.WriteLine(type);
			}
		}

		private static void PrintMemberRoles(OSM osm) {
			Console.WriteLine("Member Roles:");
			foreach (var role in osm.Relations.SelectMany(r => r.Members).Select(m => m.Role)
				.Distinct()) {
				Console.WriteLine(role);
			}
		}

		private static string GenerateMemberRoleCode(OSM osm)
			=> TagTypeCodeGenerator.BuildMemberRoles(osm.Relations.SelectMany(r => r.Members)
				.Select(m => m.Role)
				.Where(r => ZeroOSMMemberRoleConverter.FromString(r) == ZeroFormatOSM.MemberRole.ZeroUnavailable)
				.Distinct());

		private static string GenerateTagTypeCode(OSM osm)
			=> TagTypeCodeGenerator.BuildTagTypes(osm.Nodes.SelectMany(n => n.Tags)
				.Concat(osm.Relations.SelectMany(r => r.Tags))
				.Concat(osm.Ways.SelectMany(w => w.AppliedTags))
				.Select(t => t.Key)
				.Where(k => ZeroOSMTagTypeConverter.FromString(k) == ZeroFormatOSM.TagType.ZeroUnavailable)
				.Distinct());
	}
}