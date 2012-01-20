using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// The base charmap structure.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CharMap
	{
		/// <summary>
		/// A handle to the parent face object.
		/// </summary>
		Face *Face;

		/// <summary>
		/// An <see cref="Encoding"/> tag identifying the charmap. Use this
		/// with <see cref="FT.SelectCharmap"/>.
		/// </summary>
		Encoding Encoding;

		/// <summary>
		/// An ID number describing the platform for the following encoding ID.
		/// This comes directly from the TrueType specification and should be
		/// emulated for other formats.
		/// </summary>
		PlatformID PlatformID;

		/// <summary>
		/// A platform specific encoding number. This also comes from the
		/// TrueType specification and should be emulated similarly.
		/// </summary>
		EncodingID EncodingID;
	}
}
