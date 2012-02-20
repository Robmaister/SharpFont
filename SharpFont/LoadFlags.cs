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
	/// A list of bit-field constants used with <see cref="FT.LoadGlyph"/> to
	/// indicate what kind of operations to perform during glyph loading.
	/// </summary>
	/// <remarks><para>
	/// By default, hinting is enabled and the font's native hinter (see
	/// FT_FACE_FLAG_HINTER) is preferred over the auto-hinter. You can disable
	/// hinting by setting FT_LOAD_NO_HINTING or change the precedence by 
	/// setting FT_LOAD_FORCE_AUTOHINT. You can also set FT_LOAD_NO_AUTOHINT in
	/// case you don't want the auto-hinter to be used at all.
	/// </para><para>
	/// See the description of FT_FACE_FLAG_TRICKY for a special exception 
	/// (affecting only a handful of Asian fonts).
	/// </para><para>
	/// Besides deciding which hinter to use, you can also decide which hinting
	/// algorithm to use. See FT_LOAD_TARGET_XXX for details.
	/// </para></remarks>
	[Flags]
	[CLSCompliant(false)]
	public enum LoadFlags : uint
	{
		/// <summary>
		/// Corresponding to 0, this value is used as the default glyph load 
		/// operation. In this case, the following happens:
		/// <list type="number">
		/// <item><description>
		/// FreeType looks for a bitmap for the glyph corresponding to the 
		/// face's current size. If one is found, the function returns. The
		/// bitmap data can be accessed from the glyph slot (see note below).
		/// </description></item>
		/// <item><description>
		/// If no embedded bitmap is searched or found, FreeType looks for a 
		/// scalable outline. If one is found, it is loaded from the font file,
		/// scaled to device pixels, then ‘hinted’ to the pixel grid in order
		/// to optimize it. The outline data can be accessed from the glyph 
		/// slot (see note below).
		/// </description></item>
		/// </list>
		/// Note that by default, the glyph loader doesn't render outlines into
		/// bitmaps. The following flags are used to modify this default 
		/// behaviour to more specific and useful cases.
		/// </summary>
		Default =					0x0000,

		/// <summary><para>
		/// Don't scale the outline glyph loaded, but keep it in font units.
		/// </para><para>
		/// This flag implies FT_LOAD_NO_HINTING and FT_LOAD_NO_BITMAP, and
		/// unsets FT_LOAD_RENDER.
		/// </para></summary>
		NoScale =					0x0001,

		/// <summary><para>
		/// Disable hinting. This generally generates ‘blurrier’ bitmap glyph 
		/// when the glyph is rendered in any of the anti-aliased modes. See 
		/// also the note below.
		/// </para><para>
		/// This flag is implied by FT_LOAD_NO_SCALE.
		/// </para></summary>
		NoHinting =					0x0002,

		/// <summary><para>
		/// Call FT_Render_Glyph after the glyph is loaded. By default, the 
		/// glyph is rendered in FT_RENDER_MODE_NORMAL mode. This can be 
		/// overridden by FT_LOAD_TARGET_XXX or FT_LOAD_MONOCHROME.
		/// </para><para>
		/// This flag is unset by FT_LOAD_NO_SCALE.
		/// </para></summary>
		Render =					0x0004,

		/// <summary><para>
		/// Ignore bitmap strikes when loading. Bitmap-only fonts ignore this
		/// flag.
		/// </para><para>
		/// FT_LOAD_NO_SCALE always sets this flag.
		/// </para></summary>
		NoBitmap =					0x0008,

		/// <summary>
		/// Load the glyph for vertical text layout. Don't use it as it is 
		/// problematic currently.
		/// </summary>
		VerticalLayout =			0x0010,

		/// <summary>
		/// Indicates that the auto-hinter is preferred over the font's native
		/// hinter. See also the note below.
		/// </summary>
		ForceAutohint =				0x0020,

		/// <summary>
		/// Indicates that the font driver should crop the loaded bitmap glyph 
		/// (i.e., remove all space around its black bits). Not all drivers 
		/// implement this.
		/// </summary>
		CropBitmap =				0x0040,

		/// <summary>
		/// Indicates that the font driver should perform pedantic 
		/// verifications during glyph loading. This is mostly used to detect 
		/// broken glyphs in fonts. By default, FreeType tries to handle broken
		/// fonts also.
		/// </summary>
		Pedantic =					0x0080,

		/// <summary>
		/// Ignored. Deprecated.
		/// </summary>
		IgnoreGlobalAdvanceWidth =	0x0200,

		/// <summary><para>
		/// This flag is only used internally. It merely indicates that the 
		/// font driver should not load composite glyphs recursively. Instead,
		/// it should set the ‘num_subglyph’ and ‘subglyphs’ values of the 
		/// glyph slot accordingly, and set ‘glyph->format’ to 
		/// FT_GLYPH_FORMAT_COMPOSITE.
		/// </para><para>
		/// The description of sub-glyphs is not available to client 
		/// applications for now.
		/// </para><para>
		/// This flag implies FT_LOAD_NO_SCALE and FT_LOAD_IGNORE_TRANSFORM.
		/// </para></summary>
		NoRecurse =					0x0400,

		/// <summary>
		/// Indicates that the transform matrix set by FT_Set_Transform should 
		/// be ignored.
		/// </summary>
		IgnoreTransform =			0x0800,

		/// <summary><para>
		/// This flag is used with FT_LOAD_RENDER to indicate that you want to
		/// render an outline glyph to a 1-bit monochrome bitmap glyph, with 8 
		/// pixels packed into each byte of the bitmap data.
		/// </para><para>
		/// Note that this has no effect on the hinting algorithm used. You 
		/// should rather use FT_LOAD_TARGET_MONO so that the 
		/// monochrome-optimized hinting algorithm is used.
		/// </para></summary>
		Monochrome =				0x1000,

		/// <summary>
		/// Indicates that the ‘linearHoriAdvance’ and ‘linearVertAdvance’ 
		/// fields of FT_GlyphSlotRec should be kept in font units. See 
		/// FT_GlyphSlotRec for details.
		/// </summary>
		LinearDesign =				0x2000,

		/// <summary>
		/// Disable auto-hinter. See also the note below.
		/// </summary>
		NoAutohint =				0x8000,

		/// <summary><para>
		/// A bit-flag to be OR-ed with the ‘flags’ parameter of the
		/// <see cref="FT.GetAdvance"/> and <see cref="FT.GetAdvances"/>
		/// functions.
		/// </para><para>
		/// If set, it indicates that you want these functions to fail if the
		/// corresponding hinting mode or font driver doesn't allow for very
		/// quick advance computation.
		/// </para><para>
		/// Typically, glyphs which are either unscaled, unhinted, bitmapped,
		/// or light-hinted can have their advance width computed very quickly.
		/// </para><para>
		/// Normal and bytecode hinted modes, which require loading, scaling,
		/// and hinting of the glyph outline, are extremely slow by comparison.
		/// </para></summary>
		AdvanceFlagFastOnly =		0x20000000
	}
}
