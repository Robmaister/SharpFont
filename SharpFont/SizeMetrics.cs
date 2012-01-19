using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SizeMetrics
	{
		public ushort NominalWidth;
		public ushort NominalHeight;

		public int ScaleX;
		public int ScaleY;

		public int Ascender;
		public int Descender;
		public int Height;
		public int MaxAdvance;
	}
}
