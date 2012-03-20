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
	/// FreeType root face class structure. A face object models a typeface in
	/// a font file.
	/// </summary>
	/// <remarks>
	/// Fields may be changed after a call to <see cref="FT.AttachFile"/> or
	/// <see cref="FT.AttachStream"/>.
	/// </remarks>
	public sealed class Face : IDisposable
	{
		#region Fields

		internal IntPtr reference;
		internal FaceRec rec;

		private bool disposed;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Face class.
		/// </summary>
		/// <param name="reference">A pointer to the unmanaged memory containing the Face.</param>
		/// <param name="duplicate">A value indicating whether </param>
		internal Face(IntPtr reference, bool duplicate)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<FaceRec>(reference);

			if (duplicate)
				FT.ReferenceFace(this);
		}

		/// <summary>
		/// Finalizes an instance of the Face class.
		/// </summary>
		~Face()
		{
			Dispose(false);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of faces in the font file. Some font formats can
		/// have multiple faces in a font file.
		/// </summary>
		public int FaceCount
		{
			get
			{
				return (int)rec.num_faces;
			}
		}

		/// <summary>
		/// Gets the index of the face in the font file. It is set to 0 if
		/// there is only one face in the font file.
		/// </summary>
		public int FaceIndex
		{
			get
			{
				return (int)rec.face_index;
			}
		}

		/// <summary>
		/// Gets a set of bit flags that give important information about the
		/// face.
		/// </summary>
		/// <see cref="FaceFlags"/>
		public FaceFlags FaceFlags
		{
			get
			{
				return (FaceFlags)rec.face_flags;
			}
		}

		/// <summary>
		/// Gets a set of bit flags indicating the style of the face.
		/// </summary>
		/// <see cref="StyleFlags"/>
		public StyleFlags StyleFlags
		{
			get
			{
				return (StyleFlags)rec.style_flags;
			}
		}

		/// <summary><para>
		/// Gets the number of glyphs in the face. If the face is scalable and
		/// has sbits (see ‘num_fixed_sizes’), it is set to the number of
		/// outline glyphs.
		/// </para><para>
		/// For CID-keyed fonts, this value gives the highest CID used in the
		/// font.
		/// </para></summary>
		public int GlyphCount
		{
			get
			{
				return (int)rec.num_glyphs;
			}
		}

		/// <summary>
		/// Gets the face's family name. This is an ASCII string, usually in
		/// English, which describes the typeface's family (like ‘Times New
		/// Roman’, ‘Bodoni’, ‘Garamond’, etc). This is a least common
		/// denominator used to list fonts. Some formats (TrueType &amp;
		/// OpenType) provide localized and Unicode versions of this string.
		/// Applications should use the format specific interface to access
		/// them. Can be NULL (e.g., in fonts embedded in a PDF file).
		/// </summary>
		public string FamilyName
		{
			get
			{
				return rec.family_name;
			}
		}

		/// <summary>
		/// Gets the face's style name. This is an ASCII string, usually in
		/// English, which describes the typeface's style (like ‘Italic’,
		/// ‘Bold’, ‘Condensed’, etc). Not all font formats provide a style
		/// name, so this field is optional, and can be set to NULL. As for
		/// ‘family_name’, some formats provide localized and Unicode versions
		/// of this string. Applications should use the format specific
		/// interface to access them.
		/// </summary>
		public string StyleName
		{
			get
			{
				return rec.style_name;
			}
		}

		/// <summary>
		/// Gets the number of bitmap strikes in the face. Even if the face is
		/// scalable, there might still be bitmap strikes, which are called
		/// ‘sbits’ in that case.
		/// </summary>
		public int FixedSizesCount
		{
			get
			{
				return rec.num_fixed_sizes;
			}
		}

		/// <summary>
		/// Gets an array of FT_Bitmap_Size for all bitmap strikes in the face.
		/// It is set to NULL if there is no bitmap strike.
		/// </summary>
		public BitmapSize[] AvailableSizes
		{
			get
			{
				int count = FixedSizesCount;

				if (count == 0)
					return null;

				BitmapSize[] sizes = new BitmapSize[count];
				IntPtr array = rec.available_sizes;

				for (int i = 0; i < count; i++)
				{
					sizes[i] = new BitmapSize(new IntPtr(array.ToInt64() + IntPtr.Size * i));
				}

				return sizes;
			}
		}

		/// <summary>
		/// Gets the number of charmaps in the face.
		/// </summary>
		public int CharmapsCount
		{
			get
			{
				return rec.num_charmaps;
			}
		}

		/// <summary>
		/// Gets an array of the charmaps of the face.
		/// </summary>
		public CharMap[] CharMaps
		{
			get
			{
				int count = CharmapsCount;

				if (count == 0)
					return null;

				CharMap[] charmaps = new CharMap[count];
				IntPtr array = rec.charmaps;

				for (int i = 0; i < count; i++)
				{
					charmaps[i] = new CharMap(new IntPtr(array.ToInt64() + IntPtr.Size * i));
				}

				return charmaps;
			}
		}

		/// <summary>
		/// Gets a field reserved for client uses.
		/// </summary>
		/// <see cref="Generic"/>
		public Generic Generic
		{
			get
			{
				return new Generic(rec.generic);
			}

			set
			{
				//rec.generic = value;
				value.WriteToUnmanagedMemory(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceRec), "generic").ToInt64()));
				rec = (FaceRec)Marshal.PtrToStructure(reference, typeof(FaceRec));
			}
		}

		/// <summary><para>
		/// Gets the font bounding box. Coordinates are expressed in font units
		/// (see ‘units_per_EM’). The box is large enough to contain any glyph
		/// from the font. Thus, ‘bbox.yMax’ can be seen as the ‘maximal
		/// ascender’, and ‘bbox.yMin’ as the ‘minimal descender’. Only
		/// relevant for scalable formats.
		/// </para><para>
		/// Note that the bounding box might be off by (at least) one pixel for
		/// hinted fonts. See FT_Size_Metrics for further discussion.
		/// </para></summary>
		public BBox BBox
		{
			get
			{
				return new BBox(rec.bbox);
			}
		}

		/// <summary>
		/// Gets the number of font units per EM square for this face. This is
		/// typically 2048 for TrueType fonts, and 1000 for Type 1 fonts. Only
		/// relevant for scalable formats.
		/// </summary>
		[CLSCompliant(false)]
		public ushort UnitsPerEM
		{
			get
			{
				return rec.units_per_EM;
			}
		}

		/// <summary>
		/// Gets the typographic ascender of the face, expressed in font units.
		/// For font formats not having this information, it is set to
		/// ‘bbox.yMax’. Only relevant for scalable formats.
		/// </summary>
		public short Ascender
		{
			get
			{
				return rec.ascender;
			}
		}

		/// <summary>
		/// Gets the typographic descender of the face, expressed in font units.
		/// For font formats not having this information, it is set to
		/// ‘bbox.yMin’.Note that this field is usually negative. Only relevant
		/// for scalable formats.
		/// </summary>
		public short Descender
		{
			get
			{
				return rec.descender;
			}
		}

		/// <summary>
		/// Gets the height is the vertical distance between two consecutive
		/// baselines, expressed in font units. It is always positive. Only
		/// relevant for scalable formats.
		/// </summary>
		public short Height
		{
			get
			{
				return rec.height;
			}
		}

		/// <summary>
		/// Gets the maximal advance width, in font units, for all glyphs in
		/// this face. This can be used to make word wrapping computations
		/// faster. Only relevant for scalable formats.
		/// </summary>
		public short MaxAdvanceWidth
		{
			get
			{
				return rec.max_advance_width;
			}
		}

		/// <summary>
		/// Gets the maximal advance height, in font units, for all glyphs in
		/// this face. This is only relevant for vertical layouts, and is set
		/// to ‘height’ for fonts that do not provide vertical metrics. Only
		/// relevant for scalable formats.
		/// </summary>
		public short MaxAdvanceHeight
		{
			get
			{
				return rec.max_advance_height;
			}
		}

		/// <summary>
		/// Gets the position, in font units, of the underline line for this
		/// face. It is the center of the underlining stem. Only relevant for
		/// scalable formats.
		/// </summary>
		public short UnderlinePosition
		{
			get
			{
				return rec.underline_position;
			}
		}

		/// <summary>
		/// Gets the thickness, in font units, of the underline for this face.
		/// Only relevant for scalable formats.
		/// </summary>
		public short UnderlineThickness
		{
			get
			{
				return rec.underline_thickness;
			}
		}

		/// <summary>
		/// Gets the face's associated glyph slot(s).
		/// </summary>
		public GlyphSlot Glyph
		{
			get
			{
				return new GlyphSlot(rec.glyph);
			}
		}

		/// <summary>
		/// Gets the current active size for this face.
		/// </summary>
		public FTSize Size
		{
			get
			{
				return new FTSize(rec.size);
			}
		}

		/// <summary>
		/// Gets the current active charmap for this face.
		/// </summary>
		public CharMap CharMap
		{
			get
			{
				return new CharMap(rec.charmap);
			}
		}

		#endregion

		#region Public methods

		#region FaceFlags flag checks

		/// <summary>
		/// A macro that returns true whenever a face object contains
		/// horizontal metrics (this is true for all font formats though).
		/// </summary>
		/// <returns>True if the face has the horizontal flag set, false otherwise.</returns>
		public bool HasHoriziontal()
		{
			return FT.HasHorizontal(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains vertical
		/// metrics.
		/// </summary>
		/// <returns>True if the face has the vertical flag set, false otherwise.</returns>
		public bool HasVertical()
		{
			return FT.HasVertical(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains kerning
		/// data that can be accessed with <see cref="GetKerning"/>.
		/// </summary>
		/// <returns>True if the face has the kerning flag set, false otherwise.</returns>
		public bool HasKerning()
		{
			return FT.HasKerning(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a 
		/// scalable font face (true for TrueType, Type 1, Type 42, CID, 
		/// OpenType/CFF, and PFR font formats.
		/// </summary>
		/// <returns>True if the face has the scalable flag set, false otherwise.</returns>
		public bool IsScalable()
		{
			return FT.IsScalable(FaceFlags);
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
		/// <returns>True if the face has the SFNT flag set, false otherwise.</returns>
		public bool IsSFNT()
		{
			return FT.IsSFNT(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains a font 
		/// face that contains fixed-width (or ‘monospace’, ‘fixed-pitch’, 
		/// etc.) glyphs.
		/// </summary>
		/// <returns>True if the face has the fixed width flag set, false otherwise.</returns>
		public bool IsFixedWidth()
		{
			return FT.IsFixedWidth(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// embedded bitmaps.
		/// </summary>
		/// <returns>True if the face has the fixed sizes flag set, false otherwise.</returns>
		/// <see cref="Face.AvailableSizes"/>
		public bool HasFixedSizes()
		{
			return FT.HasFixedSizes(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some
		/// glyph names that can be accessed through 
		/// <see cref="FT.GetGlyphName(Face, uint, int)"/>.
		/// </summary>
		/// <returns>True if the face has the glyph names flag set, false otherwise.</returns>
		public bool HasGlyphNames()
		{
			return FT.HasGlyphNames(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face object contains some 
		/// multiple masters. The functions provided by FT_MULTIPLE_MASTERS_H
		/// are then available to choose the exact design you want.
		/// </summary>
		/// <returns>True if the face has the multiple masters flag set, false otherwise.</returns>
		public bool HasMultipleMasters()
		{
			return FT.HasMultipleMasters(FaceFlags);
		}

		/// <summary><para>
		/// A macro that returns true whenever a face object contains a 
		/// CID-keyed font. See the discussion of FT_FACE_FLAG_CID_KEYED for 
		/// more details.
		/// </para><para>
		/// If this macro is true, all functions defined in FT_CID_H are 
		/// available.
		/// </para></summary>
		/// <returns>True if the face has the CID-keyed flag set, false otherwise.</returns>
		public bool IsCIDKeyed()
		{
			return FT.IsCIDKeyed(FaceFlags);
		}

		/// <summary>
		/// A macro that returns true whenever a face represents a ‘tricky’
		/// font. See the discussion of FT_FACE_FLAG_TRICKY for more details.
		/// </summary>
		/// <returns>True if the face has the tricky flag set, false otherwise.</returns>
		public bool IsTricky()
		{
			return FT.IsTricky(FaceFlags);
		}

		#endregion

		/// <summary><para>
		/// Parse all bytecode instructions of a TrueType font file to check
		/// whether any of the patented opcodes are used. This is only useful
		/// if you want to be able to use the unpatented hinter with fonts that
		/// do not use these opcodes.
		/// </para><para>
		/// Note that this function parses all glyph instructions in the font
		/// file, which may be slow.
		/// </para></summary>
		/// <remarks>See <see cref="FT.FaceCheckTrueTypePatents"/>.</remarks>
		/// <returns>True if this is a TrueType font that uses one of the patented opcodes, false otherwise.</returns>
		public bool CheckTrueTypePatents()
		{
			return FT.FaceCheckTrueTypePatents(this);
		}

		/// <summary>
		/// Enable or disable the unpatented hinter for a given
		/// <see cref="Face"/>. Only enable it if you have determined that the
		/// face doesn't use any patented opcodes.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceSetUnpatentedHinting"/>.</remarks>
		/// <param name="value">New boolean setting.</param>
		/// <returns>The old setting value. This will always be false if this is not an SFNT font, or if the unpatented hinter is not compiled in this instance of the library.</returns>
		/// <see cref="CheckTrueTypePatents"/>
		public bool SetUnpatentedHinting(bool value)
		{
			return FT.FaceSetUnpatentedHinting(this, value);
		}

		/// <summary>
		/// This function calls <see cref="AttachStream"/> to attach a file.
		/// </summary>
		/// <param name="path">The pathname.</param>
		public void AttachFile(string path)
		{
			FT.AttachFile(this, path);
		}

		/// <summary>
		/// ‘Attach’ data to a face object. Normally, this is used to read
		/// additional information for the face object. For example, you can
		/// attach an AFM file that comes with a Type 1 font to get the kerning
		/// values and other metrics.
		/// </summary>
		/// <remarks>See <see cref="FT.AttachStream"/>.</remarks>
		/// <param name="parameters">A pointer to <see cref="OpenArgs"/> which must be filled by the caller.</param>
		public void AttachStream(OpenArgs parameters)
		{
			FT.AttachStream(this, parameters);
		}

		/// <summary>
		/// Select a bitmap strike.
		/// </summary>
		/// <param name="strikeIndex">The index of the bitmap strike in the <see cref="Face.AvailableSizes"/> field of <see cref="Face"/> structure.</param>
		public void SelectSize(int strikeIndex)
		{
			FT.SelectSize(this, strikeIndex);
		}

		/// <summary>
		/// Resize the scale of the active FT_Size object in a face.
		/// </summary>
		/// <param name="request">A pointer to a <see cref="SizeRequest"/>.</param>
		public void RequestSize(SizeRequest request)
		{
			FT.RequestSize(this, request);
		}

		/// <summary>
		/// This function calls FT_Request_Size to request the nominal size (in
		/// points).
		/// </summary>
		/// <remarks>See <see cref="FT.SetCharSize"/>.</remarks>
		/// <param name="width">The nominal width, in 26.6 fractional points.</param>
		/// <param name="height">The nominal height, in 26.6 fractional points.</param>
		/// <param name="horizontalResolution">The horizontal resolution in dpi.</param>
		/// <param name="verticalResolution">The vertical resolution in dpi.</param>
		[CLSCompliant(false)]
		public void SetCharSize(int width, int height, uint horizontalResolution, uint verticalResolution)
		{
			FT.SetCharSize(this, width, height, horizontalResolution, verticalResolution);
		}

		/// <summary>
		/// This function calls <see cref="RequestSize"/> to request the
		/// nominal size (in pixels).
		/// </summary>
		/// <param name="width">The nominal width, in pixels.</param>
		/// <param name="height">The nominal height, in pixels</param>
		[CLSCompliant(false)]
		public void SetPixelSizes(uint width, uint height)
		{
			FT.SetPixelSizes(this, width, height);
		}

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a
		/// face object.
		/// </summary>
		/// <remarks>See <see cref="FT.LoadGlyph"/>.</remarks>
		/// <param name="glyphIndex">The index of the glyph in the font file. For CID-keyed fonts (either in PS or in CFF format) this argument specifies the CID value.</param>
		/// <param name="flags">A flag indicating what to load for this glyph. The FT_LOAD_XXX constants can be used to control the glyph loading process (e.g., whether the outline should be scaled, whether to load bitmaps or not, whether to hint the outline, etc).</param>
		/// <param name="target">The target to OR with the flags.</param>
		[CLSCompliant(false)]
		public void LoadGlyph(uint glyphIndex, LoadFlags flags, LoadTarget target)
		{
			FT.LoadGlyph(this, glyphIndex, flags, target);
		}

		/// <summary>
		/// A function used to load a single glyph into the glyph slot of a
		/// face object, according to its character code.
		/// </summary>
		/// <remarks>See <see cref="FT.LoadChar"/>.</remarks>
		/// <param name="charCode">The glyph's character code, according to the current charmap used in the face.</param>
		/// <param name="flags">A flag indicating what to load for this glyph. The FT_LOAD_XXX constants can be used to control the glyph loading process (e.g., whether the outline should be scaled, whether to load bitmaps or not, whether to hint the outline, etc).</param>
		/// <param name="target">The target to OR with the flags.</param>
		[CLSCompliant(false)]
		public void LoadChar(uint charCode, LoadFlags flags, LoadTarget target)
		{
			FT.LoadChar(this, charCode, flags, target);
		}

		/// <summary>
		/// A function used to set the transformation that is applied to glyph
		/// images when they are loaded into a glyph slot through
		/// FT_Load_Glyph.
		/// </summary>
		/// <remarks>See <see cref="FT.SetTransform"/>.</remarks>
		/// <param name="matrix">A pointer to the transformation's 2x2 matrix. Use 0 for the identity matrix.</param>
		/// <param name="delta">A pointer to the translation vector. Use 0 for the null vector.</param>
		public void SetTransform(FTMatrix matrix, FTVector delta)
		{
			FT.SetTransform(this, matrix, delta);
		}

		/// <summary>
		/// Convert a given glyph image to a bitmap. It does so by inspecting
		/// the glyph image format, finding the relevant renderer, and invoking
		/// it.
		/// </summary>
		/// <param name="slot">A handle to the glyph slot containing the image to convert.</param>
		/// <param name="mode">This is the render mode used to render the glyph image into a bitmap.</param>
		public void RenderGlyph(GlyphSlot slot, RenderMode mode)
		{
			FT.RenderGlyph(slot, mode);
		}

		/// <summary>
		/// Return the kerning vector between two glyphs of a same face.
		/// </summary>
		/// <remarks>See <see cref="FT.GetKerning"/>.</remarks>
		/// <param name="leftGlyph">The index of the left glyph in the kern pair.</param>
		/// <param name="rightGlyph">The index of the right glyph in the kern pair.</param>
		/// <param name="mode">Determines the scale and dimension of the returned kerning vector.</param>
		/// <returns>The kerning vector. This is either in font units or in pixels (26.6 format) for scalable formats, and in pixels for fixed-sizes formats.</returns>
		[CLSCompliant(false)]
		public FTVector GetKerning(uint leftGlyph, uint rightGlyph, KerningMode mode)
		{
			return FT.GetKerning(this, leftGlyph, rightGlyph, mode);
		}

		/// <summary>
		/// Return the track kerning for a given face object at a given size.
		/// </summary>
		/// <param name="pointSize">The point size in 16.16 fractional points.</param>
		/// <param name="degree">The degree of tightness.</param>
		/// <returns>The kerning in 16.16 fractional points.</returns>
		public int GetTrackKerning(int pointSize, int degree)
		{
			return FT.GetTrackKerning(this, pointSize, degree);
		}

		/// <summary>
		/// Retrieve the ASCII name of a given glyph in a face. This only works
		/// for those faces where FT_HAS_GLYPH_NAMES(face) returns 1.
		/// </summary>
		/// <remarks>See <see cref="FT.GetGlyphName(Face, uint, int)"/>.</remarks>
		/// <param name="glyphIndex">The glyph index.</param>
		/// <param name="bufferSize">The maximal number of bytes available in the buffer.</param>
		/// <returns>The ASCII name of a given glyph in a face.</returns>
		[CLSCompliant(false)]
		public string GetGlyphName(uint glyphIndex, int bufferSize)
		{
			return FT.GetGlyphName(this, glyphIndex, bufferSize);
		}

		/// <summary>
		/// Retrieve the ASCII name of a given glyph in a face. This only works
		/// for those faces where FT_HAS_GLYPH_NAMES(face) returns 1.
		/// </summary>
		/// <remarks>See <see cref="FT.GetGlyphName(Face, uint, byte[])"/></remarks>
		/// <param name="glyphIndex">The glyph index.</param>
		/// <param name="buffer">The target buffer where the name is copied to.</param>
		/// <returns>The ASCII name of a given glyph in a face.</returns>
		[CLSCompliant(false)]
		public string GetGlyphName(uint glyphIndex, byte[] buffer)
		{
			return FT.GetGlyphName(this, glyphIndex, buffer);
		}

		/// <summary>
		/// Retrieve the ASCII Postscript name of a given face, if available.
		/// This only works with Postscript and TrueType fonts.
		/// </summary>
		/// <remarks>See <see cref="FT.GetPostscriptName"/>.</remarks>
		/// <returns>A pointer to the face's Postscript name. NULL if unavailable.</returns>
		public string GetPostscriptName()
		{
			return FT.GetPostscriptName(this);
		}

		/// <summary>
		/// Select a given charmap by its encoding tag (as listed in
		/// ‘freetype.h’).
		/// </summary>
		/// <remarks>See <see cref="FT.SelectCharmap"/>.</remarks>
		/// <param name="encoding">A handle to the selected encoding.</param>
		[CLSCompliant(false)]
		public void SelectCharmap(Encoding encoding)
		{
			FT.SelectCharmap(this, encoding);
		}

		/// <summary>
		/// Select a given charmap for character code to glyph index mapping.
		/// </summary>
		/// <remarks>See <see cref="FT.SetCharmap"/>.</remarks>
		/// <param name="charmap">A handle to the selected charmap.</param>
		public void SetCharmap(CharMap charmap)
		{
			FT.SetCharmap(this, charmap);
		}

		/// <summary>
		/// Return the glyph index of a given character code. This function
		/// uses a charmap object to do the mapping.
		/// </summary>
		/// <remarks>See <see cref="GetCharIndex"/>.</remarks>
		/// <param name="charCode">The character code.</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’.</returns>
		[CLSCompliant(false)]
		public uint GetCharIndex(uint charCode)
		{
			return FT.GetCharIndex(this, charCode);
		}

		/// <summary>
		/// This function is used to return the first character code in the
		/// current charmap of a given face. It also returns the corresponding
		/// glyph index.
		/// </summary>
		/// <remarks>See <see cref="FT.GetFirstChar"/>.</remarks>
		/// <param name="glyphIndex">Glyph index of first character code. 0 if charmap is empty.</param>
		/// <returns>The charmap's first character code.</returns>
		[CLSCompliant(false)]
		public uint GetFirstChar(out uint glyphIndex)
		{
			return FT.GetFirstChar(this, out glyphIndex);
		}

		/// <summary>
		/// This function is used to return the next character code in the
		/// current charmap of a given face following the value ‘char_code’, as
		/// well as the corresponding glyph index.
		/// </summary>
		/// <remarks>See <see cref="FT.GetNextChar"/>.</remarks>
		/// <param name="charCode">The starting character code.</param>
		/// <param name="glyphIndex">Glyph index of first character code. 0 if charmap is empty.</param>
		/// <returns>The charmap's next character code.</returns>
		[CLSCompliant(false)]
		public uint GetNextChar(uint charCode, out uint glyphIndex)
		{
			return FT.GetNextChar(this, charCode, out glyphIndex);
		}

		/// <summary>
		/// Return the glyph index of a given glyph name. This function uses
		/// driver specific objects to do the translation.
		/// </summary>
		/// <param name="name">The glyph name.</param>
		/// <returns>The glyph index. 0 means ‘undefined character code’.</returns>
		[CLSCompliant(false)]
		public uint GetNameIndex(string name)
		{
			return FT.GetNameIndex(this, name);
		}

		/// <summary>
		/// Return the fsType flags for a font.
		/// </summary>
		/// <remarks>See <see cref="FT.GetFSTypeFlags"/>.</remarks>
		/// <returns>The fsType flags, FT_FSTYPE_XXX.</returns>
		[CLSCompliant(false)]
		public EmbeddingTypes GetFSTypeFlags()
		{
			return FT.GetFSTypeFlags(this);
		}

		/// <summary>
		/// Return the glyph index of a given character code as modified by the
		/// variation selector.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceGetCharVariantIndex"/>.</remarks>
		/// <param name="charCode">The character code point in Unicode.</param>
		/// <param name="variantSelector">The Unicode code point of the variation selector.</param>
		/// <returns>The glyph index. 0 means either ‘undefined character code’, or ‘undefined selector code’, or ‘no variation selector cmap subtable’, or ‘current CharMap is not Unicode’.</returns>
		[CLSCompliant(false)]
		public uint GetCharVariantIndex(uint charCode, uint variantSelector)
		{
			return FT.FaceGetCharVariantIndex(this, charCode, variantSelector);
		}

		/// <summary>
		/// Check whether this variant of this Unicode character is the one to
		/// be found in the ‘cmap’.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceGetCharVariantIsDefault"/>.</remarks>
		/// <param name="charCode">The character codepoint in Unicode.</param>
		/// <param name="variantSelector">The Unicode codepoint of the variation selector.</param>
		/// <returns>1 if found in the standard (Unicode) cmap, 0 if found in the variation selector cmap, or -1 if it is not a variant.</returns>
		[CLSCompliant(false)]
		public int GetCharVariantIsDefault(uint charCode, uint variantSelector)
		{
			return FT.FaceGetCharVariantIsDefault(this, charCode, variantSelector);
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode variant selectors found in
		/// the font.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceGetVariantSelectors"/>.</remarks>
		/// <returns>A pointer to an array of selector code points, or NULL if there is no valid variant selector cmap subtable.</returns>
		[CLSCompliant(false)]
		public uint[] GetVariantSelectors()
		{
			return FT.FaceGetVariantSelectors(this);
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode variant selectors found in
		/// the font.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceGetVariantsOfChar"/>.</remarks>
		/// <param name="charCode">The character codepoint in Unicode.</param>
		/// <returns>A pointer to an array of variant selector code points which are active for the given character, or NULL if the corresponding list is empty.</returns>
		[CLSCompliant(false)]
		public uint[] GetVariantsOfChar(uint charCode)
		{
			return FT.FaceGetVariantsOfChar(this, charCode);
		}

		/// <summary>
		/// Return a zero-terminated list of Unicode character codes found for
		/// the specified variant selector.
		/// </summary>
		/// <remarks>See <see cref="FT.FaceGetCharsOfVariant"/>.</remarks>
		/// <param name="variantSelector">The variant selector code point in Unicode.</param>
		/// <returns>A list of all the code points which are specified by this selector (both default and non-default codes are returned) or NULL if there is no valid cmap or the variant selector is invalid.</returns>
		[CLSCompliant(false)]
		public uint[] GetCharsOfVariant(uint variantSelector)
		{
			return FT.FaceGetCharsOfVariant(this, variantSelector);
		}

		/// <summary>
		/// Create a new size object from a given face object.
		/// </summary>
		/// <remarks>See <see cref="FT.NewSize"/>.</remarks>
		/// <returns>A handle to a new size object.</returns>
		public FTSize NewSize()
		{
			return FT.NewSize(this);
		}

		#endregion

		#region IDisposable members

		/// <summary>
		/// Disposes the Face.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				}

				//duplicates will just decrement the reference count, actual
				//Face not destroyed until all Face copies are destroyed.
				FT.DoneFace(this);

				disposed = true;
			}
		}

		#endregion
	}
}
