#region MIT License
/*Copyright (c) 2012-2013 Robert Rouhani <robert.rouhani@gmail.com>

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

using SharpFont.PostScript.Internal;

namespace SharpFont.PostScript
{
	/// <summary>
	/// A structure used to represent CID Face information.
	/// </summary>
	public class FaceInfo
	{
		#region Fields

		private IntPtr reference;
		private FaceInfoRec rec;

		#endregion

		#region Constructors

		internal FaceInfo(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		public string CidFontName
		{
			get
			{
				return rec.cid_font_name;
			}
		}

		public int CidVersion
		{
			get
			{
				return (int)rec.cid_version;
			}
		}

		public string Registry
		{
			get
			{
				return rec.registry;
			}
		}

		public string Ordering
		{
			get
			{
				return rec.ordering;
			}
		}

		public int Supplement
		{
			get
			{
				return rec.supplement;
			}
		}

		public FontInfo FontInfo
		{
			get
			{
				return new FontInfo(PInvokeHelper.AbsoluteOffsetOf<FaceInfoRec>(Reference, "font_info"));
			}
		}

		public BBox FontBBox
		{
			get
			{
				return rec.font_bbox;
			}
		}

		[CLSCompliant(false)]
		public uint UidBase
		{
			get
			{
				return (uint)rec.uid_base;
			}
		}

		public int XuidCount
		{
			get
			{
				return rec.num_xuid;
			}
		}

		[CLSCompliant(false)]
		public uint[] Xuid
		{
			get
			{
				return Array.ConvertAll<UIntPtr, uint>(rec.xuid, new Converter<UIntPtr, uint>(delegate(UIntPtr x) { return (uint)x; }));
			}
		}

		[CLSCompliant(false)]
		public uint CidMapOffset
		{
			get
			{
				return (uint)rec.cidmap_offset;
			}
		}

		public int FDBytes
		{
			get
			{
				return rec.fd_bytes;
			}
		}

		public int GDBytes
		{
			get
			{
				return rec.gd_bytes;
			}
		}

		[CLSCompliant(false)]
		public uint CidCount
		{
			get
			{
				return (uint)rec.cid_count;
			}
		}

		public int DictsCount
		{
			get
			{
				return rec.num_dicts;
			}
		}

		public FaceDict FontDicts
		{
			get
			{
				return new FaceDict(PInvokeHelper.AbsoluteOffsetOf<FaceInfoRec>(Reference, "font_dicts"));
			}
		}

		[CLSCompliant(false)]
		public uint DataOffset
		{
			get
			{
				return (uint)rec.data_offset;
			}
		}

		internal IntPtr Reference
		{
			get
			{
				return reference;
			}

			set
			{
				reference = value;
				rec = PInvokeHelper.PtrToStructure<FaceInfoRec>(reference);
			}
		}

		#endregion
	}
}
