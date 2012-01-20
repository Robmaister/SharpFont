#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// This structure models the metrics of a bitmap strike (i.e., a set of
	/// glyphs for a given point size and resolution) in a bitmap font. It is
	/// used for the <see cref="Face.AvailableSizes"/> field of
	/// <see cref="Face"/>.
	/// </summary>
	/// <remarks>
	/// Windows FNT: The nominal size given in a FNT font is not reliable. Thus
	/// when the driver finds it incorrect, it sets ‘size’ to some calculated
	/// values and sets ‘x_ppem’ and ‘y_ppem’ to the pixel width and height
	/// given in the font, respectively.
	/// 
	/// TrueType embedded bitmaps: ‘size’, ‘width’, and ‘height’ values are not
	/// contained in the bitmap strike itself. They are computed from the
	/// global font parameters.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct BitmapSize
	{
		/// <summary>
		/// The vertical distance, in pixels, between two consecutive
		/// baselines. It is always positive.
		/// </summary>
		public short Height;

		/// <summary>
		/// The average width, in pixels, of all glyphs in the strike.
		/// </summary>
		public short Width;

		/// <summary>
		/// The nominal size of the strike in 26.6 fractional points. This
		/// field is not very useful.
		/// </summary>
		public int Size;

		/// <summary>
		/// The horizontal ppem (nominal width) in 26.6 fractional pixels.
		/// </summary>
		public int NominalWidth;

		/// <summary>
		/// The vertical ppem (nominal height) in 26.6 fractional pixels.
		/// </summary>
		public int NominalHeight;
	}
}
