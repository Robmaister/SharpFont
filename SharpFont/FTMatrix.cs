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

#if WIN64
using FT_26Dot6 = System.Int32;
using FT_Fixed = System.Int32;
using FT_Long = System.Int32;
using FT_Pos = System.Int32;
using FT_ULong = System.UInt32;
#else
using FT_26Dot6 = System.IntPtr;
using FT_Fixed = System.IntPtr;
using FT_Long = System.IntPtr;
using FT_Pos = System.IntPtr;
using FT_ULong = System.UIntPtr;
#endif

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to store a 2x2 matrix. Coefficients are in 16.16 fixed float format. The computation
	/// performed is:
	/// <code>
	/// x' = x*xx + y*xy
	/// y' = x*yx + y*yy
	/// </code>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct FTMatrix
	{
		private FT_Fixed xx, xy;
		private FT_Fixed yx, yy;

		/// <summary>
		/// Initializes a new instance of the <see cref="FTMatrix"/> struct.
		/// </summary>
		/// <param name="xx">Matrix coefficient.</param>
		/// <param name="xy">Matrix coefficient.</param>
		/// <param name="yx">Matrix coefficient.</param>
		/// <param name="yy">Matrix coefficient.</param>
		public FTMatrix(int xx, int xy, int yx, int yy)
			: this()
		{
#if WIN64
			this.xx = xx;
			this.xy = xy;
			this.yx = yx;
			this.yy = yy;
#else
			this.xx = (IntPtr)xx;
			this.xy = (IntPtr)xy;
			this.yx = (IntPtr)yx;
			this.yy = (IntPtr)yy;
#endif
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FTMatrix"/> struct.
		/// </summary>
		/// <param name="row0">Matrix coefficients.</param>
		/// <param name="row1">Matrix coefficients.</param>
		public FTMatrix(FTVector row0, FTVector row1)
			: this(row0.X, row0.Y, row1.X, row1.Y)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FTMatrix"/> struct.
		/// </summary>
		/// <param name="reference">A pointer to a matrix.</param>
		internal FTMatrix(IntPtr reference)
			: this()
		{
#if WIN64
			xx = Marshal.ReadInt32(reference);
			xy = Marshal.ReadInt32(reference, sizeof(int));
			yx = Marshal.ReadInt32(reference, sizeof(int) * 2);
			yy = Marshal.ReadInt32(reference, sizeof(int) * 3);
#else
			xx = Marshal.ReadIntPtr(reference);
			xy = Marshal.ReadIntPtr(reference, IntPtr.Size);
			yx = Marshal.ReadIntPtr(reference, IntPtr.Size * 2);
			yy = Marshal.ReadIntPtr(reference, IntPtr.Size * 3);
#endif
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XX
		{
			get
			{
				return (int)xx;
			}

			set
			{
#if WIN64
				xx = value;
#else
				xx = (IntPtr)value;
#endif
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XY
		{
			get
			{
				return (int)xy;
			}

			set
			{
#if WIN64
				xy = value;
#else
				xy = (IntPtr)value;
#endif
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YX
		{
			get
			{
				return (int)yx;
			}

			set
			{
#if WIN64
				yx = value;
#else
				yx = (IntPtr)value;
#endif
			}
		}

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YY
		{
			get
			{
				return (int)yy;
			}

			set
			{
#if WIN64
				yy = value;
#else
				yy = (IntPtr)value;
#endif
			}
		}
	}
}
