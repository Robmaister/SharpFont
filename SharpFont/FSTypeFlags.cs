using System;

namespace SharpFont
{
	/// <summary>
	/// A list of bit flags used in the ‘fsType’ field of the OS/2 table in a
	/// TrueType or OpenType font and the ‘FSType’ entry in a PostScript font.
	/// These bit flags are returned by FT_Get_FSType_Flags; they inform client
	/// applications of embedding and subsetting restrictions associated with a
	/// font.
	/// </summary>
	/// <remarks>
	/// While the fsType flags can indicate that a font may be embedded, a
	/// license with the font vendor may be separately required to use the font
	/// in this way.
	/// </remarks>
	/// <see href="http://www.adobe.com/devnet/acrobat/pdfs/FontPolicies.pdf"/>
	[Flags]
	public enum FSTypeFlags : ushort
	{
		/// <summary>
		/// Fonts with no fsType bit set may be embedded and permanently
		/// installed on the remote system by an application.
		/// </summary>
		InstallableEmbedding =			0x0000,

		/// <summary>
		/// Fonts that have only this bit set must not be modified, embedded
		/// or exchanged in any manner without first obtaining permission of
		/// the font software copyright owner.
		/// </summary>
		RestrictedLicenseEmbedding =	0x0002,

		/// <summary>
		/// If this bit is set, the font may be embedded and temporarily loaded
		/// on the remote system. Documents containing Preview &amp; Print
		/// fonts must be opened ‘read-only’; no edits can be applied to the
		/// document.
		/// </summary>
		PreviewAndPrintEmbedding =		0x0004,

		/// <summary>
		/// If this bit is set, the font may be embedded but must only be
		/// installed temporarily on other systems. In contrast to Preview
		/// &amp; Print fonts, documents containing editable fonts may be
		/// opened for reading, editing is permitted, and changes may be saved.
		/// </summary>
		EditableEmbedding =				0x0008,

		/// <summary>
		/// If this bit is set, the font may not be subsetted prior to
		/// embedding.
		/// </summary>
		NoSubsetting =					0x0100,

		/// <summary>
		/// If this bit is set, only bitmaps contained in the font may be
		/// embedded; no outline data may be embedded. If there are no bitmaps
		/// available in the font, then the font is unembeddable.
		/// </summary>
		BitmapEmbeddingOnly =			0x0200
	}
}
