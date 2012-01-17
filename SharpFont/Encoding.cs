using System;

namespace SharpFont
{
	/// <summary>
	/// An enumeration used to specify character sets supported by charmaps. Used in the FT_Select_Charmap API function.
	/// </summary>
	/// <remarks>
	/// Despite the name, this enumeration lists specific character repertories (i.e., charsets), and not text encoding methods (e.g., UTF-8, UTF-16, etc.).
	/// Other encodings might be defined in the future.
	/// </remarks>
	public enum Encoding : uint
	{
		/// <summary>
		/// The encoding value 0 is reserved.
		/// </summary>
		None = 0,

		/// <summary>
		/// Corresponds to the Microsoft Symbol encoding, used to encode mathematical symbols in the 32..255 character code range.
		/// </summary>
		/// <see cref="http://www.ceviz.net/symbol.htm"/>
		MicrosoftSymbol =	('s' << 24 | 'y' << 16 | 'm' << 8 | 'b'),

		/// <summary>
		/// Corresponds to the Unicode character set. This value covers all versions of the Unicode repertoire, including ASCII and Latin-1. Most fonts include a Unicode charmap, but not all of them.
		/// For example, if you want to access Unicode value U+1F028 (and the font contains it), use value 0x1F028 as the input value for FT_Get_Char_Index.
		/// </summary>
		Unicode =			('u' << 24 | 'n' << 16 | 'i' << 8 | 'c'),

		/// <summary>
		/// Corresponds to Japanese SJIS encoding.
		/// </summary>
		/// <see cref="http://langsupport.japanreference.com/encoding.shtml"/>
		SJIS =				('s' << 24 | 'j' << 16 | 'i' << 8 | 's'),

		/// <summary>
		/// Corresponds to an encoding system for Simplified Chinese as used used in mainland China.
		/// </summary>
		GB2312 =			('g' << 24 | 'b' << 16 | ' ' << 8 | ' '),

		/// <summary>
		/// Corresponds to an encoding system for Traditional Chinese as used in Taiwan and Hong Kong.
		/// </summary>
		Big5 =				('b' << 24 | 'i' << 16 | 'g' << 8 | '5'),

		/// <summary>
		/// Corresponds to the Korean encoding system known as Wansung.
		/// </summary>
		/// <see cref="http://www.microsoft.com/typography/unicode/949.txt"/>
		Wansung =			('w' << 24 | 'a' << 16 | 'n' << 8 | 's'),

		/// <summary>
		/// The Korean standard character set (KS C 5601-1992), which corresponds to MS Windows code page 1361. This character set includes all possible Hangeul character combinations.
		/// </summary>
		Johab =				('j' << 24 | 'o' << 16 | 'h' << 8 | 'a'),

		/// <summary>
		/// Corresponds to the Adobe Standard encoding, as found in Type 1, CFF, and OpenType/CFF fonts. It is limited to 256 character codes.
		/// </summary>
		AdobeStandard =		('A' << 24 | 'D' << 16 | 'O' << 8 | 'B'),

		/// <summary>
		/// Corresponds to the Adobe Expert encoding, as found in Type 1, CFF, and OpenType/CFF fonts. It is limited to 256 character codes.
		/// </summary>
		AdobeExpert =		('A' << 24 | 'D' << 16 | 'B' << 8 | 'E'),

		/// <summary>
		/// Corresponds to a custom encoding, as found in Type 1, CFF, and OpenType/CFF fonts. It is limited to 256 character codes.
		/// </summary>
		AdobeCustom =		('A' << 24 | 'D' << 16 | 'B' << 8 | 'C'),

		/// <summary>
		/// Corresponds to a Latin-1 encoding as defined in a Type 1 PostScript font. It is limited to 256 character codes.
		/// </summary>
		AdobeLatin1 =		('l' << 24 | 'a' << 16 | 't' << 8 | '1'),

		/// <summary>
		/// This value is deprecated and was never used nor reported by FreeType. Don't use or test for it.
		/// </summary>
		[Obsolete("Never used nor reported by FreeType")]
		OldLatin2 =			('l' << 24 | 'a' << 16 | 't' << 8 | '2'),

		/// <summary>
		/// Corresponds to the 8-bit Apple roman encoding. Many TrueType and OpenType fonts contain a charmap for this encoding, since older versions of Mac OS are able to use it.
		/// </summary>
		AppleRoman =		('a' << 24 | 'r' << 16 | 'm' << 8 | 'n'),
	}
}
