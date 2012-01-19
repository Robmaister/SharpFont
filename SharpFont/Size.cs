using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Size
	{
		public Face *Face;
		public Generic Generic;
		public SizeMetrics Metrics;
		public IntPtr Internal;
	}
}
