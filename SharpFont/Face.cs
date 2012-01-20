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
	/// <summary>
	/// FreeType root face class structure. A face object models a typeface in
	/// a font file.
	/// </summary>
	/// <remarks>
	/// Fields may be changed after a call to FT_Attach_File or
	/// FT_Attach_Stream.
	/// </remarks>
	public class Face
	{
		private IntPtr reference;

		public Face(IntPtr reference)
		{
			this.reference = reference;
		}

		public IntPtr Reference { get { return reference; } }

		/// <summary>
		/// The number of faces in the font file. Some font formats can have
		/// multiple faces in a font file.
		/// </summary>
		public int FaceCount
		{
			get
			{
				return Marshal.ReadInt32(reference);
			}
		}

		/// <summary>
		/// The index of the face in the font file. It is set to 0 if there is
		/// only one face in the font file.
		/// </summary>
		public int FaceIndex
		{
			get
			{
				return Marshal.ReadInt32(reference + sizeof(int) * 1);
			}
		}

		/// <summary>
		/// A set of bit flags that give important information about the face;
		/// see FT_FACE_FLAG_XXX for the details.
		/// </summary>
		public FaceFlags FaceFlags
		{
			get
			{
				return (FaceFlags)Marshal.ReadInt32(reference + sizeof(int) * 2);
			}
		}

		/// <summary>
		/// A set of bit flags indicating the style of the face; see
		/// FT_STYLE_FLAG_XXX for the details.
		/// </summary>
		public StyleFlags StyleFlags
		{
			get
			{
				return (StyleFlags)Marshal.ReadInt32(reference + sizeof(int) * 3);
			}
		}

		/// <summary>
		/// The number of glyphs in the face. If the face is scalable and has
		/// sbits (see ‘num_fixed_sizes’), it is set to the number of outline
		/// glyphs.
		/// 
		/// For CID-keyed fonts, this value gives the highest CID used in the
		/// font.
		/// </summary>
		public int GlyphCount
		{
			get
			{
				return Marshal.ReadInt32(reference + sizeof(int) * 4);
			}
		}

		/// <summary>
		/// The face's family name. This is an ASCII string, usually in
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
				return Marshal.PtrToStringAuto(reference + sizeof(int) * 5);
			}
		}

		/// <summary>
		/// The face's style name. This is an ASCII string, usually in
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
				return Marshal.PtrToStringAuto(reference + sizeof(int) * 6);
			}
		}

		/// <summary>
		/// The number of bitmap strikes in the face. Even if the face is
		/// scalable, there might still be bitmap strikes, which are called
		/// ‘sbits’ in that case.
		/// </summary>
		public int FixedSizesCount
		{
			get
			{
				return Marshal.ReadInt32(reference + sizeof(int) * 7);
			}
		}

		/// <summary>
		/// An array of FT_Bitmap_Size for all bitmap strikes in the face. It
		/// is set to NULL if there is no bitmap strike.
		/// </summary>
		public BitmapSize[] AvailableSizes
		{
			get
			{
				int count = FixedSizesCount;

				if (count == 0)
					return null;

				BitmapSize[] sizes = new BitmapSize[count];
				IntPtr array = Marshal.ReadIntPtr(reference + sizeof(int) * 8);

				for (int i = 0; i < count; i++)
				{
					sizes[i] = new BitmapSize(array + sizeof(int) * i);
				}

				return sizes;
			}
		}

		/// <summary>
		/// The number of charmaps in the face.
		/// </summary>
		public int CharmapsCount
		{
			get
			{
				return Marshal.ReadInt32(reference + sizeof(int) * 9);
			}
		}

		/// <summary>
		/// An array of the charmaps of the face.
		/// </summary>
		public CharMap[] CharMaps
		{
			get
			{
				int count = CharmapsCount;

				if (count == 0)
					return null;

				CharMap[] charmaps = new CharMap[count];
				IntPtr array = Marshal.ReadIntPtr(reference + sizeof(int) * 10);

				for (int i = 0; i < count; i++)
				{
					charmaps[i] = new CharMap(array + sizeof(int) * i);
				}

				return charmaps;
			}
		}

		/// <summary>
		/// A field reserved for client uses. See the FT_Generic type
		/// description.
		/// </summary>
		public Generic Generic;

		/// <summary>
		/// The font bounding box. Coordinates are expressed in font units (see
		/// ‘units_per_EM’). The box is large enough to contain any glyph from
		/// the font. Thus, ‘bbox.yMax’ can be seen as the ‘maximal ascender’,
		/// and ‘bbox.yMin’ as the ‘minimal descender’. Only relevant for
		/// scalable formats.
		/// 
		/// Note that the bounding box might be off by (at least) one pixel for
		/// hinted fonts. See FT_Size_Metrics for further discussion.
		/// </summary>
		public BBox BBox;

		/// <summary>
		/// The number of font units per EM square for this face. This is
		/// typically 2048 for TrueType fonts, and 1000 for Type 1 fonts. Only
		/// relevant for scalable formats.
		/// </summary>
		public ushort UnitsPerEM;

		/// <summary>
		/// The typographic ascender of the face, expressed in font units. For
		/// font formats not having this information, it is set to ‘bbox.yMax’.
		/// Only relevant for scalable formats.
		/// </summary>
		public short Ascender;

		/// <summary>
		/// The typographic descender of the face, expressed in font units. For
		/// font formats not having this information, it is set to ‘bbox.yMin’.
		/// Note that this field is usually negative. Only relevant for
		/// scalable formats.
		/// </summary>
		public short Descender;

		/// <summary>
		/// The height is the vertical distance between two consecutive
		/// baselines, expressed in font units. It is always positive. Only
		/// relevant for scalable formats.
		/// </summary>
		public short Height;

		/// <summary>
		/// The maximal advance width, in font units, for all glyphs in this
		/// face. This can be used to make word wrapping computations faster.
		/// Only relevant for scalable formats.
		/// </summary>
		public short MaxAdvanceWidth;

		/// <summary>
		/// The maximal advance height, in font units, for all glyphs in this
		/// face. This is only relevant for vertical layouts, and is set to
		/// ‘height’ for fonts that do not provide vertical metrics. Only
		/// relevant for scalable formats.
		/// </summary>
		public short MaxAdvanceHeight;

		/// <summary>
		/// The position, in font units, of the underline line for this face.
		/// It is the center of the underlining stem. Only relevant for
		/// scalable formats.
		/// </summary>
		public short UnderlinePosition;

		/// <summary>
		/// The thickness, in font units, of the underline for this face. Only
		/// relevant for scalable formats.
		/// </summary>
		public short UnderlineThickness;

		/// <summary>
		/// The face's associated glyph slot(s).
		/// </summary>
		public GlyphSlot Glyph;

		/// <summary>
		/// The current active size for this face.
		/// </summary>
		public Size Size;

		/// <summary>
		/// The current active charmap for this face.
		/// </summary>
		public CharMap CharMap;

		private IntPtr driver;
		private IntPtr memory;
		private IntPtr stream;
		private IntPtr sizesList;
		private IntPtr autoHint;
		private IntPtr extensions;
		private IntPtr @internal;
	}
}
