#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

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
using System.Runtime.InteropServices;
using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// This structure is used to describe an outline to the scan-line
	/// converter.
	/// </summary>
	/// <remarks>
	/// The B/W rasterizer only checks bit 2 in the ‘tags’ array for the first
	/// point of each contour. The drop-out mode as given with
	/// FT_OUTLINE_IGNORE_DROPOUTS, FT_OUTLINE_SMART_DROPOUTS, and
	/// FT_OUTLINE_INCLUDE_STUBS in ‘flags’ is then overridden.
	/// </remarks>
	public sealed class Outline
	{
		internal IntPtr reference;
		internal OutlineInternal outlineInternal;

		internal Outline(IntPtr reference)
		{
			this.reference = reference;
			this.outlineInternal = (OutlineInternal)Marshal.PtrToStructure(reference, typeof(OutlineInternal));
		}

		internal Outline(OutlineInternal outlineInt)
		{
			this.outlineInternal = outlineInt;
		}

		/*/// <summary>
		/// Gets the size of the class, in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return 8 + IntPtr.Size * 3;
			}
		}*/

		/// <summary>
		/// The number of contours in the outline.
		/// </summary>
		public short ContoursCount
		{
			get
			{
				return outlineInternal.n_contours;
			}
		}

		/// <summary>
		/// The number of points in the outline.
		/// </summary>
		public short PointsCount
		{
			get
			{
				return outlineInternal.n_points;
			}
		}

		/// <summary>
		/// A pointer to an array of ‘PointsCount’ FT_Vector elements, giving
		/// the outline's point coordinates.
		/// </summary>
		public Vector2i[] Points
		{
			get
			{
				int count = PointsCount;

				if (count == 0)
					return null;

				Vector2i[] points = new Vector2i[count];
				IntPtr array = outlineInternal.points;

				for (int i = 0; i < count; i++)
				{
					points[i] = new Vector2i(new IntPtr(array.ToInt64() + IntPtr.Size * i));
				}

				return points;
			}
		}

		/// <summary>
		/// A pointer to an array of ‘PointsCount’ chars, giving each outline
		/// point's type.
		/// 
		/// If bit 0 is unset, the point is ‘off’ the curve, i.e., a Bézier
		/// control point, while it is ‘on’ if set.
		/// 
		/// Bit 1 is meaningful for ‘off’ points only. If set, it indicates a
		/// third-order Bézier arc control point; and a second-order control
		/// point if unset.
		/// 
		/// If bit 2 is set, bits 5-7 contain the drop-out mode (as defined in
		/// the OpenType specification; the value is the same as the argument
		/// to the SCANMODE instruction).
		/// 
		/// Bits 3 and 4 are reserved for internal purposes.
		/// </summary>
		public byte[] Tags
		{
			get
			{
				int count = PointsCount;

				if (count == 0)
					return null;

				byte[] tags = new byte[count];
				IntPtr array = outlineInternal.tags;

				for (int i = 0; i < count; i++)
				{
					tags[i] = Marshal.ReadByte(array, IntPtr.Size * i);
				}

				return tags;
			}
		}

		/// <summary>
		/// An array of ‘ContoursCount’ shorts, giving the end point of each
		/// contour within the outline. For example, the first contour is
		/// defined by the points ‘0’ to ‘Contours[0]’, the second one is
		/// defined by the points ‘Contours[0]+1’ to ‘Contours[1]’, etc.
		/// </summary>
		public short[] Contours
		{
			get
			{
				int count = ContoursCount;

				if (count == 0)
					return null;

				short[] contours = new short[count];
				IntPtr array = outlineInternal.contours;

				for (int i = 0; i < count; i++)
				{
					contours[i] = Marshal.ReadInt16(array, IntPtr.Size * i);
				}

				return contours;
			}
		}

		/// <summary>
		/// A set of bit flags used to characterize the outline and give hints
		/// to the scan-converter and hinter on how to convert/grid-fit it. See
		/// FT_OUTLINE_FLAGS.
		/// </summary>
		public OutlineFlags Flags
		{
			get
			{
				return outlineInternal.flags;
			}
		}
	}
}
