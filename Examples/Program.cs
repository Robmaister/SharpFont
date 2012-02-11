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
using System.IO;

using SharpFont;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Examples
{
	class Program
	{
		public unsafe static void Main(string[] args)
		{
			//TODO have some sort of browser?
			
			try
			{
				using (Library lib = new Library())
				{
					Console.WriteLine("FreeType version: " + lib.Version());

					using (Face regular = FT.NewFace(lib, @"Fonts/Cousine-Regular-Latin.ttf", 0))
					{

						//write out some basic font information
						Console.WriteLine("Information for font " + regular.FamilyName);
						Console.WriteLine("====================================");
						Console.WriteLine("Number of faces: " + regular.FaceCount);
						Console.WriteLine("Face flags: " + regular.FaceFlags);
						Console.WriteLine("Style: " + regular.StyleName);
						Console.WriteLine("Style flags: " + regular.StyleFlags);

						//render 'A'
						uint capitalA = FT.GetCharIndex(regular, 'D');
						FT.SetCharSize(regular, 0, 32 * 64, 0, 96);
						FT.LoadGlyph(regular, capitalA, LoadFlags.Default, LoadTarget.Normal);
						FT.RenderGlyph(regular.Glyph, RenderMode.Normal);

						SharpFont.Bitmap sBitmap = regular.Glyph.Bitmap;

						//copy data to managed memory
						//HACK currently scaling to a 32bpp RGBA image, don't do this.
						byte[] data = new byte[sBitmap.Rows * sBitmap.Width * 4];
						for (int i = 0; i < data.Length; i += 4)
						{
							data[i] = (byte)(Marshal.ReadByte(sBitmap.Buffer, (i / 4)));
							data[i + 1] = data[i];
							data[i + 2] = data[i];
							data[i + 3] = 255; //no transparency
						}

						//save a bitmap of the data.
						using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(sBitmap.Width, sBitmap.Rows, PixelFormat.Format32bppArgb))
						{
							BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
							Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
							bmp.UnlockBits(bmpData);

							System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(bmp);
							bmp2.Save("A.bmp");
						}
					}
				}
			}
			catch (FreeTypeException e)
			{
				Console.Write(e.Error.ToString());
			}

			Console.Read();
		}
	}
}
