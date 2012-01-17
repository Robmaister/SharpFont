using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public struct OutlineFuncs
	{
		public IntPtr moveTo;
		public IntPtr lineTo;
		public IntPtr conicTo;
		public IntPtr cubicTo;
		public int shift;
		public int delta;
	}
}
