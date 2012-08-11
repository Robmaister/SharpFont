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
		private List<Outline> childOutlines;

		#endregion

		#region Constructors

		private Library(bool duplicate)
		{
			childFaces = new List<Face>();
			childGlyphs = new List<Glyph>();
			childOutlines = new List<Outline>();

			if (duplicate)
				FT.ReferenceLibrary(this);
		}

		/// <summary>
		/// Initializes a new instance of the Library class.
		/// </summary>
		public Library()
			: this(false)
		{
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
		public Version Version
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Version", "Cannot access a disposed object.");

				int major, minor, patch;
				FT.FT_Library_Version(Reference, out major, out minor, out patch);
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
		/// This function calls <see cref="OpenFace"/> to open a font by its pathname.
		/// </summary>
		/// <param name="path">A path to the font file.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If ‘faceIndex’ is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public Face NewFace(string path, int faceIndex)
		{
			return new Face(this, path, faceIndex);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font which has been loaded into memory.
		/// </summary>
		/// <remarks>
		/// You must not deallocate the memory before calling <see cref="Face.Dispose()"/>.
		/// </remarks>
		/// <param name="file">A pointer to the beginning of the font data.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If ‘faceIndex’ is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public Face NewMemoryFace(byte[] file, int faceIndex)
		{
			return new Face(this, file, faceIndex);
		}

		/// <summary>
		/// Create a <see cref="Face"/> object from a given resource described by <see cref="OpenArgs"/>.
		/// </summary>
		/// <remarks><para>
		/// Unlike FreeType 1.x, this function automatically creates a glyph slot for the face object which can be
		/// accessed directly through <see cref="Face.Glyph"/>.
		/// </para><para>
		/// OpenFace can be used to quickly check whether the font format of a given font resource is supported by
		/// FreeType. If the ‘faceIndex’ field is negative, the function's return value is 0 if the font format is
		/// recognized, or non-zero otherwise; the function returns a more or less empty face handle in ‘*aface’ (if
		/// ‘aface’ isn't NULL). The only useful field in this special case is <see cref="Face.FaceCount"/> which gives
		/// the number of faces within the font file. After examination, the returned <see cref="Face"/> structure
		/// should be deallocated with a call to <see cref="Face.Dispose()"/>.
		/// </para><para>
		/// Each new face object created with this function also owns a default <see cref="FTSize"/> object, accessible
		/// as <see cref="Face.Size"/>.
		/// </para><para>
		/// See the discussion of reference counters in the description of <see cref="FT.ReferenceFace"/>.
		/// </para></remarks>
		/// <param name="args">
		/// A pointer to an <see cref="OpenArgs"/> structure which must be filled by the caller.
		/// </param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <returns>
		/// A handle to a new face object. If ‘faceIndex’ is greater than or equal to zero, it must be non-NULL.
		/// </returns>
		public Face OpenFace(OpenArgs args, int faceIndex)
		{
			if (disposed)
				throw new ObjectDisposedException("library", "Cannot access a disposed object.");

			IntPtr faceRef;

			Error err = FT.FT_Open_Face(Reference, args.Reference, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, this);
		}

		/// <summary>
		/// Create a new face object from a FOND resource.
		/// </summary>
		/// <remarks>
		/// This function can be used to create <see cref="Face"/> objects from fonts that are installed in the system
		/// as follows.
		/// <code>
		/// fond = GetResource( 'FOND', fontName );
		/// error = FT_New_Face_From_FOND( library, fond, 0, &amp;face );
		/// </code>
		/// </remarks>
		/// <param name="fond">A FOND resource.</param>
		/// <param name="faceIndex">Only supported for the -1 ‘sanity check’ special case.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFOND(IntPtr fond, int faceIndex)
		{
			if (disposed)
				throw new ObjectDisposedException("library", "Cannot access a disposed object.");

			IntPtr faceRef;

			Error err = FT.FT_New_Face_From_FOND(Reference, fond, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, this);
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index using an FSSpec to the font file.
		/// </summary>
		/// <remarks>
		/// <see cref="NewFaceFromFSSpec"/> is identical to <see cref="NewFace"/> except it accepts an FSSpec instead
		/// of a path.
		/// </remarks>
		/// <param name="spec">FSSpec to the font file.</param>
		/// <param name="faceIndex">The index of the face within the resource. The first face has index 0.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFSSpec(IntPtr spec, int faceIndex)
		{
			if (disposed)
				throw new ObjectDisposedException("library", "Cannot access a disposed object.");

			IntPtr faceRef;

			Error err = FT.FT_New_Face_From_FSSpec(Reference, spec, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, this);
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index using an FSRef to the font file.
		/// </summary>
		/// <remarks>
		/// <see cref="NewFaceFromFSRef"/> is identical to <see cref="NewFace"/> except it accepts an FSRef instead of
		/// a path.
		/// </remarks>
		/// <param name="ref">FSRef to the font file.</param>
		/// <param name="faceIndex">The index of the face within the resource. The first face has index 0.</param>
		/// <returns>A handle to a new face object.</returns>
		public Face NewFaceFromFSRef(IntPtr @ref, int faceIndex)
		{
			if (disposed)
				throw new ObjectDisposedException("library", "Cannot access a disposed object.");

			IntPtr faceRef;

			Error err = FT.FT_New_Face_From_FSRef(Reference, @ref, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, this);
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

		internal void AddChildOutline(Outline child)
		{
			childOutlines.Add(child);
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

				foreach (Outline o in childOutlines)
					o.Dispose();

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
