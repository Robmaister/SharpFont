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
		private static extern Error FT_New_Face(IntPtr library, [MarshalAs(UnmanagedType.LPStr)] string filepathname, int face_index, out Face aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_New_Memory_Face(IntPtr library, [In] byte[] file_base, int file_size, int face_index, out Face aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Open_Face(IntPtr library, ref OpenArgs args, int face_index, out Face aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_File(ref Face face, [MarshalAs(UnmanagedType.LPStr)] string filepathname);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_Stream(ref Face face, ref OpenArgs parameters);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Reference_Face(ref Face face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Done_Face(ref Face face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Size(ref Face face, int strike_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Request_Size(ref Face face, ref SizeRequest req);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Char_Size(ref Face face, int char_width, int char_height, uint horz_resolution, uint vert_resolution);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Pixel_Sizes(ref Face face, uint pixel_width, uint pixel_height);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Glyph(ref Face face, uint glyph_index, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Char(ref Face face, uint char_code, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FT_Set_Transform(ref Face face, ref Matrix2i matrix, ref Vector2i delta);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Render_Glyph(ref GlyphSlot slot, RenderMode render_mode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Kerning(ref Face face, uint left_glyph, uint right_glyph, KerningMode kern_mode, out Vector2i akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Track_Kerning(ref Face face, int point_size, int degree, out int akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Glyph_Name(ref Face face, uint glyph_index, IntPtr buffer, uint buffer_max);

		/// <summary>
		/// Retrieve the ASCII Postscript name of a given face, if available. This only works with Postscript and TrueType fonts
		/// The returned pointer is owned by the face and is destroyed with it
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <returns>A pointer to the face's Postscript name. NULL if unavailable</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr FT_Get_Postscript_Name(ref Face face);

		/// <summary>
		/// Select a given charmap by its encoding tag (as listed in ‘freetype.h’).
		/// This function returns an error if no charmap in the face corresponds to the encoding queried here.
		/// Because many fonts contain more than a single cmap for Unicode encoding, this function has some special code to select the one which covers Unicode best. It is thus preferable to FT_Set_Charmap in this case
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="encoding">A handle to the selected encoding</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Charmap(ref Face face, Encoding encoding);

		/// <summary>
		/// Select a given charmap for character code to glyph index mapping
		/// This function returns an error if the charmap is not part of the face (i.e., if it is not listed in the ‘face->charmaps’ table)
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="charmap">A handle to the selected charmap</param>
		/// <returns>FreeType error code. 0 means success</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Charmap(ref Face face, ref CharMap charmap);

		/// <summary>
		/// Retrieve index of a given charmap
		/// </summary>
		/// <param name="charmap">A handle to a charmap</param>
		/// <returns>The index into the array of character maps within the face to which ‘charmap’ belongs</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_Get_Charmap_Index(ref CharMap charmap);

		/// <summary>
		/// Return the glyph index of a given character code. This function uses a charmap object to do the mapping
		/// If you use FreeType to manipulate the contents of font files directly, be aware that the glyph index returned by this function doesn't always correspond to the internal indices used within the file. This is done to ensure that value 0 always corresponds to the ‘missing glyph’.
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="charcode">The character code</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Char_Index(ref Face face, uint charcode);

		/// <summary>
		/// This function is used to return the first character code in the current charmap of a given face. It also returns the corresponding glyph index.
		/// You should use this function with FT_Get_Next_Char to be able to parse all character codes available in a given charmap.
		/// Note that ‘agindex’ is set to 0 if the charmap is empty. The result itself can be 0 in two cases: if the charmap is empty or when the value 0 is the first valid character code
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="agindex">Glyph index of first character code. 0 if charmap is empty</param>
		/// <returns>The charmap's first character code</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_First_Char(ref Face face, out uint agindex);

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
		private static extern uint FT_Get_Next_Char(ref Face face, uint char_code, out uint agindex);

		/// <summary>
		/// Return the glyph index of a given glyph name. This function uses driver specific objects to do the translation
		/// </summary>
		/// <param name="face">A handle to the source face object</param>
		/// <param name="glyph_name">The glyph name</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’</returns>
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Name_Index(ref Face face, out IntPtr glyph_name);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_SubGlyph_Info(ref GlyphSlot glyph, uint sub_index, out int p_index, out SubGlyphFlags p_flags, out int p_arg1, out int p_arg2, out Matrix2i p_transform);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_FSType_Flags(ref Face face);

		#endregion

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
