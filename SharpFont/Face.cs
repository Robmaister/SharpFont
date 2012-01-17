using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Face
	{
		public int FaceCount;
		public int FaceIndex;
		public FaceFlags FaceFlags;
		public StyleFlags StyleFlags;

		public int GlyphCount;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string FamilyName;

		[MarshalAs(UnmanagedType.LPWStr)]
		public string StyleName;

		public int FixedSizesCount;
		public BitmapSize[] AvailableSizes;

		public int CharmapsCount;
		public IntPtr Charmaps;

		public IntPtr Generic;

		public BBox BBox;

		public ushort UnitsPerEM;
		public short Ascender;
		public short Descender;
		public short Height;

		public short MaxAdvanceWidth;
		public short MaxAdvanceHeight;

		public short UnderlinePosition;
		public short UnderlineThickness;

		public IntPtr Glyph;
		public IntPtr Size;
		public IntPtr Charmap;

		private IntPtr driver;
		private IntPtr memory;
		private IntPtr stream;
		private IntPtr sizesList;
		private IntPtr autoHint;
		private IntPtr extensions;
		private IntPtr @internal;
	}
}
