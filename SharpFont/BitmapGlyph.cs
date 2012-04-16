#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

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

using SharpFont.Internal;

namespace SharpFont
{
	//TODO some sort of pseudo-inheritance for glyphs following FreeType's method.

	/// <summary>
	/// A structure used for bitmap glyph images. This really is a ‘sub-class’
	/// of <see cref="Glyph"/>.
	/// </summary>
	/// <remarks><para>
	/// You can typecast an <see cref="Glyph"/> to <see cref="BitmapGlyph"/> if
	/// you have ‘<see cref="Glyph.Format"/> ==
	/// <see cref="GlyphFormat.Bitmap"/>’. This lets you access the bitmap's
	/// contents easily.
	/// </para><para>
	/// The corresponding pixel buffer is always owned by
	/// <see cref="BitmapGlyph"/> and is thus created and destroyed with it.
	/// </para></remarks>
	public class BitmapGlyph
	{
		#region Fields

		private IntPtr reference;
		private BitmapGlyphRec rec;

		#endregion

		#region Constructors

		internal BitmapGlyph(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The root <see cref="Glyph"/> fields.
		/// </summary>
		public Glyph Root
		{
			get
			{
				//HACK fix this later.
				return new Glyph(rec.root, null);
			}
		}

		/// <summary>
		/// The left-side bearing, i.e., the horizontal distance from the
		/// current pen position to the left border of the glyph bitmap.
		/// </summary>
		public int Left
		{
			get
			{
				return rec.left;
			}
		}

		/// <summary>
		/// The top-side bearing, i.e., the vertical distance from the current
		/// pen position to the top border of the glyph bitmap. This distance
		/// is positive for upwards y!
		/// </summary>
		public int Top
		{
			get
			{
				return rec.top;
			}
		}

		/// <summary>
		/// A descriptor for the bitmap.
		/// </summary>
		public FTBitmap Bitmap
		{
			get
			{
				return new FTBitmap(rec.bitmap);
			}
		}

		internal IntPtr Reference
		{
			get
			{
				return reference;
			}

			set
			{
				reference = value;
				rec = PInvokeHelper.PtrToStructure<BitmapGlyphRec>(reference);
			}
		}

		#endregion
	}
}
