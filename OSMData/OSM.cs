using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ZeroFormatOSM;
using ZeroFormatter;

namespace OSMData {
	[XmlRoot("osm")]
	public class OSM {
		[XmlElement("bounds")]
		public Bounds Bounds { get; set; }

		[XmlElement("node")]
		public List<Node> Nodes { get; set; }

		[XmlElement("way")]
		public List<Way> Ways { get; set; }

		[XmlElement("relation")]
		public List<Relation> Relations { get; set; }

		[XmlIgnore]
		public HashSet<ulong> RequiredNodes { get; set; }
		[XmlIgnore]
		public Dictionary<ulong, Node> NodeCache { get; set; }
		[XmlIgnore]
		public Dictionary<ulong, Way> WayCache { get; set; }

		public ZeroOSM ToZero() {
			var zosm = new ZeroOSM {
				Bounds = Bounds.ToZero(),
			};

			zosm.Nodes = Nodes.Select(n => n.ToZero(zosm)).ToDictionary(n => n.Id, n => n);
			zosm.Ways = Ways.Select(w => w.ToZero(zosm)).ToDictionary(w => w.Id, w => w);
			zosm.Relations = Relations.Select(r => r.ToZero(zosm)).ToDictionary(r => r.Id, r => r);

			return zosm;
		}
	}
}