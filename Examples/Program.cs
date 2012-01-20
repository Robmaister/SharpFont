using System;

using SharpFont;
using System.Runtime.InteropServices;

namespace Examples
{
	class Program
	{
		public static void Main(string[] args)
		{
			//TODO have some sort of browser?

			IntPtr library;

			try
			{
				FT.InitFreeType(out library);
				Face regular = FT.NewFace(library, @"Fonts/Cousine-Regular-Latin.ttf", 0);
				Console.Write(regular.FaceFlags);
				FT.DoneFace(regular);
				FT.DoneFreeType(library);
			}
			catch (FreeTypeException e)
			{
				Console.Write(e.Error.ToString());
			}

			Console.Read();
		}
	}
}
