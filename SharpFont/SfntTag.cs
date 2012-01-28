using System;

namespace SharpFont
{
	/// <summary>
	/// An enumeration used to specify the index of an SFNT table. Used in the
	/// FT_Get_Sfnt_Table API function.
	/// </summary>
	public enum SfntTag
	{
		/// <summary>TT_Header</summary>
		head = 0,

		/// <summary>TT_MaxProfile</summary>
		maxp = 1,

		/// <summary>TT_OS2</summary>
		os2 = 2,

		/// <summary>TT_HoriHeader</summary>
		hhea = 3,

		/// <summary>TT_VertHeader</summary>
		vhea = 4,

		/// <summary>TT_Postscript</summary>
		post = 5,

		/// <summary>TT_PCLT</summary>
		pclt = 6
	}
}
