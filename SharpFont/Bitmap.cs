#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

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
	/// A structure used to describe a bitmap or pixmap to the raster. Note
	/// that we now manage pixmaps of various depths through the ‘pixel_mode’
	/// field.
	/// </summary>
	/// <remarks>
	/// For now, the only pixel modes supported by FreeType are mono and grays.
	/// However, drivers might be added in the future to support more
	/// ‘colorful’ options.
	/// </remarks>
	public sealed class Bitmap
	{
		internal IntPtr reference;
		internal BitmapRec rec;

		internal Bitmap(IntPtr reference)
		{
			this.reference = reference;
			this.rec = (BitmapRec)Marshal.PtrToStructure(reference, typeof(BitmapRec));
		}

		internal Bitmap(BitmapRec bmpInt)
		{
			this.rec = bmpInt;
		}

		/*/// <summary>
		/// Gets the size of a Bitmap, in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return IntPtr.Size * 8;
			}
		}*/

		/// <summary>
		/// Gets the number of bitmap rows.
		/// </summary>
		public int Rows
		{
			get
			{
				return rec.rows;
			}
		}

		/// <summary>
		/// Gets the number of pixels in bitmap row.
		/// </summary>
		public int Width
		{
			get
			{
				return rec.width;
			}
		}

		/// <summary><para>
		/// Gets the pitch's absolute value is the number of bytes taken by one
		/// bitmap row, including padding. However, the pitch is positive when
		/// the bitmap has a ‘down’ flow, and negative when it has an ‘up’
		/// flow. In all cases, the pitch is an offset to add to a bitmap
		/// pointer in order to go down one row.
		/// </para><para>
		/// Note that ‘padding’ means the alignment of a bitmap to a byte
		/// border, and FreeType functions normally align to the smallest
		/// possible integer value.
		/// </para><para>
		/// For the B/W rasterizer, ‘pitch’ is always an even number.
		/// </para><para>
		/// To change the pitch of a bitmap (say, to make it a multiple of 4),
		/// use FT_Bitmap_Convert. Alternatively, you might use callback
		/// functions to directly render to the application's surface; see the
		/// file ‘example2.cpp’ in the tutorial for a demonstration.
		/// </para></summary>
		public int Pitch
		{
			get
			{
				return rec.pitch;
			}
		}

		/// <summary>
		/// Gets a typeless pointer to the bitmap buffer. This value should be
		/// aligned on 32-bit boundaries in most cases.
		/// </summary>
		public IntPtr Buffer
		{
			get
			{
				return rec.buffer;
			}
		}

		/// <summary>
		/// This field is only used with FT_PIXEL_MODE_GRAY; it gives the
		/// number of gray levels used in the bitmap.
		/// </summary>
		public short GrayLevels
		{
			get
			{
				return rec.num_grays;
			}
		}

		/// <summary>
		/// Gets the pixel mode, i.e., how pixel bits are stored.
		/// </summary>
		public PixelMode PixelMode
		{
			get
			{
				return rec.pixel_mode;
			}
		}

		/// <summary>
		/// This field is intended for paletted pixel modes; it indicates how
		/// the palette is stored.
		/// </summary>
		[Obsolete("Not used currently.")]
		public byte PaletteMode
		{
			get
			{
				return rec.palette_mode;
			}
		}

		/// <summary>
		/// Gets a typeless pointer to the bitmap palette; this field is
		/// intended for paletted pixel modes.
		/// </summary>
		[Obsolete("Not used currently.")]
		public IntPtr Palette
		{
			get
			{
				return rec.palette;
			}
		}
	}
}
