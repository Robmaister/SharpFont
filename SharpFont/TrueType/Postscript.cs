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

using SharpFont.TrueType.Internal;

namespace SharpFont.TrueType
{
	/// <summary>
	/// A structure used to model a TrueType PostScript table. All fields
	/// comply to the TrueType specification. This structure does not reference
	/// the PostScript glyph names, which can be nevertheless accessed with the
	/// ‘ttpost’ module.
	/// </summary>
	public class Postscript
	{
		internal IntPtr reference;
		internal PostscriptRec rec;

		internal Postscript(IntPtr reference)
		{
			this.reference = reference;
			this.rec = (PostscriptRec)Marshal.PtrToStructure(reference, typeof(PostscriptRec));
		}

		public long FormatType
		{
			get
			{
				return rec.FormatType;
			}
		}
		
		public long ItalicAngle
		{
			get
			{
				return rec.italicAngle;
			}
		}
		
		public short UnderlinePosition
		{
			get
			{
				return rec.underlinePosition;
			}
		}
		
		public short UnderlineThickness
		{
			get
			{
				return rec.underlineThickness;
			}
		}

		[CLSCompliant(false)]
		public ulong IsFixedPitch
		{
			get
			{
				return rec.isFixedPitch;
			}
		}

		[CLSCompliant(false)]
		public ulong MinimumMemoryType42
		{
			get
			{
				return rec.minMemType42;
			}
		}

		[CLSCompliant(false)]
		public ulong MaximumMemoryType42
		{
			get
			{
				return rec.maxMemType42;
			}
		}

		[CLSCompliant(false)]
		public ulong MinimumMemoryType1
		{
			get
			{
				return rec.minMemType1;
			}
		}

		[CLSCompliant(false)]
		public ulong MaximumMemoryType1
		{
			get
			{
				return rec.maxMemType1;
			}
		}
	}
}
