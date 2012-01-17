using System;

namespace SharpFont
{
	/// <summary>
	/// A list of bit flags used in the ‘face_flags’ field of the FT_FaceRec structure. They inform client applications of properties of the corresponding face.
	/// </summary>
	[Flags]
	public enum FaceFlags
	{
		/// <summary>
		/// Indicates that the face contains outline glyphs. This doesn't prevent bitmap strikes, i.e., a face can have both this and and FT_FACE_FLAG_FIXED_SIZES set.
		/// </summary>
		Scalable =			0x0001,

		/// <summary>
		/// Indicates that the face contains bitmap strikes. See also the ‘num_fixed_sizes’ and ‘available_sizes’ fields of FT_FaceRec.
		/// </summary>
		FixedSizes =		0x0002,

		/// <summary>
		/// Indicates that the face contains fixed-width characters (like Courier, Lucido, MonoType, etc.).
		/// </summary>
		FixedWidth =		0x0003,

		/// <summary>
		/// Indicates that the face uses the ‘sfnt’ storage scheme. For now, this means TrueType and OpenType.
		/// </summary>
		SFNT =				0x0008,

		/// <summary>
		/// Indicates that the face contains horizontal glyph metrics. This should be set for all common formats.
		/// </summary>
		Horizontal =		0x0010,

		/// <summary>
		/// Indicates that the face contains vertical glyph metrics. This is only available in some formats, not all of them.
		/// </summary>
		Vertical =			0x0020,

		/// <summary>
		/// Indicates that the face contains kerning information. If set, the kerning distance can be retrieved through the function FT_Get_Kerning. Otherwise the function always return the vector (0,0). Note that FreeType doesn't handle kerning data from the ‘GPOS’ table (as present in some OpenType fonts).
		/// </summary>
		Kerning =			0x0040,

		/// <summary>
		/// Indicates that the font contains multiple masters and is capable of interpolating between them. See the multiple-masters specific API for details.
		/// </summary>
		MultipleMasters =	0x0100,

		/// <summary>
		/// Indicates that the font contains glyph names that can be retrieved through FT_Get_Glyph_Name. Note that some TrueType fonts contain broken glyph name tables. Use the function FT_Has_PS_Glyph_Names when needed.
		/// </summary>
		GlyphNames =		0x0200,

		/// <summary>
		/// Used internally by FreeType to indicate that a face's stream was provided by the client application and should not be destroyed when FT_Done_Face is called. Don't read or test this flag.
		/// </summary>
		ExternalStream =	0x0400,

		/// <summary>
		/// Set if the font driver has a hinting machine of its own. For example, with TrueType fonts, it makes sense to use data from the SFNT ‘gasp’ table only if the native TrueType hinting engine (with the bytecode interpreter) is available and active.
		/// </summary>
		Hinter =			0x0800,

		/// <summary>
		/// Set if the font is CID-keyed. In that case, the font is not accessed by glyph indices but by CID values. For subsetted CID-keyed fonts this has the consequence that not all index values are a valid argument to FT_Load_Glyph. Only the CID values for which corresponding glyphs in the subsetted font exist make FT_Load_Glyph return successfully; in all other cases you get an ‘FT_Err_Invalid_Argument’ error.
		/// Note that CID-keyed fonts which are in an SFNT wrapper don't have this flag set since the glyphs are accessed in the normal way (using contiguous indices); the ‘CID-ness’ isn't visible to the application.
		/// </summary>
		CIDKeyed =			0x1000,

		/// <summary>
		/// Set if the font is ‘tricky’, this is, it always needs the font format's native hinting engine to get a reasonable result. A typical example is the Chinese font ‘mingli.ttf’ which uses TrueType bytecode instructions to move and scale all of its subglyphs.
		/// It is not possible to autohint such fonts using FT_LOAD_FORCE_AUTOHINT; it will also ignore FT_LOAD_NO_HINTING. You have to set both FT_LOAD_NO_HINTING and FT_LOAD_NO_AUTOHINT to really disable hinting; however, you probably never want this except for demonstration purposes.
		/// Currently, there are about a dozen TrueType fonts in the list of tricky fonts; they are hard-coded in file ‘ttobjs.c’.
		/// </summary>
		Tricky =			0x2000
	}
}
