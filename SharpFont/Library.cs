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
	/// <summary><para>
	/// A handle to a FreeType library instance. Each ‘library’ is completely
	/// independent from the others; it is the ‘root’ of a set of objects like
	/// fonts, faces, sizes, etc.
	/// </para><para>
	/// It also embeds a memory manager (see FT_Memory), as well as a scan-line
	/// converter object (see FT_Raster).
	/// </para><para>
	/// For multi-threading applications each thread should have its own
	/// FT_Library object.
	/// </para></summary>
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

		/// <summary>
		/// Finalizes an instance of the Library class.
		/// </summary>
		~Library()
		{
			Dispose(false);
		}

		/// <summary>
		/// Return the version of the FreeType library being used.
		/// </summary>
		/// <remarks><para>
		/// The reason why this function takes a "library" argument is because
		/// certain programs implement library initialization in a custom way
		/// that doesn't use <see cref="FT.InitFreeType"/>.
		/// </para><para>
		/// In such cases, the library version might not be available before
		/// the library object has been created.
		/// </para></remarks>
		/// <returns>The version of the FreeType library being used.</returns>
		public Version Version()
		{
			int major, minor, patch;
			FT.LibraryVersion(this, out major, out minor, out patch);
			return new Version(major, minor, patch);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font by its 
		/// pathname.
		/// </summary>
		/// <param name="path">A path to the font file.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns> A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.</returns>
		/// <see cref="OpenFace"/>
		public Face NewFace(string path, int faceIndex)
		{
			return FT.NewFace(this, path, faceIndex);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font which has
		/// been loaded into memory.
		/// </summary>
		/// <remarks>
		/// You must not deallocate the memory before calling
		/// <see cref="FT.DoneFace"/>.
		/// </remarks>
		/// <param name="file">A pointer to the beginning of the font data.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.</returns>
		/// <see cref="OpenFace"/>
		public Face NewMemoryFace(byte[] file, int faceIndex)
		{
			return FT.NewMemoryFace(this, ref file, faceIndex);
		}

		/// <summary>
		/// Create a <see cref="Face"/> object from a given resource described
		/// by <see cref="OpenArgs"/>.
		/// </summary>
		/// <remarks><para>
		/// Unlike FreeType 1.x, this function automatically creates a glyph
		/// slot for the face object which can be accessed directly through
		/// <see cref="Face.Glyph"/>.
		/// </para><para>
		/// OpenFace can be used to quickly check whether the font format of
		/// a given font resource is supported by FreeType. If the faceIndex
		/// field is negative, the function's return value is 0 if the font
		/// format is recognized, or non-zero otherwise; the function returns
		/// a more or less empty face handle in ‘*aface’ (if ‘aface’ isn't
		/// NULL). The only useful field in this special case is
		/// <see cref="Face.FaceCount"/> which gives the number of faces within
		/// the font file. After examination, the returned FT_Face structure
		/// should be deallocated with a call to <see cref="FT.DoneFace"/>.
		/// </para><para>
		/// Each new face object created with this function also owns a default
		/// <see cref="Size"/> object, accessible as <see cref="Face.Size"/>.
		/// </para><para>
		/// See the discussion of reference counters in the description of
		/// FT_Reference_Face.
		/// </para></remarks>
		/// <param name="args">A pointer to an <see cref="OpenArgs"/> structure which must be filled by the caller.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>A handle to a new face object. If ‘face_index’ is greater than or equal to zero, it must be non-NULL.</returns>
		public Face OpenFace(OpenArgs args, int faceIndex)
		{
			return FT.OpenFace(this, args, faceIndex);
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
	}
}
