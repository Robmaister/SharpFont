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

namespace SharpFont.TrueType
{
	/// <summary>
	/// A list of valid values for the ‘encoding_id’ for
	/// TT_PLATFORM_APPLE_UNICODE charmaps and name entries.
	/// </summary>
	public enum AppleEncodingID : ushort
	{
		/// <summary>
		/// Unicode version 1.0.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Unicode 1.1; specifies Hangul characters starting at U+34xx.
		/// </summary>
		Unicode11,

		/// <summary>
		/// Deprecated (identical to preceding).
		/// </summary>
		ISO10646,

		/// <summary>
		/// Unicode 2.0 and beyond (UTF-16 BMP only).
		/// </summary>
		Unicode20,

		/// <summary>
		/// Unicode 3.1 and beyond, using UTF-32.
		/// </summary>
		Unicode32,

		/// <summary>
		/// From Adobe, not Apple. Not a normal cmap. Specifies variations on a
		/// real cmap.
		/// </summary>
		VariantSelector,
	}

	/// <summary>
	/// A list of valid values for the ‘encoding_id’ for TT_PLATFORM_MACINTOSH
	/// charmaps and name entries.
	/// </summary>
	public enum MacEncodingID : ushort
	{
		Roman = 0,
		Japanese = 1,
		TraditionalChinese = 2,
		Korean = 3,
		Arabic = 4,
		Hebrew = 5,
		Greek = 6,
		Russian = 7,
		RSymbol = 8,
		Devanagari = 9,
		Gurmukhi = 10,
		Gujarati = 11,
		Oriya = 12,
		Bengali = 13,
		Tamil = 14,
		Telugu = 15,
		Kannada = 16,
		Malayalam = 17,
		Sinhalese = 18,
		Burmese = 19,
		Khmer = 20,
		Thai = 21,
		Laotian = 22,
		Georgian = 23,
		Armenian = 24,
		Maldivian = 25,
		SimplifiedChinese = 25,
		Tibetan = 26,
		Mongolian = 27,
		Geez = 28,
		Slavic = 29,
		Vietnamese = 30,
		Sindhi = 31,
		Uninterpreted = 32,
	}

	/// <summary>
	/// A list of valid values for the ‘encoding_id’ for TT_PLATFORM_MICROSOFT
	/// charmaps and name entries.
	/// </summary>
	public enum MicrosoftEncodingID : ushort
	{
		/// <summary>
		/// Corresponds to Microsoft symbol encoding. See
		/// FT_ENCODING_MS_SYMBOL.
		/// </summary>
		Symbol = 0,

		/// <summary>
		/// Corresponds to a Microsoft WGL4 charmap, matching Unicode. See
		/// FT_ENCODING_UNICODE.
		/// </summary>
		Unicode = 1,

		/// <summary>
		/// Corresponds to SJIS Japanese encoding. See FT_ENCODING_SJIS.
		/// </summary>
		SJIS = 2,

		/// <summary>
		/// Corresponds to Simplified Chinese as used in Mainland China. See
		/// FT_ENCODING_GB2312.
		/// </summary>
		GB2312 = 3,

		/// <summary>
		/// Corresponds to Traditional Chinese as used in Taiwan and Hong Kong.
		/// See FT_ENCODING_BIG5.
		/// </summary>
		Big5 = 4,

		/// <summary>
		/// Corresponds to Korean Wansung encoding. See FT_ENCODING_WANSUNG.
		/// </summary>
		Wansung = 5,

		/// <summary>
		/// Corresponds to Johab encoding. See FT_ENCODING_JOHAB.
		/// </summary>
		Johab = 6,

		/// <summary>
		/// Corresponds to UCS-4 or UTF-32 charmaps. This has been added to the
		/// OpenType specification version 1.4 (mid-2001.)
		/// </summary>
		UCS4 = 10,
	}

	/// <summary>
	/// A list of valid values for the ‘encoding_id’ for TT_PLATFORM_ADOBE
	/// charmaps. This is a FreeType-specific extension!
	/// </summary>
	public enum AdobeEncodingID : ushort
	{
		/// <summary>
		/// Adobe standard encoding.
		/// </summary>
		Standard = 0,

		/// <summary>
		/// Adobe expert encoding.
		/// </summary>
		Expert = 1,

		/// <summary>
		/// Adobe custom encoding.
		/// </summary>
		Custom = 2,

		/// <summary>
		/// Adobe Latin 1 encoding.
		/// </summary>
		Latin1 = 3
	}
}
