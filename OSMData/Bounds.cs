using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OSMData {
	[XmlRoot("bounds")]
	public class Bounds {
		[XmlAttribute("minlat")]
		public float MinLatitude { get; set; }
		[XmlAttribute("maxlat")]
		public float MaxLatitude { get; set; }

		[XmlAttribute("minlon")]
		public float MinLongitude { get; set; }
		[XmlAttribute("maxlon")]
		public float MaxLongitude { get; set; }

		public ZeroFormatOSM.Bounds ToZero() => new ZeroFormatOSM.Bounds {
			MinLatitude = MinLatitude,
			MaxLatitude = MaxLatitude,
			MinLongitude = MinLongitude,
			MaxLongitude = MaxLongitude
		};
	}
}