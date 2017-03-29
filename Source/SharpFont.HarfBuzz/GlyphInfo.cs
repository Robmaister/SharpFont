//Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
//Licensed under the MIT License - https://raw.github.com/Robmaister/SharpFont.HarfBuzz/master/LICENSE

using System.Runtime.InteropServices;

namespace SharpFont.HarfBuzz
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphInfo
	{
		public uint codepoint;
		public uint mask;
		public uint cluster;
	}
}
