using System;
using System.Collections.Generic;
using ZeroFormatter;

namespace ZeroFormatOSM {
	[ZeroFormattable]
	public class ZeroOSM {
		[Index(0)]
		public virtual Bounds Bounds { get; set; }

		[Index(1)]
		public virtual Dictionary<ulong, Node> Nodes { get; set; }

		[Index(2)]
		public virtual Dictionary<ulong, Way> Ways { get; set; }

		[Index(3)]
		public virtual Dictionary<ulong, Relation> Relations { get; set; }

		[Index(4)]
		public virtual Dictionary<string, int> TagValueDictionary { get; set; }

		public int RegisterTagValue(string value) {
			if (TagValueDictionary == null)
				TagValueDictionary = new Dictionary<string, int>();

			if (TagValueDictionary.TryGetValue(value, out var id))
				return id;

			id = TagValueDictionary.Count;
			TagValueDictionary.Add(value, id);
			return id;
		}
	}
}