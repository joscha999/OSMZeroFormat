using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSMData.Query {
	public class HighwayQuerry {
		private readonly Way Way;

		private readonly Dictionary<string, bool> TagCache = new Dictionary<string, bool>();

		public HighwayQuerry(Way way) {
			Way = way;
		}

		public static implicit operator bool(HighwayQuerry hq) => hq?.InternalIsHighway() ?? false;

		private bool? IsHighway;
		private bool InternalIsHighway()
			=> IsHighway ?? (IsHighway = Way.AppliedTags.Any(t => t.Key == Tags.Highway)).Value;

		private bool GetCached(string key, string tag) {
			if (TagCache.TryGetValue(key, out var b))
				return b;

			b = Way.AppliedTags.Any(t => t.Is(Tags.Highway, tag));
			TagCache.Add(key, b);
			return b;
		}

		public bool Path => GetCached(nameof(Path), Tags.Path);
		public bool Cycleway => GetCached(nameof(Cycleway), Tags.Cycleway);
		public bool Footway => GetCached(nameof(Footway), Tags.Footway);
		public bool Pedestrian => GetCached(nameof(Pedestrian), Tags.Pedestrian);
		public bool Residential => GetCached(nameof(Residential), Tags.Residential);

		public bool UnderConstruction => GetCached(nameof(UnderConstruction), Tags.Construction);
		public bool Proposed => GetCached(nameof(Proposed), Tags.Proposed);

		public bool Motorway => GetCached(nameof(Motorway), Tags.Motorway);
		public bool Trunk => GetCached(nameof(Trunk), Tags.Trunk);
		public bool Primary => GetCached(nameof(Primary), Tags.Primary);
		public bool Secondary => GetCached(nameof(Secondary), Tags.Secondary);

		public bool MotorwayLink => GetCached(nameof(MotorwayLink), Tags.MotorwayLink);
		public bool TrunkLink => GetCached(nameof(TrunkLink), Tags.TrunkLink);
		public bool PrimaryLink => GetCached(nameof(PrimaryLink), Tags.PrimaryLink);
		public bool SecondaryLink => GetCached(nameof(SecondaryLink), Tags.SecondaryLink);
	}
}