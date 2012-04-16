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
	/// A structure used to model a TrueType font header table. All fields
	/// follow the TrueType specification.
	/// </summary>
	public class Header
	{
		#region Fields

		private IntPtr reference;
		private HeaderRec rec;

		#endregion

		#region Constructors

		internal Header(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		public int TableVersion
		{
			get
			{
				return (int)rec.Table_Version;
			}
		}

		public int FontRevision
		{
			get
			{
				return (int)rec.Font_Revision;
			}
		}

		public int CheckSumAdjust
		{
			get
			{
				return (int)rec.CheckSum_Adjust;
			}
		}

		public int MagicNumber
		{
			get
			{
				return (int)rec.Magic_Number;
			}
		}

		[CLSCompliant(false)]
		public ushort Flags
		{
			get
			{
				return rec.Flags;
			}
		}

		[CLSCompliant(false)]
		public ushort UnitsPerEM
		{
			get
			{
				return rec.Units_Per_EM;
			}
		}

		public int[] Created
		{
			get
			{
				#if WIN64
				return rec.Created;
				#else
				return Array.ConvertAll<IntPtr, int>(rec.Created, new Converter<IntPtr, int>(delegate(IntPtr i) { return (int)i; }));
				#endif
			}
		}

		public int[] Modified
		{
			get
			{
				#if WIN64
				return rec.Created;
				#else
				return Array.ConvertAll<IntPtr, int>(rec.Created, new Converter<IntPtr, int>(delegate(IntPtr i) { return (int)i; }));
				#endif
			}
		}

		public short MinimumX
		{
			get
			{
				return rec.xMin;
			}
		}

		public short MinimumY
		{
			get
			{
				return rec.yMin;
			}
		}

		public short MaximumX
		{
			get
			{
				return rec.xMax;
			}
		}

		public short MaximumY
		{
			get
			{
				return rec.yMax;
			}
		}

		[CLSCompliant(false)]
		public ushort MacStyle
		{
			get
			{
				return rec.Mac_Style;
			}
		}

		[CLSCompliant(false)]
		public ushort LowestRecPPEM
		{
			get
			{
				return rec.Lowest_Rec_PPEM;
			}
		}

		public short FontDirection
		{
			get
			{
				return rec.Font_Direction;
			}
		}

		public short IndexToLocFormat
		{
			get
			{
				return rec.Index_To_Loc_Format;
			}
		}

		public short GlyphDataFormat
		{
			get
			{
				return rec.Glyph_Data_Format;
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
				rec = PInvokeHelper.PtrToStructure<HeaderRec>(reference);
			}
		}

		#endregion
	}
}
