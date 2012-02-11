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
	/// Internally represents a Face.
	/// </summary>
	/// <remarks>
	/// Refer to <see cref="Face"/> for FreeType documentation.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	internal class FaceRec
	{
		internal FT_Long num_faces;
		internal FT_Long face_index;

		internal FT_Long face_flags;
		internal FT_Long style_flags;

		internal FT_Long num_glyphs;

		[MarshalAs(UnmanagedType.LPStr)]
		internal string family_name;

		[MarshalAs(UnmanagedType.LPStr)]
		internal string style_name;

		internal int num_fixed_sizes;
		internal IntPtr available_sizes;

		internal int num_charmaps;
		internal IntPtr charmaps;

		//HACK change Generic to a struct!
		internal IntPtr generic;
		internal IntPtr generic_finalizer;

		internal BBoxInternal bbox;

		internal ushort units_per_EM;
		internal short ascender;
		internal short descender;
		internal short height;

		internal short max_advance_width;
		internal short max_advance_height;

		internal short underline_position;
		internal short underline_thickness;

		internal IntPtr glyph;
		internal IntPtr size;
		internal IntPtr charmap;

		private IntPtr driver;
		private IntPtr memory;
		private IntPtr stream;

		private IntPtr sizes_list;
		//HACK generic again
		private IntPtr autohint;
		private IntPtr autohint_1;
		private IntPtr extensions;

		private IntPtr @internal;

		//TODO adjust for the hacked in Generic IntPtrs.
		internal static int SizeInBytes { get { return 24 + sizeof(FT_Long) * 5 + IntPtr.Size * 17 + BBoxInternal.SizeInBytes; } }
	}
}
