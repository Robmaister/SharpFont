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
	/// The base charmap structure.
	/// </summary>
	public sealed class CharMap
	{
		internal IntPtr reference;

		public CharMap(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// A handle to the parent face object.
		/// </summary>
		public Face Face
		{
			get
			{
				return new Face(Marshal.ReadIntPtr(reference + 0));
			}
		}

		/// <summary>
		/// An <see cref="Encoding"/> tag identifying the charmap. Use this
		/// with <see cref="FT.SelectCharmap"/>.
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return (Encoding)Marshal.ReadInt32(reference + IntPtr.Size);
			}
		}

		/// <summary>
		/// An ID number describing the platform for the following encoding ID.
		/// This comes directly from the TrueType specification and should be
		/// emulated for other formats.
		/// </summary>
		public PlatformID PlatformID
		{
			get
			{
				return (PlatformID)Marshal.ReadInt32(reference + 4 + IntPtr.Size);
			}
		}

		/// <summary>
		/// A platform specific encoding number. This also comes from the
		/// TrueType specification and should be emulated similarly.
		/// </summary>
		public ushort EncodingID
		{
			get
			{
				return (ushort)Marshal.ReadInt16(reference + 8 + IntPtr.Size);
			}
		}
	}
}
