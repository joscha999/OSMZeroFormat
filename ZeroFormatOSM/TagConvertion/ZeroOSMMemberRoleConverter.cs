using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ZeroFormatOSM.TagConvertion {
	public static class ZeroOSMMemberRoleConverter {
		private static readonly Dictionary<string, MemberRole> MemberRoleConversion = new Dictionary<string, MemberRole>();

		static ZeroOSMMemberRoleConverter() {
			foreach (var member in typeof(MemberRole).GetFields(BindingFlags.Public | BindingFlags.Static)) {
				var attr = member.GetCustomAttribute<OSMMemberRoleAttribute>();

				if (attr != null)
					MemberRoleConversion.Add(attr.OSMName, (MemberRole)Enum.Parse(typeof(MemberRole), member.Name));
			}
		}

		public static MemberRole FromString(string str) {
			if (str == "")
				return MemberRole.Empty;

			if (MemberRoleConversion.TryGetValue(str, out var mr))
				return mr;

			return MemberRole.ZeroUnavailable;
		}
	}
}
