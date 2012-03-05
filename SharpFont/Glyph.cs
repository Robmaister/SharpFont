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
	/// <summary>
	/// The root glyph structure contains a given glyph image plus its advance
	/// width in 16.16 fixed float format.
	/// </summary>
	public class Glyph
	{
		internal IntPtr reference;
		internal GlyphRec rec;

		internal Glyph(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<GlyphRec>(reference);
		}

		internal Glyph(GlyphRec rec)
		{
			this.reference = IntPtr.Zero;
			this.rec = rec;
		}

		/// <summary>
		/// A handle to the FreeType library object.
		/// </summary>
		public Library Library
		{
			get
			{
				return new Library(rec.library, true);
			}
		}

		/// <summary>
		/// The format of the glyph's image.
		/// </summary>
		[CLSCompliant(false)]
		public GlyphFormat Format
		{
			get
			{
				return rec.format;
			}
		}

		/// <summary>
		/// A 16.16 vector that gives the glyph's advance width.
		/// </summary>
		public Vector2i Advance
		{
			get
			{
				return new Vector2i(rec.advance);
			}
		}
	}
}
