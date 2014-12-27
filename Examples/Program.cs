#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using SharpFont;

namespace Examples
{
	class Program
	{
		//HACK if only Windows had a package manager... sigh
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetDllDirectory(string path);

		public static void Main(string[] args)
		{
			//TODO make several examples in an example browser

			//HACK I'm making the assumption that the .dll.config will correctly resolve Linux and OS X.
			//Therefore only Windows needs to switch dirs.
			int p = (int)Environment.OSVersion.Platform;
			if (p != 4 && p != 6 && p != 128)
			{
				//Thanks StackOverflow! http://stackoverflow.com/a/2594135/1122135
				string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				path = Path.Combine(path, IntPtr.Size == 8 ? "x64" : "x86");
				if (!SetDllDirectory(path))
					throw new System.ComponentModel.Win32Exception();
			}

			try
			{
				using (Library lib = new Library())
				{
					Console.WriteLine("FreeType version: " + lib.Version + "\n");

					using (Face face = lib.NewFace(@"Fonts/Cousine-Regular-Latin.ttf", 0))
					{
						//attach a finalizer delegate
						face.Generic = new Generic(IntPtr.Zero, OnFaceDestroyed);

						//write out some basic font information
						Console.WriteLine("Information for font " + face.FamilyName);
						Console.WriteLine("====================================");
						Console.WriteLine("Number of faces: " + face.FaceCount);
						Console.WriteLine("Face flags: " + face.FaceFlags);
						Console.WriteLine("Style: " + face.StyleName);
						Console.WriteLine("Style flags: " + face.StyleFlags);

						face.SetCharSize(0, 32 * 64, 0, 96);

						Console.WriteLine("\nWriting string \"Hello World!\":");
						Bitmap bmp = RenderString(face, "Hello World!");
						bmp.Save("helloworld.png", ImageFormat.Png);
						bmp.Dispose();

						Console.WriteLine("Done!\n");
					}
				}
			}
			catch (FreeTypeException e)
			{
				Console.Write(e.Error.ToString());
			}

			Console.ReadKey();
		}

		public static Bitmap RenderString(Face face, string text)
		{
			int penX = 0, penY = 0;
			int width = 0;
			int height = 0;

			//measure the size of the string before rendering it, requirement of Bitmap.
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = face.GetCharIndex(c);
				face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

				width += (int)face.Glyph.Advance.X >> 6;

				if (face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					width += (int)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X >> 6;
				}

				if (face.Glyph.Metrics.Height >> 6 > height)
					height = (int)face.Glyph.Metrics.Height >> 6;
			}

			//create a new bitmap that fits the string.
			Bitmap bmp = new Bitmap(width, height);
			Graphics g = Graphics.FromImage(bmp);
			g.Clear(Color.Gray);

			//draw the string
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = face.GetCharIndex(c);
				face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
				face.Glyph.RenderGlyph(RenderMode.Normal);

				if (c == ' ')
				{
					penX += (int)face.Glyph.Advance.X >> 6;

					if (face.HasKerning && i < text.Length - 1)
					{
						char cNext = text[i + 1];
						width += (int)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X >> 6;
					}

					penY += (int)face.Glyph.Advance.Y >> 6;
					continue;
				}

				Bitmap cBmp = face.Glyph.Bitmap.ToGdipBitmap(Color.Firebrick);
				g.DrawImageUnscaled(cBmp, penX, penY + (bmp.Height - face.Glyph.Bitmap.Rows));

				penX += (int)face.Glyph.Advance.X >> 6;
				penY += (int)face.Glyph.Advance.Y >> 6;

				if (face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					penX += (int)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X >> 6;
				}
			}

			g.Dispose();
			return bmp;
		}

		/// <summary>
		/// Called when Face is destroyed.
		/// </summary>
		/// <param name="face">Pointer to the face</param>
		public static void OnFaceDestroyed(IntPtr face)
		{
			//Console.WriteLine(face.FamilyName);
			Console.WriteLine("Face destroyed!");
		}
	}
}
