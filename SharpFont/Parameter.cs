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

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to pass more or less generic parameters to
	/// FT_Open_Face.
	/// </summary>
	/// <remarks>
	/// The ID and function of parameters are driver-specific. See the various
	/// FT_PARAM_TAG_XXX flags for more information.
	/// </remarks>
	public sealed class Parameter
	{
		internal IntPtr reference;

		internal Parameter(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// Gets the size of this class, in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 4 + IntPtr.Size;
			}
		}

		/// <summary>
		/// A four-byte identification tag.
		/// </summary>
		[CLSCompliant(false)]
		public ParamTag Tag
		{
			get
			{
				return (ParamTag)Marshal.ReadInt32(reference, 0);
			}

			set
			{
				Marshal.WriteInt32(reference, 0, (int)value);
			}
		}

		/// <summary>
		/// A pointer to the parameter data.
		/// </summary>
		public IntPtr Data
		{
			get
			{
				return Marshal.ReadIntPtr(reference, 4);
			}

			set
			{
				Marshal.WriteIntPtr(reference, 4, value);
			}
		}
	}
}
