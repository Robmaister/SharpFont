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
	/// A structure used to describe a bitmap or pixmap to the raster. Note that we now manage pixmaps of various depths
	/// through the ‘pixel_mode’ field.
	/// </summary>
	/// <remarks>
	/// For now, the only pixel modes supported by FreeType are mono and grays. However, drivers might be added in the
	/// future to support more ‘colorful’ options.
	/// </remarks>
	public sealed class FTBitmap : IDisposable
	{
		#region Fields

		private IntPtr reference;
		private BitmapRec rec;

		private Library library;

		private bool disposed;

		#endregion

		#region Constructors

		public FTBitmap(Library library)
		{
			IntPtr bitmapRef;
			FT.FT_Bitmap_New(out bitmapRef);
			Reference = bitmapRef;

			this.library = library;
		}

		internal FTBitmap(IntPtr reference, Library library)
		{
			Reference = reference;
			this.library = library;
		}

		internal FTBitmap(BitmapRec bmpInt, Library library)
		{
			this.rec = bmpInt;
			this.library = library;
		}

		internal FTBitmap(IntPtr reference)
			: this(reference, null)
		{
		}

		internal FTBitmap(BitmapRec bmpInt)
			: this(bmpInt, null)
		{
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="FTBitmap"/> class.
		/// </summary>
		~FTBitmap()
		{
			Dispose(false);
		}

		#endregion

		#region Properties

		public bool IsDisposed { get { return disposed; } }

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
		/// Gets the pitch's absolute value is the number of bytes taken by one bitmap row, including padding. However,
		/// the pitch is positive when the bitmap has a ‘down’ flow, and negative when it has an ‘up’ flow. In all
		/// cases, the pitch is an offset to add to a bitmap pointer in order to go down one row.
		/// </para><para>
		/// Note that ‘padding’ means the alignment of a bitmap to a byte border, and FreeType functions normally align
		/// to the smallest possible integer value.
		/// </para><para>
		/// For the B/W rasterizer, ‘pitch’ is always an even number.
		/// </para><para>
		/// To change the pitch of a bitmap (say, to make it a multiple of 4), use <see cref="FTBitmap.Convert"/>.
		/// Alternatively, you might use callback functions to directly render to the application's surface; see the
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
		/// Gets a typeless pointer to the bitmap buffer. This value should be aligned on 32-bit boundaries in most
		/// cases.
		/// </summary>
		public IntPtr Buffer
		{
			get
			{
				return rec.buffer;
			}
		}

		/// <summary>
		/// This field is only used with <see cref="SharpFont.PixelMode.Gray"/>; it gives the number of gray levels
		/// used in the bitmap.
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
		/// This field is intended for paletted pixel modes; it indicates how the palette is stored.
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
		/// Gets a typeless pointer to the bitmap palette; this field is intended for paletted pixel modes.
		/// </summary>
		[Obsolete("Not used currently.")]
		public IntPtr Palette
		{
			get
			{
				return rec.palette;
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
				rec = PInvokeHelper.PtrToStructure<BitmapRec>(reference);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Copy a bitmap into another one.
		/// </summary>
		/// <param name="library">A handle to a library object.</param>
		/// <returns>A handle to the target bitmap.</returns>
		public FTBitmap Copy(Library library)
		{
			IntPtr bitmapRef;
			Error err = FT.FT_Bitmap_Copy(library.Reference, Reference, out bitmapRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTBitmap(bitmapRef);
		}

		/// <summary>
		/// Embolden a bitmap. The new bitmap will be about ‘xStrength’ pixels wider and ‘yStrength’ pixels higher. The
		/// left and bottom borders are kept unchanged.
		/// </summary>
		/// <remarks><para>
		/// The current implementation restricts ‘xStrength’ to be less than or equal to 8 if bitmap is of pixel_mode
		/// <see cref="SharpFont.PixelMode.Mono"/>.
		/// </para><para>
		/// If you want to embolden the bitmap owned by a <see cref="GlyphSlot"/>, you should call
		/// <see cref="GlyphSlot.OwnBitmap"/> on the slot first.
		/// </para></remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="xStrength">
		/// How strong the glyph is emboldened horizontally. Expressed in 26.6 pixel format.
		/// </param>
		/// <param name="yStrength">
		/// How strong the glyph is emboldened vertically. Expressed in 26.6 pixel format.
		/// </param>
		public void Embolden(Library library, int xStrength, int yStrength)
		{
			Error err = FT.FT_Bitmap_Embolden(library.Reference, Reference, xStrength, yStrength);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Convert a bitmap object with depth 1bpp, 2bpp, 4bpp, or 8bpp to a bitmap object with depth 8bpp, making the
		/// number of used bytes per line (a.k.a. the ‘pitch’) a multiple of ‘alignment’.
		/// </summary>
		/// <remarks><para>
		/// It is possible to call <see cref="Convert"/> multiple times without calling
		/// <see cref="Dispose()"/> (the memory is simply reallocated).
		/// </para><para>
		/// Use <see cref="Dispose()"/> to finally remove the bitmap object.
		/// </para><para>
		/// The ‘library’ argument is taken to have access to FreeType's memory handling functions.
		/// </para></remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="alignment">
		/// The pitch of the bitmap is a multiple of this parameter. Common values are 1, 2, or 4.
		/// </param>
		/// <returns>The target bitmap.</returns>
		public FTBitmap Convert(Library library, int alignment)
		{
			IntPtr bitmapRef;
			Error err = FT.FT_Bitmap_Convert(library.Reference, Reference, out bitmapRef, alignment);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTBitmap(bitmapRef);
		}

		#region IDisposable

		/// <summary>
		/// Disposes an instance of the <see cref="FTBitmap"/> class.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (library != null && !library.IsDisposed) //HACK set this up properly
				{
					Error err = FT.FT_Bitmap_Done(library.Reference, Reference);

					if (err != Error.Ok)
						throw new FreeTypeException(err);
				}

				disposed = true;
				reference = IntPtr.Zero;
				library = null;
			}
		}

		#endregion

		#endregion
	}
}
