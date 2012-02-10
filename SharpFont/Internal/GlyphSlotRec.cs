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

#if FT64
using FT_Long = System.Int64;
using FT_ULong = System.UInt64;
using FT_Fixed = System.Int64;
using FT_Pos = System.Int64;
using FT_26Dot6 = System.Int64;
#else
using FT_Long = System.Int32;
using FT_ULong = System.UInt32;
using FT_Fixed = System.Int32;
using FT_Pos = System.Int32;
using FT_26Dot6 = System.Int32;
#endif

namespace SharpFont.Internal
{
	/// <summary>
	/// Internally represents a GlyphSlot.
	/// </summary>
	/// <remarks>
	/// Refer to <see cref="GlyphSlot"/> for FreeType documentation.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	internal class GlyphSlotRec
	{
		internal IntPtr library;
		internal IntPtr face;
		internal IntPtr next;
		internal uint reserved;
		//HACK generic
		internal IntPtr generic_1;
		internal IntPtr generic_2;

		internal GlyphMetricsInternal metrics;
		internal FT_Fixed linearHoriAdvance;
		internal FT_Fixed linearVertAdvance;
		internal VectorInternal advance;

		internal GlyphFormat format;

		internal BitmapInternal bitmap;
		internal int bitmap_left;
		internal int bitmap_top;

		internal OutlineInternal outline;

		internal uint num_subglyphs;
		internal IntPtr subglyphs;

		internal IntPtr control_data;
		internal FT_Long control_len;

		internal FT_Pos lsb_delta;
		internal FT_Pos rsb_delta;

		internal IntPtr other;

		internal IntPtr @internal;
	}
}
