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
	/// A structure used to model a Type 1 or Type 2 FontInfo dictionary. Note
	/// that for Multiple Master fonts, each instance has its own FontInfo
	/// dictionary.
	/// </summary>
	public class FontInfo
	{
		#region Fields

		private IntPtr reference;
		private FontInfoRec rec;

		#endregion

		#region Constructors

		internal FontInfo(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		public string Version
		{
			get
			{
				return rec.version;
			}
		}

		public string Notice
		{
			get
			{
				return rec.notice;
			}
		}

		public string FullName
		{
			get
			{
				return rec.full_name;
			}
		}

		public string FamilyName
		{
			get
			{
				return rec.family_name;
			}
		}

		public string Weight
		{
			get
			{
				return rec.weight;
			}
		}

		public int ItalicAngle
		{
			get
			{
				return (int)rec.italic_angle;
			}
		}

		public bool IsFixedPitch
		{
			get
			{
				return rec.is_fixed_pitch == 1;
			}
		}

		public short UnderlinePosition
		{
			get
			{
				return rec.underline_position;
			}
		}

		[CLSCompliant(false)]
		public ushort UnderlineThickness
		{
			get
			{
				return rec.underline_thickness;
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
				rec = PInvokeHelper.PtrToStructure<FontInfoRec>(reference);
			}
		}

		#endregion
	}
}
