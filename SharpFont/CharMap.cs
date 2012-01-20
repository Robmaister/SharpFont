using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// The base charmap structure.
	/// </summary>
	public class CharMap
	{
		private IntPtr reference;

		public CharMap(IntPtr reference)
		{
			this.reference = reference;
		}

		public IntPtr Reference { get { return reference; } }

		/// <summary>
		/// A handle to the parent face object.
		/// </summary>
		public Face Face
		{
			get
			{
				return new Face(Marshal.ReadIntPtr(reference + sizeof(int) * 0));
			}
		}

		/// <summary>
		/// An <see cref="Encoding"/> tag identifying the charmap. Use this
		/// with <see cref="FT.SelectCharmap"/>.
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return (Encoding)Marshal.ReadInt32(reference + sizeof(int) * 1);
			}
		}

		/// <summary>
		/// An ID number describing the platform for the following encoding ID.
		/// This comes directly from the TrueType specification and should be
		/// emulated for other formats.
		/// </summary>
		public PlatformID PlatformID
		{
			get
			{
				return (PlatformID)Marshal.ReadInt32(reference + sizeof(int) * 2);
			}
		}

		/// <summary>
		/// A platform specific encoding number. This also comes from the
		/// TrueType specification and should be emulated similarly.
		/// </summary>
		public EncodingID EncodingID
		{
			get
			{
				return (EncodingID)Marshal.ReadInt32(reference + sizeof(int) * 3);
			}
		}
	}
}
