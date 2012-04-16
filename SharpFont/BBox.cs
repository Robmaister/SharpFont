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
	/// A structure used to hold an outline's bounding box, i.e., the
	/// coordinates of its extrema in the horizontal and vertical directions.
	/// </summary>
	public sealed class BBox
	{
		#region Fields

		private IntPtr reference;
		private BBoxRec rec;

		#endregion

		#region Constructors

		internal BBox(IntPtr reference)
		{
			Reference = reference;
		}

		internal BBox(BBoxRec bboxInt)
		{
			this.rec = bboxInt;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the horizontal minimum (left-most).
		/// </summary>
		public int Left
		{
			get
			{
				return (int)rec.xMin;
			}
		}

		/// <summary>
		/// Gets the vertical minimum (bottom-most).
		/// </summary>
		public int Bottom
		{
			get
			{
				return (int)rec.yMin;
			}
		}

		/// <summary>
		/// Gets the horizontal maximum (right-most).
		/// </summary>
		public int Right
		{
			get
			{
				return (int)rec.xMax;
			}
		}

		/// <summary>
		/// Gets the vertical maximum (top-most).
		/// </summary>
		public int Top
		{
			get
			{
				return (int)rec.yMax;
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
				rec = PInvokeHelper.PtrToStructure<BBoxRec>(reference);
			}
		}

		#endregion
	}
}
