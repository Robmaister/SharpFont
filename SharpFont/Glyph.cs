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
	public class Glyph : IDisposable
	{
		#region Fields

		private bool disposed;

		private IntPtr reference;
		private GlyphRec rec;

		private Library parentLibrary;

		#endregion

		#region Constructors

		internal Glyph(IntPtr reference, Library parentLibrary)
		{
			Reference = reference;

			this.parentLibrary = parentLibrary;
			parentLibrary.AddChildGlyph(this);
		}

		internal Glyph(GlyphRec rec, Library parentLibrary)
		{
			this.rec = rec;

			this.parentLibrary = parentLibrary;
			parentLibrary.AddChildGlyph(this);
		}

		/// <summary>
		/// Finalizes an instance of the Glyph class.
		/// </summary>
		~Glyph()
		{
			Dispose(false);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the object has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get
			{
				return disposed;
			}
		}

		/// <summary>
		/// A handle to the FreeType library object.
		/// </summary>
		public Library Library
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Library", "Cannot access a disposed object.");

				return parentLibrary;
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
				if (disposed)
					throw new ObjectDisposedException("Format", "Cannot access a disposed object.");

				return rec.format;
			}
		}

		/// <summary>
		/// A 16.16 vector that gives the glyph's advance width.
		/// </summary>
		public FTVector Advance
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Advance", "Cannot access a disposed object.");

				return rec.advance;
			}
		}

		internal IntPtr Reference
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				return reference;
			}

			set
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				reference = value;
				rec = PInvokeHelper.PtrToStructure<GlyphRec>(reference);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// A function used to copy a glyph image. Note that the created
		/// <see cref="Glyph"/> object must be released with
		/// <see cref="FT.DoneGlyph"/>.
		/// </summary>
		/// <returns>A handle to the target glyph object. 0 in case of error.</returns>
		public Glyph Copy()
		{
			return FT.GlyphCopy(this);
		}

		/// <summary>
		/// Transform a glyph image if its format is scalable.
		/// </summary>
		/// <param name="matrix">A pointer to a 2x2 matrix to apply.</param>
		/// <param name="delta">A pointer to a 2d vector to apply. Coordinates are expressed in 1/64th of a pixel.</param>
		public void Transform(FTMatrix matrix, FTVector delta)
		{
			FT.GlyphTransform(this, matrix, delta);
		}

		/// <summary><para>
		/// Return a glyph's ‘control box’. The control box encloses all the
		/// outline's points, including Bézier control points. Though it
		/// coincides with the exact bounding box for most glyphs, it can be
		/// slightly larger in some situations (like when rotating an outline
		/// which contains Bézier outside arcs).
		/// </para><para>
		/// Computing the control box is very fast, while getting the bounding
		/// box can take much more time as it needs to walk over all segments
		/// and arcs in the outline. To get the latter, you can use the
		/// ‘ftbbox’ component which is dedicated to this single task.
		/// </para></summary>
		/// <remarks>See <see cref="FT.GlyphGetCBox"/>.</remarks>
		/// <param name="mode">The mode which indicates how to interpret the returned bounding box values.</param>
		/// <returns>The glyph coordinate bounding box. Coordinates are expressed in 1/64th of pixels if it is grid-fitted.</returns>
		[CLSCompliant(false)]
		public BBox GetCBox(GlyphBBoxMode mode)
		{
			return FT.GlyphGetCBox(this, mode);
		}

		/// <summary>
		/// Disposes the Glyph.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Private Methods

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;

				FT.FT_Done_Glyph(reference);

				reference = IntPtr.Zero;
			}
		}

		#endregion
	}
}
