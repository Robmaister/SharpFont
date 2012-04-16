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

using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// A function used to create a new raster object.
	/// </summary>
	/// <remarks>
	/// The ‘memory’ parameter is a typeless pointer in order to avoid
	/// un-wanted dependencies on the rest of the FreeType code. In practice,
	/// it is an <see cref="Memory"/> object, i.e., a handle to the standard
	/// FreeType memory allocator. However, this field can be completely
	/// ignored by a given raster implementation.
	/// </remarks>
	/// <param name="memory">A handle to the memory allocator.</param>
	/// <param name="raster">A handle to the new raster object.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate Error RasterNewFunc(IntPtr memory, IntPtr raster);

	/// <summary>
	/// A function used to destroy a given raster object.
	/// </summary>
	/// <param name="raster">A handle to the raster object.</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void RasterDoneFunc(IntPtr raster);

	/// <summary><para>
	/// FreeType provides an area of memory called the ‘render pool’, available
	/// to all registered rasters. This pool can be freely used during a given
	/// scan-conversion but is shared by all rasters. Its content is thus
	/// transient.
	/// </para><para>
	/// This function is called each time the render pool changes, or just
	/// after a new raster object is created.
	/// </para></summary>
	/// <remarks>
	/// Rasters can ignore the render pool and rely on dynamic memory
	/// allocation if they want to (a handle to the memory allocator is passed
	/// to the raster constructor). However, this is not recommended for
	/// efficiency purposes.
	/// </remarks>
	/// <param name="raster">A handle to the new raster object.</param>
	/// <param name="pool_base">The address in memory of the render pool.</param>
	/// <param name="pool_size">The size in bytes of the render pool.</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void RasterResetFunc(IntPtr raster, IntPtr pool_base, int pool_size);

	/// <summary>
	/// This function is a generic facility to change modes or attributes in a
	/// given raster. This can be used for debugging purposes, or simply to
	/// allow implementation-specific ‘features’ in a given raster module.
	/// </summary>
	/// <param name="raster">A handle to the new raster object.</param>
	/// <param name="mode">A 4-byte tag used to name the mode or property.</param>
	/// <param name="args">A pointer to the new mode/property to use.</param>
	[CLSCompliant(false)]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void RasterSetModeFunc(IntPtr raster, uint mode, IntPtr args);

	/// <summary>
	/// Invoke a given raster to scan-convert a given glyph image into a target
	/// bitmap.
	/// </summary>
	/// <remarks>
	/// The exact format of the source image depends on the raster's glyph
	/// format defined in its <see cref="RasterFuncs"/> structure. It can be an
	/// <see cref="Outline"/> or anything else in order to support a large
	/// array of glyph formats.
	/// 
	/// Note also that the render function can fail and return a
	/// <see cref="Error.UnimplementedFeature"/> error code if the raster used
	/// does not support direct composition.
	/// 
	/// XXX: For now, the standard raster doesn't support direct composition
	/// but this should change for the final release (see the files
	/// ‘demos/src/ftgrays.c’ and ‘demos/src/ftgrays2.c’ for examples of
	/// distinct implementations which support direct composition).
	/// </remarks>
	/// <param name="raster">A handle to the raster object.</param>
	/// <param name="RasterParams">A pointer to an <see cref="RasterParams"/> structure used to store the rendering parameters.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate Error RasterRenderFunc(IntPtr raster, IntPtr RasterParams);

	/// <summary>
	/// A structure used to describe a given raster class to the library.
	/// </summary>
	public class RasterFuncs
	{
		#region Fields

		private IntPtr reference;
		private RasterFuncsRec rec;

		#endregion

		#region Constructors

		internal RasterFuncs(IntPtr reference)
		{
			Reference = reference;
		}

		internal RasterFuncs(IntPtr reference, IntPtr offset)
			: this(new IntPtr(reference.ToInt64() + offset.ToInt64()))
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// The supported glyph format for this raster.
		/// </summary>
		[CLSCompliant(false)]
		public GlyphFormat Format
		{
			get
			{
				return rec.glyph_format;
			}
		}

		/// <summary>
		/// The raster constructor.
		/// </summary>
		public RasterNewFunc New
		{
			get
			{
				return rec.raster_new;
			}
		}

		/// <summary>
		/// Used to reset the render pool within the raster.
		/// </summary>
		public RasterResetFunc Reset
		{
			get
			{
				return rec.raster_reset;
			}
		}

		/// <summary>
		/// A function to set or change modes.
		/// </summary>
		[CLSCompliant(false)]
		public RasterSetModeFunc SetMode
		{
			get
			{
				return rec.raster_set_mode;
			}
		}

		/// <summary>
		/// A function to render a glyph into a given bitmap.
		/// </summary>
		public RasterRenderFunc Render
		{
			get
			{
				return rec.raster_render;
			}
		}

		/// <summary>
		/// The raster destructor.
		/// </summary>
		public RasterDoneFunc Done
		{
			get
			{
				return rec.raster_done;
			}
		}

		internal IntPtr Reference
		{
			get
			{
				return reference;
			}

			set
			{
				reference = value;
				rec = PInvokeHelper.PtrToStructure<RasterFuncsRec>(reference);
			}
		}

		#endregion
	}
}
