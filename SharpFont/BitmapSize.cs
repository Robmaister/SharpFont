using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BitmapSize
	{
		public short Height;
		public short Width;

		public int Size;

		public int NominalWidth;
		public int NominalHeight;
	}
}
