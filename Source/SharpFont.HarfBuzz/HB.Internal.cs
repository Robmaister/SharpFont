//Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
//Licensed under the MIT License - https://raw.github.com/Robmaister/SharpFont.HarfBuzz/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace SharpFont.HarfBuzz
{
	public static partial class HB
	{
		private const string HarfBuzzDll = "libharfbuzz-0.dll";

		private const CallingConvention CallConvention = CallingConvention.Cdecl;

		#region hb-blob

		#endregion

		#region hb-buffer

		#endregion

		#region hb-common

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern Direction hb_direction_from_string(string str, int len);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern string hb_direction_to_string(Direction dir);

		#endregion

		#region hb-coretext

		#endregion

		#region hb-face

		#endregion

		#region hb-font

		#endregion

		#region hb-ft

		//TODO get proper delegate type for "destroy" parameters
		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_ft_face_create(IntPtr ft_face, IntPtr destroy);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_ft_face_create_cached(IntPtr ft_face);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_ft_font_create(IntPtr ft_face, IntPtr destroy);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ft_font_set_funcs(IntPtr font);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_ft_font_get_face(IntPtr font);

		#endregion

		#region hb-glib

		#endregion

		#region hb-gobject

		#endregion

		#region hb-graphite2

		#endregion

		#region hb-icu

		#endregion

		#region hb-ot

		#endregion

		#region hb-set

		#endregion

		#region hb-shape

		#endregion

		#region hb-unicode

		#endregion

		#region hb-uniscribe

		#endregion

		#region hb-version

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_version(out uint major, out uint minor, out uint micro);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern bool hb_version_atleast(uint major, uint minor, uint micro);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_version_string();

		#endregion

		#region Unfinished Entry Points

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_create_sub_blob();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_get_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_get_data_writable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_get_length();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_is_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_make_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_blob_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_add();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_add_utf16();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_add_utf32();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_add_utf8(IntPtr buffer, byte[] text, int text_length, int item_offset, int item_length);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_allocation_successful();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_clear_contents();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_buffer_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_deserialize_glyphs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_content_type();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_flags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_buffer_get_glyph_infos(IntPtr buf, out int length);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern IntPtr hb_buffer_get_glyph_positions(IntPtr buf, out int length);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_language();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern int hb_buffer_get_length(IntPtr buf);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_segment_properties();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_unicode_funcs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_guess_segment_properties();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_normalize_glyphs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_pre_allocate();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_reset();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_reverse();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_reverse_clusters();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_serialize_format_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_serialize_format_to_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_serialize_glyphs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_serialize_list_formats();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_content_type();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_direction(IntPtr buffer, Direction direction);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_flags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_language();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_length();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_script(IntPtr ptr, Script script);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_segment_properties();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_unicode_funcs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_buffer_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_create_for_tables();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_get_glyph_count();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_get_index();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_get_upem();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_is_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_make_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_reference_blob();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_reference_table();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_set_glyph_count();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_set_index();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_set_upem();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_face_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_feature_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_feature_to_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_add_glyph_origin_for_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_create_sub_font();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_is_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_make_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_contour_point_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_extents_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_from_name_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_h_advance_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_h_kerning_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_h_origin_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_name_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_v_advance_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_v_kerning_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_glyph_v_origin_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_funcs_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_face();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_advance_for_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_contour_point();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_contour_point_for_origin();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_extents();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_extents_for_origin();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_from_name();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_h_advance();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_h_kerning();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_h_origin();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_kerning_for_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_name();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_origin_for_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_v_advance();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_v_kerning();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_glyph_v_origin();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_parent();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_ppem();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_scale();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_glyph_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_glyph_to_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_is_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_make_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_set_funcs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_set_funcs_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_set_ppem();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_set_scale();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_font_subtract_glyph_origin_for_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_glib_get_unicode_funcs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_glib_script_from_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_glib_script_to_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_language_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_language_get_default();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_language_to_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_collect_lookups();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_feature_get_lookups();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_get_attach_points();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_get_glyph_class();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_get_glyphs_in_class();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_get_ligature_carets();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_get_size_params();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_has_glyph_classes();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_has_positioning();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_has_substitution();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_language_find_feature();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_language_get_feature_indexes();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_language_get_feature_tags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_language_get_required_feature_index();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_lookup_collect_glyphs();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_lookup_substitute_closure();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_lookup_would_substitute();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_script_find_language();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_script_get_language_tags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_table_choose_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_table_find_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_table_get_feature_tags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_table_get_lookup_count();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_layout_table_get_script_tags();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_shape_glyphs_closure();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_shape_plan_collect_lookups();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_tag_from_language();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_tag_to_language();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_tag_to_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_ot_tags_from_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_script_from_iso15924_tag();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_script_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_script_get_horizontal_direction();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_script_to_iso15924_tag();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_segment_properties_equal();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_segment_properties_hash();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_add();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_add_range();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_allocation_successful();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_clear();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_del();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_del_range();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_get_max();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_get_min();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_get_population();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_has();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_intersect();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_invert();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_is_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_is_equal();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_next();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_next_range();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_set();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_subtract();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_symmetric_difference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_set_union();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape(IntPtr font, IntPtr buffer, IntPtr features, int num_features);

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_full();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_list_shapers();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_create_cached();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_execute();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_get_shaper();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_shape_plan_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_tag_from_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_tag_to_string();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_combining_class();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_compose();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_decompose();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_decompose_compatibility();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_eastasian_width();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_create();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_destroy();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_get_default();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_get_empty();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_get_parent();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_get_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_is_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_make_immutable();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_reference();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_combining_class_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_compose_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_decompose_compatibility_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_decompose_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_eastasian_width_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_general_category_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_mirroring_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_script_func();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_funcs_set_user_data();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_general_category();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_mirroring();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_unicode_script();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_uniscribe_font_get_hfont();

		[DllImport(HarfBuzzDll, CallingConvention = CallConvention)]
		internal static extern void hb_uniscribe_font_get_logfontw();

		#endregion
	}
}
