using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroFormatOSM {
	public static class PorjectionHelper {
		public static long LatLonToId(float lat, float lon, int Resolution)
			=> (((long)(lat * Resolution)) << 32) + (int)(lon * Resolution);
	}
}