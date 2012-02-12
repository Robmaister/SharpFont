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

namespace SharpFont
{
	/// <summary>
	/// A handle to a FreeType library instance. Each ‘library’ is completely
	/// independent from the others; it is the ‘root’ of a set of objects like
	/// fonts, faces, sizes, etc.
	/// 
	/// It also embeds a memory manager (see FT_Memory), as well as a scan-line
	/// converter object (see FT_Raster).
	/// 
	/// For multi-threading applications each thread should have its own
	/// FT_Library object.
	/// </summary>
	public sealed class Library : IDisposable
	{
		internal IntPtr reference;

		private bool duplicate;
		private bool disposed;

		/// <summary>
		/// Initializes a new instance of the Library class.
		/// </summary>
		public Library()
		{
			//duplicate the error checking code from FT.InitFreeType, it's the
			//simplest way to create a new Library without copies
			IntPtr libraryRef;
			Error err = FT.FT_Init_FreeType(out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			reference = libraryRef;
			duplicate = false;
		}

		internal Library(IntPtr reference, bool duplicate)
		{
			this.reference = reference;
			this.duplicate = duplicate;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				}

				if (!duplicate)
					FT.DoneFreeType(this);

				disposed = true;
			}
		}

		~Library()
		{
			Dispose(false);
		}

		public Version Version()
		{
			int major, minor, patch;
			FT.LibraryVersion(this, out major, out minor, out patch);
			return new Version(major, minor, patch);
		}

		public Face NewFace(string path, int faceIndex)
		{
			return FT.NewFace(this, path, faceIndex);
		}

		public Face NewMemoryFace(byte[] file, int faceIndex)
		{
			return FT.NewMemoryFace(this, ref file, faceIndex);
		}

		public Face OpenFace(OpenArgs args, int faceIndex)
		{
			return FT.OpenFace(this, args, faceIndex);
		}
	}
}
