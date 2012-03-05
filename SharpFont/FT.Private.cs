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

using SharpFont.TrueType;
using SharpFont.PostScript;

namespace SharpFont
{
	
	public static partial class FT
	{
		#region Core API

		#region FreeType Version

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Library_Version(IntPtr library, out int amajor, out int aminor, out int apatch);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FT_Face_CheckTrueTypePatents(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FT_Face_SetUnpatentedHinting(IntPtr face, bool value);

		#endregion

		#region Base Interface
		
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Init_FreeType(out IntPtr alibrary);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Done_FreeType(IntPtr library);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Face(IntPtr library, [MarshalAs(UnmanagedType.LPStr)] string filepathname, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Memory_Face(IntPtr library, IntPtr file_base, int file_size, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Open_Face(IntPtr library, IntPtr args, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Attach_File(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] string filepathname);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Attach_Stream(IntPtr face, IntPtr parameters);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Reference_Face(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Done_Face(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Select_Size(IntPtr face, int strike_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Request_Size(IntPtr face, IntPtr req);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_Char_Size(IntPtr face, int char_width, int char_height, uint horz_resolution, uint vert_resolution);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_Pixel_Sizes(IntPtr face, uint pixel_width, uint pixel_height);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Load_Glyph(IntPtr face, uint glyph_index, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Load_Char(IntPtr face, uint char_code, int load_flags);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Set_Transform(IntPtr face, IntPtr matrix, IntPtr delta);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Render_Glyph(IntPtr slot, RenderMode render_mode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Kerning(IntPtr face, uint left_glyph, uint right_glyph, KerningMode kern_mode, out IntPtr akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Track_Kerning(IntPtr face, int point_size, int degree, out int akerning);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Glyph_Name(IntPtr face, uint glyph_index, IntPtr buffer, uint buffer_max);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Get_Postscript_Name(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Select_Charmap(IntPtr face, Encoding encoding);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_Charmap(IntPtr face, IntPtr charmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Get_Charmap_Index(IntPtr charmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_Char_Index(IntPtr face, uint charcode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_First_Char(IntPtr face, out uint agindex);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_Next_Char(IntPtr face, uint char_code, out uint agindex);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_Name_Index(IntPtr face, IntPtr glyph_name);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_SubGlyph_Info(IntPtr glyph, uint sub_index, out int p_index, out SubGlyphFlags p_flags, out int p_arg1, out int p_arg2, out IntPtr p_transform);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern EmbeddingTypes FT_Get_FSType_Flags(IntPtr face);

		#endregion

		#region Glyph Variants

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Face_GetCharVariantIndex(IntPtr face, uint charcode, uint variantSelector);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Face_GetCharVariantIsDefault(IntPtr face, uint charcode, uint variantSelector);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Face_GetVariantSelectors(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Face_GetVariantsOfChar(IntPtr face, uint charcode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Face_GetCharsOfVariant(IntPtr face, uint variantSelector);

		#endregion

		#region Glyph Management

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Glyph(IntPtr slot, out IntPtr aglyph);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Glyph_Copy(IntPtr source, out IntPtr target);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Glyph_Transform(IntPtr glyph, IntPtr matrix, IntPtr delta);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Glyph_Get_CBox(IntPtr glyph, GlyphBBoxMode bbox_mode, out IntPtr acbox);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Glyph_To_Bitmap(ref IntPtr the_glyph, RenderMode render_mode, IntPtr origin, bool destroy);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Done_Glyph(IntPtr glyph);

		#endregion

		#region Mac Specific Interface

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Face_From_FOND(IntPtr library, IntPtr fond, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_GetFile_From_Mac_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, out IntPtr pathSpec, out int face_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_GetFile_From_Mac_ATS_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, out IntPtr pathSpec, out int face_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_GetFilePath_From_Mac_ATS_Name([MarshalAs(UnmanagedType.LPStr)] string fontName, IntPtr path, int maxPathSize, out int face_index);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Face_From_FSSpec(IntPtr library, IntPtr spec, int face_index, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Face_From_FSRef(IntPtr library, IntPtr @ref, int face_index, out IntPtr aface);

		#endregion

		#region Size Management

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_New_Size(IntPtr face, out IntPtr size);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Done_Size(IntPtr size);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Activate_Size(IntPtr size);

		#endregion

		#endregion

		#region Format-Specific API

		#region Multiple Masters

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Multi_Master(IntPtr face, out IntPtr amaster);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_MM_Var(IntPtr face, out IntPtr amaster);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_MM_Design_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_Var_Design_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_MM_Blend_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Set_Var_Blend_Coordinates(IntPtr face, uint num_coords, IntPtr coords);

		#endregion

		#region TrueType Tables

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Get_Sfnt_Table(IntPtr face, SfntTag tag);

		//TODO find FT_TRUETYPE_TAGS_H and create an enum for "tag"
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Load_Sfnt_Table(IntPtr face, uint tag, int offset, IntPtr buffer, ref uint length);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern Error FT_Sfnt_Table_Info(IntPtr face, uint table_index, SfntTag *tag, out uint length);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_CMap_Language_ID(IntPtr charmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Get_CMap_Format(IntPtr charmap);

		#endregion

		#region Type 1 Tables

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool FT_Has_PS_Glyph_Names(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_PS_Font_Info(IntPtr face, out IntPtr afont_info);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_PS_Font_Private(IntPtr face, out IntPtr afont_private);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Get_PS_Font_Value(IntPtr face, DictionaryKeys key, uint idx, ref IntPtr value, int value_len);

		#endregion

		#region SFNT Names

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FT_Get_Sfnt_Name_Count(IntPtr face);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Sfnt_Name(IntPtr face, uint idx, out IntPtr aname);

		#endregion

		#region BDF and PCF Files

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_BDF_Charset_ID(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] out string acharset_encoding, [MarshalAs(UnmanagedType.LPStr)] out string acharset_registry);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_BDF_Property(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] string prop_name, out IntPtr aproperty);

		#endregion

		#region CID Fonts

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_CID_Registry_Ordering_Supplement(IntPtr face, [MarshalAs(UnmanagedType.LPStr)] out string registry, [MarshalAs(UnmanagedType.LPStr)] out string ordering, out int aproperty);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_CID_Is_Internally_CID_Keyed(IntPtr face, out byte is_cid);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_CID_From_Glyph_Index(IntPtr face, uint glyph_index, out uint cid);

		#endregion

		#region PFR Fonts

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_PFR_Metrics(IntPtr face, out uint aoutline_resolution, out uint ametrics_resolution, out int ametrics_x_scale, out int ametrics_y_scale);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_PFR_Kerning(IntPtr face, uint left, uint right, out IntPtr avector);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_PFR_Advance(IntPtr face, uint gindex, out int aadvance);

		#endregion

		#region Window FNT Files

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_WinFNT_Header(IntPtr face, out IntPtr aheader);

		#endregion

		#region Font Formats

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_Get_X11_Font_Format(IntPtr face);

		#endregion

		#region Gasp Table

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Gasp FT_Get_Gasp(IntPtr face, uint ppem);

		#endregion

		#endregion

		#region Support API

		#region Computations

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_MulDiv(int a, int b, int c);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_MulFix(int a, int b);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_DivFix(int a, int b);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_RoundFix(int a);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_CeilFix(int a);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_FloorFix(int a);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Vector_Transform(ref IntPtr vec, IntPtr matrix);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Matrix_Multiply(IntPtr a, ref IntPtr b);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Matrix_Invert(ref IntPtr matrix);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Sin(int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Cos(int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Tan(int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Atan2(int x, int y);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Angle_Diff(int angle1, int angle2);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Vector_Unit(out IntPtr vec, int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Vector_Rotate(ref IntPtr vec, int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int FT_Vector_Length(IntPtr vec);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Vector_Polarize(IntPtr vec, out int length, out int angle);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Vector_From_Polar(out IntPtr vec, int length, int angle);

		#endregion

		#region List Processing

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_List_Find(IntPtr list, IntPtr data);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_List_Add(IntPtr list, IntPtr node);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_List_Insert(IntPtr list, IntPtr node);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_List_Remove(IntPtr list, IntPtr data);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_List_Up(IntPtr list, IntPtr data);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr FT_List_Iterator(IntPtr node, IntPtr user);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_List_Iterate(IntPtr list, IntPtr iterator, IntPtr user);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_List_Finalize(IntPtr list, IntPtr destroy, IntPtr memory, IntPtr user);

		#endregion

		#region Outline Processing

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_New(IntPtr library, uint numPoints, int numContours, out IntPtr anoutline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_New_Internal(IntPtr memory, uint numPoints, int numContours, out IntPtr anoutline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Done(IntPtr library, IntPtr outline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Done_Internal(IntPtr memory, IntPtr outline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Copy(IntPtr source, out IntPtr target);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Translate(IntPtr outline, int xOffset, int yOffset);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Transform(IntPtr outline, IntPtr matrix);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Embolden(IntPtr outline, int strength);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Reverse(IntPtr outline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Check(IntPtr outline);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Get_BBox(IntPtr outline, out IntPtr abbox);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Outline_Decompose(IntPtr outline, IntPtr func_interface, IntPtr user);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Get_CBox(IntPtr outline, out IntPtr acbox);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Get_Bitmap(IntPtr library, IntPtr outline, out IntPtr abitmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Outline_Render(IntPtr library, IntPtr outline, IntPtr @params);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Orientation FT_Outline_Get_Orientation(IntPtr outline);

		#endregion

		#region Quick retrieval of advance values

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Advance(IntPtr face, uint gIndex, LoadFlags load_flags, out int padvance);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Get_Advances(IntPtr face, uint start, uint count, LoadFlags load_flags, out IntPtr padvance);

		#endregion

		#region Bitmap Handling

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Bitmap_New(out IntPtr abitmap);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Bitmap_Copy(IntPtr library, IntPtr source, out IntPtr target);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Bitmap_Embolden(IntPtr library, IntPtr bitmap, int xStrength, int yStrength);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Bitmap_Convert(IntPtr library, IntPtr source, out IntPtr target, int alignment);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_GlyphSlot_Own_Bitmap(IntPtr slot);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Bitmap_Done(IntPtr library, IntPtr bitmap);

		#endregion

		#region Scanline Converter

		#endregion

		#region Glyph Stroker

		#endregion

		#region System Interface

		#endregion

		#region Module Management

		#endregion

		#region GZIP Streams

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Stream_OpenGzip(IntPtr stream, IntPtr source);

		#endregion

		#region LZW Streams

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Stream_OpenLZW(IntPtr stream, IntPtr source);

		#endregion

		#region BZIP2 Streams

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FT_Stream_OpenBzip2(IntPtr stream, IntPtr source);

		#endregion

		#region LCD Filtering

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Library_SetLcdFilter(IntPtr library, LcdFilter filter);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FT_Library_SetLcdFilterWeights(IntPtr library, byte[] weights);

		#endregion

		#endregion

		#region Miscellaneous

		#region OpenType Validation

		#endregion

		#region Incremental Loading

		#endregion

		#region The TrueType Engine

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TrueType.EngineType FT_Get_TrueType_Engine_Type(IntPtr library);

		#endregion

		#region TrueTypeGX/AAT Validation

		#endregion

		#endregion
	}
}
