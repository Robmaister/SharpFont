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
	/// A simple structure used to store a 2D vector.
	/// </summary>
	public sealed class Vector2i
	{
		internal IntPtr reference;

		internal Vector2i(IntPtr reference)
		{
			this.reference = reference;
		}

		internal Vector2i(IntPtr reference, int offset)
		{
			this.reference = new IntPtr(reference.ToInt64() + offset);
		}

		/// <summary>
		/// Initializes a new instance of the Vector2i class. X and Y default
		/// to 0.
		/// </summary>
		public Vector2i()
		{
			reference = Marshal.AllocHGlobal(SizeInBytes);
		}

		/// <summary>
		/// Initializes a new instance of the Vector2i class.
		/// </summary>
		/// <param name="x">The X component of the vector.</param>
		/// <param name="y">The Y component of the vector.</param>
		public Vector2i(int x, int y)
			: this()
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Gets the size of the vector in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 8;
			}
		}

		/// <summary>
		/// The horizontal coordinate.
		/// </summary>
		public int X
		{
			get
			{
				return Marshal.ReadInt32(reference, 0);
			}

			set
			{
				Marshal.WriteInt32(reference, 0, value);
			}
		}

		/// <summary>
		/// The vertical coordinate.
		/// </summary>
		public int Y
		{
			get
			{
				return Marshal.ReadInt32(reference, 4);
			}

			set
			{
				Marshal.WriteInt32(reference, 4, value);
			}
		}
	}
}
