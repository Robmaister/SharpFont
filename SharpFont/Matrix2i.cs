#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

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

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to store a 2x2 matrix. Coefficients are in 16.16 fixed float format. The computation performed is:
	///     <code>
	///     x' = x*xx + y*xy
	///     y' = x*yx + y*yy
	///     </code>
	/// </summary>
	public sealed class Matrix2i
	{
		internal IntPtr reference;

		internal Matrix2i(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// Gets the size of a Matrix2i, in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 16;
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XX
		{
			get
			{
				return Marshal.ReadInt32(reference + 0);
			}

			set
			{
				Marshal.WriteInt32(reference + 0, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XY
		{
			get
			{
				return Marshal.ReadInt32(reference + 4);
			}

			set
			{
				Marshal.WriteInt32(reference + 4, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YX
		{
			get
			{
				return Marshal.ReadInt32(reference + 8);
			}

			set
			{
				Marshal.WriteInt32(reference + 8, value);
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YY
		{
			get
			{
				return Marshal.ReadInt32(reference + 12);
			}

			set
			{
				Marshal.WriteInt32(reference + 12, value);
			}
		}
	}
}
