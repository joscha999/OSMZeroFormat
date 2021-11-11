using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class Way {
		[Index(0)]
		public virtual ulong Id { get; set; }

		[Index(1)]
		public virtual IList<ulong> NodeIds { get; set; }

		[Index(2)]
		public virtual IList<Tag> Tags { get; set; }
	}
}