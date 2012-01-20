using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// The size metrics structure gives the metrics of a size object.
	/// </summary>
	/// <remarks>
	/// The scaling values, if relevant, are determined first during a size
	/// changing operation. The remaining fields are then set by the driver.
	/// For scalable formats, they are usually set to scaled values of the
	/// corresponding fields in <see cref="Face"/>.
	/// 
	/// Note that due to glyph hinting, these values might not be exact for
	/// certain fonts. Thus they must be treated as unreliable with an error
	/// margin of at least one pixel!
	/// 
	/// Indeed, the only way to get the exact metrics is to render all glyphs.
	/// As this would be a definite performance hit, it is up to client
	/// applications to perform such computations.
	/// 
	/// The SizeMetrics structure is valid for bitmap fonts also.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct SizeMetrics
	{
		/// <summary>
		/// The width of the scaled EM square in pixels, hence the term ‘ppem’
		/// (pixels per EM). It is also referred to as ‘nominal width’.
		/// </summary>
		public ushort NominalWidth;

		/// <summary>
		/// The height of the scaled EM square in pixels, hence the term ‘ppem’
		/// (pixels per EM). It is also referred to as ‘nominal width’.
		/// </summary>
		public ushort NominalHeight;

		/// <summary>
		/// A 16.16 fractional scaling value used to convert horizontal metrics
		/// from font units to 26.6 fractional pixels. Only relevant for
		/// scalable font formats.
		/// </summary>
		public int ScaleX;

		/// <summary>
		/// A 16.16 fractional scaling value used to convert vertical metrics
		/// from font units to 26.6 fractional pixels. Only relevant for
		/// scalable font formats.
		/// </summary>
		public int ScaleY;

		/// <summary>
		/// The ascender in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Ascender;

		/// <summary>
		/// The descender in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Descender;

		/// <summary>
		/// The height in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Height;

		/// <summary>
		/// The maximal advance width in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int MaxAdvance;
	}
}
