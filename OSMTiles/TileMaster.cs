using OSMData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace OSMTiles {
	public class TileMaster {
		private string TileFolder;

		private Dictionary<string, TileState> TileStates = new();
		private Dictionary<string, Bitmap> BitmapCache = new();
		private BlockingCollection<TileCreateInfo> RenderQueue = new();

		private List<Thread> Workers = new();

		private OSM Data;

		public TileMaster(OSM data, string tileFolder, int workerCount = 1) {
			Data = data;
			TileFolder = tileFolder;
			Directory.CreateDirectory(TileFolder);

			for (int i = 0; i < workerCount; i++) {
				var t = new Thread(WorkerMethod);
				Workers.Add(t);
				t.Start();
			}
		}

		public Bitmap RequestTile(Bounds bounds, double mapScale) {
			var key = KeyForTile(bounds, mapScale);
			if (BitmapCache.TryGetValue(key, out var bmp))
				return bmp;
			if (TileStates.TryGetValue(key, out var ts) && ts >= TileState.Requested)
				return null;

			TileStates.Add(key, TileState.Requested);
			RenderQueue.Add(new TileCreateInfo { Key = key, Bounds = bounds, MapScale = mapScale });
			return null;
		}

		private string KeyForTile(Bounds b, double scale)
			=> $"s{(int)(scale * 1000)};{b.MinLatitude}-{b.MaxLatitude};{b.MinLongitude}-{b.MaxLongitude}";

		private void WorkerMethod() {
			while (true) {
				var createInfo = RenderQueue.Take();
				TileStates[createInfo.Key] = TileState.Rendering;

				using var bmp = new Bitmap(200, 200);
				using var g = Graphics.FromImage(bmp);

				g.DrawArc(Pens.Black, 25, 25, 50, 50, 0, 360);

				TileStates[createInfo.Key] = TileState.Saving;
				bmp.Save(Path.Combine(TileFolder, createInfo.Key + ".png"), ImageFormat.Png);

				BitmapCache.Add(createInfo.Key, bmp);
				TileStates[createInfo.Key] = TileState.Cached;
			}
		}

		private class TileCreateInfo {
			public string Key { get; init; }
			public Bounds Bounds { get; init; }
			public double MapScale { get; init; }
		}
	}

	public enum TileState {
		None,
		Requested,
		Rendering,
		Saving,
		Cached
	}
}