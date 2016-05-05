using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Examples
{
	internal class FontService
	{
		private Library lib;

		#region Properties

		internal Face FontFace { get { return _fontFace; } set { SetFont(value); } }
		private Face _fontFace;

		internal float Size { get { return _size; } set { SetSize(value); } }
		private float _size;

		#endregion // Properties

		#region Constructor

		/// <summary>
		/// If multithreading, each thread should have its own FontService.
		/// </summary>
		internal FontService()
		{
			lib = new Library();
			_size = 8.25f;
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

		#region RenderString

		/// <summary>
		/// Render the string into a bitmap with <see cref="SystemColors.ControlText"/> text color and a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		internal Bitmap RenderString(string text)
		{
			return RenderString(this.lib, this.FontFace, text, SystemColors.ControlText, Color.Transparent);
		}

		/// <summary>
		/// Render the string into a bitmap with a transparent background.
		/// </summary>
		/// <param name="text">The string to render.</param>
		/// <param name="foreColor">The color of the text.</param>
		/// <returns></returns>
		internal Bitmap RenderString(string text, Color foreColor)
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
		internal Bitmap RenderString(string text, Color foreColor, Color backColor)
		{
			return RenderString(this.lib, this.FontFace, text, foreColor, backColor);
		}

		internal static Bitmap RenderString(Library library, Face face, string text, Color foreColor, Color backColor)
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
			g.Clear(backColor);

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
					Bitmap cBmp = ftbmp.ToGdipBitmap(foreColor);

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

		#endregion // RenderString

	}

}
