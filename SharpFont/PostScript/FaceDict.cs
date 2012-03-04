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

using SharpFont.PostScript.Internal;

namespace SharpFont.PostScript
{
	/// <summary>
	/// A structure used to represent data in a CID top-level dictionary.
	/// </summary>
	public class FaceDict
	{
		internal IntPtr reference;
		internal FaceDictRec rec;

		internal FaceDict(IntPtr reference)
		{
			this.reference = reference;
			this.rec = (FaceDictRec)Marshal.PtrToStructure(reference, typeof(FaceDictRec));
		}

		public Private PrivateDictionary
		{
			get
			{
				return new Private(reference);
			}
		}

		[CLSCompliant(false)]
		public uint BuildCharLength
		{
			get
			{
				return rec.len_buildchar;
			}
		}

		public int ForceBoldThreshold
		{
			get
			{
				return (int)rec.forcebold_threshold;
			}
		}

		public int StrokeWidth
		{
			get
			{
				return (int)rec.stroke_width;
			}
		}

		public int ExpansionFactor
		{
			get
			{
				return (int)rec.expansion_factor;
			}
		}

		public byte PaintType
		{
			get
			{
				return rec.paint_type;
			}
		}

		public byte FontType
		{
			get
			{
				return rec.font_type;
			}
		}

		public Matrix2i FontMatrix
		{
			get
			{
				return new Matrix2i(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceDictRec), "font_matrix").ToInt64()));
			}
		}

		public Vector2i FontOffset
		{
			get
			{
				return new Vector2i(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceDictRec), "font_offset").ToInt64()));
			}
		}

		[CLSCompliant(false)]
		public uint SubrsCount
		{
			get
			{
				return rec.num_subrs;
			}
		}

		[CLSCompliant(false)]
		public uint SubrmapOffset
		{
			get
			{
				return (uint)rec.subrmap_offset;
			}
		}

		public int SDBytes
		{
			get
			{
				return rec.sd_bytes;
			}
		}
	}
}
