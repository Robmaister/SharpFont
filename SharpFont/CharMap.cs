using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CharMap
	{
		Face *Face;
		Encoding Encoding;
		PlatformID PlatformID;
		EncodingID EncodingID;
	}
}
