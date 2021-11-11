using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Member {
		[Index(0)]
		public virtual MemberType Type { get; set; }

		[Index(1)]
		public virtual ulong NodeId { get; set; }

		[Index(2)]
		public virtual MemberRole Role { get; set; }
	}
}