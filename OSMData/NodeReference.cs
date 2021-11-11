using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OSMData {
	public class NodeReference {
		[XmlAttribute("ref")]
		public ulong NodeId { get; set; }
	}
}