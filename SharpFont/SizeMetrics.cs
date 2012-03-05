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
	/// The size metrics structure gives the metrics of a size object.
	/// </summary>
	/// <remarks><para>
	/// The scaling values, if relevant, are determined first during a size
	/// changing operation. The remaining fields are then set by the driver.
	/// For scalable formats, they are usually set to scaled values of the
	/// corresponding fields in <see cref="Face"/>.
	/// </para><para>
	/// Note that due to glyph hinting, these values might not be exact for
	/// certain fonts. Thus they must be treated as unreliable with an error
	/// margin of at least one pixel!
	/// </para><para>
	/// Indeed, the only way to get the exact metrics is to render all glyphs.
	/// As this would be a definite performance hit, it is up to client
	/// applications to perform such computations.
	/// </para><para>
	/// The SizeMetrics structure is valid for bitmap fonts also.
	/// </para></remarks>
	public sealed class SizeMetrics
	{
		internal IntPtr reference;
		internal SizeMetricsRec rec;

		internal SizeMetrics(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<SizeMetricsRec>(reference);
		}

		internal SizeMetrics(SizeMetricsRec metricsInternal)
		{
			this.reference = IntPtr.Zero;
			this.rec = metricsInternal;
		}

		/// <summary>
		/// The width of the scaled EM square in pixels, hence the term ‘ppem’
		/// (pixels per EM). It is also referred to as ‘nominal width’.
		/// </summary>
		[CLSCompliant(false)]
		public ushort NominalWidth
		{
			get
			{
				return rec.x_ppem;
			}
		}

		/// <summary>
		/// The height of the scaled EM square in pixels, hence the term ‘ppem’
		/// (pixels per EM). It is also referred to as ‘nominal width’.
		/// </summary>
		[CLSCompliant(false)]
		public ushort NominalHeight
		{
			get
			{
				return rec.y_ppem;
			}
		}

		/// <summary>
		/// A 16.16 fractional scaling value used to convert horizontal metrics
		/// from font units to 26.6 fractional pixels. Only relevant for
		/// scalable font formats.
		/// </summary>
		public int ScaleX
		{
			get
			{
				return (int)rec.x_scale;
			}
		}

		/// <summary>
		/// A 16.16 fractional scaling value used to convert vertical metrics
		/// from font units to 26.6 fractional pixels. Only relevant for
		/// scalable font formats.
		/// </summary>
		public int ScaleY
		{
			get
			{
				return (int)rec.y_scale;
			}
		}

		/// <summary>
		/// The ascender in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Ascender
		{
			get
			{
				return (int)rec.ascender;
			}
		}

		/// <summary>
		/// The descender in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Descender
		{
			get
			{
				return (int)rec.descender;
			}
		}

		/// <summary>
		/// The height in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int Height
		{
			get
			{
				return (int)rec.height;
			}
		}

		/// <summary>
		/// The maximal advance width in 26.6 fractional pixels.
		/// </summary>
		/// <see cref="Face"/>
		public int MaxAdvance
		{
			get
			{
				return (int)rec.max_advance;
			}
		}
	}
}
