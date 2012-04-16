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
	/// A simple structure used to store a 2D vector.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct FTVector
	{
		private FT_Long x;
		private FT_Long y;

		public FTVector(int x, int y)
			: this()
		{
#if WIN64
			this.x = x;
			this.y = y;
#else
			this.x = (IntPtr)x;
			this.y = (IntPtr)y;
#endif
		}

		internal FTVector(IntPtr reference)
			: this()
		{
#if WIN64
			this.x = Marshal.ReadInt32(reference);
			this.y = Marshal.ReadInt32(reference, sizeof(int));
#else
			this.x = Marshal.ReadIntPtr(reference);
			this.y = Marshal.ReadIntPtr(reference, IntPtr.Size);
#endif
		}

		/// <summary>
		/// Gets the horizontal coordinate.
		/// </summary>
		public int X
		{
			get
			{
				return (int)x;
			}

			set
			{
#if WIN64
				x = value;
#else
				x = (IntPtr)value;
#endif
			}
		}

		/// <summary>
		/// Gets the vertical coordinate.
		/// </summary>
		public int Y
		{
			get
			{
				return (int)y;
			}

			set
			{
#if WIN64
				y = value;
#else
				y = (IntPtr)value;
#endif
			}
		}
	}
}
