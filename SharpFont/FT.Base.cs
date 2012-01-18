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

namespace SharpFont
{
	public partial class FT
	{
		#region FaceFlags flag checks

		#region HasHorizontal

		/// <summary>
		/// A macro that returns true whenever a face object contains 
		/// horizontal metrics (this is true for all font formats though).
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the horizontal flag set, false otherwise.</returns>
		public bool HasHorizontal(FaceFlags face)
		{
			return (face & FaceFlags.Horizontal) == FaceFlags.Horizontal;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains
		/// horizontal metrics (this is true for all font formats though).
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the horizontal flag set, false otherwise.</returns>
		public bool HasHorizontal(ref Face face)
		{
			return HasHorizontal(face.FaceFlags);
		}

		#endregion

		#region HasVertical

		/// <summary>
		/// A macro that returns true whenever a face object contains vertical
		/// metrics.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the vertical flag set, false otherwise.</returns>
		public bool HasVertical(FaceFlags face)
		{
			return (face & FaceFlags.Vertical) == FaceFlags.Vertical;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains vertical 
		/// metrics.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the vertical flag set, false otherwise.</returns>
		public bool HasVertical(ref Face face)
		{
			return HasVertical(face.FaceFlags);
		}

		#endregion

		#region HasKerning

		/// <summary>
		/// A macro that returns true whenever a face object contains kerning
		/// data that can be accessed with <see cref="GetKerning"/>.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the kerning flag set, false otherwise.</returns>
		public bool HasKerning(FaceFlags face)
		{
			return (face & FaceFlags.Kerning) == FaceFlags.Kerning;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains kerning
		/// data that can be accessed with <see cref="GetKerning"/>.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the kerning flag set, false otherwise.</returns>
		public bool HasKerning(ref Face face)
		{
			return HasKerning(face.FaceFlags);
		}

		#endregion

		#region IsScalable

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// scalable font face (true for TrueType, Type 1, Type 42, CID, 
		/// OpenType/CFF, and PFR font formats.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the scalable flag set, false otherwise.</returns>
		public bool IsScalable(FaceFlags face)
		{
			return (face & FaceFlags.Scalable) == FaceFlags.Scalable;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// scalable font face (true for TrueType, Type 1, Type 42, CID, 
		/// OpenType/CFF, and PFR font formats.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the scalable flag set, false otherwise.</returns>
		public bool IsScalable(ref Face face)
		{
			return IsScalable(face.FaceFlags);
		}

		#endregion

		#region IsSFNT

		/// <summary>
		/// A macro that returns true whenever a face object contains a font 
		/// whose format is based on the SFNT storage scheme. This usually 
		/// means: TrueType fonts, OpenType fonts, as well as SFNT-based 
		/// embedded bitmap fonts.
		/// 
		/// If this macro is true, all functions defined in FT_SFNT_NAMES_H and
		/// FT_TRUETYPE_TABLES_H are available.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the SFNT flag set, false otherwise.</returns>
		public bool IsSFNT(FaceFlags face)
		{
			return (face & FaceFlags.SFNT) == FaceFlags.SFNT;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a font
		/// whose format is based on the SFNT storage scheme. This usually
		/// means: TrueType fonts, OpenType fonts, as well as SFNT-based
		/// embedded bitmap fonts.
		/// 
		/// If this macro is true, all functions defined in FT_SFNT_NAMES_H and
		/// FT_TRUETYPE_TABLES_H are available.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the SFNT flag set, false otherwise.</returns>
		public bool IsSFNT(ref Face face)
		{
			return IsSFNT(face.FaceFlags);
		}

		#endregion

		#region IsFixedWidth

		/// <summary>
		/// A macro that returns true whenever a face object contains a font 
		/// face that contains fixed-width (or ‘monospace’, ‘fixed-pitch’, 
		/// etc.) glyphs.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the fixed width flag set, false otherwise.</returns>
		public bool IsFixedWidth(FaceFlags face)
		{
			return (face & FaceFlags.FixedWidth) == FaceFlags.FixedWidth;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a font 
		/// face that contains fixed-width (or ‘monospace’, ‘fixed-pitch’, 
		/// etc.) glyphs.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the fixed width flag set, false otherwise.</returns>
		public bool IsFixedWidth(ref Face face)
		{
			return IsFixedWidth(face.FaceFlags);
		}

		#endregion

		#region HasFixedSizes

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// embedded bitmaps.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the fixed sizes flag set, false otherwise.</returns>
		/// <see cref="Face.AvailableSizes"/>
		public bool HasFixedSizes(FaceFlags face)
		{
			return (face & FaceFlags.FixedSizes) == FaceFlags.FixedSizes;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// embedded bitmaps.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the fixed sizes flag set, false otherwise.</returns>
		/// <see cref="Face.AvailableSizes"/>
		public bool HasFixedSizes(ref Face face)
		{
			return HasFixedSizes(face.FaceFlags);
		}

		#endregion

		#region HasGlyphNames

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// glyph names that can be accessed through 
		/// <see cref="GetGlyphName"/>.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the glyph names flag set, false otherwise.</returns>
		public bool HasGlyphNames(FaceFlags face)
		{
			return (face & FaceFlags.GlyphNames) == FaceFlags.GlyphNames;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// glyph names that can be accessed through 
		/// <see cref="GetGlyphName"/>.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the glyph names flag set, false otherwise.</returns>
		public bool HasGlyphNames(ref Face face)
		{
			return HasGlyphNames(face.FaceFlags);
		}

		#endregion

		#region HasMultipleMasters

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// multiple masters. The functions provided by FT_MULTIPLE_MASTERS_H
		/// are then available to choose the exact design you want.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the multiple masters flag set, false otherwise.</returns>
		public bool HasMultipleMasters(FaceFlags face)
		{
			return (face & FaceFlags.MultipleMasters) == FaceFlags.MultipleMasters;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some
		/// multiple masters. The functions provided by FT_MULTIPLE_MASTERS_H
		/// are then available to choose the exact design you want.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the multiple masters flag set, false otherwise.</returns>
		public bool HasMultipleMasters(ref Face face)
		{
			return HasMultipleMasters(face.FaceFlags);
		}

		#endregion

		#region IsCIDKeyed

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// CID-keyed font. See the discussion of FT_FACE_FLAG_CID_KEYED for 
		/// more details.
		/// 
		/// If this macro is true, all functions defined in FT_CID_H are 
		/// available.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the CID-keyed flag set, false otherwise.</returns>
		public bool IsCIDKeyed(FaceFlags face)
		{
			return (face & FaceFlags.CIDKeyed) == FaceFlags.CIDKeyed;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// CID-keyed font. See the discussion of FT_FACE_FLAG_CID_KEYED for 
		/// more details.
		/// 
		/// If this macro is true, all functions defined in FT_CID_H are 
		/// available.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the CID-keyed flag set, false otherwise.</returns>
		public bool IsCIDKeyed(ref Face face)
		{
			return IsCIDKeyed(face.FaceFlags);
		}

		#endregion

		#region IsTricky

		/// <summary>
		/// A macro that returns true whenever a face represents a ‘tricky’ font. See the discussion of FT_FACE_FLAG_TRICKY for more details.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>True if the face has the tricky flag set, false otherwise.</returns>
		public bool IsTricky(FaceFlags face)
		{
			return (face & FaceFlags.Tricky) == FaceFlags.Tricky;
		}

		/// <summary>
		/// A macro that returns true whenever a face represents a ‘tricky’ font. See the discussion of FT_FACE_FLAG_TRICKY for more details.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>True if the face has the tricky flag set, false otherwise.</returns>
		public bool IsTricky(ref Face face)
		{
			return IsTricky(face.FaceFlags);
		}

		#endregion

		#endregion

		/// <summary>
		/// Initialize a new FreeType library object. The set of modules that
		/// are registered by this function is determined at build time.
		/// </summary>
		/// <param name="alibrary">A handle to a new library object.</param>
		public static void InitFreeType(out IntPtr alibrary)
		{
			Error err = FT_Init_FreeType(out alibrary);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Destroy a given FreeType library object and all of its children, 
		/// including resources, drivers, faces, sizes, etc.
		/// </summary>
		/// <param name="library">A handle to the target library object.</param>
		public static void DoneFreeType(IntPtr library)
		{
			Error err = FT_Done_FreeType(library);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font by its 
		/// pathname.
		/// </summary>
		/// <param name="library">A handle to the library resource.</param>
		/// <param name="filepathname">A path to the font file</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0</param>
		/// <param name="aface"> A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.</param>
		/// <see cref="OpenFace"/>
		public static void NewFace(IntPtr library, string filepathname, int faceIndex, out Face aface)
		{
			Error err = FT_New_Face(library, filepathname, faceIndex, out aface);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font which has
		/// been loaded into memory.
		/// </summary>
		/// <remarks>
		/// You must not deallocate the memory before calling
		/// <see cref="DoneFace"/>.
		/// </remarks>
		/// <param name="library">A handle to the library resource</param>
		/// <param name="fileBase">A pointer to the beginning of the font data</param>
		/// <param name="fileSize">The size of the memory chunk used by the font data</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0</param>
		/// <param name="aface">A handle to a new face object. If faceIndex is greater than or equal to zero, it must be non-NULL.</param>
		/// <see cref="OpenFace"/>
		public static void NewMemoryFace(IntPtr library, ref byte[] fileBase, int fileSize, int faceIndex, out Face aface)
		{
			Error err = FT_New_Memory_Face(library, fileBase, fileSize, faceIndex, out aface);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Create a <see cref="Face"/> object from a given resource described
		/// by <see cref="OpenArgs"/>.
		/// </summary>
		/// <remarks>
		/// Unlike FreeType 1.x, this function automatically creates a glyph
		/// slot for the face object which can be accessed directly through
		/// <see cref="Face.Glyph"/>.
		/// 
		/// OpenFace can be used to quickly check whether the font format of
		/// a given font resource is supported by FreeType. If the faceIndex 
		/// field is negative, the function's return value is 0 if the font 
		/// format is recognized, or non-zero otherwise; the function returns 
		/// a more or less empty face handle in ‘*aface’ (if ‘aface’ isn't 
		/// NULL). The only useful field in this special case is 
		/// <see cref="Face.FaceCount"/> which gives the number of faces within
		/// the font file. After examination, the returned FT_Face structure
		/// should be deallocated with a call to <see cref="DoneFace"/>.
		/// 
		/// Each new face object created with this function also owns a default
		/// <see cref="Size"/> object, accessible as <see cref="Face.Size"/>.
		/// 
		/// See the discussion of reference counters in the description of 
		/// FT_Reference_Face.
		/// </remarks>
		/// <param name="library">A handle to the library resource</param>
		/// <param name="args">A pointer to an <see cref="OpenArgs"/> structure which must be filled by the caller.</param>
		/// <param name="faceIndex">The index of the face within the font. The first face has index 0.</param>
		/// <param name="aface">A handle to a new face object. If ‘face_index’ is greater than or equal to zero, it must be non-NULL.</param>
		public static void OpenFace(IntPtr library, ref OpenArgs args, int faceIndex, out Face aface)
		{
			Error err = FT_Open_Face(library, ref args, faceIndex, out aface);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}
	}
}
