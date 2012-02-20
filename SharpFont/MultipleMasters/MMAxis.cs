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

using SharpFont.MultipleMasters.Internal;

namespace SharpFont.MultipleMasters
{
	/// <summary><para>
	/// A simple structure used to model a given axis in design space for
	/// Multiple Masters fonts.
	/// </para><para>
	/// This structure can't be used for GX var fonts.
	/// </para></summary>
	public class MMAxis
	{
		internal IntPtr reference;
		internal MMAxisInternal axisInternal;

		internal MMAxis(IntPtr reference)
		{
			this.reference = reference;
			this.axisInternal = (MMAxisInternal)Marshal.PtrToStructure(reference, typeof(MMAxisInternal));
		}

		internal MMAxis(MMAxisInternal axisInternal)
		{
			this.reference = IntPtr.Zero;
			this.axisInternal = axisInternal;
		}

		/// <summary>
		/// Gets the axis's name.
		/// </summary>
		public string Name
		{
			get
			{
				return axisInternal.name;
			}
		}

		/// <summary>
		/// Gets the axis's minimum design coordinate.
		/// </summary>
		public long Minimum
		{
			get
			{
				return axisInternal.minimum;
			}
		}

		/// <summary>
		/// Gets the axis's maximum design coordinate.
		/// </summary>
		public long Maximum
		{
			get
			{
				return axisInternal.maximum;
			}
		}
	}
}
