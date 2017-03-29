//Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
//Licensed under the MIT License - https://raw.github.com/Robmaister/SharpFont.HarfBuzz/master/LICENSE

using System;

namespace SharpFont.HarfBuzz
{
	public class Font
	{
		#region Members
		private IntPtr reference;
		#endregion

		#region Constructors
		public static Font FromFTFace(Face face)
		{
			return new Font { reference = HB.hb_ft_font_create(face.Reference, IntPtr.Zero) };
		}
		#endregion

		#region Properties
		internal IntPtr Reference { get { return reference; } }
		#endregion

		#region Methods
		#endregion
	}
}
