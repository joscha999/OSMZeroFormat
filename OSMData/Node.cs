using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ZeroFormatOSM;

namespace OSMData {
	[XmlRoot("node")]
	public class Node {
		[XmlAttribute("id")]
		public ulong Id { get; set; }

		[XmlAttribute("lat")]
		public float Latitude { get; set; }
		[XmlAttribute("lon")]
		public float Longitude { get; set; }

		[XmlElement("tag")]
		public List<Tag> Tags { get; set; }

		[XmlIgnore]
		public Dictionary<ulong, Way> Ways { get; set; } = new Dictionary<ulong, Way>();

		public ZeroFormatOSM.Node ToZero(ZeroOSM zosm) => new ZeroFormatOSM.Node {
			Id = Id,
			Latitude = Latitude,
			Longitude = Longitude,
			Tags = Tags.ConvertAll(t => t.ToZero(zosm))
		};
	}
}