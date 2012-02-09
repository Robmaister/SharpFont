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

		//HACK change BBox to a struct!
		internal FT_Pos bbox_1;
		internal FT_Pos bbox_2;
		internal FT_Pos bbox_3;
		internal FT_Pos bbox_4;

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
	}
}
