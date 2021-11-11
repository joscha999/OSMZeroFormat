using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFormatOSM.TagConvertion {
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
	public sealed class OSMMemberRoleAttribute : Attribute {
		public string OSMName { get; set; }

		// This is a positional argument
		public OSMMemberRoleAttribute(string osmName) {
			OSMName = osmName;
		}
	}
}
