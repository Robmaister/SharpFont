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
using System.Runtime.InteropServices;
using System.Threading;

using SharpFont;

namespace Examples
{
	class Program
	{
		public static void Main(string[] args)
		{
			//TODO make several examples in an example browser

			try
			{
				using (Library lib = new Library())
				{
					Console.WriteLine("FreeType version: " + lib.Version() + "\n");

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
						bmp.Save("helloworld5.png", ImageFormat.Png);
						bmp.Dispose();

						Console.WriteLine("Done!\n");
					}
				}
			}
			catch (FreeTypeException e)
			{
				Console.Write(e.Error.ToString());
			}

			Console.ReadLine();
		}

		public static Bitmap RenderString(Face f, string text)
		{
			int penX = 0, penY = 0;
			int width = 0;
			int height = 0;

			//measure the size of the string before rendering it, requirement of Bitmap.
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = f.GetCharIndex(c);
				f.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

				width += (int)f.Glyph.Advance.X >> 6;

				if (FT.HasKerning(f) && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					width += (int)f.GetKerning(glyphIndex, f.GetCharIndex(cNext), KerningMode.Default).X >> 6;
				}

				if (f.Glyph.Metrics.Height >> 6 > height)
					height = (int)f.Glyph.Metrics.Height >> 6;
			}

			//create a new bitmap that fits the string.
			Bitmap bmp = new Bitmap(width, height);

			penX = 0;
			penY = 0;

			//draw the string
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = f.GetCharIndex(c);
				f.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
				f.RenderGlyph(f.Glyph, RenderMode.Normal);

				if (c == ' ')
				{
					penX += (int)f.Glyph.Advance.X >> 6;

					if (FT.HasKerning(f) && i < text.Length - 1)
					{
						char cNext = text[i + 1];
						width += (int)f.GetKerning(glyphIndex, f.GetCharIndex(cNext), KerningMode.Default).X >> 6;
					}

					penY += (int)f.Glyph.Advance.Y >> 6;
					continue;
				}

				BitmapData data = bmp.LockBits(new Rectangle(penX, penY + (bmp.Height - f.Glyph.Bitmap.Rows), f.Glyph.Bitmap.Width, f.Glyph.Bitmap.Rows), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				byte[] pixelAlphas = new byte[f.Glyph.Bitmap.Width * f.Glyph.Bitmap.Rows];
				Marshal.Copy(f.Glyph.Bitmap.Buffer, pixelAlphas, 0, pixelAlphas.Length);

				for (int j = 0; j < pixelAlphas.Length; j++)
				{
					int pixelOffset = (j / data.Width) * data.Stride + (j % data.Width * 4);
					Marshal.WriteByte(data.Scan0, pixelOffset + 3, pixelAlphas[j]);
				}

				bmp.UnlockBits(data);

				penX += (int)f.Glyph.Advance.X >> 6;
				penY += (int)f.Glyph.Advance.Y >> 6;

				if (FT.HasKerning(f) && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					width += (int)f.GetKerning(glyphIndex, f.GetCharIndex(cNext), KerningMode.Default).X >> 6;
				}
			}

			return bmp;
		}

		/// <summary>
		/// Called when Face is destroyed.
		/// </summary>
		/// <param name="face">Pointer to the face</param>
		public static void OnFaceDestroyed(IntPtr face)
		{
			Console.WriteLine("Face destroyed!");
		}
	}
}
