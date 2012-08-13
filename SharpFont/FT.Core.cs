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
using System.Collections.Generic;
using System.Runtime.InteropServices;

using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// Provides an API very similar to the original FreeType API.
	/// </summary>
	/// <remarks>
	/// Useful for porting over C code that relies on FreeType. For everything else, use the instance methods of the
	/// classes provided by SharpFont, they are designed to follow .NET naming and style conventions.
	/// </remarks>
	public static partial class FT
	{
		#region Mac Specific Interface

		/// <summary>
		/// Return an FSSpec for the disk file containing the named font.
		/// </summary>
		/// <param name="fontName">Mac OS name of the font (e.g., Times New Roman Bold).</param>
		/// <param name="faceIndex">Index of the face. For passing to <see cref="Library.NewFaceFromFSSpec"/>.</param>
		/// <returns>FSSpec to the file. For passing to <see cref="Library.NewFaceFromFSSpec"/>.</returns>
		public static IntPtr GetFileFromMacName(string fontName, out int faceIndex)
		{
			IntPtr fsspec;

			Error err = FT_GetFile_From_Mac_Name(fontName, out fsspec, out faceIndex);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return fsspec;
		}

		/// <summary>
		/// Return an FSSpec for the disk file containing the named font.
		/// </summary>
		/// <param name="fontName">Mac OS name of the font in ATS framework.</param>
		/// <param name="faceIndex">Index of the face. For passing to <see cref="Library.NewFaceFromFSSpec"/>.</param>
		/// <returns>FSSpec to the file. For passing to <see cref="Library.NewFaceFromFSSpec"/>.</returns>
		public static IntPtr GetFileFromMacATSName(string fontName, out int faceIndex)
		{
			IntPtr fsspec;

			Error err = FT_GetFile_From_Mac_ATS_Name(fontName, out fsspec, out faceIndex);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return fsspec;
		}

		/// <summary>
		/// Return a pathname of the disk file and face index for given font name which is handled by ATS framework.
		/// </summary>
		/// <param name="fontName">Mac OS name of the font in ATS framework.</param>
		/// <param name="path">
		/// Buffer to store pathname of the file. For passing to <see cref="Library.NewFace"/>. The client must
		/// allocate this buffer before calling this function.
		/// </param>
		/// <returns>Index of the face. For passing to <see cref="Library.NewFace"/>.</returns>
		public unsafe static int GetFilePathFromMacATSName(string fontName, byte[] path)
		{
			int faceIndex;

			fixed (void* ptr = path)
			{
				Error err = FT_GetFilePath_From_Mac_ATS_Name(fontName, (IntPtr)ptr, path.Length, out faceIndex);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}

			return faceIndex;
		}

		#endregion
	}
}
