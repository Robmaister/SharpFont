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
	/// A structure used to indicate how to open a new font file or stream. A
	/// pointer to such a structure can be used as a parameter for the
	/// functions <see cref="FT.OpenFace"/> and <see cref="FT.AttachStream"/>.
	/// </summary>
	/// <remarks>
	/// The stream type is determined by the contents of <see cref="Flags"/>
	/// which are tested in the following order by <see cref="FT.OpenFace"/>:
	/// <list type="bullet">
	/// <item><description>
	/// If the <see cref="OpenFlags.Memory"/> bit is set, assume that this is a
	/// memory file of <see cref="MemorySize"/> bytes, located at
	/// <see cref="MemoryBase"/>. The data are are not copied, and the client
	/// is responsible for releasing and destroying them after the
	/// corresponding call to <see cref="FT.DoneFace"/>.
	/// </description></item>
	/// <item><description>
	/// Otherwise, if the <see cref="OpenFlags.Stream"/> bit is set, assume
	/// that a custom input stream <see cref="Stream"/> is used.
	/// </description></item>
	/// <item><description>
	/// Otherwise, if the <see cref="OpenFlags.Pathname"/> bit is set, assume
	/// that this is a normal file and use <see cref="Pathname"/> to open it.
	/// </description></item>
	/// <item><description>
	/// If the <see cref="OpenFlags.Driver"/> bit is set,
	/// <see cref="FT.OpenFace"/> only tries to open the file with the driver
	/// whose handler is in <see cref="Driver"/>.
	/// </description></item>
	/// <item><description>
	/// If the <see cref="OpenFlags.Params"/> bit is set, the parameters given
	/// by <see cref="ParamsCount"/> and <see cref="Params"/> is used. They are
	/// ignored otherwise.
	/// </description></item>
	/// </list>
	/// Ideally, both the <see cref="Pathname"/> and <see cref="Params"/>
	/// fields should be tagged as ‘const’; this is missing for API backwards
	/// compatibility. In other words, applications should treat them as
	/// read-only.
	/// </remarks>
	public sealed class OpenArgs
	{
		internal IntPtr reference;

		internal OpenArgs(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// A set of bit flags indicating how to use the structure.
		/// </summary>
		public OpenFlags Flags
		{
			get
			{
				return (OpenFlags)Marshal.ReadInt32(reference + 0);
			}
		}

		/// <summary>
		/// The first byte of the file in memory.
		/// </summary>
		public IntPtr MemoryBase
		{
			get
			{
				return Marshal.ReadIntPtr(reference + 4);
			}
		}

		/// <summary>
		/// The size in bytes of the file in memory.
		/// </summary>
		public int MemorySize
		{
			get
			{
				return Marshal.ReadInt32(reference + 4 
					+ IntPtr.Size);
			}
		}

		/// <summary>
		/// A pointer to an 8-bit file pathname.
		/// </summary>
		public string Pathname
		{
			get
			{
				return Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(reference + 8
					+ IntPtr.Size));
			}
		}

		/// <summary>
		/// A handle to a source stream object.
		/// </summary>
		public Stream Stream
		{
			get
			{
				return new Stream(Marshal.ReadIntPtr(reference + 8
					+ IntPtr.Size * 2));
			}
		}

		/// <summary>
		/// This field is exclusively used by <see cref="FT.OpenFace"/>; it
		/// simply specifies the font driver to use to open the face. If set to
		/// 0, FreeType tries to load the face with each one of the drivers in
		/// its list.
		/// </summary>
		public Module Driver
		{
			get
			{
				return new Module(Marshal.ReadIntPtr(reference + 8
					+ IntPtr.Size * 3));
			}
		}

		/// <summary>
		/// The number of extra parameters.
		/// </summary>
		public int ParamsCount
		{
			get
			{
				return Marshal.ReadInt32(reference + 8
					+ IntPtr.Size * 4);
			}
		}

		/// <summary>
		/// Extra parameters passed to the font driver when opening a new face.
		/// </summary>
		public IntPtr Params;
	}
}
