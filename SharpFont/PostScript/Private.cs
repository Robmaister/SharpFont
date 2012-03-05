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
	/// A structure used to model a Type 1 or Type 2 private dictionary. Note
	/// that for Multiple Master fonts, each instance has its own Private
	/// dictionary.
	/// </summary>
	public class Private
	{
		internal IntPtr reference;
		internal PrivateRec rec;

		internal Private(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<PrivateRec>(reference);
		}

		public int UniqueID
		{
			get
			{
				return rec.unique_id;
			}
		}

		public int LenIV
		{
			get
			{
				return rec.lenIV;
			}
		}

		public byte BlueValuesCount
		{
			get
			{
				return rec.num_blue_values;
			}
		}

		public byte OtherBluesCount
		{
			get
			{
				return rec.num_other_blues;
			}
		}

		public byte FamilyBluesCount
		{
			get
			{
				return rec.num_family_blues;
			}
		}

		public byte FamilyOtherBluesCount
		{
			get
			{
				return rec.num_family_other_blues;
			}
		}

		public short[] BlueValues
		{
			get
			{
				return rec.blue_values;
			}
		}

		public short[] OtherBlues
		{
			get
			{
				return rec.other_blues;
			}
		}

		public short[] FamilyBlues
		{
			get
			{
				return rec.family_blues;
			}
		}

		public short[] FamilyOtherBlues
		{
			get
			{
				return rec.family_other_blues;
			}
		}

		public int BlueScale
		{
			get
			{
				return (int)rec.blue_scale;
			}
		}

		public int BlueShift
		{
			get
			{
				return rec.blue_shift;
			}
		}

		public int BlueFuzz
		{
			get
			{
				return rec.blue_fuzz;
			}
		}

		[CLSCompliant(false)]
		public ushort StandardWidth
		{
			get
			{
				return rec.standard_width;
			}
		}

		[CLSCompliant(false)]
		public ushort StandardHeight
		{
			get
			{
				return rec.standard_height;
			}
		}

		public byte SnapWidthsCount
		{
			get
			{
				return rec.num_snap_widths;
			}
		}

		public byte SnapHeightsCount
		{
			get
			{
				return rec.num_snap_heights;
			}
		}

		public bool ForceBold
		{
			get
			{
				return rec.force_bold == 1;
			}
		}

		public bool RoundStemUp
		{
			get
			{
				return rec.round_stem_up == 1;
			}
		}

		public short[] SnapWidths
		{
			get
			{
				return rec.snap_widths;
			}
		}

		public short[] SnapHeights
		{
			get
			{
				return rec.snap_heights;
			}
		}

		public int ExpansionFactor
		{
			get
			{
				return (int)rec.expansion_factor;
			}
		}

		public int LanguageGroup
		{
			get
			{
				return (int)rec.language_group;
			}
		}

		public int Password
		{
			get
			{
				return (int)rec.password;
			}
		}

		public short[] MinFeature
		{
			get
			{
				return rec.min_feature;
			}
		}
	}
}
