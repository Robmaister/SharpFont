using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharpFont.Gdi
{
    public static class FTBitmapExtensions
    {
		//HACK these variables exist to reduce the cost of reflection at runtime.
		//Meant to be a temporary fix to https://github.com/Robmaister/SharpFont/issues/62
		//until libgdiplus gets patched.
		private static bool hasCheckedForMono;
		private static bool isRunningOnMono;
		private static System.Reflection.FieldInfo monoPaletteFlagsField;

		/// <summary>
		/// Copies the contents of the <see cref="FTBitmap"/> to a GDI+ <see cref="Bitmap"/>.
		/// </summary>
		/// <returns>A GDI+ <see cref="Bitmap"/> containing this bitmap's data.</returns>
		public static Bitmap ToGdipBitmap(this FTBitmap ftBitmap)
		{
			return ToGdipBitmap(ftBitmap, Color.Black);
		}

		/// <summary>
		/// Copies the contents of the <see cref="FTBitmap"/> to a GDI+ <see cref="Bitmap"/>.
		/// </summary>
		/// <param name="color">The color of the text.</param>
		/// <returns>A GDI+ <see cref="Bitmap"/> containing this bitmap's data with a transparent background.</returns>
		public static Bitmap ToGdipBitmap(this FTBitmap b, Color color)
		{
			if (b.IsDisposed)
				throw new ObjectDisposedException("FTBitmap", "Cannot access a disposed object.");

			if (b.Width == 0 || b.Rows == 0)
				throw new InvalidOperationException("Invalid image size - one or both dimensions are 0.");

			//TODO deal with negative pitch
			switch (b.PixelMode)
			{
				case PixelMode.Mono:
					{
						Bitmap bmp = new Bitmap(b.Width, b.Rows, PixelFormat.Format1bppIndexed);
						var locked = bmp.LockBits(new Rectangle(0, 0, b.Width, b.Rows), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);

						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, locked.Stride);

						bmp.UnlockBits(locked);

						ColorPalette palette = bmp.Palette;
						palette.Entries[0] = Color.FromArgb(0, color);
						palette.Entries[1] = Color.FromArgb(255, color);

						bmp.Palette = palette;
						return bmp;
					}

				case PixelMode.Gray4:
					{
						Bitmap bmp = new Bitmap(b.Width, b.Rows, PixelFormat.Format4bppIndexed);
						var locked = bmp.LockBits(new Rectangle(0, 0, b.Width, b.Rows), ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);

						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, locked.Stride);

						bmp.UnlockBits(locked);

						ColorPalette palette = bmp.Palette;
						for (int i = 0; i < palette.Entries.Length; i++)
						{
							float a = (i * 17) / 255f;
							palette.Entries[i] = Color.FromArgb(i * 17, (int)(color.R * a), (int)(color.G * a), (int)(color.B * a));
						}

						bmp.Palette = palette;
						return bmp;
					}

				case PixelMode.Gray:
					{
						Bitmap bmp = new Bitmap(b.Width, b.Rows, PixelFormat.Format8bppIndexed);
						var locked = bmp.LockBits(new Rectangle(0, 0, b.Width, b.Rows), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, locked.Stride);

						bmp.UnlockBits(locked);

						ColorPalette palette = bmp.Palette;
						for (int i = 0; i < palette.Entries.Length; i++)
						{
							float a = i / 255f;
							palette.Entries[i] = Color.FromArgb(i, (int)(color.R * a), (int)(color.G * a), (int)(color.B * a));
						}

						//HACK There's a bug in Mono's libgdiplus requiring the "PaletteHasAlpha" flag to be set for transparency to work properly
						//See https://github.com/Robmaister/SharpFont/issues/62
						if (!hasCheckedForMono)
						{
							hasCheckedForMono = true;
							isRunningOnMono = Type.GetType("Mono.Runtime") != null;
							if (isRunningOnMono)
							{
								monoPaletteFlagsField = typeof(ColorPalette).GetField("flags", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
							}
						}

						if (isRunningOnMono)
							monoPaletteFlagsField.SetValue(palette, palette.Flags | 1);

						bmp.Palette = palette;
						return bmp;
					}

				case PixelMode.Lcd:
					{
						//TODO apply color
						int bmpWidth = b.Width / 3;
						Bitmap bmp = new Bitmap(bmpWidth, b.Rows, PixelFormat.Format24bppRgb);
						var locked = bmp.LockBits(new Rectangle(0, 0, bmpWidth, b.Rows), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

						for (int i = 0; i < b.Rows; i++)
							Copy(b.Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, locked.Stride);

						bmp.UnlockBits(locked);

						return bmp;
					}
				/*case PixelMode.VerticalLcd:
				{
					int bmpHeight = b.Rows / 3;
					Bitmap bmp = new Bitmap(b.Width, bmpHeight, PixelFormat.Format24bppRgb);
					var locked = bmp.LockBits(new Rectangle(0, 0, b.Width, bmpHeight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
					for (int i = 0; i < bmpHeight; i++)
						PInvokeHelper.Copy(Buffer, i * b.Pitch, locked.Scan0, i * locked.Stride, b.Width);
					bmp.UnlockBits(locked);

					return bmp;
				}*/

				default:
					throw new InvalidOperationException("System.Drawing.Bitmap does not support this pixel mode.");
			}
		}

		/// <summary>
		/// A method to copy data from one pointer to another, byte by byte.
		/// </summary>
		/// <param name="source">The source pointer.</param>
		/// <param name="sourceOffset">An offset into the source buffer.</param>
		/// <param name="destination">The destination pointer.</param>
		/// <param name="destinationOffset">An offset into the destination buffer.</param>
		/// <param name="count">The number of bytes to copy.</param>
		static unsafe void Copy(IntPtr source, int sourceOffset, IntPtr destination, int destinationOffset, int count)
		{
			byte* src = (byte*)source + sourceOffset;
			byte* dst = (byte*)destination + destinationOffset;
			byte* end = dst + count;

			while (dst != end)
				*dst++ = *src++;
		}
	}
}
