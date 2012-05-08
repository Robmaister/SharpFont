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

using SharpFont.TrueType;

namespace SharpFont
{
	/// <summary><para>
	/// A handle to a FreeType library instance. Each ‘library’ is completely independent from the others; it is the
	/// ‘root’ of a set of objects like fonts, faces, sizes, etc.
	/// </para><para>
	/// It also embeds a memory manager (see <see cref="Memory"/>), as well as a scan-line converter object (see
	/// <see cref="Raster"/>).
	/// </para><para>
	/// For multi-threading applications each thread should have its own <see cref="Library"/> object.
	/// </para></summary>
	public sealed class Library : IDisposable
	{
		#region Fields

		private IntPtr reference;

		private bool customMemory;
		private bool disposed;

		private List<Face> childFaces;
		private List<Glyph> childGlyphs;

		#endregion

		#region Constructors

		private Library(bool duplicate)
		{
			childFaces = new List<Face>();
			childGlyphs = new List<Glyph>();

			if (duplicate)
				FT.ReferenceLibrary(this);
		}

		/// <summary>
		/// Initializes a new instance of the Library class.
		/// </summary>
		public Library()
			: this(false)
		{
			//duplicate the error checking code from FT.InitFreeType, it's the
			//simplest way to create a new Library without making copies.
			IntPtr libraryRef;
			Error err = FT.FT_Init_FreeType(out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			Reference = libraryRef;
		}

		/// <summary>
		/// Initializes a new instance of the Library class.
		/// </summary>
		/// <param name="memory">A custom FreeType memory manager.</param>
		public Library(Memory memory)
			: this(false)
		{
			IntPtr libraryRef;
			Error err = FT.FT_New_Library(memory.Reference, out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			Reference = libraryRef;
			customMemory = true;
		}

		internal Library(IntPtr reference, bool duplicate)
			: this(duplicate)
		{
			Reference = reference;
		}

		/// <summary>
		/// Finalizes an instance of the Library class.
		/// </summary>
		~Library()
		{
			Dispose(false);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the object has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get
			{
				return disposed;
			}
		}

		/// <summary>
		/// Return the version of the FreeType library being used.
		/// </summary>
		/// <remarks>See <see cref="FT.LibraryVersion"/>.</remarks>
		public Version Version
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Version", "Cannot access a disposed object.");

				int major, minor, patch;
				FT.LibraryVersion(this, out major, out minor, out patch);
				return new Version(major, minor, patch);
			}
		}

		internal IntPtr Reference
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				return reference;
			}

			set
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				reference = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font by its 
		/// pathname.
		/// </summary>
		/// <param name="path">A path to the font file.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public Face NewFace(string path, int faceIndex)
		{
			return FT.NewFace(this, path, faceIndex);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font which has
		/// been loaded into memory.
		/// </summary>
		/// <remarks>See <see cref="FT.NewMemoryFace"/>.</remarks>
		/// <param name="file">A pointer to the beginning of the font data.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public Face NewMemoryFace(byte[] file, int faceIndex)
		{
			return FT.NewMemoryFace(this, file, faceIndex);
		}

		/// <summary>
		/// Create a <see cref="Face"/> object from a given resource described
		/// by <see cref="OpenArgs"/>.
		/// </summary>
		/// <remarks>See <see cref="FT.OpenFace"/>.</remarks>
		/// <param name="args">
		/// A pointer to an <see cref="OpenArgs"/> structure which must be filled by the caller.
		/// </param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If ‘face_index’ is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		public Face OpenFace(OpenArgs args, int faceIndex)
		{
			return FT.OpenFace(this, args, faceIndex);
		}

		/// <summary>
		/// Create a new face object from a FOND resource.
		/// </summary>
		/// <remarks>See <see cref="FT.NewFaceFromFOND"/>.</remarks>
		/// <param name="fond">A FOND resource.</param>
		/// <param name="faceIndex">Only supported for the -1 ‘sanity check’ special case.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFOND(IntPtr fond, int faceIndex)
		{
			return FT.NewFaceFromFOND(this, fond, faceIndex);
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index using an FSSpec to the font file.
		/// </summary>
		/// <remarks>See <see cref="FT.NewFaceFromFSSpec"/>.</remarks>
		/// <param name="spec">FSSpec to the font file.</param>
		/// <param name="faceIndex">The index of the face within the resource. The first face has index 0.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFSSpec(IntPtr spec, int faceIndex)
		{
			return FT.NewFaceFromFSSpec(this, spec, faceIndex);
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index using an FSRef to the font file.
		/// </summary>
		/// <remarks>See <see cref="FT.NewFaceFromFSRef"/>.</remarks>
		/// <param name="ref">FSRef to the font file.</param>
		/// <param name="faceIndex">The index of the face within the resource. The first face has index 0.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFSRef(IntPtr @ref, int faceIndex)
		{
			return FT.NewFaceFromFSRef(this, @ref, faceIndex);
		}

		/// <summary>
		/// Add a new module to a given library instance.
		/// </summary>
		/// <remarks>See <see cref="FT.AddModule"/>.</remarks>
		/// <param name="clazz">A pointer to class descriptor for the module.</param>
		public void AddModule(ModuleClass clazz)
		{
			FT.AddModule(this, clazz);
		}

		/// <summary>
		/// Find a module by its name.
		/// </summary>
		/// <remarks>See <see cref="FT.GetModule"/>.</remarks>
		/// <param name="moduleName">The module's name (as an ASCII string).</param>
		/// <returns>A module handle. 0 if none was found.</returns>
		public Module GetModule(string moduleName)
		{
			return FT.GetModule(this, moduleName);
		}

		/// <summary>
		/// Remove a given module from a library instance.
		/// </summary>
		/// <remarks>See <see cref="FT.RemoveModule"/>.</remarks>
		/// <param name="module">A handle to a module object.</param>
		public void RemoveModule(Module module)
		{
			FT.RemoveModule(this, module);
		}

		/// <summary>
		/// Set a debug hook function for debugging the interpreter of a font format.
		/// </summary>
		/// <remarks>See <see cref="FT.SetDebugHook"/>.</remarks>
		/// <param name="hookIndex">
		/// The index of the debug hook. You should use the values defined in ‘ftobjs.h’, e.g.,
		/// ‘FT_DEBUG_HOOK_TRUETYPE’.
		/// </param>
		/// <param name="debugHook">The function used to debug the interpreter.</param>
		[CLSCompliant(false)]
		public void SetDebugHook(uint hookIndex, IntPtr debugHook)
		{
			FT.SetDebugHook(this, hookIndex, debugHook);
		}

		/// <summary>
		/// Add the set of default drivers to a given library object. This is only useful when you create a library
		/// object with <see cref="FT.NewLibrary"/> (usually to plug a custom memory manager).
		/// </summary>
		public void AddDefaultModules()
		{
			FT.AddDefaultModules(this);
		}

		/// <summary>
		/// Retrieve the current renderer for a given glyph format.
		/// </summary>
		/// <remarks>See <see cref="FT.GetRenderer"/>.</remarks>
		/// <param name="format">The glyph format.</param>
		/// <returns>A renderer handle. 0 if none found.</returns>
		[CLSCompliant(false)]
		public Renderer GetRenderer(GlyphFormat format)
		{
			return FT.GetRenderer(this, format);
		}

		/// <summary>
		/// Set the current renderer to use, and set additional mode.
		/// </summary>
		/// <remarks>See <see cref="FT.SetRenderer"/>.</remarks>
		/// <param name="renderer">A handle to the renderer object.</param>
		/// <param name="numParams">The number of additional parameters.</param>
		/// <param name="parameters">Additional parameters.</param>
		[CLSCompliant(false)]
		public void SetRenderer(Renderer renderer, uint numParams, Parameter[] parameters)
		{
			FT.SetRenderer(this, renderer, numParams, parameters);
		}

		/// <summary>
		/// This function is used to apply color filtering to LCD decimated bitmaps, like the ones used when calling
		/// <see cref="FT.RenderGlyph"/> with <see cref="RenderMode.LCD"/> or <see cref="RenderMode.VerticalLCD"/>.
		/// </summary>
		/// <remarks>See <see cref="FT.LibrarySetLcdFilter"/>.</remarks>
		/// <param name="filter"><para>
		/// The filter type.
		/// </para><para>
		/// You can use <see cref="LcdFilter.None"/> here to disable this feature, or <see cref="LcdFilter.Default"/>
		/// to use a default filter that should work well on most LCD screens.
		/// </para></param>
		public void SetLcdFilter(LcdFilter filter)
		{
			FT.LibrarySetLcdFilter(this, filter);
		}

		/// <summary>
		/// Use this function to override the filter weights selected by <see cref="FT.LibrarySetLcdFilter"/>. By
		/// default, FreeType uses the quintuple (0x00, 0x55, 0x56, 0x55, 0x00) for <see cref="LcdFilter.Light"/>, and
		/// (0x10, 0x40, 0x70, 0x40, 0x10) for <see cref="LcdFilter.Default"/> and <see cref="LcdFilter.Legacy"/>.
		/// </summary>
		/// <remarks>See <see cref="FT.LibrarySetLcdFilterWeights"/>.</remarks>
		/// <param name="weights">
		/// A pointer to an array; the function copies the first five bytes and uses them to specify the filter
		/// weights.
		/// </param>
		public void SetLcdFilterWeights(byte[] weights)
		{
			FT.LibrarySetLcdFilterWeights(this, weights);
		}

		/// <summary>
		/// Return an <see cref="EngineType"/> value to indicate which level of the TrueType virtual machine a given
		/// library instance supports.
		/// </summary>
		/// <returns>A value indicating which level is supported.</returns>
		public EngineType GetTrueTypeEngineType()
		{
			return FT.GetTrueTypeEngineType(this);
		}

		#endregion

		#region Internal Methods

		internal void AddChildFace(Face child)
		{
			childFaces.Add(child);
		}

		internal void AddChildGlyph(Glyph child)
		{
			childGlyphs.Add(child);
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes the Library.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;

				//dipose all the children before disposing the library.
				foreach (Face f in childFaces)
					f.Dispose();

				foreach (Glyph g in childGlyphs)
					g.Dispose();

				childFaces.Clear();
				childGlyphs.Clear();

				Error err = (customMemory) ? FT.FT_Done_Library(reference) : FT.FT_Done_FreeType(reference);

				if (err != Error.Ok)
					throw new FreeTypeException(err);

				reference = IntPtr.Zero;
			}
		}

		#endregion
	}
}
