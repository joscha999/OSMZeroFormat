using System;
using System.Collections.Generic;
using System.Text;

namespace OSMData.Query {
	public class WayQuerry {
		private readonly Way Way;

		public WayQuerry(Way way) {
			Way = way;
		}

		private HighwayQuerry _highway;
		public HighwayQuerry Highway => _highway ?? (_highway = new HighwayQuerry(Way));
	}
}