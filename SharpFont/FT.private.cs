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
		private static extern bool FT_Face_CheckTrueTypePatents(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern bool FT_Face_SetUnpatentedHinting(IntPtr face, bool value);

		#endregion

		#region Base Interface
		
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Init_FreeType(out IntPtr alibrary);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Done_FreeType(IntPtr library);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_New_Face(IntPtr library, [MarshalAs(UnmanagedType.LPStr)] string filepathname, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_New_Memory_Face(IntPtr library, IntPtr file_base, int file_size, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Open_Face(IntPtr library, IntPtr args, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_File(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] string filepathname);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Attach_Stream(IntPtr face, IntPtr parameters);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Reference_Face(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Done_Face(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Size(IntPtr face, int strike_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Request_Size(IntPtr face, IntPtr req);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Char_Size(IntPtr face, int char_width, int char_height, uint horz_resolution, uint vert_resolution);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Pixel_Sizes(IntPtr face, uint pixel_width, uint pixel_height);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Glyph(IntPtr face, uint glyph_index, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Load_Char(IntPtr face, uint char_code, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FT_Set_Transform(IntPtr face, IntPtr matrix, IntPtr delta);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Render_Glyph(IntPtr slot, RenderMode render_mode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Kerning(IntPtr face, uint left_glyph, uint right_glyph, KerningMode kern_mode, out IntPtr akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Track_Kerning(IntPtr face, int point_size, int degree, out int akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_Glyph_Name(IntPtr face, uint glyph_index, IntPtr buffer, uint buffer_max);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr FT_Get_Postscript_Name(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Select_Charmap(IntPtr face, Encoding encoding);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Set_Charmap(IntPtr face, IntPtr charmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int FT_Get_Charmap_Index(IntPtr charmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Char_Index(IntPtr face, uint charcode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_First_Char(IntPtr face, out uint agindex);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Next_Char(IntPtr face, uint char_code, out uint agindex);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint FT_Get_Name_Index(IntPtr face, IntPtr glyph_name);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern Error FT_Get_SubGlyph_Info(IntPtr glyph, uint sub_index, out int p_index, out SubGlyphFlags p_flags, out int p_arg1, out int p_arg2, out IntPtr p_transform);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern FSTypeFlags FT_Get_FSType_Flags(IntPtr face);

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
