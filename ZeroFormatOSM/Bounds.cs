using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Bounds {
		[Index(0)]
		public virtual float MinLatitude { get; set; }
		[Index(1)]
		public virtual float MaxLatitude { get; set; }

		[Index(2)]
		public virtual float MinLongitude { get; set; }
		[Index(3)]
		public virtual float MaxLongitude { get; set; }
	}
}