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
	/// Fields may be changed after a call to FT_Attach_File or
	/// FT_Attach_Stream.
	/// </remarks>
	public sealed class Face : IDisposable
	{
		#region Fields

		internal IntPtr reference;
		internal FaceRec rec;

		private bool duplicate;
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
			this.rec = (FaceRec)Marshal.PtrToStructure(reference, typeof(FaceRec));
			this.duplicate = duplicate;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The number of faces in the font file. Some font formats can have
		/// multiple faces in a font file.
		/// </summary>
		public long FaceCount
		{
			get
			{
				return rec.num_faces;
			}
		}

		/// <summary>
		/// The index of the face in the font file. It is set to 0 if there is
		/// only one face in the font file.
		/// </summary>
		public long FaceIndex
		{
			get
			{
				return rec.face_index;
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
				return (FaceFlags)rec.face_flags;
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
				return (StyleFlags)rec.style_flags;
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
		public long GlyphCount
		{
			get
			{
				return rec.num_glyphs;
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
				return rec.family_name;
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
				return rec.style_name;
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
				return rec.num_fixed_sizes;
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
				IntPtr array = rec.available_sizes;

				for (int i = 0; i < count; i++)
				{
					sizes[i] = new BitmapSize(new IntPtr(array.ToInt64() + IntPtr.Size * i));
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
				return rec.num_charmaps;
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
				IntPtr array = rec.charmaps;

				for (int i = 0; i < count; i++)
				{
					charmaps[i] = new CharMap(new IntPtr(array.ToInt64() + IntPtr.Size * i));
				}

				return charmaps;
			}
		}

		/// <summary>
		/// A field reserved for client uses. See the FT_Generic type
		/// description.
		/// </summary>
		public Generic Generic
		{
			get
			{
				//HACK fix this later.
				return new Generic(reference, IntPtr.Size * 11);
			}
		}

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
		public BBox BBox
		{
			get
			{
				return new BBox(rec.bbox);
			}
		}

		/// <summary>
		/// The number of font units per EM square for this face. This is
		/// typically 2048 for TrueType fonts, and 1000 for Type 1 fonts. Only
		/// relevant for scalable formats.
		/// </summary>
		public ushort UnitsPerEM
		{
			get
			{
				return rec.units_per_EM;
			}
		}

		/// <summary>
		/// The typographic ascender of the face, expressed in font units. For
		/// font formats not having this information, it is set to ‘bbox.yMax’.
		/// Only relevant for scalable formats.
		/// </summary>
		public short Ascender
		{
			get
			{
				return rec.ascender;
			}
		}

		/// <summary>
		/// The typographic descender of the face, expressed in font units. For
		/// font formats not having this information, it is set to ‘bbox.yMin’.
		/// Note that this field is usually negative. Only relevant for
		/// scalable formats.
		/// </summary>
		public short Descender
		{
			get
			{
				return rec.descender;
			}
		}

		/// <summary>
		/// The height is the vertical distance between two consecutive
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
		/// The maximal advance width, in font units, for all glyphs in this
		/// face. This can be used to make word wrapping computations faster.
		/// Only relevant for scalable formats.
		/// </summary>
		public short MaxAdvanceWidth
		{
			get
			{
				return rec.max_advance_width;
			}
		}

		/// <summary>
		/// The maximal advance height, in font units, for all glyphs in this
		/// face. This is only relevant for vertical layouts, and is set to
		/// ‘height’ for fonts that do not provide vertical metrics. Only
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
		/// The position, in font units, of the underline line for this face.
		/// It is the center of the underlining stem. Only relevant for
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
		/// The thickness, in font units, of the underline for this face. Only
		/// relevant for scalable formats.
		/// </summary>
		public short UnderlineThickness
		{
			get
			{
				return rec.underline_thickness;
			}
		}

		/// <summary>
		/// The face's associated glyph slot(s).
		/// </summary>
		public GlyphSlot Glyph
		{
			get
			{
				return new GlyphSlot(rec.glyph);
			}
		}

		/// <summary>
		/// The current active size for this face.
		/// </summary>
		public Size Size
		{
			get
			{
				return new Size(rec.size);
			}
		}

		/// <summary>
		/// The current active charmap for this face.
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

		public bool CheckTrueTypePatents()
		{
			return FT.FaceCheckTrueTypePatents(this);
		}

		public bool SetUnpatentedHinting(bool value)
		{
			return FT.FaceSetUnpatentedHinting(this, value);
		}

		public void AttachFile(string path)
		{
			FT.AttachFile(this, path);
		}

		public void AttachStream(OpenArgs parameters)
		{
			FT.AttachStream(this, parameters);
		}

		public void SelectSize(int strikeIndex)
		{
			FT.SelectSize(this, strikeIndex);
		}

		public void RequestSize(SizeRequest request)
		{
			FT.RequestSize(this, request);
		}

		public void SetCharSize(int width, int height, uint horizontalResolution, uint verticalResolution)
		{
			FT.SetCharSize(this, width, height, horizontalResolution, verticalResolution);
		}

		public void SetPixelSizes(uint width, uint height)
		{
			FT.SetPixelSizes(this, width, height);
		}

		public void LoadGlyph(uint glyphIndex, LoadFlags flags, LoadTarget target)
		{
			FT.LoadGlyph(this, glyphIndex, flags, target);
		}

		public void LoadChar(uint charCode, LoadFlags flags, LoadTarget target)
		{
			FT.LoadChar(this, charCode, flags, target);
		}

		public void SetTransform(Matrix2i matrix, Vector2i delta)
		{
			FT.SetTransform(this, matrix, delta);
		}

		public void RenderGlyph(GlyphSlot slot, RenderMode mode)
		{
			FT.RenderGlyph(slot, mode);
		}

		public Vector2i GetKerning(uint leftGlyph, uint rightGlyph, KerningMode mode)
		{
			return FT.GetKerning(this, leftGlyph, rightGlyph, mode);
		}

		public int GetTrackKerning(int pointSize, int degree)
		{
			return FT.GetTrackKerning(this, pointSize, degree);
		}

		public string GetGlyphName(uint glyphIndex, int bufferSize)
		{
			return FT.GetGlyphName(this, glyphIndex, bufferSize);
		}

		public string GetGlyphName(uint glyphIndex, byte[] buffer)
		{
			return FT.GetGlyphName(this, glyphIndex, buffer);
		}

		public string GetPostscriptName()
		{
			return FT.GetPostscriptName(this);
		}

		public void SelectCharmap(Encoding encoding)
		{
			FT.SelectCharmap(this, encoding);
		}

		public void SetCharmap(CharMap charmap)
		{
			FT.SetCharmap(this, charmap);
		}

		public uint GetCharIndex(uint charCode)
		{
			return FT.GetCharIndex(this, charCode);
		}

		public uint GetFirstChar(out uint glyphIndex)
		{
			return FT.GetFirstChar(this, out glyphIndex);
		}

		public uint GetNextChar(uint charCode, out uint glyphIndex)
		{
			return FT.GetNextChar(this, charCode, out glyphIndex);
		}

		public uint GetNameIndex(string name)
		{
			return FT.GetNameIndex(this, name);
		}

		#endregion

		#region IDisposable members

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

				//only DoneFace if this was the original Face and not a duplicate reference.
				if (!duplicate)
					FT.DoneFace(this);

				disposed = true;
			}
		}

		~Face()
		{
			Dispose(false);
		}

		#endregion
	}
}
