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

using SharpFont.MultipleMasters;
using SharpFont.TrueType;
using SharpFont.PostScript;

namespace SharpFont
{
	public static partial class FT
	{
		#region Multiple Masters

		/// <summary><para>
		/// Retrieve the Multiple Master descriptor of a given font.
		/// </para><para>
		/// This function can't be used with GX fonts.
		/// </para></summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The Multiple Masters descriptor.</returns>
		public static MultiMaster GetMultiMaster(Face face)
		{
			IntPtr masterRef;

			Error err = FT_Get_Multi_Master(face.reference, out masterRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new MultiMaster(masterRef);
		}

		/// <summary>
		/// Retrieve the Multiple Master/GX var descriptor of a given font.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The Multiple Masters/GX var descriptor. Allocates a data structure, which the user must free (a single call to FT_FREE will do it).</returns>
		public static MMVar GetMMVar(Face face)
		{
			IntPtr varRef;

			Error err = FT_Get_MM_Var(face.reference, out varRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new MMVar(varRef);
		}

		/// <summary><para>
		/// For Multiple Masters fonts, choose an interpolated font design through design coordinates.
		/// </para><para>
		/// This function can't be used with GX fonts.
		/// </para></summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">An array of design coordinates.</param>
		public unsafe static void SetMMDesignCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_MM_Design_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// For Multiple Master or GX Var fonts, choose an interpolated font
		/// design through design coordinates.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">An array of design coordinates.</param>
		public unsafe static void SetVarDesignCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_Var_Design_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// For Multiple Masters and GX var fonts, choose an interpolated font
		/// design through normalized blend coordinates.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">The design coordinates array (each element must be between 0 and 1.0).</param>
		public unsafe static void SetMMBlendCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_MM_Blend_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// This is another name of <see cref="SetMMBlendCoordinates"/>.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">The design coordinates array (each element must be between 0 and 1.0).</param>
		public unsafe static void SetVarBlendCoordinates(Face face, long[] coords)
		{
			SetMMBlendCoordinates(face, coords);
		}

		#endregion

		#region TrueType Tables

		/// <summary>
		/// Return a pointer to a given SFNT table within a face.
		/// </summary>
		/// <remarks><para>
		/// The table is owned by the face object and disappears with it.
		/// </para><para>
		/// This function is only useful to access SFNT tables that are loaded
		/// by the sfnt, truetype, and opentype drivers. See
		/// <see cref="SfntTag"/> for a list.
		/// </para></remarks>
		/// <param name="face">A handle to the source.</param>
		/// <param name="tag">The index of the SFNT table.</param>
		/// <returns><para>A type-less pointer to the table. This will be 0 in case of error, or if the corresponding table was not found OR loaded from the file.
		/// </para><para>
		/// Use a typecast according to ‘tag’ to access the structure elements.</para></returns>
		public static object GetSfntTable(Face face, SfntTag tag)
		{
			IntPtr tableRef = FT_Get_Sfnt_Table(face.reference, tag);

			if (tableRef == IntPtr.Zero)
				return null;

			switch (tag)
			{
				case SfntTag.Header:
					return new Header(tableRef);
				case SfntTag.HorizontalHeader:
					return new HoriHeader(tableRef);
				case SfntTag.MaxProfile:
					return new MaxProfile(tableRef);
				case SfntTag.OS2:
					return new OS2(tableRef);
				case SfntTag.PCLT:
					return new PCLT(tableRef);
				case SfntTag.Postscript:
					return new Postscript(tableRef);
				case SfntTag.VertHeader:
					return new VertHeader(tableRef);
				default:
					return null;
			}
		}

		/// <summary>
		/// Load any font table into client memory.
		/// </summary>
		/// <remarks>
		/// If you need to determine the table's length you should first call
		/// this function with ‘*length’ set to 0, as in the following example:
		/// <code>
		/// FT_ULong  length = 0;
		/// 
		/// 
		/// error = FT_Load_Sfnt_Table( face, tag, 0, NULL, &amp;length );
		/// if ( error ) { ... table does not exist ... }
		/// 
		/// buffer = malloc( length );
		/// if ( buffer == NULL ) { ... not enough memory ... }
		/// 
		/// error = FT_Load_Sfnt_Table( face, tag, 0, buffer, &amp;length );
		/// if ( error ) { ... could not load table ... }
		/// </code>
		/// </remarks>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="tag">The four-byte tag of the table to load. Use the value 0 if you want to access the whole font file. Otherwise, you can use one of the definitions found in the FT_TRUETYPE_TAGS_H file, or forge a new one with FT_MAKE_TAG.</param>
		/// <param name="offset">The starting offset in the table (or file if tag == 0).</param>
		/// <param name="buffer">The target buffer address. The client must ensure that the memory array is big enough to hold the data.</param>
		/// <param name="length"><para>If the ‘length’ parameter is NULL, then try to load the whole table. Return an error code if it fails.
		/// </para><para>
		/// Else, if ‘*length’ is 0, exit immediately while returning the table's (or file) full size in it.
		/// </para><para>
		/// Else the number of bytes to read from the table or file, from the starting offset.</para></param>
		[CLSCompliant(false)]
		public static void LoadSfntTable(Face face, uint tag, int offset, IntPtr buffer, ref uint length)
		{
			Error err = FT_Load_Sfnt_Table(face.reference, tag, offset, buffer, ref length);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Return information on an SFNT table.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="tableIndex">The index of an SFNT table. The function returns FT_Err_Table_Missing for an invalid value.</param>
		/// <param name="tag">The name tag of the SFNT table. If the value is NULL, ‘table_index’ is ignored, and ‘length’ returns the number of SFNT tables in the font.</param>
		/// <returns>The length of the SFNT table (or the number of SFNT tables, depending on ‘tag’).</returns>
		[CLSCompliant(false)]
		public unsafe static uint SfntTableInfo(Face face, uint tableIndex, SfntTag tag)
		{
			uint length;
			Error err = FT_Sfnt_Table_Info(face.reference, tableIndex, &tag, out length);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return length;
		}

		/// <summary>
		/// Only gets the number of SFNT tables.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The number of SFNT tables.</returns>
		[CLSCompliant(false)]
		public unsafe static uint SfntTableInfo(Face face)
		{
			uint length;
			Error err = FT_Sfnt_Table_Info(face.reference, 0, null, out length);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return length;
		}

		/// <summary>
		/// Return TrueType/sfnt specific cmap language ID. Definitions of
		/// language ID values are in ‘freetype/ttnameid.h’.
		/// </summary>
		/// <param name="charMap">The target charmap.</param>
		/// <returns>The language ID of ‘charmap’. If ‘charmap’ doesn't belong to a TrueType/sfnt face, just return 0 as the default value.</returns>
		[CLSCompliant(false)]
		public static uint GetCMapLanguageID(CharMap charMap)
		{
			return FT_Get_CMap_Language_ID(charMap.reference);
		}

		/// <summary>
		/// Return TrueType/sfnt specific cmap format.
		/// </summary>
		/// <param name="charMap">The target charmap.</param>
		/// <returns>The format of ‘charmap’. If ‘charmap’ doesn't belong to a TrueType/sfnt face, return -1.</returns>
		public static int GetCMapFormat(CharMap charMap)
		{
			return FT_Get_CMap_Format(charMap.reference);
		}

		#endregion

		#region Type 1 Tables

		/// <summary><para>
		/// Return true if a given face provides reliable PostScript glyph
		/// names. This is similar to using the FT_HAS_GLYPH_NAMES macro,
		/// except that certain fonts (mostly TrueType) contain incorrect glyph
		/// name tables.
		/// </para><para>
		/// When this function returns true, the caller is sure that the glyph
		/// names returned by FT_Get_Glyph_Name are reliable.
		/// </para></summary>
		/// <param name="face">face handle</param>
		/// <returns>Boolean. True if glyph names are reliable.</returns>
		public static bool HasPSGlyphNames(Face face)
		{
			return FT_Has_PS_Glyph_Names(face.reference);
		}

		/// <summary>
		/// Retrieve the PS_FontInfoRec structure corresponding to a given
		/// PostScript font.
		/// </summary>
		/// <remarks><para>
		/// The string pointers within the font info structure are owned by the
		/// face and don't need to be freed by the caller.
		/// </para><para>
		/// If the font's format is not PostScript-based, this function will
		/// return the ‘FT_Err_Invalid_Argument’ error code.
		/// </para></remarks>
		/// <param name="face">PostScript face handle.</param>
		/// <returns>Output font info structure pointer.</returns>
		public static FontInfo GetPSFontInfo(Face face)
		{
			IntPtr fontInfoRef;
			Error err = FT_Get_PS_Font_Info(face.reference, out fontInfoRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FontInfo(fontInfoRef);
		}

		/// <summary>
		/// Retrieve the PS_PrivateRec structure corresponding to a given
		/// PostScript font.
		/// </summary>
		/// <remarks><para>
		/// The string pointers within the PS_PrivateRec structure are owned by
		/// the face and don't need to be freed by the caller.
		/// </para><para>
		/// If the font's format is not PostScript-based, this function returns
		/// the ‘FT_Err_Invalid_Argument’ error code.
		/// </para></remarks>
		/// <param name="face">PostScript face handle.</param>
		/// <returns>Output private dictionary structure pointer.</returns>
		public static Private GetPSFontPrivate(Face face)
		{
			IntPtr privateRef;
			Error err = FT_Get_PS_Font_Private(face.reference, out privateRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Private(privateRef);
		}

		/// <summary>
		/// Retrieve the value for the supplied key from a PostScript font.
		/// </summary>
		/// <remarks><para>
		/// The values returned are not pointers into the internal structures
		/// of the face, but are ‘fresh’ copies, so that the memory containing
		/// them belongs to the calling application. This also enforces the
		/// ‘read-only’ nature of these values, i.e., this function cannot be
		/// used to manipulate the face.
		/// </para><para>
		/// ‘value’ is a void pointer because the values returned can be of
		/// various types.
		/// </para><para>
		/// If either ‘value’ is NULL or ‘value_len’ is too small, just the
		/// required memory size for the requested entry is returned.
		/// </para><para>
		/// The ‘idx’ parameter is used, not only to retrieve elements of, for
		/// example, the FontMatrix or FontBBox, but also to retrieve name keys
		/// from the CharStrings dictionary, and the charstrings themselves. It
		/// is ignored for atomic values.
		/// </para><para>
		/// PS_DICT_BLUE_SCALE returns a value that is scaled up by 1000. To
		/// get the value as in the font stream, you need to divide by
		/// 65536000.0 (to remove the FT_Fixed scale, and the x1000 scale).
		/// </para><para>
		/// IMPORTANT: Only key/value pairs read by the FreeType interpreter
		/// can be retrieved. So, for example, PostScript procedures such as
		/// NP, ND, and RD are not available. Arbitrary keys are, obviously,
		/// not be available either.
		/// </para><para>
		/// If the font's format is not PostScript-based, this function returns
		/// the ‘FT_Err_Invalid_Argument’ error code.
		/// </para></remarks>
		/// <param name="face">PostScript face handle.</param>
		/// <param name="key">An enumeration value representing the dictionary key to retrieve.</param>
		/// <param name="idx">For array values, this specifies the index to be returned.</param>
		/// <param name="value">A pointer to memory into which to write the value.</param>
		/// <param name="valueLength">The size, in bytes, of the memory supplied for the value.</param>
		/// <returns>The amount of memory (in bytes) required to hold the requested value (if it exists, -1 otherwise).</returns>
		public static int GetPSFontValue(Face face, DictionaryKeys key, uint idx, ref IntPtr value, int valueLength)
		{
			return FT_Get_PS_Font_Value(face.reference, key, idx, ref value, valueLength);
		}

		#endregion

		#region SFNT Names

		/// <summary>
		/// Retrieve the number of name strings in the SFNT ‘name’ table.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The number of strings in the ‘name’ table.</returns>
		[CLSCompliant(false)]
		public static uint GetSfntNameCount(Face face)
		{
			return FT_Get_Sfnt_Name_Count(face.reference);
		}

		/// <summary>
		/// Retrieve a string of the SFNT ‘name’ table for a given index.
		/// </summary>
		/// <remarks><para>
		/// The ‘string’ array returned in the ‘aname’ structure is not
		/// null-terminated. The application should deallocate it if it is no
		/// longer in use.
		/// </para><para>
		/// Use FT_Get_Sfnt_Name_Count to get the total number of available
		/// ‘name’ table entries, then do a loop until you get the right
		/// platform, encoding, and name ID.
		/// </para></remarks>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="idx">The index of the ‘name’ string.</param>
		/// <returns>The indexed FT_SfntName structure.</returns>
		[CLSCompliant(false)]
		public static SfntName GetSfntName(Face face, uint idx)
		{
			IntPtr nameRef;

			Error err = FT_Get_Sfnt_Name(face.reference, idx, out nameRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new SfntName(nameRef);
		}

		#endregion

		#region BDF and PCF Files

		#endregion

		#region CID Fonts

		#endregion

		#region PFR Fonts

		#endregion

		#region Windows FNT Files

		#endregion

		#region Font Formats

		#endregion

		#region Gasp Table

		#endregion
	}
}
