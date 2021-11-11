using System;
using System.Collections.Generic;
using System.Text;

namespace OSMData {
	public static class Tags {
		//path types
		public const string Highway = "highway";
		public const string Path = "path";
		public const string Cycleway = "cycleway";
		public const string Footway = "footway";
		public const string Pedestrian = "pedestrian";

		//road misc
		public const string Construction = "construction";
		public const string Proposed = "proposed";

		//road types
		public const string Motorway = "motorway";
		public const string Trunk = "trunk";
		public const string Primary = "primary";
		public const string Secondary = "secondary";

		//road sub-types
		public const string MotorwayLink = "motorway_link";
		public const string TrunkLink = "trunk_link";
		public const string PrimaryLink = "primary_link";
		public const string SecondaryLink = "secondary_link";
		public const string Residential = "residential";
	}
}