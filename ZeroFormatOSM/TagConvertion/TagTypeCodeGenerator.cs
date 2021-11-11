using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ZeroFormatOSM.TagConvertion {
	public static class TagTypeCodeGenerator {
		public static readonly TextInfo Ti = new CultureInfo("en-US", false).TextInfo;

		public static string BuildTagTypes(IEnumerable<string> tags) {
			var sb = new StringBuilder();

			foreach (var tag in tags) {
				sb.Append("[OSMTagType(\"").Append(tag).AppendLine("\")]");
				sb.Append(ToPascalCase(tag)).AppendLine(",").AppendLine();
			}

			return sb.ToString();
		}

		public static string BuildMemberRoles(IEnumerable<string> tags) {
			var sb = new StringBuilder();

			foreach (var tag in tags) {
				sb.Append("[OSMMemberRole(\"").Append(tag).AppendLine("\")]");
				sb.Append(ToPascalCase(tag)).AppendLine(",").AppendLine();
			}

			return sb.ToString();
		}

		private static string ToPascalCase(string str) {
			var sb = new StringBuilder();

			foreach (var c in str) {
				if (!char.IsLetterOrDigit(c))
					sb.Append(" ");
				else
					sb.Append(c);
			}

			return Ti.ToTitleCase(sb.ToString()).Replace(" ", string.Empty);
		}
	}
}