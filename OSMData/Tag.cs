using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using ZeroFormatOSM;
using ZeroFormatOSM.TagConvertion;

namespace OSMData {
	public class Tag {
		[XmlAttribute("k")]
		public string Key { get; set; }
		[XmlAttribute("v")]
		public string Value { get; set; }

		public bool Is(string key) => Key == key;
		public bool Is(string key, string value) => Key == key && Value == value;

		public ZeroFormatOSM.Tag ToZero(ZeroOSM zosm) => new ZeroFormatOSM.Tag {
			Key = ZeroOSMTagTypeConverter.FromString(Key),
			ValueId = zosm.RegisterTagValue(Value)
		};
	}
}