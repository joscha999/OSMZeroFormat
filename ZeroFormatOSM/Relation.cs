using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Relation {
		[Index(0)]
		public virtual ulong Id { get; set; }

		[Index(1)]
		public virtual IList<Member> Members { get; set; }

		[Index(2)]
		public virtual IList<Tag> Tags { get; set; }
	}
}