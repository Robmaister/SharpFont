using System;

namespace SharpFont
{
	public partial class FT
	{
		/// <summary>
		/// Return the version of the FreeType library being used.
		/// </summary>
		/// <remarks>
		/// The reason why this function takes a "library" argument is because certain programs implement library initialization in a custom way that doesn't use <see cref="InitFreeType"/>.
		/// In such cases, the library version might not be available before the library object has been created.
		/// </remarks>
		/// <param name="library">A source library handle.</param>
		/// <param name="amajor">The major version number.</param>
		/// <param name="aminor">The minor version number.</param>
		/// <param name="apatch">The patch version number.</param>
		public void LibraryVersion(IntPtr library, out int amajor, out int aminor, out int apatch)
		{
			FT_Library_Version(library, out amajor, out aminor, out apatch);
		}

		/// <summary>
		/// Parse all bytecode instructions of a TrueType font file to check whether any of the patented opcodes are used. This is only useful if you want to be able to use the unpatented hinter with fonts that do not use these opcodes.
		/// Note that this function parses all glyph instructions in the font file, which may be slow.
		/// </summary>
		/// <remarks>
		/// Since May 2010, TrueType hinting is no longer patented.
		/// </remarks>
		/// <param name="face">A <see cref="Face"/> handle.</param>
		/// <returns>True if this is a TrueType font that uses one of the patented opcodes, false otherwise.</returns>
		public bool FaceCheckTrueTypePatents(Face face)
		{
			return FT_Face_CheckTrueTypePatents(face.Reference);
		}

		/// <summary>
		/// Enable or disable the unpatented hinter for a given <see cref="Face"/>. Only enable it if you have determined that the face doesn't use any patented opcodes.
		/// </summary>
		/// <remarks>
		/// Since May 2010, TrueType hinting is no longer patented.
		/// </remarks>
		/// <param name="face">A face handle.</param>
		/// <param name="value">New boolean setting.</param>
		/// <returns>The old setting value. This will always be false if this is not an SFNT font, or if the unpatented hinter is not compiled in this instance of the library.</returns>
		/// <see cref="FaceCheckTrueTypePatents"/>
		public bool FaceSetUnpatentedHinting(Face face, bool value)
		{
			return FT_Face_SetUnpatentedHinting(face.Reference, value);
		}
	}
}
