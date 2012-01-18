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
	public partial class FT
	{
		#region FreeType Version

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FT_Library_Version(IntPtr library, out int amajor, out int aminor, out int apatch);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool FT_Face_CheckTrueTypePatents(ref Face face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool FT_Face_SetUnpatentedHinting(ref Face face, bool value);

		#endregion

		#region Base Interface
		
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Init_FreeType(out IntPtr alibrary);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Done_FreeType(IntPtr library);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_New_Face(IntPtr library, string filepathname, int face_index, out Face aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_New_Memory_Face(IntPtr library, [In] byte[] file_base, int file_size, int face_index, out Face aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Open_Face(IntPtr library, ref OpenArgs args, int face_index, out Face aface);

		#endregion

		/// <summary>
		/// This function calls FT_Attach_Stream to attach a file.
		/// </summary>
		/// <param name="face">The target face object.</param>
		/// <param name="filepathname">The pathname</param>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_File(ref Face face, string filepathname);

		/// <summary>
		/// ‘Attach’ data to a face object. Normally, this is used to read additional information for the face object. For example, you can attach an AFM file that comes with a Type 1 font to get the kerning values and other metrics
		/// </summary>
		/// <param name="face">The target face object</param>
		/// <param name="parameters">A pointer to FT_Open_Args which must be filled by the caller</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_Stream(ref Face face, ref OpenArgs parameters);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Reference_Face(ref Face face);

		/// <summary>
		/// Discard a given face object, as well as all of its child slots and sizes.
		/// </summary>
		/// <param name="face">A handle to a target face object.</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Done_Face(ref Face face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Size(ref Face face, int strike_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Request_Size(ref Face face, ref SizeRequest req);

		/// <summary>
		/// This function calls FT_Request_Size to request the nominal size (in points).
		/// If either the character width or height is zero, it is set equal to the other value.
		/// If either the horizontal or vertical resolution is zero, it is set equal to the other value.
		/// A character width or height smaller than 1pt is set to 1pt; if both resolution values are zero, they are set to 72dpi.
		/// </summary>
		/// <param name="face">A handle to a target face object</param>
		/// <param name="char_width">The nominal width, in 26.6 fractional points</param>
		/// <param name="char_height">The nominal height, in 26.6 fractional points</param>
		/// <param name="horz_resolution">The horizontal resolution in dpi</param>
		/// <param name="vert_resolution">The vertical resolution in dpi</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Char_Size(ref Face face, int char_width, int char_height, uint horz_resolution, uint vert_resolution);

		/// <summary>
		/// This function calls FT_Request_Size to request the nominal size (in pixels).
		/// </summary>
		/// <param name="face">A handle to the target face object.</param>
		/// <param name="pixel_width">The nominal width, in pixels.</param>
		/// <param name="pixel_height">The nominal height, in pixels</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Pixel_Sizes(ref Face face, uint pixel_width, uint pixel_height);

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a face object.
		/// The loaded glyph may be transformed. See FT_Set_Transform for the details.
		/// </summary>
		/// <param name="face">A handle to the target face object where the glyph is loaded.</param>
		/// <param name="glyph_index">The index of the glyph in the font file. For CID-keyed fonts (either in PS or in CFF format) this argument specifies the CID value.</param>
		/// <param name="load_flags">A flag indicating what to load for this glyph. The FT_LOAD_XXX constants can be used to control the glyph loading process (e.g., whether the outline should be scaled, whether to load bitmaps or not, whether to hint the outline, etc).</param>
		/// <returns>FreeType error code. 0 means success.</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Glyph(ref Face face, uint glyph_index, int load_flags);

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a face object, according to its character code.
		/// This function simply calls FT_Get_Char_Index and FT_Load_Glyph.
		/// </summary>
		/// <param name="face">A handle to a target face object where the glyph is loaded.</param>
		/// <param name="char_code">The glyph's character code, according to the current charmap used in the face</param>
		/// <param name="load_flags">A flag indicating what to load for this glyph. The FT_LOAD_XXX constants can be used to control the glyph loading process (e.g., whether the outline should be scaled, whether to load bitmaps or not, whether to hint the outline, etc).</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Char(ref Face face, uint char_code, int load_flags);

		/// <summary>
		/// A function used to set the transformation that is applied to glyph images when they are loaded into a glyph slot through FT_Load_Glyph.
		/// The transformation is only applied to scalable image formats after the glyph has been loaded. It means that hinting is unaltered by the transformation and is performed on the character size given in the last call to FT_Set_Char_Size or FT_Set_Pixel_Sizes.
		/// Note that this also transforms the ‘face.glyph.advance’ field, but not the values in ‘face.glyph.metrics’
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="matrix">A pointer to the transformation's 2x2 matrix. Use 0 for the identity matrix</param>
		/// <param name="delta">A pointer to the translation vector. Use 0 for the null vector</param>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FT_Set_Transform(ref Face face, ref Matrix2i matrix, ref Vector2i delta);

		/// <summary>
		/// Convert a given glyph image to a bitmap. It does so by inspecting the glyph image format, finding the relevant renderer, and invoking it
		/// </summary>
		/// <param name="slot">A handle to the glyph slot containing the image to convert</param>
		/// <param name="render_mode">This is the render mode used to render the glyph image into a bitmap. See FT_Render_Mode for a list of possible values</param>
		/// <returns>FreeType error code. 0 means success</returns>
		/*[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Render_Glyph(ref FT_GlyphSlotRec slot, FT_Render_Mode render_mode);*/

		/// <summary>
		/// Return the kerning vector between two glyphs of a same face
		/// </summary>
		/// <param name="face">A handle to a source face object</param>
		/// <param name="left_glyph">The index of the left glyph in the kern pair</param>
		/// <param name="right_glyph">The index of the right glyph in the kern pair</param>
		/// <param name="kern_mode">See FT_Kerning_Mode for more information. Determines the scale and dimension of the returned kerning vector</param>
		/// <param name="akerning">The kerning vector. This is either in font units or in pixels (26.6 format) for scalable formats, and in pixels for fixed-sizes formats</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Kerning(ref Face face, uint left_glyph, uint right_glyph, uint kern_mode, out Vector2i akerning);

		/// <summary>
		/// Retrieve the ASCII name of a given glyph in a face. This only works for those faces where FT_HAS_GLYPH_NAMES(face) returns 1
		/// An error is returned if the face doesn't provide glyph names or if the glyph index is invalid. In all cases of failure, the first byte of ‘buffer’ is set to 0 to indicate an empty name.
		/// The glyph name is truncated to fit within the buffer if it is too long. The returned string is always zero-terminated.
		/// This function is not compiled within the library if the config macro ‘FT_CONFIG_OPTION_NO_GLYPH_NAMES’ is defined in ‘include/freetype/config/ftoptions.h’
		/// </summary>
		/// <param name="face">A handle to a source face object</param>
		/// <param name="glyph_index">The glyph index</param>
		/// <param name="buffer">A pointer to a target buffer where the name is copied to</param>
		/// <param name="buffer_max">The maximal number of bytes available in the buffer</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Glyph_Name(IntPtr /*FaceRec*/ face, uint glyph_index, IntPtr buffer, uint buffer_max);

		/// <summary>
		/// Retrieve the ASCII Postscript name of a given face, if available. This only works with Postscript and TrueType fonts
		/// The returned pointer is owned by the face and is destroyed with it
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <returns>A pointer to the face's Postscript name. NULL if unavailable</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr /*sbyte*/ FT_Get_Postscript_Name(IntPtr /*FaceRec*/ face);

		/// <summary>
		/// Select a given charmap by its encoding tag (as listed in ‘freetype.h’).
		/// This function returns an error if no charmap in the face corresponds to the encoding queried here.
		/// Because many fonts contain more than a single cmap for Unicode encoding, this function has some special code to select the one which covers Unicode best. It is thus preferable to FT_Set_Charmap in this case
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="encoding">A handle to the selected encoding</param>
		/// <returns>FreeType error code. 0 means success</returns>
		/*[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Charmap(IntPtr face, FT_Encoding encoding);*/

		/// <summary>
		/// Select a given charmap for character code to glyph index mapping
		/// This function returns an error if the charmap is not part of the face (i.e., if it is not listed in the ‘face->charmaps’ table)
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="charmap">A handle to the selected charmap</param>
		/// <returns>FreeType error code. 0 means success</returns>
		/*[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Charmap(IntPtr face, ref FT_CharMapRec charmap);*/

		/// <summary>
		/// Retrieve index of a given charmap
		/// </summary>
		/// <param name="charmap">A handle to a charmap</param>
		/// <returns>The index into the array of character maps within the face to which ‘charmap’ belongs</returns>
		/*[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_Get_Charmap_Index(ref FT_CharMapRec charmap);*/

		/// <summary>
		/// Return the glyph index of a given character code. This function uses a charmap object to do the mapping
		/// If you use FreeType to manipulate the contents of font files directly, be aware that the glyph index returned by this function doesn't always correspond to the internal indices used within the file. This is done to ensure that value 0 always corresponds to the ‘missing glyph’.
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="charcode">The character code</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Char_Index(IntPtr /*FaceRec*/ face, uint charcode);

		/// <summary>
		/// This function is used to return the first character code in the current charmap of a given face. It also returns the corresponding glyph index.
		/// You should use this function with FT_Get_Next_Char to be able to parse all character codes available in a given charmap.
		/// Note that ‘agindex’ is set to 0 if the charmap is empty. The result itself can be 0 in two cases: if the charmap is empty or when the value 0 is the first valid character code
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="agindex">Glyph index of first character code. 0 if charmap is empty</param>
		/// <returns>The charmap's first character code</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_First_Char(IntPtr /*FaceRec*/ face, [In, Out] uint[] agindex);

		/// <summary>
		/// This function is used to return the next character code in the current charmap of a given face following the value ‘char_code’, as well as the corresponding glyph index.
		/// You should use this function with FT_Get_First_Char to walk over all character codes available in a given charmap. See the note for this function for a simple code example.
		/// Note that ‘*agindex’ is set to 0 when there are no more codes in the charmap.
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="char_code">The starting character code</param>
		/// <param name="agindex">Glyph index of first character code. 0 if charmap is empty</param>
		/// <returns>The charmap's next character code</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Next_Char(IntPtr /*FaceRec*/ face, uint char_code, [In, Out] uint[] agindex);

		/// <summary>
		/// Return the glyph index of a given glyph name. This function uses driver specific objects to do the translation
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="glyph_name">The glyph name</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Name_Index(IntPtr /*FaceRec*/ face, [In, Out] sbyte[] glyph_name);

		/// <summary>
		/// A very simple function used to perform the computation ‘(a*b)/c’ with maximal accuracy (it uses a 64-bit intermediate integer whenever necessary).
		/// This function isn't necessarily as fast as some processor specific operations, but is at least completely portable.
		/// </summary>
		/// <param name="a">The first multiplier</param>
		/// <param name="b">The second multiplier</param>
		/// <param name="c">The divisor</param>
		/// <returns>The result of ‘(a*b)/c’. This function never traps when trying to divide by zero; it simply returns ‘MaxInt’ or ‘MinInt’ depending on the signs of ‘a’ and ‘b’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_MulDiv(int a, int b, int c);

		/// <summary>
		/// A very simple function used to perform the computation ‘(a*b)/0x10000’ with maximal accuracy. Most of the time this is used to multiply a given value by a 16.16 fixed float factor
		/// This function has been optimized for the case where the absolute value of ‘a’ is less than 2048, and ‘b’ is a 16.16 scaling factor. As this happens mainly when scaling from notional units to fractional pixels in FreeType, it resulted in noticeable speed improvements between versions 2.x and 1.x.
		/// As a conclusion, always try to place a 16.16 factor as the second argument of this function; this can make a great difference
		/// </summary>
		/// <param name="a">The first multiplier</param>
		/// <param name="b">The second multiplier. Use a 16.16 factor here whenever possible</param>
		/// <returns>The result of ‘(a*b)/0x10000’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_MulFix(int a, int b);

		/// <summary>
		/// A very simple function used to perform the computation ‘(a*0x10000)/b’ with maximal accuracy. Most of the time, this is used to divide a given value by a 16.16 fixed float factor
		/// The optimization for FT_DivFix() is simple: If (a &lt;&lt; 16) fits in 32 bits, then the division is computed directly. Otherwise, we use a specialized version of FT_MulDiv
		/// </summary>
		/// <param name="a">The first multiplier</param>
		/// <param name="b">The second multiplier. Use a 16.16 factor here whenever possible</param>
		/// <returns>The result of ‘(a*0x10000)/b’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_DivFix(int a, int b);

		/// <summary>
		/// A very simple function used to round a 16.16 fixed number
		/// </summary>
		/// <param name="a">The number to be rounded</param>
		/// <returns>The result of ‘(a + 0x8000) &amp; -0x10000’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_RoundFix(int a);

		/// <summary>
		/// A very simple function used to compute the ceiling function of a 16.16 fixed number
		/// </summary>
		/// <param name="a">The number for which the ceiling function is to be computed</param>
		/// <returns>The result of ‘(a + 0x10000 - 1) &amp;-0x10000’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_CeilFix(int a);

		/// <summary>
		/// A very simple function used to compute the floor function of a 16.16 fixed number
		/// </summary>
		/// <param name="a">The number for which the floor function is to be computed</param>
		/// <returns>The result of ‘a &amp; -0x10000’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_FloorFix(int a);

		/// <summary>
		/// Transform a single vector through a 2x2 matrix.  
		/// The result is undefined if either ‘vector’ or ‘matrix’ is invalid 
		/// </summary>
		/// <param name="vec">The target vector to transform</param>
		/// <param name="matrix">A pointer to the source 2x2 matrix</param>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FT_Vector_Transform(ref Vector2i vec, ref Matrix2i matrix);
	}
}
