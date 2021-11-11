using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ZeroFormatOSM;

namespace OSMData {
	[XmlRoot("relation")]
	public class Relation {
		[XmlAttribute("id")]
		public ulong Id { get; set; }

		[XmlElement("member")]
		public List<Member> Members { get; set; }

		[XmlElement("tag")]
		public List<Tag> Tags { get; set; }

		public ZeroFormatOSM.Relation ToZero(ZeroOSM zosm) => new ZeroFormatOSM.Relation {
			Id = Id,
			Members = Members.ConvertAll(m => m.ToZero()),
			Tags = Tags.ConvertAll(t => t.ToZero(zosm))
		};
	}
}