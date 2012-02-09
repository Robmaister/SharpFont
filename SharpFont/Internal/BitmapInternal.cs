using System;
using System.Runtime.InteropServices;

namespace SharpFont.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	internal class BitmapInternal
	{
		internal int rows;
		internal int width;
		internal int pitch;
		internal IntPtr buffer;
		internal short num_grays;
		internal PixelMode pixel_mode;
		internal byte palette_mode;
		internal IntPtr palette;
	}
}
