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

using SharpFont.PostScript.Internal;

namespace SharpFont.PostScript
{
	/// <summary>
	/// A structure used to represent CID Face information.
	/// </summary>
	public class FaceInfo
	{
		internal IntPtr reference;
		internal FaceInfoRec rec;

		internal FaceInfo(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<FaceInfoRec>(reference);
		}

		public string CIDFontName
		{
			get
			{
				return rec.cid_font_name;
			}
		}

		public int CIDVersion
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
				return new FontInfo(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceInfoRec), "font_info").ToInt64()));
			}
		}

		public BBox FontBBox
		{
			get
			{
				return new BBox(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceInfoRec), "font_bbox").ToInt64()));
			}
		}

		[CLSCompliant(false)]
		public uint UIDBase
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
				#if WIN64
				return rec.xuid;
				#else
				return Array.ConvertAll<UIntPtr, uint>(rec.xuid, new Converter<UIntPtr,uint>(delegate(UIntPtr x) { return (uint)x; }));
				#endif
			}
		}

		[CLSCompliant(false)]
		public uint CIDMapOffset
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
		public uint CIDCount
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
				return new FaceDict(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceInfo), "font_dicts").ToInt64()));
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
	}
}
