using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Node {
		[Index(0)]
		public virtual ulong Id { get; set; }

		[Index(1)]
		public virtual float Latitude { get; set; }
		[Index(2)]
		public virtual float Longitude { get; set; }

		[Index(3)]
		public virtual IList<Tag> Tags { get; set; }
	}
}