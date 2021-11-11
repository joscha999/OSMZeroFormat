using OSMData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ZeroFormatOSM;

namespace OSMData {
	[XmlRoot("way")]
	public class Way {
		[XmlAttribute("id")]
		public ulong Id { get; set; }

		[XmlElement("nd")]
		public List<NodeReference> NodeReferences { get; set; }

		[XmlElement("tag")]
		public List<Tag> AppliedTags { get; set; }

		private WayQuerry _is;
		public WayQuerry Is => _is ?? (_is = new WayQuerry(this));

		public ZeroFormatOSM.Way ToZero(ZeroOSM zosm) => new ZeroFormatOSM.Way {
			Id = Id,
			NodeIds = NodeReferences.ConvertAll(nr => nr.NodeId),
			Tags = AppliedTags.ConvertAll(t => t.ToZero(zosm))
		};
	}
}