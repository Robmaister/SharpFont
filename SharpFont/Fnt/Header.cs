#region MIT License
/*Copyright (c) 2012-2013 Robert Rouhani <robert.rouhani@gmail.com>

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

using SharpFont.Fnt.Internal;

namespace SharpFont.Fnt
{
	/// <summary>
	/// Windows FNT Header info.
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

		[CLSCompliant(false)]
		public ushort Version
		{
			get
			{
				return rec.version;
			}
		}

		[CLSCompliant(false)]
		public uint FileSize
		{
			get
			{
				return (uint)rec.file_size;
			}
		}

		public byte[] Copyright
		{
			get
			{
				return rec.copyright;
			}
		}

		[CLSCompliant(false)]
		public ushort FileType
		{
			get
			{
				return rec.file_type;
			}
		}

		[CLSCompliant(false)]
		public ushort NominalPointSize
		{
			get
			{
				return rec.nominal_point_size;
			}
		}

		[CLSCompliant(false)]
		public ushort VerticalResolution
		{
			get
			{
				return rec.vertical_resolution;
			}
		}

		[CLSCompliant(false)]
		public ushort HorizontalResolution
		{
			get
			{
				return rec.horizontal_resolution;
			}
		}

		[CLSCompliant(false)]
		public ushort Ascent
		{
			get
			{
				return rec.ascent;
			}
		}

		[CLSCompliant(false)]
		public ushort InternalLeading
		{
			get
			{
				return rec.internal_leading;
			}
		}

		[CLSCompliant(false)]
		public ushort ExternalLeading
		{
			get
			{
				return rec.external_leading;
			}
		}

		public byte Italic
		{
			get
			{
				return rec.italic;
			}
		}

		public byte Underline
		{
			get
			{
				return rec.underline;
			}
		}

		public byte Strikeout
		{
			get
			{
				return rec.strike_out;
			}
		}

		[CLSCompliant(false)]
		public ushort Weight
		{
			get
			{
				return rec.weight;
			}
		}

		public byte Charset
		{
			get
			{
				return rec.charset;
			}
		}

		[CLSCompliant(false)]
		public ushort PixelWidth
		{
			get
			{
				return rec.pixel_width;
			}
		}

		[CLSCompliant(false)]
		public ushort PixelHeight
		{
			get
			{
				return rec.pixel_height;
			}
		}

		public byte PitchAndFamily
		{
			get
			{
				return rec.pitch_and_family;
			}
		}

		[CLSCompliant(false)]
		public ushort AverageWidth
		{
			get
			{
				return rec.avg_width;
			}
		}

		[CLSCompliant(false)]
		public ushort MaximumWidth
		{
			get
			{
				return rec.max_width;
			}
		}

		public byte FirstChar
		{
			get
			{
				return rec.first_char;
			}
		}

		public byte LastChar
		{
			get
			{
				return rec.last_char;
			}
		}

		public byte DefaultChar
		{
			get
			{
				return rec.default_char;
			}
		}

		public byte BreakChar
		{
			get
			{
				return rec.break_char;
			}
		}

		[CLSCompliant(false)]
		public ushort BytesPerRow
		{
			get
			{
				return rec.bytes_per_row;
			}
		}

		[CLSCompliant(false)]
		public uint DeviceOffset
		{
			get
			{
				return (uint)rec.device_offset;
			}
		}

		[CLSCompliant(false)]
		public uint FaceNameOffset
		{
			get
			{
				return (uint)rec.face_name_offset;
			}
		}

		[CLSCompliant(false)]
		public uint BitsPointer
		{
			get
			{
				return (uint)rec.bits_pointer;
			}
		}

		[CLSCompliant(false)]
		public uint BitsOffset
		{
			get
			{
				return (uint)rec.bits_offset;
			}
		}

		public byte Reserved
		{
			get
			{
				return rec.reserved;
			}
		}

		[CLSCompliant(false)]
		public uint Flags
		{
			get
			{
				return (uint)rec.flags;
			}
		}

		[CLSCompliant(false)]
		public ushort ASpace
		{
			get
			{
				return rec.A_space;
			}
		}

		[CLSCompliant(false)]
		public ushort BSpace
		{
			get
			{
				return rec.B_space;
			}
		}

		[CLSCompliant(false)]
		public ushort CSpace
		{
			get
			{
				return rec.C_space;
			}
		}

		[CLSCompliant(false)]
		public ushort ColorTableOffset
		{
			get
			{
				return rec.color_table_offset;
			}
		}

		[CLSCompliant(false)]
		public uint[] Reserved1
		{
			get
			{
				#if WIN64
				return rec.reserved1;
				#else
				return Array.ConvertAll<UIntPtr, uint>(rec.reserved1, new Converter<UIntPtr, uint>(delegate(UIntPtr u) { return (uint)u; }));
				#endif
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
