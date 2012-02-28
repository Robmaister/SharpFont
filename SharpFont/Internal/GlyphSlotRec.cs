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

#if WIN64
using FT_26Dot6 = System.Int32;
using FT_Fixed = System.Int32;
using FT_Long = System.Int32;
using FT_Pos = System.Int32;
using FT_ULong = System.UInt32;
#else
using FT_26Dot6 = System.IntPtr;
using FT_Fixed = System.IntPtr;
using FT_Long = System.IntPtr;
using FT_Pos = System.IntPtr;
using FT_ULong = System.UIntPtr;
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
		internal GenericInternal generic;

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

		private IntPtr @internal;
	}
}
