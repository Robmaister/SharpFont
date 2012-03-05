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
using SharpFont.Internal;

namespace SharpFont
{
	public static partial class FT
	{
		#region Computations

		/// <summary>
		/// The angle pi expressed in FT_Angle units.
		/// </summary>
		public const int AnglePI = 180 << 16;

		/// <summary>
		/// The angle 2*pi expressed in FT_Angle units.
		/// </summary>
		public const int Angle2PI = AnglePI * 2;

		/// <summary>
		/// The angle pi/2 expressed in FT_Angle units.
		/// </summary>
		public const int AnglePI2 = AnglePI / 2;

		/// <summary>
		/// The angle pi/4 expressed in FT_Angle units.
		/// </summary>
		public const int AnglePI4 = AnglePI / 4;

		/// <summary><para>
		/// A very simple function used to perform the computation ‘(a*b)/c’
		/// with maximal accuracy (it uses a 64-bit intermediate integer
		/// whenever necessary).
		/// </para><para>
		/// This function isn't necessarily as fast as some processor specific
		/// operations, but is at least completely portable.
		/// </para></summary>
		/// <param name="a">The first multiplier.</param>
		/// <param name="b">The second multiplier.</param>
		/// <param name="c">The divisor.</param>
		/// <returns>The result of ‘(a*b)/c’. This function never traps when trying to divide by zero; it simply returns ‘MaxInt’ or ‘MinInt’ depending on the signs of ‘a’ and ‘b’.</returns>
		public static int MulDiv(int a, int b, int c)
		{
			return FT_MulDiv(a, b, c);
		}

		/// <summary>
		/// A very simple function used to perform the computation
		/// ‘(a*b)/0x10000’ with maximal accuracy. Most of the time this is
		/// used to multiply a given value by a 16.16 fixed float factor.
		/// </summary>
		/// <remarks><para>
		/// This function has been optimized for the case where the absolute
		/// value of ‘a’ is less than 2048, and ‘b’ is a 16.16 scaling factor.
		/// As this happens mainly when scaling from notional units to
		/// fractional pixels in FreeType, it resulted in noticeable speed
		/// improvements between versions 2.x and 1.x.
		/// </para><para>
		/// As a conclusion, always try to place a 16.16 factor as the second
		/// argument of this function; this can make a great difference.
		/// </para></remarks>
		/// <param name="a">The first multiplier.</param>
		/// <param name="b">The second multiplier. Use a 16.16 factor here whenever possible (see note below).</param>
		/// <returns>The result of ‘(a*b)/0x10000’.</returns>
		public static int MulFix(int a, int b)
		{
			return FT_MulFix(a, b);
		}

		/// <summary>
		/// A very simple function used to perform the computation
		/// ‘(a*0x10000)/b’ with maximal accuracy. Most of the time, this is
		/// used to divide a given value by a 16.16 fixed float factor.
		/// </summary>
		/// <remarks>
		/// The optimization for <see cref="DivFix"/> is simple: If (a &lt;&lt;
		/// 16) fits in 32 bits, then the division is computed directly.
		/// Otherwise, we use a specialized version of <see cref="MulDiv"/>.
		/// </remarks>
		/// <param name="a">The first multiplier.</param>
		/// <param name="b">The second multiplier. Use a 16.16 factor here whenever possible (see note below).</param>
		/// <returns>The result of ‘(a*0x10000)/b’.</returns>
		public static int DivFix(int a, int b)
		{
			return FT_DivFix(a, b);
		}

		/// <summary>
		/// A very simple function used to round a 16.16 fixed number.
		/// </summary>
		/// <param name="a">The number to be rounded.</param>
		/// <returns>The result of ‘(a + 0x8000) &amp; -0x10000’.</returns>
		public static int RoundFix(int a)
		{
			return FT_RoundFix(a);
		}

		/// <summary>
		/// A very simple function used to compute the ceiling function of a
		/// 16.16 fixed number.
		/// </summary>
		/// <param name="a">The number for which the ceiling function is to be computed.</param>
		/// <returns>The result of ‘(a + 0x10000 - 1) &amp; -0x10000’.</returns>
		public static int CeilFix(int a)
		{
			return FT_CeilFix(a);
		}

		/// <summary>
		/// A very simple function used to compute the floor function of a
		/// 16.16 fixed number.
		/// </summary>
		/// <param name="a">The number for which the floor function is to be computed.</param>
		/// <returns>The result of ‘a &amp; -0x10000’.</returns>
		public static int FloorFix(int a)
		{
			return FT_FloorFix(a);
		}

		/// <summary>
		/// Transform a single vector through a 2x2 matrix.
		/// </summary>
		/// <remarks>
		/// The result is undefined if either ‘vector’ or ‘matrix’ is invalid.
		/// </remarks>
		/// <param name="vec">The target vector to transform.</param>
		/// <param name="matrix">A pointer to the source 2x2 matrix.</param>
		public static void VectorTransform(Vector2i vec, Matrix2i matrix)
		{
			FT_Vector_Transform(ref vec.reference, matrix.reference);

			//update the vector record.
			vec.rec = PInvokeHelper.PtrToStructure<VectorRec>(vec.reference);
		}

		/// <summary>
		/// Perform the matrix operation ‘b = a*b’.
		/// </summary>
		/// <remarks>
		/// The result is undefined if either ‘a’ or ‘b’ is zero.
		/// </remarks>
		/// <param name="a">A pointer to matrix ‘a’.</param>
		/// <param name="b">A pointer to matrix ‘b’.</param>
		public static void MatrixMultiply(Matrix2i a, Matrix2i b)
		{
			FT_Matrix_Multiply(a.reference, ref b.reference);

			//update the matrix record.
			b.rec = PInvokeHelper.PtrToStructure<MatrixRec>(b.reference);
		}

		/// <summary>
		/// Invert a 2x2 matrix. Return an error if it can't be inverted.
		/// </summary>
		/// <param name="matrix">A pointer to the target matrix. Remains untouched in case of error.</param>
		public static void MatrixInvert(Matrix2i matrix)
		{
			Error err = FT_Matrix_Invert(ref matrix.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			matrix.rec = PInvokeHelper.PtrToStructure<MatrixRec>(matrix.reference);
		}

		/// <summary>
		/// Return the sinus of a given angle in fixed point format.
		/// </summary>
		/// <remarks>
		/// If you need both the sinus and cosinus for a given angle, use the
		/// function <see cref="VectorUnit"/>.
		/// </remarks>
		/// <param name="angle">The input angle.</param>
		/// <returns>The sinus value.</returns>
		public static int Sin(int angle)
		{
			return FT_Sin(angle);
		}

		/// <summary>
		/// Return the cosinus of a given angle in fixed point format.
		/// </summary>
		/// <remarks>
		/// If you need both the sinus and cosinus for a given angle, use the
		/// function <see cref="VectorUnit"/>.
		/// </remarks>
		/// <param name="angle">The input angle.</param>
		/// <returns>The cosinus value.</returns>
		public static int Cos(int angle)
		{
			return FT_Cos(angle);
		}

		/// <summary>
		/// Return the tangent of a given angle in fixed point format.
		/// </summary>
		/// <param name="angle">The input angle.</param>
		/// <returns>The tangent value.</returns>
		public static int Tan(int angle)
		{
			return FT_Tan(angle);
		}

		/// <summary>
		/// Return the arc-tangent corresponding to a given vector (x,y) in the
		/// 2d plane.
		/// </summary>
		/// <param name="x">The horizontal vector coordinate.</param>
		/// <param name="y">The vertical vector coordinate.</param>
		/// <returns>The arc-tangent value (i.e. angle).</returns>
		public static int Atan2(int x, int y)
		{
			return FT_Atan2(x, y);
		}

		/// <summary>
		/// Return the difference between two angles. The result is always
		/// constrained to the [-PI..PI] interval.
		/// </summary>
		/// <param name="angle1">First angle.</param>
		/// <param name="angle2">Second angle.</param>
		/// <returns>Constrained value of ‘value2-value1’.</returns>
		public static int AngleDiff(int angle1, int angle2)
		{
			return FT_Angle_Diff(angle1, angle2);
		}
		
		/// <summary><para>
		/// Return the unit vector corresponding to a given angle. After the
		/// call, the value of ‘vec.x’ will be ‘sin(angle)’, and the value of
		/// ‘vec.y’ will be ‘cos(angle)’.
		/// </para><para>
		/// This function is useful to retrieve both the sinus and cosinus of a
		/// given angle quickly.
		/// </para></summary>
		/// <param name="angle">The address of angle.</param>
		/// <returns>The address of target vector.</returns>
		public static Vector2i VectorUnit(int angle)
		{
			IntPtr vecRef;
			FT_Vector_Unit(out vecRef, angle);

			return new Vector2i(vecRef);
		}

		/// <summary>
		/// Rotate a vector by a given angle.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <param name="angle">The address of angle.</param>
		public static void VectorRotate(Vector2i vec, int angle)
		{
			FT_Vector_Rotate(ref vec.reference, angle);
		}

		/// <summary>
		/// Return the length of a given vector.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <returns>The vector length, expressed in the same units that the original vector coordinates.</returns>
		public static int VectorLength(Vector2i vec)
		{
			return FT_Vector_Length(vec.reference);
		}

		/// <summary>
		/// Compute both the length and angle of a given vector.
		/// </summary>
		/// <param name="vec">The address of source vector.</param>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		public static void VectorPolarize(Vector2i vec, out int length, out int angle)
		{
			FT_Vector_Polarize(vec.reference, out length, out angle);
		}

		/// <summary>
		/// Compute vector coordinates from a length and angle.
		/// </summary>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		/// <returns>The address of source vector.</returns>
		public static Vector2i VectorFromPolar(int length, int angle)
		{
			IntPtr vecRef;
			FT_Vector_From_Polar(out vecRef, length, angle);

			return new Vector2i(vecRef);
		}

		#endregion

		#region List Processing

		#endregion

		#region Outline Processing

		#endregion

		#region Quick retrieval of advance values

		/// <summary>
		/// Retrieve the advance value of a given glyph outline in a
		/// <see cref="Face"/>. By default, the unhinted advance is returned in
		/// font units.
		/// </summary>
		/// <remarks><para>
		/// This function may fail if you use
		/// <see cref="LoadFlags.AdvanceFlagFastOnly"/> and if the
		/// corresponding font backend doesn't have a quick way to retrieve the
		/// advances.
		/// </para><para>
		/// A scaled advance is returned in 16.16 format but isn't transformed
		/// by the affine transformation specified by
		/// <see cref="SetTransform"/>.
		/// </para></remarks>
		/// <param name="face">The source <see cref="Face"/> handle.</param>
		/// <param name="glyphIndex">The glyph index.</param>
		/// <param name="flags">A set of bit flags similar to those used when calling <see cref="LoadGlyph"/>, used to determine what kind of advances you need.</param>
		/// <returns><para>The advance value, in either font units or 16.16 format.
		/// </para><para>
		/// If <see cref="LoadFlags.VerticalLayout"/> is set, this is the vertical advance corresponding to a vertical layout. Otherwise, it is the horizontal advance in a horizontal layout.</para></returns>
		[CLSCompliant(false)]
		public static int GetAdvance(Face face, uint glyphIndex, LoadFlags flags)
		{
			int padvance;
			Error err = FT_Get_Advance(face.reference, glyphIndex, flags, out padvance);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return padvance;
		}

		/// <summary>
		/// Retrieve the advance values of several glyph outlines in an
		/// <see cref="Face"/>. By default, the unhinted advances are returned
		/// in font units.
		/// </summary>
		/// <remarks><para>
		/// This function may fail if you use
		/// <see cref="LoadFlags.AdvanceFlagFastOnly"/> and if the
		/// corresponding font backend doesn't have a quick way to retrieve the
		/// advances.
		/// </para><para>
		/// Scaled advances are returned in 16.16 format but aren't transformed
		/// by the affine transformation specified by
		/// <see cref="SetTransform"/>.
		/// </para></remarks>
		/// <param name="face">The source <see cref="Face"/> handle.</param>
		/// <param name="start">The first glyph index.</param>
		/// <param name="count">The number of advance values you want to retrieve.</param>
		/// <param name="flags">A set of bit flags similar to those used when calling <see cref="LoadGlyph"/>.</param>
		/// <returns><para>The advances, in either font units or 16.16 format. This array must contain at least ‘count’ elements.
		/// </para><para>
		/// If <see cref="LoadFlags.VerticalLayout"/> is set, these are the vertical advances corresponding to a vertical layout. Otherwise, they are the horizontal advances in a horizontal layout.</para></returns>
		[CLSCompliant(false)]
		public unsafe static int[] GetAdvances(Face face, uint start, uint count, LoadFlags flags)
		{
			IntPtr advPtr;
			Error err = FT_Get_Advances(face.reference, start, count, flags, out advPtr);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			//create a new array and copy the data from the pointer over
			int[] advances = new int[count];
			int* ptr = (int*)advPtr;

			for (int i = 0; i < count; i++)
				advances[i] = ptr[i];

			return advances;
		}

		#endregion

		#region Bitmap Handling

		#endregion

		#region Scanline Converter

		#endregion

		#region Glyph Stroker

		#endregion

		#region System Interface

		#endregion

		#region Module Management

		#endregion

		#region GZIP Streams

		/// <summary>
		/// Open a new stream to parse gzip-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.gz’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, gzip compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a gzipped stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with zlib support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenGzip(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenGzip(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LZW Streams

		/// <summary>
		/// Open a new stream to parse LZW-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.Z’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, LZW compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a LZW stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with LZW support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenLZW(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenLZW(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region BZIP2 Streams

		/// <summary>
		/// Open a new stream to parse bzip2-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.bz2’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, bzip2 compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a bzip2 stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with bzip2 support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenBzip2(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenBzip2(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LCD Filtering

		#endregion
	}
}
