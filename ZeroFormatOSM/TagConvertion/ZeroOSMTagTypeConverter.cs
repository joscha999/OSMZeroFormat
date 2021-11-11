using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ZeroFormatOSM.TagConvertion {
	public static class ZeroOSMTagTypeConverter {
		private static readonly Dictionary<string, TagType> TagTypeConversion = new Dictionary<string, TagType>();

		static ZeroOSMTagTypeConverter() {
			foreach (var member in typeof(TagType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
				var attr = member.GetCustomAttribute<OSMTagTypeAttribute>();

				if (attr != null)
					TagTypeConversion.Add(attr.OSMName, (TagType)Enum.Parse(typeof(TagType), member.Name));
			}
		}

		public static TagType FromString(string str) {
			if (TagTypeConversion.TryGetValue(str, out var tt))
				return tt;

			return TagType.ZeroUnavailable;
		}
	}
}