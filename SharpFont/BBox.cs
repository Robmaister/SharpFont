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
	/// A structure used to hold an outline's bounding box, i.e., the coordinates of its extrema in the horizontal and vertical directions.
	/// </summary>
	public sealed class BBox
	{
		internal IntPtr reference;

		public BBox(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// The horizontal minimum (left-most).
		/// </summary>
		public int Left
		{
			get
			{
				return Marshal.ReadInt32(reference + 0);
			}
		}

		/// <summary>
		/// The vertical minimum (bottom-most).
		/// </summary>
		public int Bottom
		{
			get
			{
				return Marshal.ReadInt32(reference + 4);
			}
		}

		/// <summary>
		/// The horizontal maximum (right-most).
		/// </summary>
		public int Right
		{
			get
			{
				return Marshal.ReadInt32(reference + 8);
			}
		}

		/// <summary>
		/// The vertical maximum (top-most).
		/// </summary>
		public int Top
		{
			get
			{
				return Marshal.ReadInt32(reference + 12);
			}
		}
	}
}
