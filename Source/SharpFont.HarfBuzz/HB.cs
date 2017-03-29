//Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
//Licensed under the MIT License - https://raw.github.com/Robmaister/SharpFont.HarfBuzz/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace SharpFont.HarfBuzz
{
	public static partial class HB
	{
		public static string VersionString
		{
			get
			{
				return Marshal.PtrToStringAnsi(hb_version_string());
			}
		}

		public static Version Version
		{
			get
			{
				uint major, minor, micro;
				hb_version(out major, out minor, out micro);
				return new Version((int)major, (int)minor, (int)micro);
			}
		}

		public static bool VersionAtLeast(Version version)
		{
			return VersionAtLeast(version.Major, version.Minor, version.Build);
		}

		public static bool VersionAtLeast(int major, int minor, int micro)
		{
			return hb_version_atleast((uint)major, (uint)minor, (uint)micro);
		}

		public static void Shape(this Font font, Buffer buffer)
		{
			HB.hb_shape(font.Reference, buffer.Reference, IntPtr.Zero, 0);
		}
	}
}
