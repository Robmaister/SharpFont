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
	/// <summary><para>
	/// A structure used to model a TrueType OS/2 table. This is the long table
	/// version. All fields comply to the TrueType specification.
	/// </para><para>
	/// Note that we now support old Mac fonts which do not include an OS/2
	/// table. In this case, the ‘version’ field is always set to 0xFFFF.
	/// </para></summary>
	public class OS2
	{
		#region Fields

		private IntPtr reference;
		private OS2Rec rec;

		#endregion

		#region Constructors

		internal OS2(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		[CLSCompliant(false)]
		public ushort Version
		{
			get
			{
				return rec.version;
			}
		}

		public short AverageCharWidth
		{
			get
			{
				return rec.xAvgCharWidth;
			}
		}

		[CLSCompliant(false)]
		public ushort WeightClass
		{
			get
			{
				return rec.usWeightClass;
			}
		}

		[CLSCompliant(false)]
		public ushort WidthClass
		{
			get
			{
				return rec.usWidthClass;
			}
		}

		[CLSCompliant(false)]
		public EmbeddingTypes EmbeddingType
		{
			get
			{
				return rec.fsType;
			}
		}

		public short SubscriptSizeX
		{
			get
			{
				return rec.ySubscriptXSize;
			}
		}

		public short SubscriptSizeY
		{
			get
			{
				return rec.ySubscriptYSize;
			}
		}

		public short SubscriptOffsetX
		{
			get
			{
				return rec.ySubscriptXOffset;
			}
		}

		public short SubscriptOffsetY
		{
			get
			{
				return rec.ySubscriptYOffset;
			}
		}

		public short SuperscriptSizeX
		{
			get
			{
				return rec.ySuperscriptXSize;
			}
		}

		public short SuperscriptSizeY
		{
			get
			{
				return rec.ySuperscriptYSize;
			}
		}

		public short SuperscriptOffsetX
		{
			get
			{
				return rec.ySuperscriptXOffset;
			}
		}

		public short SuperscriptOffsetY
		{
			get
			{
				return rec.ySuperscriptYOffset;
			}
		}

		public short StrikeoutSize
		{
			get
			{
				return rec.yStrikeoutSize;
			}
		}

		public short StrikeoutPosition
		{
			get
			{
				return rec.yStrikeoutPosition;
			}
		}

		public short FamilyClass
		{
			get
			{
				return rec.sFamilyClass;
			}
		}

		//TODO write a PANOSE class from TrueType spec?
		public byte[] panose
		{
			get
			{
				return rec.panose;
			}
		}

		[CLSCompliant(false)]
		public uint UnicodeRange1
		{
			get
			{
				return (uint)rec.ulUnicodeRange1;
			}
		}

		[CLSCompliant(false)]
		public uint UnicodeRange2
		{
			get
			{
				return (uint)rec.ulUnicodeRange2;
			}
		}

		[CLSCompliant(false)]
		public uint UnicodeRange3
		{
			get
			{
				return (uint)rec.ulUnicodeRange3;
			}
		}

		[CLSCompliant(false)]
		public uint UnicodeRange4
		{
			get
			{
				return (uint)rec.ulUnicodeRange4;
			}
		}

		public byte[] VendorID
		{
			get
			{
				return rec.achVendID;
			}
		}

		//TODO make a flags enum
		[CLSCompliant(false)]
		public ushort SelectionFlags
		{
			get
			{
				return rec.fsSelection;
			}
		}

		[CLSCompliant(false)]
		public ushort FirstCharIndex
		{
			get
			{
				return rec.usFirstCharIndex;
			}
		}

		[CLSCompliant(false)]
		public ushort LastCharIndex
		{
			get
			{
				return rec.usLastCharIndex;
			}
		}
		
		public short TypographicAscender
		{
			get
			{
				return rec.sTypoAscender;
			}
		}
		
		public short TypographicDescender
		{
			get
			{
				return rec.sTypoDescender;
			}
		}
		
		public short TypographicLineGap
		{
			get
			{
				return rec.sTypoLineGap;
			}
		}

		[CLSCompliant(false)]
		public ushort WindowsAscent
		{
			get
			{
				return rec.usWinAscent;
			}
		}

		[CLSCompliant(false)]
		public ushort WindowsDescent
		{
			get
			{
				return rec.usWinDescent;
			}
		}

		[CLSCompliant(false)]
		public uint CodePageRange1
		{
			get
			{
				return (uint)rec.ulCodePageRange1;
			}
		}

		[CLSCompliant(false)]
		public uint CodePageRange2
		{
			get
			{
				return (uint)rec.ulUnicodeRange1;
			}
		}

		public short Height
		{
			get
			{
				return rec.sxHeight;
			}
		}
		
		public short CapHeight
		{
			get
			{
				return rec.sCapHeight;
			}
		}

		[CLSCompliant(false)]
		public ushort DefaultChar
		{
			get
			{
				return rec.usDefaultChar;
			}
		}

		[CLSCompliant(false)]
		public ushort BreakChar
		{
			get
			{
				return rec.usBreakChar;
			}
		}

		[CLSCompliant(false)]
		public ushort MaxContext
		{
			get
			{
				return rec.usMaxContext;
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
				rec = PInvokeHelper.PtrToStructure<OS2Rec>(reference);
			}
		}

		#endregion
	}
}
