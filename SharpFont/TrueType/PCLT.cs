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
	/// A structure used to model a TrueType PCLT table. All fields comply to
	/// the TrueType specification.
	/// </summary>
	public class PCLT
	{
		#region Fields

		private IntPtr reference;
		private PCLTRec rec;

		#endregion

		#region Constructors

		internal PCLT(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		public int Version
		{
			get
			{
				return (int)rec.Version;
			}
		}
		
		[CLSCompliant(false)]
		public uint FontNumber
		{
			get
			{
				return (uint)rec.FontNumber;
			}
		}

		[CLSCompliant(false)]
		public ushort Pitch
		{
			get
			{
				return rec.Pitch;
			}
		}

		[CLSCompliant(false)]
		public ushort Height
		{
			get
			{
				return rec.xHeight;
			}
		}

		[CLSCompliant(false)]
		public ushort Style
		{
			get
			{
				return rec.Style;
			}
		}

		[CLSCompliant(false)]
		public ushort TypeFamily
		{
			get
			{
				return rec.TypeFamily;
			}
		}

		[CLSCompliant(false)]
		public ushort CapHeight
		{
			get
			{
				return rec.CapHeight;
			}
		}

		[CLSCompliant(false)]
		public ushort SymbolSet
		{
			get
			{
				return rec.SymbolSet;
			}
		}

		public string TypeFace
		{
			get
			{
				return rec.TypeFace;
			}
		}

		public byte[] CharacterComplement
		{
			get
			{
				return rec.CharacterComplement;
			}
		}

		public byte[] FileName
		{
			get
			{
				return rec.FileName;
			}
		}

		public byte StrokeWeight
		{
			get
			{
				return rec.StrokeWeight;
			}
		}
		
		public byte WidthType
		{
			get
			{
				return rec.WidthType;
			}
		}
		
		public byte SerifStyle
		{
			get
			{
				return rec.SerifStyle;
			}
		}
		
		public byte Reserved
		{
			get
			{
				return rec.Reserved;
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
				rec = PInvokeHelper.PtrToStructure<PCLTRec>(reference);
			}
		}

		#endregion
	}
}
