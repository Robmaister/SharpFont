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
	/// A simple structure used to store a 2x2 matrix. Coefficients are in 16.16 fixed float format. The computation performed is:
	///     <code>
	///     x' = x*xx + y*xy
	///     y' = x*yx + y*yy
	///     </code>
	/// </summary>
	public sealed class FTMatrix
	{
		internal IntPtr reference;
		internal MatrixRec rec;

		internal FTMatrix(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<MatrixRec>(reference);
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XX
		{
			get
			{
				return (int)rec.xx;
			}

			set
			{
				//TODO fix this.
				//Marshal.WriteInt32(reference, 0, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XY
		{
			get
			{
				return (int)rec.xy;
			}

			set
			{
				//TODO fix this.
				//Marshal.WriteInt32(reference, 4, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YX
		{
			get
			{
				return (int)rec.yx;
			}

			set
			{
				//TODO fix this.
				//Marshal.WriteInt32(reference, 8, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YY
		{
			get
			{
				return (int)rec.yy;
			}

			set
			{
				//TODO fix this.
				//Marshal.WriteInt32(reference, 12, value);
			}
		}
	}
}
