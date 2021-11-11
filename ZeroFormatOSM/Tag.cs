using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Tag {
		[Index(0)]
		public virtual TagType Key { get; set; }
		[Index(1)]
		public virtual int ValueId { get; set; }
	}
}