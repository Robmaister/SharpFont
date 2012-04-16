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
	/// The renderer module class descriptor.
	/// </summary>
	public class RendererClass
	{
		#region Fields

		private IntPtr reference;
		private RendererClassRec rec;

		#endregion

		#region Constructors

		internal RendererClass(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The root <see cref="ModuleClass"/> fields.
		/// </summary>
		public ModuleClass Root
		{
			get
			{
				return new ModuleClass(reference);
			}
		}

		/// <summary>
		/// The glyph image format this renderer handles.
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
		/// A method used to render the image that is in a given glyph slot
		/// into a bitmap.
		/// </summary>
		public IntPtr RenderGlyph
		{
			get
			{
				return rec.render_glyph;
			}
		}

		/// <summary>
		/// A method used to transform the image that is in a given glyph slot.
		/// </summary>
		public IntPtr TransformGlyph
		{
			get
			{
				return rec.transform_glyph;
			}
		}

		/// <summary>
		/// A method used to access the glyph's cbox.
		/// </summary>
		public IntPtr GetGlyphCBox
		{
			get
			{
				return rec.get_glyph_cbox;
			}
		}

		/// <summary>
		/// A method used to pass additional parameters.
		/// </summary>
		public IntPtr SetMode
		{
			get
			{
				return rec.set_mode;
			}
		}

		/// <summary>
		/// For <see cref="GlyphFormat.Outline"/> renderers only. This is a
		/// pointer to its raster's class.
		/// </summary>
		public RasterFuncs RasterClass
		{
			get
			{
				
				return new RasterFuncs(reference, Marshal.OffsetOf(typeof(RendererClassRec), "raster_class"));
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
				rec = PInvokeHelper.PtrToStructure<RendererClassRec>(reference);
			}
		}

		#endregion
	}
}
