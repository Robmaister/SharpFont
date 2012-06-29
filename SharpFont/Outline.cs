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
	/// This structure is used to describe an outline to the scan-line converter.
	/// </summary>
	/// <remarks>
	/// The B/W rasterizer only checks bit 2 in the ‘tags’ array for the first point of each contour. The drop-out mode
	/// as given with <see cref="OutlineFlags.IgnoreDropouts"/>, <see cref="OutlineFlags.SmartDropouts"/>, and
	/// <see cref="OutlineFlags.IncludeStubs"/> in ‘flags’ is then overridden.
	/// </remarks>
	public sealed class Outline : IDisposable
	{
		#region Fields

		private bool disposed;
		private bool duplicate;

		private IntPtr reference;
		private OutlineRec rec;

		private Library parentLibrary;
		private Memory parentMemory;

		#endregion

		#region Constructor

		[CLSCompliant(false)]
		public Outline(Library library, uint pointsCount, int contoursCount)
		{
			IntPtr reference;
			Error err = FT.FT_Outline_New(library.Reference, pointsCount, contoursCount, out reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			parentLibrary = library;
			parentLibrary.AddChildOutline(this);
		}

		[CLSCompliant(false)]
		public Outline(Memory memory, uint pointsCount, int contoursCount)
		{
			IntPtr reference;
			Error err = FT.FT_Outline_New_Internal(memory.Reference, pointsCount, contoursCount, out reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			parentMemory = memory; //TODO Should Memory be disposable as well?
		}

		internal Outline(IntPtr reference, Library parent)
		{
			Reference = reference;

			parentLibrary = parent;
			parentLibrary.AddChildOutline(this);
		}

		internal Outline(OutlineRec outlineInt)
		{
			this.rec = outlineInt;

			duplicate = true;
		}

		~Outline()
		{
			Dispose(false);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of contours in the outline.
		/// </summary>
		public short ContoursCount
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("ContoursCount", "Cannot access a disposed object.");

				return rec.n_contours;
			}
		}

		/// <summary>
		/// Gets the number of points in the outline.
		/// </summary>
		public short PointsCount
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("PointsCount", "Cannot access a disposed object.");

				return rec.n_points;
			}
		}

		/// <summary>
		/// Gets a pointer to an array of ‘PointsCount’ <see cref="FTVector"/> elements, giving the outline's point
		/// coordinates.
		/// </summary>
		public FTVector[] Points
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Points", "Cannot access a disposed object.");

				int count = PointsCount;

				if (count == 0)
					return null;

				FTVector[] points = new FTVector[count];
				IntPtr array = rec.points;

				for (int i = 0; i < count; i++)
				{
					points[i] = new FTVector(new IntPtr(array.ToInt64() + IntPtr.Size * i));
				}

				return points;
			}
		}

		/// <summary><para>
		/// Gets a pointer to an array of ‘PointsCount’ chars, giving each outline point's type.
		/// </para><para>
		/// If bit 0 is unset, the point is ‘off’ the curve, i.e., a Bézier control point, while it is ‘on’ if set.
		/// </para><para>
		/// Bit 1 is meaningful for ‘off’ points only. If set, it indicates a third-order Bézier arc control point; and
		/// a second-order control point if unset.
		/// </para><para>
		/// If bit 2 is set, bits 5-7 contain the drop-out mode (as defined in the OpenType specification; the value is
		/// the same as the argument to the SCANMODE instruction).
		/// </para><para>
		/// Bits 3 and 4 are reserved for internal purposes.
		/// </para></summary>
		public byte[] Tags
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Tags", "Cannot access a disposed object.");

				int count = PointsCount;

				if (count == 0)
					return null;

				byte[] tags = new byte[count];
				IntPtr array = rec.tags;

				for (int i = 0; i < count; i++)
				{
					tags[i] = Marshal.ReadByte(array, IntPtr.Size * i);
				}

				return tags;
			}
		}

		/// <summary>
		/// Gets an array of ‘ContoursCount’ shorts, giving the end point of each contour within the outline. For
		/// example, the first contour is defined by the points ‘0’ to ‘Contours[0]’, the second one is defined by the
		/// points ‘Contours[0]+1’ to ‘Contours[1]’, etc.
		/// </summary>
		public short[] Contours
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Contours", "Cannot access a disposed object.");

				int count = ContoursCount;

				if (count == 0)
					return null;

				short[] contours = new short[count];
				IntPtr array = rec.contours;

				for (int i = 0; i < count; i++)
				{
					contours[i] = Marshal.ReadInt16(array, IntPtr.Size * i);
				}

				return contours;
			}
		}

		/// <summary>
		/// Gets a set of bit flags used to characterize the outline and give hints to the scan-converter and hinter on
		/// how to convert/grid-fit it.
		/// </summary>
		/// <see cref="OutlineFlags"/>
		public OutlineFlags Flags
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Flags", "Cannot access a disposed object.");

				return rec.flags;
			}
		}

		internal IntPtr Reference
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				return reference;
			}

			set
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				reference = value;
				rec = PInvokeHelper.PtrToStructure<OutlineRec>(reference);
			}
		}

		#endregion

		#region Methods

		public void Copy(Outline target)
		{
			IntPtr targetRef = target.Reference;
			Error err = FT.FT_Outline_Copy(reference, ref targetRef);
			target.Reference = reference;

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void Translate(int offsetX, int offsetY)
		{
			FT.FT_Outline_Translate(reference, offsetX, offsetY);
		}

		public void Transform(FTMatrix matrix)
		{
			FT.FT_Outline_Transform(reference, ref matrix);
		}

		public void Embolden(int strength)
		{
			Error err = FT.FT_Outline_Embolden(reference, strength);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void EmboldenXY(int strengthX, int strengthY)
		{
			Error err = FT.FT_Outline_EmboldenXY(reference, strengthX, strengthY);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void Reverse()
		{
			FT.FT_Outline_Reverse(reference);
		}

		public void Check()
		{
			Error err = FT.FT_Outline_Check(reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public BBox GetBBox()
		{
			IntPtr bboxRef;
			Error err = FT.FT_Outline_Get_BBox(reference, out bboxRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new BBox(bboxRef);
		}

		public void Decompose(OutlineFuncs funcInterface, IntPtr user)
		{
			//TODO cleanup/move to the outlinefuncs class
			IntPtr funcInterfaceRef = Marshal.AllocHGlobal(OutlineFuncsRec.SizeInBytes);
			Marshal.WriteIntPtr(funcInterfaceRef, Marshal.GetFunctionPointerForDelegate(funcInterface.MoveFunction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "line_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.LineFuction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "conic_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.ConicFunction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "cubic_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.CubicFunction));

			Marshal.WriteInt32(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "shift"), funcInterface.Shift);
			Marshal.WriteInt32(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "delta"), funcInterface.Delta);

			Error err = FT.FT_Outline_Decompose(reference, funcInterfaceRef, user);

			Marshal.FreeHGlobal(funcInterfaceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public BBox GetCBox()
		{
			IntPtr cboxRef;
			FT.FT_Outline_Get_CBox(reference, out cboxRef);

			return new BBox(cboxRef);
		}

		public void GetBitmap(FTBitmap bitmap)
		{
			Error err = FT.FT_Outline_Get_Bitmap(parentLibrary.Reference, reference, bitmap.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void GetBitmap(Library library, FTBitmap bitmap)
		{
			Error err = FT.FT_Outline_Get_Bitmap(library.Reference, reference, bitmap.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void Render(RasterParams parameters)
		{
			Error err = FT.FT_Outline_Render(parentLibrary.Reference, reference, parameters.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public void Render(Library library, RasterParams parameters)
		{
			Error err = FT.FT_Outline_Render(library.Reference, reference, parameters.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		public Orientation GetOrientation()
		{
			return FT.FT_Outline_Get_Orientation(reference);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;

				if (!duplicate)
				{
					Error err;
					if (parentLibrary != null)
						err = FT.FT_Outline_Done(parentLibrary.Reference, reference);
					else
						err = FT.FT_Outline_Done_Internal(parentMemory.Reference, reference);

					if (err != Error.Ok)
						throw new FreeTypeException(err);
				}

				reference = IntPtr.Zero;
				rec = default(OutlineRec);
			}
		}

		#endregion
	}
}
