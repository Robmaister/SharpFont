#region MIT License
/*Copyright (c) 2012-2016 Robert Rouhani <robert.rouhani@gmail.com>

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
using System.Windows.Forms;

using SharpFont;

namespace Examples
{
	class Program
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetDllDirectory(string path);

		[STAThread]
		public static void Main(string[] args)
		{
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

			var form = new ExampleForm();
			Application.EnableVisualStyles();
			Application.Run(form);
		}

		public static Bitmap RenderString(Library library, Face face, string text)
		{
			float penX = 0, penY = 0;
			float width = 0;
			float height = 0;

			//both bottom and top are positive for simplicity
			float top = 0, bottom = 0;

			//measure the size of the string before rendering it, requirement of Bitmap.
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = face.GetCharIndex(c);
				face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

				width += (float)face.Glyph.Advance.X;

				if (face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					width += (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
				}

				float glyphTop = (float)face.Glyph.Metrics.HorizontalBearingY;
				float glyphBottom = (float)(face.Glyph.Metrics.Height - face.Glyph.Metrics.HorizontalBearingY);

				if (glyphTop > top)
					top = glyphTop;
				if (glyphBottom > bottom)
					bottom = glyphBottom;
			}

			height = top + bottom;

			if (width == 0 || height == 0)
				return null;

			//create a new bitmap that fits the string.
			Bitmap bmp = new Bitmap((int)Math.Ceiling(width), (int)Math.Ceiling(height));
			Graphics g = Graphics.FromImage(bmp);
			g.Clear(SystemColors.Control);

			//draw the string
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];

				uint glyphIndex = face.GetCharIndex(c);
				face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
				face.Glyph.RenderGlyph(RenderMode.Normal);

				if (c == ' ')
				{
					penX += (float)face.Glyph.Advance.X;

					if (face.HasKerning && i < text.Length - 1)
					{
						char cNext = text[i + 1];
						width += (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
					}

					penY += (float)face.Glyph.Advance.Y;
				}
				else
				{
					//FTBitmap ftbmp = face.Glyph.Bitmap.Copy(library);
					FTBitmap ftbmp = face.Glyph.Bitmap;
					Bitmap cBmp = ftbmp.ToGdipBitmap(Color.Black);

					//Not using g.DrawImage because some characters come out blurry/clipped.
					g.DrawImageUnscaled(cBmp, (int)Math.Round(penX + face.Glyph.BitmapLeft), (int)Math.Round(penY + (top - (float)face.Glyph.Metrics.HorizontalBearingY)));

					penX += (float)face.Glyph.Metrics.HorizontalAdvance;
					penY += (float)face.Glyph.Advance.Y;

					if (face.HasKerning && i < text.Length - 1)
					{
						char cNext = text[i + 1];
						var kern = face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default);
						penX += (float)kern.X;
					}
				}
			}

			g.Dispose();
			return bmp;
		}
	}
}
