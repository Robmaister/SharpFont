#region MIT License
/*Copyright (c) 2016 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

using SharpFont;
using SharpFont.Gdi;

namespace Examples
{
	internal class FontService : IDisposable
	{
		private Library lib;

		#region Properties

		internal Face FontFace { get { return _fontFace; } set { SetFont(value); } }
		private Face _fontFace;

		internal float Size { get { return _size; } set { SetSize(value); } }
		private float _size;

		internal FontFormatCollection SupportedFormats { get; private set; }

		#endregion // Properties

		#region Constructor

		/// <summary>
		/// If multithreading, each thread should have its own FontService.
		/// </summary>
		internal FontService()
		{
			lib = new Library();
			_size = 8.25f;
			SupportedFormats = new FontFormatCollection();
			AddFormat("TrueType", "ttf");
			AddFormat("OpenType", "otf");
			// Not so sure about these...
			//AddFormat("TrueType Collection", "ttc");
			//AddFormat("Type 1", "pfa"); // pfb?
			//AddFormat("PostScript", "pfm"); // ext?
			//AddFormat("FNT", "fnt");
			//AddFormat("X11 PCF", "pcf");
			//AddFormat("BDF", "bdf");
			//AddFormat("Type 42", "");
		}

		private void AddFormat(string name, string ext)
		{
			SupportedFormats.Add(name, ext);
		}

		#endregion

		#region Setters

		internal void SetFont(Face face)
		{
			_fontFace = face;
			SetSize(this.Size);
		}

		internal void SetFont(string filename)
		{
			FontFace = new Face(lib, filename);
			SetSize(this.Size);
		}

		internal void SetSize(float size)
		{
			_size = size;
			if (FontFace != null)
				FontFace.SetCharSize(0, size, 0, 96);
		}

		#endregion // Setters

		#region FileEnumeration

		internal IEnumerable<FileInfo> GetFontFiles(DirectoryInfo folder, bool recurse)
		{
			var files = new List<FileInfo>();
			var option = recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			foreach (var file in folder.GetFiles("*.*", option))
			{
				if (SupportedFormats.ContainsExt(file.Extension))
				{
					//yield return file;
					files.Add(file);
				}
			}
			return files;
		}

		#endregion // FileEnumeration

		#region RenderString

		/// <summary>
		/// Render the string into a bitmap with <see cref="SystemColors.ControlText"/> text color and a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		internal virtual Bitmap RenderString(string text)
		{
			try
			{
				return RenderString(this.lib, this.FontFace, text, SystemColors.ControlText, Color.Transparent);
			}
			catch { }
			return null;
		}

		/// <summary>
		/// Render the string into a bitmap with a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		/// <param name="foreColor">The color of the text.</param>
		/// <returns></returns>
		internal virtual Bitmap RenderString(string text, Color foreColor)
		{
			return RenderString(this.lib, this.FontFace, text, foreColor, Color.Transparent);
		}

		/// <summary>
		/// Render the string into a bitmap with an opaque background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		/// <param name="foreColor">The color of the text.</param>
		/// <param name="backColor">The color of the background behind the text.</param>
		/// <returns></returns>
		internal virtual Bitmap RenderString(string text, Color foreColor, Color backColor)
		{
			return RenderString(this.lib, this.FontFace, text, foreColor, backColor);
		}

		internal static Bitmap RenderString(Library library, Face face, string text, Color foreColor, Color backColor)
		{
			var measuredChars = new List<DebugChar>();
			var renderedChars = new List<DebugChar>();
			float penX = 0, penY = 0;
			float stringWidth = 0; // the measured width of the string
			float stringHeight = 0; // the measured height of the string
			float overrun = 0;
			float underrun = 0;
			float kern = 0;
			int spacingError = 0;
			bool trackingUnderrun = true;
			int rightEdge = 0; // tracking rendered right side for debugging

			// Bottom and top are both positive for simplicity.
			// Drawing in .Net has 0,0 at the top left corner, with positive X to the right
			// and positive Y downward.
			// Glyph metrics have an origin typically on the left side and at baseline
			// of the visual data, but can draw parts of the glyph in any quadrant, and
			// even move the origin (via kerning).
			float top = 0, bottom = 0;

			// Measure the size of the string before rendering it. We need to do this so
			// we can create the proper size of bitmap (canvas) to draw the characters on.
			for (int i = 0; i < text.Length; i++)
			{
				#region Load character
				char c = text[i];

				// Look up the glyph index for this character.
				uint glyphIndex = face.GetCharIndex(c);

				// Load the glyph into the font's glyph slot. There is usually only one slot in the font.
				face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

				// Refer to the diagram entitled "Glyph Metrics" at http://www.freetype.org/freetype2/docs/tutorial/step2.html.
				// There is also a glyph diagram included in this example (glyph-dims.svg).
				// The metrics below are for the glyph loaded in the slot.
				float gAdvanceX = (float)face.Glyph.Advance.X; // same as the advance in metrics
				float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
				float gWidth = face.Glyph.Metrics.Width.ToSingle();
				var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
				#endregion
				#region Underrun
				// Negative bearing would cause clipping of the first character
				// at the left boundary, if not accounted for.
				// A positive bearing would cause empty space.
				underrun += -(gBearingX);
				if (stringWidth == 0)
					stringWidth += underrun;
				if (trackingUnderrun)
					rc.Underrun = underrun;
				if (trackingUnderrun && underrun <= 0)
				{
					underrun = 0;
					trackingUnderrun = false;
				}
				#endregion
				#region Overrun
				// Accumulate overrun, which coould cause clipping at the right side of characters near
				// the end of the string (typically affects fonts with slanted characters)
				if (gBearingX + gWidth > 0 || gAdvanceX > 0)
				{
					overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
					if (overrun <= 0) overrun = 0;
				}
				overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
				// On the last character, apply whatever overrun we have to the overall width.
				// Positive overrun prevents clipping, negative overrun prevents extra space.
				if (i == text.Length - 1)
					stringWidth += overrun;
				rc.Overrun = overrun; // accumulating (per above)
				#endregion

				#region Top/Bottom
				// If this character goes higher or lower than any previous character, adjust
				// the overall height of the bitmap.
				float glyphTop = (float)face.Glyph.Metrics.HorizontalBearingY;
				float glyphBottom = (float)(face.Glyph.Metrics.Height - face.Glyph.Metrics.HorizontalBearingY);
				if (glyphTop > top)
					top = glyphTop;
				if (glyphBottom > bottom)
					bottom = glyphBottom;
				#endregion

				// Accumulate the distance between the origin of each character (simple width).
				stringWidth += gAdvanceX;
				rc.RightEdge = stringWidth;
				measuredChars.Add(rc);

				#region Kerning (for NEXT character)
				// Calculate kern for the NEXT character (if any)
				// The kern value adjusts the origin of the next character (positive or negative).
				if (face.HasKerning && i < text.Length - 1)
				{
					char cNext = text[i + 1];
					kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
					// sanity check for some fonts that have kern way out of whack
					if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
						kern = 0;
					rc.Kern = kern;
					stringWidth += kern;
				}

				#endregion
			}

			stringHeight = top + bottom;

			// If any dimension is 0, we can't create a bitmap
			if (stringWidth == 0 || stringHeight == 0)
				return null;

			// Create a new bitmap that fits the string.
			Bitmap bmp = new Bitmap((int)Math.Ceiling(stringWidth), (int)Math.Ceiling(stringHeight));
			trackingUnderrun = true;
			underrun = 0;
			overrun = 0;
			stringWidth = 0;
			using (var g = Graphics.FromImage(bmp))
			{
				#region Set up graphics
				// HighQuality and GammaCorrected both specify gamma correction be applied (2.2 in sRGB)
				// https://msdn.microsoft.com/en-us/library/windows/desktop/ms534094(v=vs.85).aspx
				g.CompositingQuality = CompositingQuality.HighQuality;
				// HighQuality and AntiAlias both specify antialiasing
				g.SmoothingMode = SmoothingMode.HighQuality;
				// If a background color is specified, blend over it.
				g.CompositingMode = CompositingMode.SourceOver;

				g.Clear(backColor);
				#endregion

				// Draw the string into the bitmap.
				// A lot of this is a repeat of the measuring steps, but this time we have
				// an actual bitmap to work with (both canvas and bitmaps in the glyph slot).
				for (int i = 0; i < text.Length; i++)
				{
					#region Load character
					char c = text[i];

					// Same as when we were measuring, except RenderGlyph() causes the glyph data
					// to be converted to a bitmap.
					uint glyphIndex = face.GetCharIndex(c);
					face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
					face.Glyph.RenderGlyph(RenderMode.Normal);
					FTBitmap ftbmp = face.Glyph.Bitmap;

					float gAdvanceX = (float)face.Glyph.Advance.X;
					float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
					float gWidth = (float)face.Glyph.Metrics.Width;

					var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
					#endregion
					#region Underrun
					// Underrun
					underrun += -(gBearingX);
					if (penX == 0)
						penX += underrun;
					if (trackingUnderrun)
						rc.Underrun = underrun;
					if (trackingUnderrun && underrun <= 0)
					{
						underrun = 0;
						trackingUnderrun = false;
					}
					#endregion
					#region Draw glyph
					// Whitespace characters sometimes have a bitmap of zero size, but a non-zero advance.
					// We can't draw a 0-size bitmap, but the pen position will still get advanced (below).
					if ((ftbmp.Width > 0 && ftbmp.Rows > 0))
					{
						// Get a bitmap that .Net can draw (GDI+ in this case).
						Bitmap cBmp = ftbmp.ToGdipBitmap(foreColor);
						rc.Width = cBmp.Width;
						rc.BearingX = face.Glyph.BitmapLeft;
						int x = (int)Math.Round(penX + face.Glyph.BitmapLeft);
						int y = (int)Math.Round(penY + top - (float)face.Glyph.Metrics.HorizontalBearingY);
						//Not using g.DrawImage because some characters come out blurry/clipped. (Is this still true?)
						g.DrawImageUnscaled(cBmp, x, y);
						rc.Overrun = face.Glyph.BitmapLeft + cBmp.Width - gAdvanceX;
						// Check if we are aligned properly on the right edge (for debugging)
						rightEdge = Math.Max(rightEdge, x + cBmp.Width);
						spacingError = bmp.Width - rightEdge;
					}
					else
					{
						rightEdge = (int)(penX + gAdvanceX);
						spacingError = bmp.Width - rightEdge;
					}
					#endregion

					#region Overrun
					if (gBearingX + gWidth > 0 || gAdvanceX > 0)
					{
						overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
						if (overrun <= 0) overrun = 0;
					}
					overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
					if (i == text.Length - 1) penX += overrun;
					rc.Overrun = overrun;
					#endregion

					// Advance pen positions for drawing the next character.
					penX += (float)face.Glyph.Advance.X; // same as Metrics.HorizontalAdvance?
					penY += (float)face.Glyph.Advance.Y;

					rc.RightEdge = penX;
					spacingError = bmp.Width - (int)Math.Round(rc.RightEdge);
					renderedChars.Add(rc);

					#region Kerning (for NEXT character)
					// Adjust for kerning between this character and the next.
					if (face.HasKerning && i < text.Length - 1)
					{
						char cNext = text[i + 1];
						kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
						if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
							kern = 0;
						rc.Kern = kern;
						penX += (float)kern;
					}
					#endregion

				}

			}
			bool printedHeader = false;
			if (spacingError != 0)
			{
				for (int i = 0; i < renderedChars.Count; i++)
				{
					//if (measuredChars[i].RightEdge != renderedChars[i].RightEdge)
					//{
					if (!printedHeader)
						DebugChar.PrintHeader();
					printedHeader = true;
					Debug.Print(measuredChars[i].ToString());
					Debug.Print(renderedChars[i].ToString());
					//}
				}
				string msg = string.Format("Right edge: {0,3} ({1}) {2}",
					spacingError,
					spacingError == 0 ? "perfect" : spacingError > 0 ? "space  " : "clipped",
					face.FamilyName);
				System.Diagnostics.Debug.Print(msg);
				//throw new ApplicationException(msg);
			}
			return bmp;
		}

		#endregion // RenderString

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (this.FontFace != null && !FontFace.IsDisposed)
						try
						{
							FontFace.Dispose();
						}
						catch { }
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~FontService() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

		#region class DebugChar

		private class DebugChar
		{
			public char Char { get; set; }
			public float AdvanceX { get; set; }
			public float BearingX { get; set; }
			public float Width { get; set; }
			public float Underrun { get; set; }
			public float Overrun { get; set; }
			public float Kern { get; set; }
			public float RightEdge { get; set; }
			internal DebugChar(char c, float advanceX, float bearingX, float width)
			{
				this.Char = c; this.AdvanceX = advanceX; this.BearingX = bearingX; this.Width = width;
			}

			public override string ToString()
			{
				return string.Format("'{0}' {1,5:F0} {2,5:F0} {3,5:F0} {4,5:F0} {5,5:F0} {6,5:F0} {7,5:F0}",
					this.Char, this.AdvanceX, this.BearingX, this.Width, this.Underrun, this.Overrun,
					this.Kern, this.RightEdge);
			}
			public static void PrintHeader()
			{
				Debug.Print("    {1,5} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5}",
					"", "adv", "bearing", "wid", "undrn", "ovrrn", "kern", "redge");
			}
		}

		#endregion

	}

}
