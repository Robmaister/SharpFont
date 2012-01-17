using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A structure used to model the metrics of a single glyph. The values are expressed in 26.6 fractional pixel format; if the flag FT_LOAD_NO_SCALE has been used while loading the glyph, values are expressed in font units instead.
	/// </summary>
	/// <remarks>
	/// If not disabled with FT_LOAD_NO_HINTING, the values represent dimensions of the hinted glyph (in case hinting is applicable).
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct GlyphMetrics
	{
		/// <summary>
		/// The glyph's width.
		/// </summary>
		public int Width;

		/// <summary>
		/// The glyph's height.
		/// </summary>
		public int Height;

		/// <summary>
		/// Left side bearing for horizontal layout.
		/// </summary>
		public int HorizontalBearingX;

		/// <summary>
		/// Top side bearing for horizontal layout.
		/// </summary>
		public int HorizontalBearingY;

		/// <summary>
		/// Advance width for horizontal layout.
		/// </summary>
		public int HorizontalAdvance;

		/// <summary>
		/// Left side bearing for vertical layout.
		/// </summary>
		public int VerticalBearingX;

		/// <summary>
		/// Top side bearing for vertical layout. Larger positive values mean further below the vertical glyph origin.
		/// </summary>
		public int VerticalBearingY;

		/// <summary>
		/// Advance height for vertical layout. Positive values mean the glyph has a positive advance downward.
		/// </summary>
		public int VerticalAdvance;
	}
}
