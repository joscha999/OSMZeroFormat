using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ZeroFormatOSM;
using ZeroFormatOSM.TagConvertion;

namespace OSMData {
	public class Member {
		[XmlAttribute("type")]
		public string Type { get; set; }

		[XmlAttribute("ref")]
		public ulong NodeReference { get; set; }

		[XmlAttribute("role")]
		public string Role { get; set; }

		public ZeroFormatOSM.Member ToZero() => new ZeroFormatOSM.Member {
			Type = (MemberType)Enum.Parse(typeof(MemberType), TagTypeCodeGenerator.Ti.ToTitleCase(Type)),
			NodeId = NodeReference,
			Role = ZeroOSMMemberRoleConverter.FromString(Role)
		};
	}
}