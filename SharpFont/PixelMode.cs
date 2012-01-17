using System;

namespace SharpFont
{
	/// <summary>
	/// An enumeration type used to describe the format of pixels in a given bitmap. Note that additional formats may be added in the future.
	/// </summary>
	public enum PixelMode : sbyte
	{
		/// <summary>
		/// Value 0 is reserved.
		/// </summary>
		None = 0,

		/// <summary>
		/// A monochrome bitmap, using 1 bit per pixel. Note that pixels are stored in most-significant order (MSB), which means that the left-most pixel in a byte has value 128.
		/// </summary>
		Mono,

		/// <summary>
		/// An 8-bit bitmap, generally used to represent anti-aliased glyph images. Each pixel is stored in one byte. Note that the number of ‘gray’ levels is stored in the ‘num_grays’ field of the FT_Bitmap structure (it generally is 256).
		/// </summary>
		Gray,

		/// <summary>
		/// A 2-bit per pixel bitmap, used to represent embedded anti-aliased bitmaps in font files according to the OpenType specification. We haven't found a single font using this format, however.
		/// </summary>
		Gray2,

		/// <summary>
		/// A 4-bit per pixel bitmap, representing embedded anti-aliased bitmaps in font files according to the OpenType specification. We haven't found a single font using this format, however.
		/// </summary>
		Gray4,

		/// <summary>
		/// An 8-bit bitmap, representing RGB or BGR decimated glyph images used for display on LCD displays; the bitmap is three times wider than the original glyph image. See also FT_RENDER_MODE_LCD.
		/// </summary>
		LCD,

		/// <summary>
		/// An 8-bit bitmap, representing RGB or BGR decimated glyph images used for display on rotated LCD displays; the bitmap is three times taller than the original glyph image. See also FT_RENDER_MODE_LCD_V.
		/// </summary>
		VerticalLCD
	}
}
