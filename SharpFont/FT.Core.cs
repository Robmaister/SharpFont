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
using System.Runtime.InteropServices;

using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// Provides an API very similar to the original FreeType API.
	/// </summary>
	/// <remarks>
	/// Useful for porting over C code that relies on FreeType. For everything
	/// else, use the instance methods of the classes provided by SharpFont,
	/// they are designed to follow .NET naming and style conventions.
	/// </remarks>
	public static partial class FT
	{
		#region FaceFlags flag checks

		#region HasHorizontal

		/// <summary>
		/// A macro that returns true whenever a face object contains 
		/// horizontal metrics (this is true for all font formats though).
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>
		/// True if the face has the horizontal flag set, false otherwise.
		/// </returns>
		public static bool HasHorizontal(FaceFlags face)
		{
			return (face & FaceFlags.Horizontal) == FaceFlags.Horizontal;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains
		/// horizontal metrics (this is true for all font formats though).
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the horizontal flag set, false otherwise.
		/// </returns>
		public static bool HasHorizontal(Face face)
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
		/// <returns>
		/// True if the face has the vertical flag set, false otherwise.
		/// </returns>
		public static bool HasVertical(FaceFlags face)
		{
			return (face & FaceFlags.Vertical) == FaceFlags.Vertical;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains vertical 
		/// metrics.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the vertical flag set, false otherwise.
		/// </returns>
		public static bool HasVertical(Face face)
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
		/// <returns>
		/// True if the face has the kerning flag set, false otherwise.
		/// </returns>
		public static bool HasKerning(FaceFlags face)
		{
			return (face & FaceFlags.Kerning) == FaceFlags.Kerning;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains kerning
		/// data that can be accessed with <see cref="GetKerning"/>.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the kerning flag set, false otherwise.
		/// </returns>
		public static bool HasKerning(Face face)
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
		/// <returns>
		/// True if the face has the scalable flag set, false otherwise.
		/// </returns>
		public static bool IsScalable(FaceFlags face)
		{
			return (face & FaceFlags.Scalable) == FaceFlags.Scalable;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// scalable font face (true for TrueType, Type 1, Type 42, CID, 
		/// OpenType/CFF, and PFR font formats.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the scalable flag set, false otherwise.
		/// </returns>
		public static bool IsScalable(Face face)
		{
			return IsScalable(face.FaceFlags);
		}

		#endregion

		#region IsSFNT

		/// <summary><para>
		/// A macro that returns true whenever a face object contains a font 
		/// whose format is based on the SFNT storage scheme. This usually 
		/// means: TrueType fonts, OpenType fonts, as well as SFNT-based 
		/// embedded bitmap fonts.
		/// </para><para>
		/// If this macro is true, all functions defined in FT_SFNT_NAMES_H and
		/// FT_TRUETYPE_TABLES_H are available.
		/// </para></summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>
		/// True if the face has the SFNT flag set, false otherwise.
		/// </returns>
		public static bool IsSFNT(FaceFlags face)
		{
			return (face & FaceFlags.SFNT) == FaceFlags.SFNT;
		}

		/// <summary><para>
		/// A macro that returns true whenever a face object contains a font
		/// whose format is based on the SFNT storage scheme. This usually
		/// means: TrueType fonts, OpenType fonts, as well as SFNT-based
		/// embedded bitmap fonts.
		/// </para><para>
		/// If this macro is true, all functions defined in FT_SFNT_NAMES_H and
		/// FT_TRUETYPE_TABLES_H are available.
		/// </para></summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the SFNT flag set, false otherwise.
		/// </returns>
		public static bool IsSFNT(Face face)
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
		/// <returns>
		/// True if the face has the fixed width flag set, false otherwise.
		/// </returns>
		public static bool IsFixedWidth(FaceFlags face)
		{
			return (face & FaceFlags.FixedWidth) == FaceFlags.FixedWidth;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a font 
		/// face that contains fixed-width (or ‘monospace’, ‘fixed-pitch’, 
		/// etc.) glyphs.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the fixed width flag set, false otherwise.
		/// </returns>
		public static bool IsFixedWidth(Face face)
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
		/// <returns>
		/// True if the face has the fixed sizes flag set, false otherwise.
		/// </returns>
		/// <see cref="Face.AvailableSizes"/>
		public static bool HasFixedSizes(FaceFlags face)
		{
			return (face & FaceFlags.FixedSizes) == FaceFlags.FixedSizes;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// embedded bitmaps.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the fixed sizes flag set, false otherwise.
		/// </returns>
		/// <see cref="Face.AvailableSizes"/>
		public static bool HasFixedSizes(Face face)
		{
			return HasFixedSizes(face.FaceFlags);
		}

		#endregion

		#region HasGlyphNames

		/// <summary>
		/// A macro that returns true whenever a face object contains some
		/// glyph names that can be accessed through 
		/// <see cref="GetGlyphName(Face, uint, int)"/>.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>
		/// True if the face has the glyph names flag set, false otherwise.
		/// </returns>
		public static bool HasGlyphNames(FaceFlags face)
		{
			return (face & FaceFlags.GlyphNames) == FaceFlags.GlyphNames;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some
		/// glyph names that can be accessed through 
		/// <see cref="GetGlyphName(Face, uint, int)"/>.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the glyph names flag set, false otherwise.
		/// </returns>
		public static bool HasGlyphNames(Face face)
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
		/// <returns>
		/// True if the face has the multiple masters flag set, false otherwise.
		/// </returns>
		public static bool HasMultipleMasters(FaceFlags face)
		{
			return (face & FaceFlags.MultipleMasters) == FaceFlags.MultipleMasters;
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some
		/// multiple masters. The functions provided by FT_MULTIPLE_MASTERS_H
		/// are then available to choose the exact design you want.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the multiple masters flag set, false otherwise.
		/// </returns>
		public static bool HasMultipleMasters(Face face)
		{
			return HasMultipleMasters(face.FaceFlags);
		}

		#endregion

		#region IsCIDKeyed

		/// <summary><para>
		/// A macro that returns true whenever a face object contains a 
		/// CID-keyed font. See the discussion of FT_FACE_FLAG_CID_KEYED for 
		/// more details.
		/// </para><para>
		/// If this macro is true, all functions defined in FT_CID_H are 
		/// available.
		/// </para></summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>
		/// True if the face has the CID-keyed flag set, false otherwise.
		/// </returns>
		public static bool IsCIDKeyed(FaceFlags face)
		{
			return (face & FaceFlags.CIDKeyed) == FaceFlags.CIDKeyed;
		}

		/// <summary><para>
		/// A macro that returns true whenever a face object contains a 
		/// CID-keyed font. See the discussion of FT_FACE_FLAG_CID_KEYED for 
		/// more details.
		/// </para><para>
		/// If this macro is true, all functions defined in FT_CID_H are 
		/// available.
		/// </para></summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the CID-keyed flag set, false otherwise.
		/// </returns>
		public static bool IsCIDKeyed(Face face)
		{
			return IsCIDKeyed(face.FaceFlags);
		}

		#endregion

		#region IsTricky

		/// <summary>
		/// A macro that returns true whenever a face represents a ‘tricky’
		/// font. See the discussion of FT_FACE_FLAG_TRICKY for more details.
		/// </summary>
		/// <param name="face">The flags for a face.</param>
		/// <returns>
		/// True if the face has the tricky flag set, false otherwise.
		/// </returns>
		public static bool IsTricky(FaceFlags face)
		{
			return (face & FaceFlags.Tricky) == FaceFlags.Tricky;
		}

		/// <summary>
		/// A macro that returns true whenever a face represents a ‘tricky’
		/// font. See the discussion of FT_FACE_FLAG_TRICKY for more details.
		/// </summary>
		/// <param name="face">The face object to test.</param>
		/// <returns>
		/// True if the face has the tricky flag set, false otherwise.
		/// </returns>
		public static bool IsTricky(Face face)
		{
			return IsTricky(face.FaceFlags);
		}

		#endregion

		#endregion

		#region FreeType Version

		/// <summary>
		/// Return the version of the FreeType library being used.
		/// </summary>
		/// <remarks><para>
		/// The reason why this function takes a "library" argument is because
		/// certain programs implement library initialization in a custom way
		/// that doesn't use <see cref="InitFreeType"/>.
		/// </para><para>
		/// In such cases, the library version might not be available before
		/// the library object has been created.
		/// </para></remarks>
		/// <param name="library">A source library handle.</param>
		/// <param name="amajor">The major version number.</param>
		/// <param name="aminor">The minor version number.</param>
		/// <param name="apatch">The patch version number.</param>
		public static void LibraryVersion(Library library, out int amajor, out int aminor, out int apatch)
		{
			FT_Library_Version(library.reference, out amajor, out aminor, out apatch);
		}

		/// <summary><para>
		/// Parse all bytecode instructions of a TrueType font file to check
		/// whether any of the patented opcodes are used. This is only useful
		/// if you want to be able to use the unpatented hinter with fonts that
		/// do not use these opcodes.
		/// </para><para>
		/// Note that this function parses all glyph instructions in the font
		/// file, which may be slow.
		/// </para></summary>
		/// <remarks>
		/// Since May 2010, TrueType hinting is no longer patented.
		/// </remarks>
		/// <param name="face">A <see cref="Face"/> handle.</param>
		/// <returns>
		/// True if this is a TrueType font that uses one of the patented
		/// opcodes, false otherwise.
		/// </returns>
		public static bool FaceCheckTrueTypePatents(Face face)
		{
			return FT_Face_CheckTrueTypePatents(face.reference);
		}

		/// <summary>
		/// Enable or disable the unpatented hinter for a given
		/// <see cref="Face"/>. Only enable it if you have determined that the
		/// face doesn't use any patented opcodes.
		/// </summary>
		/// <remarks>
		/// Since May 2010, TrueType hinting is no longer patented.
		/// </remarks>
		/// <param name="face">A face handle.</param>
		/// <param name="value">New boolean setting.</param>
		/// <returns>
		/// The old setting value. This will always be false if this is not an
		/// SFNT font, or if the unpatented hinter is not compiled in this
		/// instance of the library.
		/// </returns>
		/// <see cref="FaceCheckTrueTypePatents"/>
		public static bool FaceSetUnpatentedHinting(Face face, bool value)
		{
			return FT_Face_SetUnpatentedHinting(face.reference, value);
		}

		#endregion

		#region Base Interface

		/// <summary>
		/// Initialize a new FreeType library object. The set of modules that
		/// are registered by this function is determined at build time.
		/// </summary>
		/// <returns>A handle to a new library object.</returns>
		public static Library InitFreeType()
		{
			IntPtr libraryRef;
			Error err = FT_Init_FreeType(out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Library(libraryRef, false);
		}

		/// <summary>
		/// Destroy a given FreeType library object and all of its children, 
		/// including resources, drivers, faces, sizes, etc.
		/// </summary>
		/// <param name="library">
		/// A handle to the target library object.
		/// </param>
		public static void DoneFreeType(Library library)
		{
			Error err = FT_Done_FreeType(library.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			library.reference = IntPtr.Zero;
		}

		/// <summary>
		/// This function calls <see cref="OpenFace"/> to open a font by its 
		/// pathname.
		/// </summary>
		/// <param name="library">A handle to the library resource.</param>
		/// <param name="filepathname">A path to the font file.</param>
		/// <param name="faceIndex">
		/// The index of the face within the font. The first face has index 0.
		/// </param>
		/// <returns>
		/// A handle to a new face object. If faceIndex is greater than or
		/// equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public static Face NewFace(Library library, string filepathname, int faceIndex)
		{
			IntPtr faceRef;
			Error err = FT_New_Face(library.reference, filepathname, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, false);
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
		/// <param name="fileBase">
		/// A pointer to the beginning of the font data.
		/// </param>
		/// <param name="faceIndex">
		/// The index of the face within the font. The first face has index 0.
		/// </param>
		/// <returns>
		/// A handle to a new face object. If faceIndex is greater than or
		/// equal to zero, it must be non-NULL.
		/// </returns>
		/// <see cref="OpenFace"/>
		public static unsafe Face NewMemoryFace(Library library, ref byte[] fileBase, int faceIndex)
		{
			fixed (byte* ptr = fileBase)
			{
				IntPtr faceRef;

				Error err = FT_New_Memory_Face(library.reference, new IntPtr(ptr), fileBase.Length, faceIndex, out faceRef);

				if (err != Error.Ok)
					throw new FreeTypeException(err);

				return new Face(faceRef, false);
			}
		}

		/// <summary>
		/// Create a <see cref="Face"/> object from a given resource described
		/// by <see cref="OpenArgs"/>.
		/// </summary>
		/// <remarks><para>
		/// Unlike FreeType 1.x, this function automatically creates a glyph
		/// slot for the face object which can be accessed directly through
		/// <see cref="Face.Glyph"/>.
		/// </para><para>
		/// OpenFace can be used to quickly check whether the font format of
		/// a given font resource is supported by FreeType. If the faceIndex
		/// field is negative, the function's return value is 0 if the font
		/// format is recognized, or non-zero otherwise; the function returns
		/// a more or less empty face handle in ‘*aface’ (if ‘aface’ isn't
		/// NULL). The only useful field in this special case is
		/// <see cref="Face.FaceCount"/> which gives the number of faces within
		/// the font file. After examination, the returned FT_Face structure
		/// should be deallocated with a call to <see cref="DoneFace"/>.
		/// </para><para>
		/// Each new face object created with this function also owns a default
		/// <see cref="FTSize"/> object, accessible as <see cref="Face.Size"/>.
		/// </para><para>
		/// See the discussion of reference counters in the description of
		/// FT_Reference_Face.
		/// </para></remarks>
		/// <param name="library">A handle to the library resource</param>
		/// <param name="args">
		/// A pointer to an <see cref="OpenArgs"/> structure which must be
		/// filled by the caller.
		/// </param>
		/// <param name="faceIndex">
		/// The index of the face within the font. The first face has index 0.
		/// </param>
		/// <returns>
		/// A handle to a new face object. If ‘face_index’ is greater than or
		/// equal to zero, it must be non-NULL.
		/// </returns>
		public static Face OpenFace(Library library, OpenArgs args, int faceIndex)
		{
			IntPtr faceRef;

			Error err = FT_Open_Face(library.reference, args.reference, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, false);
		}

		/// <summary>
		/// This function calls <see cref="AttachStream"/> to attach a file.
		/// </summary>
		/// <param name="face">The target face object.</param>
		/// <param name="path">The pathname.</param>
		public static void AttachFile(Face face, string path)
		{
			Error err = FT_Attach_File(face.reference, path);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Attach’ data to a face object. Normally, this is used to read
		/// additional information for the face object. For example, you can
		/// attach an AFM file that comes with a Type 1 font to get the kerning
		/// values and other metrics.
		/// </summary>
		/// <remarks><para>
		/// The meaning of the ‘attach’ (i.e., what really happens when the new
		/// file is read) is not fixed by FreeType itself. It really depends on
		/// the font format (and thus the font driver).
		/// </para><para>
		/// Client applications are expected to know what they are doing when
		/// invoking this function. Most drivers simply do not implement file
		/// attachments.
		/// </para></remarks>
		/// <param name="face">The target face object.</param>
		/// <param name="parameters">
		/// A pointer to <see cref="OpenArgs"/> which must be filled by the
		/// caller.
		/// </param>
		public static void AttachStream(Face face, OpenArgs parameters)
		{
			Error err = FT_Attach_Stream(face.reference, parameters.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Discard a given face object, as well as all of its child slots and
		/// sizes.
		/// </summary>
		/// <param name="face">A handle to a target face object.</param>
		public static void DoneFace(Face face)
		{
			Error err = FT_Done_Face(face.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			face.reference = IntPtr.Zero;
		}

		/// <summary>
		/// Select a bitmap strike.
		/// </summary>
		/// <param name="face">A handle to a target face object.</param>
		/// <param name="strikeIndex">
		/// The index of the bitmap strike in the
		/// <see cref="Face.AvailableSizes"/> field of <see cref="Face"/>
		/// structure.
		/// </param>
		public static void SelectSize(Face face, int strikeIndex)
		{
			Error err = FT_Select_Size(face.reference, strikeIndex);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Resize the scale of the active FT_Size object in a face.
		/// </summary>
		/// <param name="face">A handle to a target face object.</param>
		/// <param name="request">
		/// A pointer to a <see cref="SizeRequest"/>.
		/// </param>
		public static void RequestSize(Face face, SizeRequest request)
		{
			Error err = FT_Request_Size(face.reference, request.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// This function calls FT_Request_Size to request the nominal size (in
		/// points).
		/// </summary>
		/// <remarks><para>
		/// If either the character width or height is zero, it is set equal to
		/// the other value.
		/// </para><para>
		/// If either the horizontal or vertical resolution is zero, it is set
		/// equal to the other value.
		/// </para><para>
		/// A character width or height smaller than 1pt is set to 1pt; if both
		/// resolution values are zero, they are set to 72dpi.
		/// </para></remarks>
		/// <param name="face">A handle to a target face object</param>
		/// <param name="charWidth">
		/// The nominal width, in 26.6 fractional points.
		/// </param>
		/// <param name="charHeight">
		/// The nominal height, in 26.6 fractional points.
		/// </param>
		/// <param name="horizontalRes">
		/// The horizontal resolution in dpi.
		/// </param>
		/// <param name="verticalRes">The vertical resolution in dpi.</param>
		[CLSCompliant(false)]
		public static void SetCharSize(Face face, int charWidth, int charHeight, uint horizontalRes, uint verticalRes)
		{
			Error err = FT_Set_Char_Size(face.reference, charWidth, charHeight, horizontalRes, verticalRes);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// This function calls <see cref="RequestSize"/> to request the
		/// nominal size (in pixels).
		/// </summary>
		/// <param name="face">A handle to the target face object.</param>
		/// <param name="pixelWidth">The nominal width, in pixels.</param>
		/// <param name="pixelHeight">The nominal height, in pixels</param>
		[CLSCompliant(false)]
		public static void SetPixelSizes(Face face, uint pixelWidth, uint pixelHeight)
		{
			Error err = FT_Set_Pixel_Sizes(face.reference, pixelWidth, pixelHeight);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a
		/// face object.
		/// </summary>
		/// <remarks><para>
		/// The loaded glyph may be transformed. See FT_Set_Transform for the
		/// details.
		/// </para><para>
		/// For subsetted CID-keyed fonts, ‘FT_Err_Invalid_Argument’ is
		/// returned for invalid CID values (this is, for CID values which
		/// don't have a corresponding glyph in the font). See the discussion
		/// of the FT_FACE_FLAG_CID_KEYED flag for more details.
		/// </para></remarks>
		/// <param name="face">
		/// A handle to the target face object where the glyph is loaded.
		/// </param>
		/// <param name="glyphIndex">
		/// The index of the glyph in the font file. For CID-keyed fonts
		/// (either in PS or in CFF format) this argument specifies the CID
		/// value.
		/// </param>
		/// <param name="flags">
		/// A flag indicating what to load for this glyph. The FT_LOAD_XXX
		/// constants can be used to control the glyph loading process (e.g.,
		/// whether the outline should be scaled, whether to load bitmaps or
		/// not, whether to hint the outline, etc).
		/// </param>
		/// <param name="target">The target to OR with the flags.</param>
		[CLSCompliant(false)]
		public static void LoadGlyph(Face face, uint glyphIndex, LoadFlags flags, LoadTarget target)
		{
			Error err = FT_Load_Glyph(face.reference, glyphIndex, (int)flags | (int)target);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a
		/// face object, according to its character code.
		/// </summary>
		/// <remarks>
		/// This function simply calls <see cref="GetCharIndex"/> and
		/// <see cref="LoadGlyph"/>
		/// </remarks>
		/// <param name="face">
		/// A handle to a target face object where the glyph is loaded.
		/// </param>
		/// <param name="charCode">
		/// The glyph's character code, according to the current charmap used
		/// in the face.
		/// </param>
		/// <param name="flags">
		/// A flag indicating what to load for this glyph. The FT_LOAD_XXX
		/// constants can be used to control the glyph loading process (e.g.,
		/// whether the outline should be scaled, whether to load bitmaps or
		/// not, whether to hint the outline, etc).
		/// </param>
		/// <param name="target">The target to OR with the flags.</param>
		[CLSCompliant(false)]
		public static void LoadChar(Face face, uint charCode, LoadFlags flags, LoadTarget target)
		{
			Error err = FT_Load_Char(face.reference, charCode, (int)flags | (int)target);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// A function used to set the transformation that is applied to glyph
		/// images when they are loaded into a glyph slot through
		/// FT_Load_Glyph.
		/// </summary>
		/// <remarks><para>
		/// The transformation is only applied to scalable image formats after
		/// the glyph has been loaded. It means that hinting is unaltered by
		/// the transformation and is performed on the character size given in
		/// the last call to FT_Set_Char_Size or FT_Set_Pixel_Sizes.
		/// </para><para>
		/// Note that this also transforms the ‘face.glyph.advance’ field, but
		/// not the values in ‘face.glyph.metrics’.
		/// </para></remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="matrix">
		/// A pointer to the transformation's 2x2 matrix. Use 0 for the
		/// identity matrix.
		/// </param>
		/// <param name="delta">
		/// A pointer to the translation vector. Use 0 for the null vector.
		/// </param>
		public static void SetTransform(Face face, FTMatrix matrix, FTVector delta)
		{
			FT_Set_Transform(face.reference, matrix.reference, delta.reference);
		}

		/// <summary>
		/// Convert a given glyph image to a bitmap. It does so by inspecting
		/// the glyph image format, finding the relevant renderer, and invoking
		/// it.
		/// </summary>
		/// <param name="slot">
		/// A handle to the glyph slot containing the image to convert.
		/// </param>
		/// <param name="mode">
		/// This is the render mode used to render the glyph image into a
		/// bitmap.
		/// </param>
		public static void RenderGlyph(GlyphSlot slot, RenderMode mode)
		{
			Error err = FT_Render_Glyph(slot.reference, mode);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Return the kerning vector between two glyphs of a same face.
		/// </summary>
		/// <remarks>
		/// Only horizontal layouts (left-to-right &amp; right-to-left) are
		/// supported by this method. Other layouts, or more sophisticated
		/// kernings, are out of the scope of this API function -- they can be
		/// implemented through format-specific interfaces.
		/// </remarks>
		/// <param name="face">A handle to a source face object.</param>
		/// <param name="leftGlyph">
		/// The index of the left glyph in the kern pair.
		/// </param>
		/// <param name="rightGlyph">
		/// The index of the right glyph in the kern pair.
		/// </param>
		/// <param name="mode">
		/// Determines the scale and dimension of the returned kerning vector.
		/// </param>
		/// <returns>
		/// The kerning vector. This is either in font units or in pixels (26.6
		/// format) for scalable formats, and in pixels for fixed-sizes
		/// formats.
		/// </returns>
		[CLSCompliant(false)]
		public static FTVector GetKerning(Face face, uint leftGlyph, uint rightGlyph, KerningMode mode)
		{
			IntPtr kernRef;
			Error err = FT_Get_Kerning(face.reference, leftGlyph, rightGlyph, mode, out kernRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			if (kernRef == IntPtr.Zero)
				return new FTVector(new VectorRec());

			return new FTVector(kernRef);
		}

		/// <summary>
		/// Return the track kerning for a given face object at a given size.
		/// </summary>
		/// <param name="face">A handle to a source face object.</param>
		/// <param name="pointSize">
		/// The point size in 16.16 fractional points.
		/// </param>
		/// <param name="degree">The degree of tightness.</param>
		/// <returns>The kerning in 16.16 fractional points.</returns>
		public static int GetTrackKerning(Face face, int pointSize, int degree)
		{
			int kerning;

			Error err = FT_Get_Track_Kerning(face.reference, pointSize, degree, out kerning);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return kerning;
		}

		/// <summary>
		/// Retrieve the ASCII name of a given glyph in a face. This only works
		/// for those faces where FT_HAS_GLYPH_NAMES(face) returns 1.
		/// </summary>
		/// <remarks><para>
		/// An error is returned if the face doesn't provide glyph names or if
		/// the glyph index is invalid. In all cases of failure, the first byte
		/// of ‘buffer’ is set to 0 to indicate an empty name.
		/// </para><para>
		/// The glyph name is truncated to fit within the buffer if it is too
		/// long. The returned string is always zero-terminated.
		/// </para><para>
		/// Be aware that FreeType reorders glyph indices internally so that
		/// glyph index 0 always corresponds to the ‘missing glyph’ (called
		/// ‘.notdef’).
		/// </para><para>
		/// This function is not compiled within the library if the config
		/// macro ‘FT_CONFIG_OPTION_NO_GLYPH_NAMES’ is defined in
		/// ‘include/freetype/config/ftoptions.h’.
		/// </para></remarks>
		/// <param name="face">A handle to a source face object.</param>
		/// <param name="glyphIndex">The glyph index.</param>
		/// <param name="bufferSize">
		/// The maximal number of bytes available in the buffer.
		/// </param>
		/// <returns>The ASCII name of a given glyph in a face.</returns>
		[CLSCompliant(false)]
		public static string GetGlyphName(Face face, uint glyphIndex, int bufferSize)
		{
			return GetGlyphName(face, glyphIndex, new byte[bufferSize]);
		}

		/// <summary>
		/// Retrieve the ASCII name of a given glyph in a face. This only works
		/// for those faces where FT_HAS_GLYPH_NAMES(face) returns 1.
		/// </summary>
		/// <remarks><para>
		/// An error is returned if the face doesn't provide glyph names or if
		/// the glyph index is invalid. In all cases of failure, the first byte
		/// of ‘buffer’ is set to 0 to indicate an empty name.
		/// </para><para>
		/// The glyph name is truncated to fit within the buffer if it is too
		/// long. The returned string is always zero-terminated.
		/// </para><para>
		/// Be aware that FreeType reorders glyph indices internally so that
		/// glyph index 0 always corresponds to the ‘missing glyph’ (called
		/// ‘.notdef’).
		/// </para><para>
		/// This function is not compiled within the library if the config
		/// macro ‘FT_CONFIG_OPTION_NO_GLYPH_NAMES’ is defined in
		/// ‘include/freetype/config/ftoptions.h’.
		/// </para></remarks>
		/// <param name="face">A handle to a source face object.</param>
		/// <param name="glyphIndex">The glyph index.</param>
		/// <param name="buffer">
		/// The target buffer where the name is copied to.
		/// </param>
		/// <returns>The ASCII name of a given glyph in a face.</returns>
		[CLSCompliant(false)]
		public static unsafe string GetGlyphName(Face face, uint glyphIndex, byte[] buffer)
		{
			fixed (byte* ptr = buffer)
			{
				IntPtr intptr = new IntPtr(ptr);
				Error err = FT_Get_Glyph_Name(face.reference, glyphIndex, intptr, (uint)buffer.Length);

				if (err != Error.Ok)
					throw new FreeTypeException(err);

				return Marshal.PtrToStringAnsi(intptr);
			}
		}

		/// <summary>
		/// Retrieve the ASCII Postscript name of a given face, if available.
		/// This only works with Postscript and TrueType fonts.
		/// </summary>
		/// <remarks>
		/// The returned pointer is owned by the face and is destroyed with it.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <returns>
		/// A pointer to the face's Postscript name. NULL if unavailable.
		/// </returns>
		public static string GetPostscriptName(Face face)
		{
			return Marshal.PtrToStringAnsi(FT_Get_Postscript_Name(face.reference));
		}

		/// <summary>
		/// Select a given charmap by its encoding tag (as listed in
		/// ‘freetype.h’).
		/// </summary>
		/// <remarks><para>
		/// This function returns an error if no charmap in the face
		/// corresponds to the encoding queried here.
		/// </para><para>
		/// Because many fonts contain more than a single cmap for Unicode
		/// encoding, this function has some special code to select the one
		/// which covers Unicode best. It is thus preferable to FT_Set_Charmap
		/// in this case.
		/// </para></remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="encoding">A handle to the selected encoding.</param>
		[CLSCompliant(false)]
		public static void SelectCharmap(Face face, Encoding encoding)
		{
			Error err = FT_Select_Charmap(face.reference, encoding);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Select a given charmap for character code to glyph index mapping.
		/// </summary>
		/// <remarks>
		/// This function returns an error if the charmap is not part of the
		/// face (i.e., if it is not listed in the ‘face->charmaps’ table).
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charmap">A handle to the selected charmap.</param>
		public static void SetCharmap(Face face, CharMap charmap)
		{
			Error err = FT_Set_Charmap(face.reference, charmap.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Retrieve index of a given charmap.
		/// </summary>
		/// <param name="charmap">A handle to a charmap.</param>
		/// <returns>
		/// The index into the array of character maps within the face to which
		/// ‘charmap’ belongs.
		/// </returns>
		public static int GetCharmapIndex(CharMap charmap)
		{
			return FT_Get_Charmap_Index(charmap.reference);
		}

		/// <summary>
		/// Return the glyph index of a given character code. This function
		/// uses a charmap object to do the mapping.
		/// </summary>
		/// <remarks>
		/// If you use FreeType to manipulate the contents of font files
		/// directly, be aware that the glyph index returned by this function
		/// doesn't always correspond to the internal indices used within the
		/// file. This is done to ensure that value 0 always corresponds to the
		/// ‘missing glyph’.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charCode">The character code.</param>
		/// <returns>
		/// The glyph index. 0 means ‘undefined character code’.
		/// </returns>
		[CLSCompliant(false)]
		public static uint GetCharIndex(Face face, uint charCode)
		{
			return FT_Get_Char_Index(face.reference, charCode);
		}

		/// <summary>
		/// This function is used to return the first character code in the
		/// current charmap of a given face. It also returns the corresponding
		/// glyph index.
		/// </summary>
		/// <remarks><para>
		/// You should use this function with FT_Get_Next_Char to be able to
		/// parse all character codes available in a given charmap.
		/// </para><para>
		/// Note that ‘agindex’ is set to 0 if the charmap is empty. The result
		/// itself can be 0 in two cases: if the charmap is empty or when the
		/// value 0 is the first valid character code.
		/// </para></remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="glyphIndex">
		/// Glyph index of first character code. 0 if charmap is empty.
		/// </param>
		/// <returns>The charmap's first character code.</returns>
		[CLSCompliant(false)]
		public static uint GetFirstChar(Face face, out uint glyphIndex)
		{
			return FT_Get_First_Char(face.reference, out glyphIndex);
		}

		/// <summary>
		/// This function is used to return the next character code in the
		/// current charmap of a given face following the value ‘char_code’, as
		/// well as the corresponding glyph index.
		/// </summary>
		/// <remarks><para>
		/// You should use this function with FT_Get_First_Char to walk over
		/// all character codes available in a given charmap. See the note for
		/// this function for a simple code example.
		/// </para><para>
		/// Note that ‘*agindex’ is set to 0 when there are no more codes in
		/// the charmap.
		/// </para></remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charCode">The starting character code.</param>
		/// <param name="glyphIndex">
		/// Glyph index of first character code. 0 if charmap is empty.
		/// </param>
		/// <returns>The charmap's next character code.</returns>
		[CLSCompliant(false)]
		public static uint GetNextChar(Face face, uint charCode, out uint glyphIndex)
		{
			return FT_Get_Next_Char(face.reference, charCode, out glyphIndex);
		}

		/// <summary>
		/// Return the glyph index of a given glyph name. This function uses
		/// driver specific objects to do the translation.
		/// </summary>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="name">The glyph name.</param>
		/// <returns>
		/// The glyph index. 0 means ‘undefined character code’.
		/// </returns>
		[CLSCompliant(false)]
		public static uint GetNameIndex(Face face, string name)
		{
			return FT_Get_Name_Index(face.reference, Marshal.StringToHGlobalAuto(name));
		}

		/// <summary>
		/// Retrieve a description of a given subglyph. Only use it if
		/// ‘glyph->format’ is FT_GLYPH_FORMAT_COMPOSITE; an error is returned
		/// otherwise.
		/// </summary>
		/// <remarks>
		/// The values of ‘*p_arg1’, ‘*p_arg2’, and ‘*p_transform’ must be
		/// interpreted depending on the flags returned in ‘*p_flags’. See the
		/// TrueType specification for details.
		/// </remarks>
		/// <param name="glyph">The source glyph slot.</param>
		/// <param name="subIndex">
		/// The index of the subglyph. Must be less than
		/// ‘glyph->num_subglyphs’.
		/// </param>
		/// <param name="index">The glyph index of the subglyph.</param>
		/// <param name="flags">
		/// The subglyph flags, see FT_SUBGLYPH_FLAG_XXX.
		/// </param>
		/// <param name="arg1">The subglyph's first argument (if any).</param>
		/// <param name="arg2">
		/// The subglyph's second argument (if any).
		/// </param>
		/// <param name="transform">
		/// The subglyph transformation (if any).
		/// </param>
		[CLSCompliant(false)]
		public static void GetSubGlyphInfo(GlyphSlot glyph, uint subIndex, out int index, out SubGlyphFlags flags, out int arg1, out int arg2, out FTMatrix transform)
		{
			IntPtr matrixRef;
			Error err = FT_Get_SubGlyph_Info(glyph.reference, subIndex, out index, out flags, out arg1, out arg2, out matrixRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			transform = new FTMatrix(matrixRef);
		}

		/// <summary>
		/// Return the fsType flags for a font.
		/// </summary>
		/// <remarks>
		/// Use this function rather than directly reading the ‘fs_type’ field
		/// in the PS_FontInfoRec structure which is only guaranteed to return
		/// the correct results for Type 1 fonts.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <returns>The fsType flags, FT_FSTYPE_XXX.</returns>
		[CLSCompliant(false)]
		public static EmbeddingTypes GetFSTypeFlags(Face face)
		{
			return FT_Get_FSType_Flags(face.reference);
		}

		/// <summary><para>
		/// A counter gets initialized to 1 at the time an FT_Face structure is
		/// created. This function increments the counter. FT_Done_Face then
		/// only destroys a face if the counter is 1, otherwise it simply
		/// decrements the counter.
		/// </para><para>
		/// This function helps in managing life-cycles of structures which
		/// reference FT_Face objects.
		/// </para></summary>
		/// <param name="face">A handle to a target face object.</param>
		internal static void ReferenceFace(Face face)
		{
			//marked as internal because the Face class wraps this funcitonality.
			Error err = FT_Reference_Face(face.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region Glyph Variants

		/// <summary>
		/// Return the glyph index of a given character code as modified by the
		/// variation selector.
		/// </summary>
		/// <remarks><para>
		/// If you use FreeType to manipulate the contents of font files
		/// directly, be aware that the glyph index returned by this function
		/// doesn't always correspond to the internal indices used within the
		/// file. This is done to ensure that value 0 always corresponds to the
		/// ‘missing glyph’.
		/// </para><para>
		/// This function is only meaningful if a) the font has a variation
		/// selector cmap sub table, and b) the current charmap has a Unicode
		/// encoding.
		/// </para></remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charCode">The character code point in Unicode.</param>
		/// <param name="variantSelector">
		/// The Unicode code point of the variation selector.
		/// </param>
		/// <returns>
		/// The glyph index. 0 means either ‘undefined character code’, or
		/// ‘undefined selector code’, or ‘no variation selector cmap
		/// subtable’, or ‘current CharMap is not Unicode’.
		/// </returns>
		[CLSCompliant(false)]
		public static uint FaceGetCharVariantIndex(Face face, uint charCode, uint variantSelector)
		{
			return FT_Face_GetCharVariantIndex(face.reference, charCode, variantSelector);
		}

		/// <summary>
		/// Check whether this variant of this Unicode character is the one to
		/// be found in the ‘cmap’.
		/// </summary>
		/// <remarks>
		/// This function is only meaningful if the font has a variation
		/// selector cmap subtable.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charCode">The character codepoint in Unicode.</param>
		/// <param name="variantSelector">
		/// The Unicode codepoint of the variation selector.
		/// </param>
		/// <returns>
		/// 1 if found in the standard (Unicode) cmap, 0 if found in the
		/// variation selector cmap, or -1 if it is not a variant.
		/// </returns>
		[CLSCompliant(false)]
		public static int FaceGetCharVariantIsDefault(Face face, uint charCode, uint variantSelector)
		{
			return FT_Face_GetCharVariantIsDefault(face.reference, charCode, variantSelector);
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode variant selectors found in
		/// the font.
		/// </summary>
		/// <remarks>
		/// The last item in the array is 0; the array is owned by the FT_Face
		/// object but can be overwritten or released on the next call to a
		/// FreeType function.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <returns>
		/// A pointer to an array of selector code points, or NULL if there is
		/// no valid variant selector cmap subtable.
		/// </returns>
		[CLSCompliant(false)]
		public static uint[] FaceGetVariantSelectors(Face face)
		{
			IntPtr ptr = FT_Face_GetVariantSelectors(face.reference);

			List<uint> list = new List<uint>();

			//temporary non-zero value to prevent complaining about uninitialized variable.
			uint curValue = 1;

			for (int i = 0; curValue != 0; i++)
			{
				curValue = (uint)Marshal.ReadInt32(face.reference, sizeof(uint) * i);
				list.Add(curValue);
			}

			return list.ToArray();
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode variant selectors found in
		/// the font.
		/// </summary>
		/// <remarks>
		/// The last item in the array is 0; the array is owned by the FT_Face
		/// object but can be overwritten or released on the next call to a
		/// FreeType function.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="charCode">The character codepoint in Unicode.</param>
		/// <returns>
		/// A pointer to an array of variant selector code points which are
		/// active for the given character, or NULL if the corresponding list
		/// is empty.
		/// </returns>
		[CLSCompliant(false)]
		public static uint[] FaceGetVariantsOfChar(Face face, uint charCode)
		{
			IntPtr ptr = FT_Face_GetVariantsOfChar(face.reference, charCode);

			List<uint> list = new List<uint>();

			//temporary non-zero value to prevent complaining about uninitialized variable.
			uint curValue = 1;

			for (int i = 0; curValue != 0; i++)
			{
				curValue = (uint)Marshal.ReadInt32(face.reference, sizeof(uint) * i);
				list.Add(curValue);
			}

			return list.ToArray();
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode character codes found for
		/// the specified variant selector.
		/// </summary>
		/// <remarks>
		/// The last item in the array is 0; the array is owned by the FT_Face
		/// object but can be overwritten or released on the next call to a
		/// FreeType function.
		/// </remarks>
		/// <param name="face">A handle to the source face object.</param>
		/// <param name="variantSelector">
		/// The variant selector code point in Unicode.
		/// </param>
		/// <returns>
		/// A list of all the code points which are specified by this selector
		/// (both default and non-default codes are returned) or NULL if there
		/// is no valid cmap or the variant selector is invalid.
		/// </returns>
		[CLSCompliant(false)]
		public static uint[] FaceGetCharsOfVariant(Face face, uint variantSelector)
		{
			IntPtr ptr = FT_Face_GetCharsOfVariant(face.reference, variantSelector);

			List<uint> list = new List<uint>();

			//temporary non-zero value to prevent complaining about uninitialized variable.
			uint curValue = 1;

			for (int i = 0; curValue != 0; i++)
			{
				curValue = (uint)Marshal.ReadInt32(face.reference, sizeof(uint) * i);
				list.Add(curValue);
			}

			return list.ToArray();
		}

		#endregion

		#region Glyph Management

		/// <summary>
		/// A function used to extract a glyph image from a slot. Note that the
		/// created <see cref="Glyph"/> object must be released with
		/// <see cref="DoneGlyph"/>.
		/// </summary>
		/// <param name="slot">A handle to the source glyph slot.</param>
		/// <returns>A handle to the glyph object.</returns>
		public static Glyph GetGlyph(GlyphSlot slot)
		{
			IntPtr glyphRef;

			Error err = FT_Get_Glyph(slot.reference, out glyphRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Glyph(glyphRef);
		}

		/// <summary>
		/// A function used to copy a glyph image. Note that the created
		/// <see cref="Glyph"/> object must be released with
		/// <see cref="DoneGlyph"/>.
		/// </summary>
		/// <param name="source">A handle to the source glyph object.</param>
		/// <returns>
		/// A handle to the target glyph object. 0 in case of error.
		/// </returns>
		public static Glyph GlyphCopy(Glyph source)
		{
			IntPtr glyphRef;

			Error err = FT_Glyph_Copy(source.reference, out glyphRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Glyph(glyphRef);
		}

		/// <summary>
		/// Transform a glyph image if its format is scalable.
		/// </summary>
		/// <param name="glyph">A handle to the target glyph object.</param>
		/// <param name="matrix">A pointer to a 2x2 matrix to apply.</param>
		/// <param name="delta">
		/// A pointer to a 2d vector to apply. Coordinates are expressed in
		/// 1/64th of a pixel.
		/// </param>
		public static void GlyphTransform(Glyph glyph, FTMatrix matrix, FTVector delta)
		{
			Error err = FT_Glyph_Transform(glyph.reference, matrix.reference, delta.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Return a glyph's ‘control box’. The control box encloses all the
		/// outline's points, including Bézier control points. Though it
		/// coincides with the exact bounding box for most glyphs, it can be
		/// slightly larger in some situations (like when rotating an outline
		/// which contains Bézier outside arcs).
		/// </para><para>
		/// Computing the control box is very fast, while getting the bounding
		/// box can take much more time as it needs to walk over all segments
		/// and arcs in the outline. To get the latter, you can use the
		/// ‘ftbbox’ component which is dedicated to this single task.
		/// </para></summary>
		/// <remarks><para>
		/// Coordinates are relative to the glyph origin, using the y upwards
		/// convention.
		/// </para><para>
		/// If the glyph has been loaded with <see cref="LoadFlags.NoScale"/>,
		/// ‘bbox_mode’ must be set to <see cref="GlyphBBoxMode.Unscaled"/> to
		/// get unscaled font units in 26.6 pixel format. The value
		/// <see cref="GlyphBBoxMode.Subpixels"/> is another name for this
		/// constant.
		/// </para><para>
		/// If the font is tricky and the glyph has been loaded with
		/// <see cref="LoadFlags.NoScale"/>, the resulting CBox is meaningless.
		/// To get reasonable values for the CBox it is necessary to load the
		/// glyph at a large ppem value (so that the hinting instructions can
		/// properly shift and scale the subglyphs), then extracting the CBox
		/// which can be eventually converted back to font units.
		/// </para><para>
		/// Note that the maximum coordinates are exclusive, which means that
		/// one can compute the width and height of the glyph image (be it in
		/// integer or 26.6 pixels) as:
		/// </para><para>
		/// <code>
		/// width  = bbox.xMax - bbox.xMin;
		/// height = bbox.yMax - bbox.yMin;
		/// </code>
		/// </para><para>
		/// Note also that for 26.6 coordinates, if ‘bbox_mode’ is set to
		/// <see cref="GlyphBBoxMode.Gridfit"/>, the coordinates will also be
		/// grid-fitted, which corresponds to:
		/// </para><para>
		/// <code>
		/// bbox.xMin = FLOOR(bbox.xMin);
		/// bbox.yMin = FLOOR(bbox.yMin);
		/// bbox.xMax = CEILING(bbox.xMax);
		/// bbox.yMax = CEILING(bbox.yMax);
		/// </code>
		/// </para><para>
		/// To get the bbox in pixel coordinates, set ‘bbox_mode’ to
		/// <see cref="GlyphBBoxMode.Truncate"/>.
		/// </para><para>
		/// To get the bbox in grid-fitted pixel coordinates, set ‘bbox_mode’
		/// to <see cref="GlyphBBoxMode.Pixels"/>.
		/// </para></remarks>
		/// <param name="glyph">A handle to the source glyph object.</param>
		/// <param name="mode">
		/// The mode which indicates how to interpret the returned bounding box
		/// values.
		/// </param>
		/// <returns>
		/// The glyph coordinate bounding box. Coordinates are expressed in
		/// 1/64th of pixels if it is grid-fitted.
		/// </returns>
		[CLSCompliant(false)]
		public static BBox GlyphGetCBox(Glyph glyph, GlyphBBoxMode mode)
		{
			IntPtr cboxRef;

			FT_Glyph_Get_CBox(glyph.reference, mode, out cboxRef);

			return new BBox(cboxRef);
		}

		/// <summary>
		/// Convert a given glyph object to a bitmap glyph object.
		/// </summary>
		/// <remarks><para>
		/// This function does nothing if the glyph format isn't scalable.
		/// </para><para>
		/// The glyph image is translated with the ‘origin’ vector before
		/// rendering.
		/// </para><para>
		/// The first parameter is a pointer to an <see cref="Glyph"/> handle,
		/// that will be replaced by this function (with newly allocated data).
		/// Typically, you would use (omitting error handling):
		/// </para><para>
		/// --sample code ommitted--
		/// </para></remarks>
		/// <param name="glyph">
		/// A pointer to a handle to the target glyph.
		/// </param>
		/// <param name="renderMode">
		/// An enumeration that describes how the data is rendered.
		/// </param>
		/// <param name="origin">
		/// A pointer to a vector used to translate the glyph image before
		/// rendering. Can be 0 (if no translation). The origin is expressed in
		/// 26.6 pixels.
		/// </param>
		/// <param name="destroy">
		/// A boolean that indicates that the original glyph image should be
		/// destroyed by this function. It is never destroyed in case of error.
		/// </param>
		public static void GlyphToBitmap(Glyph glyph, RenderMode renderMode, FTVector origin, bool destroy)
		{
			Error err = FT_Glyph_To_Bitmap(ref glyph.reference, renderMode, origin.reference, destroy);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Destroy a given glyph.
		/// </summary>
		/// <param name="glyph">A handle to the target glyph object.</param>
		public static void DoneGlyph(Glyph glyph)
		{
			FT_Done_Glyph(glyph.reference);
		}

		#endregion

		#region Mac Specific Interface

		/// <summary>
		/// Create a new face object from a FOND resource.
		/// </summary>
		/// <remarks>
		/// This function can be used to create <see cref="Face"/> objects from
		/// fonts that are installed in the system as follows.
		/// <code>
		/// fond = GetResource( 'FOND', fontName );
		/// error = FT_New_Face_From_FOND( library, fond, 0, &amp;face );
		/// </code>
		/// </remarks>
		/// <param name="library">A handle to the library resource.</param>
		/// <param name="fond">A FOND resource.</param>
		/// <param name="faceIndex">
		/// Only supported for the -1 ‘sanity check’ special case.
		/// </param>
		/// <returns>A handle to a new face object.</returns>
		public static Face NewFaceFromFOND(Library library, IntPtr fond, int faceIndex)
		{
			IntPtr faceRef;

			Error err = FT_New_Face_From_FOND(library.reference, fond, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, false);
		}

		/// <summary>
		/// Return an FSSpec for the disk file containing the named font.
		/// </summary>
		/// <param name="fontName">
		/// Mac OS name of the font (e.g., Times New Roman Bold).
		/// </param>
		/// <param name="faceIndex">
		/// Index of the face. For passing to <see cref="NewFaceFromFSSpec"/>.
		/// </param>
		/// <returns>
		/// FSSpec to the file. For passing to <see cref="NewFaceFromFSSpec"/>.
		/// </returns>
		public static IntPtr GetFileFromMacName(string fontName, out int faceIndex)
		{
			IntPtr fsspec;

			Error err = FT_GetFile_From_Mac_Name(fontName, out fsspec, out faceIndex);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return fsspec;
		}

		/// <summary>
		/// Return an FSSpec for the disk file containing the named font.
		/// </summary>
		/// <param name="fontName">
		/// Mac OS name of the font in ATS framework.
		/// </param>
		/// <param name="faceIndex">
		/// Index of the face. For passing to <see cref="NewFaceFromFSSpec"/>.
		/// </param>
		/// <returns>
		/// FSSpec to the file. For passing to <see cref="NewFaceFromFSSpec"/>.
		/// </returns>
		public static IntPtr GetFileFromMacATSName(string fontName, out int faceIndex)
		{
			IntPtr fsspec;

			Error err = FT_GetFile_From_Mac_ATS_Name(fontName, out fsspec, out faceIndex);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return fsspec;
		}

		/// <summary>
		/// Return a pathname of the disk file and face index for given font
		/// name which is handled by ATS framework.
		/// </summary>
		/// <param name="fontName">
		/// Mac OS name of the font in ATS framework.
		/// </param>
		/// <param name="path">
		/// Buffer to store pathname of the file. For passing to
		/// <see cref="NewFace"/>. The client must allocate this buffer before
		/// calling this function.
		/// </param>
		/// <returns>
		/// Index of the face. For passing to <see cref="NewFace"/>.
		/// </returns>
		public unsafe static int GetFilePathFromMacATSName(string fontName, byte[] path)
		{
			int faceIndex;

			fixed (void* ptr = path)
			{
				Error err = FT_GetFilePath_From_Mac_ATS_Name(fontName, (IntPtr)ptr, path.Length, out faceIndex);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}

			return faceIndex;
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index
		/// using an FSSpec to the font file.
		/// </summary>
		/// <remarks>
		/// <see cref="NewFaceFromFSSpec"/> is identical to
		/// <see cref="NewFace"/> except it accepts an FSSpec instead of a
		/// path.
		/// </remarks>
		/// <param name="library">A handle to the library resource.</param>
		/// <param name="spec">FSSpec to the font file.</param>
		/// <param name="faceIndex">
		/// The index of the face within the resource. The first face has index
		/// 0.
		/// </param>
		/// <returns>A handle to a new face object.</returns>
		public static Face NewFaceFromFSSpec(Library library, IntPtr spec, int faceIndex)
		{
			IntPtr faceRef;

			Error err = FT_New_Face_From_FSSpec(library.reference, spec, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, false);
		}

		/// <summary>
		/// Create a new face object from a given resource and typeface index
		/// using an FSRef to the font file.
		/// </summary>
		/// <remarks>
		/// <see cref="NewFaceFromFSRef"/> is identical to
		/// <see cref="NewFace"/> except it accepts an FSRef instead of a path.
		/// </remarks>
		/// <param name="library">A handle to the library resource.</param>
		/// <param name="ref">FSRef to the font file.</param>
		/// <param name="faceIndex">
		/// The index of the face within the resource. The first face has index
		/// 0.
		/// </param>
		/// <returns>A handle to a new face object.</returns>
		public static Face NewFaceFromFSRef(Library library, IntPtr @ref, int faceIndex)
		{
			IntPtr faceRef;

			Error err = FT_New_Face_From_FSRef(library.reference, @ref, faceIndex, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, false);
		}

		#endregion

		#region Size Management

		/// <summary>
		/// Create a new size object from a given face object.
		/// </summary>
		/// <remarks>
		/// You need to call <see cref="ActivateSize"/> in order to select the
		/// new size for upcoming calls to <see cref="SetPixelSizes"/>,
		/// <see cref="SetCharSize"/>, <see cref="LoadGlyph"/>,
		/// <see cref="LoadChar"/>, etc.
		/// </remarks>
		/// <param name="face">A handle to a parent face object.</param>
		/// <returns>A handle to a new size object.</returns>
		public static FTSize NewSize(Face face)
		{
			IntPtr sizeRef;

			Error err = FT_New_Size(face.reference, out sizeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTSize(sizeRef, true);
		}

		/// <summary>
		/// Discard a given size object. Note that <see cref="DoneFace"/>
		/// automatically discards all size objects allocated with
		/// <see cref="NewSize"/>.
		/// </summary>
		/// <param name="size">A handle to a target size object.</param>
		public static void DoneSize(FTSize size)
		{
			Error err = FT_Done_Size(size.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			size.reference = IntPtr.Zero;
		}

		/// <summary><para>
		/// Even though it is possible to create several size objects for a
		/// given face (see <see cref="FT.NewSize"/> for details), functions
		/// like <see cref="FT.LoadGlyph"/> or <see cref="FT.LoadChar"/> only
		/// use the one which has been activated last to determine the ‘current
		/// character pixel size’.
		/// </para><para>
		/// This function can be used to ‘activate’ a previously created size
		/// object.
		/// </para></summary>
		/// <remarks>
		/// If ‘face’ is the size's parent face object, this function changes
		/// the value of ‘face->size’ to the input size handle.
		/// </remarks>
		/// <param name="size">A handle to a target size object.</param>
		public static void ActivateSize(FTSize size)
		{
			Error err = FT_Activate_Size(size.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion
	}
}
