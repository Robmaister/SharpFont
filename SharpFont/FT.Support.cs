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
		/// A very simple function used to perform the computation ‘(a*b)/c’ with maximal accuracy (it uses a 64-bit
		/// intermediate integer whenever necessary).
		/// </para><para>
		/// This function isn't necessarily as fast as some processor specific operations, but is at least completely
		/// portable.
		/// </para></summary>
		/// <param name="a">The first multiplier.</param>
		/// <param name="b">The second multiplier.</param>
		/// <param name="c">The divisor.</param>
		/// <returns>
		/// The result of ‘(a*b)/c’. This function never traps when trying to divide by zero; it simply returns
		/// ‘MaxInt’ or ‘MinInt’ depending on the signs of ‘a’ and ‘b’.
		/// </returns>
		public static int MulDiv(int a, int b, int c)
		{
			return FT_MulDiv(a, b, c);
		}

		/// <summary>
		/// A very simple function used to perform the computation ‘(a*b)/0x10000’ with maximal accuracy. Most of the
		/// time this is used to multiply a given value by a 16.16 fixed float factor.
		/// </summary>
		/// <remarks><para>
		/// This function has been optimized for the case where the absolute value of ‘a’ is less than 2048, and ‘b’ is
		/// a 16.16 scaling factor. As this happens mainly when scaling from notional units to fractional pixels in
		/// FreeType, it resulted in noticeable speed improvements between versions 2.x and 1.x.
		/// </para><para>
		/// As a conclusion, always try to place a 16.16 factor as the second argument of this function; this can make
		/// a great difference.
		/// </para></remarks>
		/// <param name="a">The first multiplier.</param>
		/// <param name="b">The second multiplier. Use a 16.16 factor here whenever possible (see note below).</param>
		/// <returns>The result of ‘(a*b)/0x10000’.</returns>
		public static int MulFix(int a, int b)
		{
			return FT_MulFix(a, b);
		}

		/// <summary>
		/// A very simple function used to perform the computation ‘(a*0x10000)/b’ with maximal accuracy. Most of the
		/// time, this is used to divide a given value by a 16.16 fixed float factor.
		/// </summary>
		/// <remarks>
		/// The optimization for <see cref="DivFix"/> is simple: If (a &lt;&lt; 16) fits in 32 bits, then the division
		/// is computed directly. Otherwise, we use a specialized version of <see cref="MulDiv"/>.
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
		/// A very simple function used to compute the ceiling function of a 16.16 fixed number.
		/// </summary>
		/// <param name="a">The number for which the ceiling function is to be computed.</param>
		/// <returns>The result of ‘(a + 0x10000 - 1) &amp; -0x10000’.</returns>
		public static int CeilFix(int a)
		{
			return FT_CeilFix(a);
		}

		/// <summary>
		/// A very simple function used to compute the floor function of a 16.16 fixed number.
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
		public unsafe static void VectorTransform(FTVector vec, FTMatrix matrix)
		{
			FT_Vector_Transform(ref vec, ref matrix);
		}

		/// <summary>
		/// Perform the matrix operation ‘b = a*b’.
		/// </summary>
		/// <remarks>
		/// The result is undefined if either ‘a’ or ‘b’ is zero.
		/// </remarks>
		/// <param name="a">A pointer to matrix ‘a’.</param>
		/// <param name="b">A pointer to matrix ‘b’.</param>
		public static void MatrixMultiply(FTMatrix a, FTMatrix b)
		{
			FT_Matrix_Multiply(ref a, ref b);
		}

		/// <summary>
		/// Invert a 2x2 matrix. Return an error if it can't be inverted.
		/// </summary>
		/// <param name="matrix">A pointer to the target matrix. Remains untouched in case of error.</param>
		public static void MatrixInvert(FTMatrix matrix)
		{
			Error err = FT_Matrix_Invert(ref matrix);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Return the sinus of a given angle in fixed point format.
		/// </summary>
		/// <remarks>
		/// If you need both the sinus and cosinus for a given angle, use the function <see cref="VectorUnit"/>.
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
		/// If you need both the sinus and cosinus for a given angle, use the function <see cref="VectorUnit"/>.
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
		/// Return the arc-tangent corresponding to a given vector (x,y) in the 2d plane.
		/// </summary>
		/// <param name="x">The horizontal vector coordinate.</param>
		/// <param name="y">The vertical vector coordinate.</param>
		/// <returns>The arc-tangent value (i.e. angle).</returns>
		public static int Atan2(int x, int y)
		{
			return FT_Atan2(x, y);
		}

		/// <summary>
		/// Return the difference between two angles. The result is always constrained to the [-PI..PI] interval.
		/// </summary>
		/// <param name="angle1">First angle.</param>
		/// <param name="angle2">Second angle.</param>
		/// <returns>Constrained value of ‘value2-value1’.</returns>
		public static int AngleDiff(int angle1, int angle2)
		{
			return FT_Angle_Diff(angle1, angle2);
		}
		
		/// <summary><para>
		/// Return the unit vector corresponding to a given angle. After the call, the value of ‘vec.x’ will be
		/// ‘sin(angle)’, and the value of ‘vec.y’ will be ‘cos(angle)’.
		/// </para><para>
		/// This function is useful to retrieve both the sinus and cosinus of a given angle quickly.
		/// </para></summary>
		/// <param name="angle">The address of angle.</param>
		/// <returns>The address of target vector.</returns>
		public static FTVector VectorUnit(int angle)
		{
			FTVector vec;
			FT_Vector_Unit(out vec, angle);

			return vec;
		}

		/// <summary>
		/// Rotate a vector by a given angle.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <param name="angle">The address of angle.</param>
		public static void VectorRotate(FTVector vec, int angle)
		{
			FT_Vector_Rotate(ref vec, angle);
		}

		/// <summary>
		/// Return the length of a given vector.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <returns>The vector length, expressed in the same units that the original vector coordinates.</returns>
		public static int VectorLength(FTVector vec)
		{
			return FT_Vector_Length(ref vec);
		}

		/// <summary>
		/// Compute both the length and angle of a given vector.
		/// </summary>
		/// <param name="vec">The address of source vector.</param>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		public static void VectorPolarize(FTVector vec, out int length, out int angle)
		{
			FT_Vector_Polarize(ref vec, out length, out angle);
		}

		/// <summary>
		/// Compute vector coordinates from a length and angle.
		/// </summary>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		/// <returns>The address of source vector.</returns>
		public static FTVector VectorFromPolar(int length, int angle)
		{
			FTVector vec;
			FT_Vector_From_Polar(out vec, length, angle);

			return vec;
		}

		#endregion

		#region Glyph Stroker

		/// <summary>
		/// Create a new stroker object.
		/// </summary>
		/// <param name="library">FreeType library handle.</param>
		/// <returns>A new stroker object handle. NULL in case of error.</returns>
		public static Stroker StrokerNew(Library library)
		{
			IntPtr strokerRef;
			Error err = FT_Stroker_New(library.Reference, out strokerRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Stroker(strokerRef);
		}

		/// <summary>
		/// Reset a stroker object's attributes.
		/// </summary>
		/// <remarks>
		/// The radius is expressed in the same units as the outline coordinates.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="radius">The border radius.</param>
		/// <param name="lineCap">The line cap style.</param>
		/// <param name="lineJoin">The line join style.</param>
		/// <param name="miterLimit">
		/// The miter limit for the <see cref="StrokerLineJoin.MiterFixed"/> and
		/// <see cref="StrokerLineJoin.MiterVariable"/> line join styles, expressed as 16.16 fixed point value.
		/// </param>
		public static void StrokerSet(Stroker stroker, int radius, StrokerLineCap lineCap, StrokerLineJoin lineJoin, int miterLimit)
		{
			FT_Stroker_Set(stroker.Reference, radius, lineCap, lineJoin, miterLimit);
		}

		/// <summary>
		/// Reset a stroker object without changing its attributes. You should call this function before beginning a
		/// new series of calls to <see cref="FT.StrokerBeginSubPath"/> or <see cref="FT.StrokerEndSubPath"/>.
		/// </summary>
		/// <param name="stroker">The target stroker handle.</param>
		public static void StrokerRewind(Stroker stroker)
		{
			FT_Stroker_Rewind(stroker.Reference);
		}
		
		/// <summary>
		/// A convenience function used to parse a whole outline with the stroker. The resulting outline(s) can be
		/// retrieved later by functions like <see cref="FT.StrokerGetCounts"/> and <see cref="FT.StrokerExport"/>.
		/// </summary>
		/// <remarks><para>
		/// If ‘opened’ is 0 (the default), the outline is treated as a closed path, and the stroker generates two
		/// distinct ‘border’ outlines.
		/// </para><para>
		/// If ‘opened’ is 1, the outline is processed as an open path, and the stroker generates a single ‘stroke’
		/// outline.
		/// </para><para>
		/// This function calls <see cref="FT.StrokerRewind"/> automatically.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="outline">The source outline.</param>
		/// <param name="opened">
		/// A boolean. If 1, the outline is treated as an open path instead of a closed one.
		/// </param>
		public static void StrokerParseOutline(Stroker stroker, Outline outline, bool opened)
		{
			Error err = FT_Stroker_ParseOutline(stroker.Reference, outline.Reference, opened);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Start a new sub-path in the stroker.
		/// </summary>
		/// <remarks>
		/// This function is useful when you need to stroke a path that is not stored as an <see cref="Outline"/>
		/// object.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="to">A pointer to the start vector.</param>
		/// <param name="open">A boolean. If 1, the sub-path is treated as an open one.</param>
		public static void StrokerBeginSubPath(Stroker stroker, FTVector to, bool open)
		{
			Error err = FT_Stroker_BeginSubPath(stroker.Reference, ref to, open);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Close the current sub-path in the stroker.
		/// </summary>
		/// <remarks>
		/// You should call this function after <see cref="FT.StrokerBeginSubPath"/>. If the subpath was not ‘opened’,
		/// this function ‘draws’ a single line segment to the start position when needed.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		public static void StrokerEndSubPath(Stroker stroker)
		{
			Error err = FT_Stroker_EndSubPath(stroker.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single line segment in the stroker's current sub-path, from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between <see cref="FT.StrokerBeginSubPath"/> and
		/// <see cref="FT.StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerLineTo(Stroker stroker, FTVector to)
		{
			Error err = FT_Stroker_LineTo(stroker.Reference, ref to);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single quadratic Bézier in the stroker's current sub-path, from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between <see cref="FT.StrokerBeginSubPath"/> and
		/// <see cref="FT.StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="control">A pointer to a Bézier control point.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerConicTo(Stroker stroker, FTVector control, FTVector to)
		{
			Error err = FT_Stroker_ConicTo(stroker.Reference, ref control, ref to);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single cubic Bézier in the stroker's current sub-path, from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between <see cref="FT.StrokerBeginSubPath"/> and
		/// <see cref="FT.StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="control1">A pointer to the first Bézier control point.</param>
		/// <param name="control2">A pointer to second Bézier control point.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerCubicTo(Stroker stroker, FTVector control1, FTVector control2, FTVector to)
		{
			Error err = FT_Stroker_CubicTo(stroker.Reference, ref control1, ref control2, ref to);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Call this function once you have finished parsing your paths with the stroker. It returns the number of
		/// points and contours necessary to export one of the ‘border’ or ‘stroke’ outlines generated by the stroker.
		/// </summary>
		/// <remarks><para>
		/// When an outline, or a sub-path, is ‘closed’, the stroker generates two independent ‘border’ outlines, named
		/// ‘left’ and ‘right’.
		/// </para><para>
		/// When the outline, or a sub-path, is ‘opened’, the stroker merges the ‘border’ outlines with caps. The
		/// ‘left’ border receives all points, while the ‘right’ border becomes empty.
		/// </para><para>
		/// Use the function <see cref="FT.StrokerGetCounts"/> instead if you want to retrieve the counts associated to
		/// both borders.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="border">The border index.</param>
		/// <param name="pointsCount">The number of points.</param>
		/// <param name="contoursCount">The number of contours.</param>
		[CLSCompliant(false)]
		public static void StrokerGetBorderCounts(Stroker stroker, StrokerBorder border, out uint pointsCount, out uint contoursCount)
		{
			Error err = FT_Stroker_GetBorderCounts(stroker.Reference, border, out pointsCount, out contoursCount);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Call this function after <see cref="StrokerGetBorderCounts"/> to export the corresponding border to your
		/// own <see cref="Outline"/> structure.
		/// </para><para>
		/// Note that this function appends the border points and contours to your outline, but does not try to resize
		/// its arrays.
		/// </para></summary>
		/// <remarks><para>
		/// Always call this function after <see cref="FT.StrokerGetBorderCounts"/> to get sure that there is enough
		/// room in your <see cref="Outline"/> object to receive all new data.
		/// </para><para>
		/// When an outline, or a sub-path, is ‘closed’, the stroker generates two independent ‘border’ outlines, named
		/// ‘left’ and ‘right’.
		/// </para><para>
		/// When the outline, or a sub-path, is ‘opened’, the stroker merges the ‘border’ outlines with caps. The
		/// ‘left’ border receives all points, while the ‘right’ border becomes empty.
		/// </para><para>
		/// Use the function <see cref="FT.StrokerExport"/> instead if you want to retrieve all borders at once.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="border">The border index.</param>
		/// <param name="outline">The target outline handle.</param>
		public static void StrokerExportBorder(Stroker stroker, StrokerBorder border, Outline outline)
		{
			FT_Stroker_ExportBorder(stroker.Reference, border, outline.Reference);
		}

		/// <summary>
		/// Call this function once you have finished parsing your paths with the stroker. It returns the number of
		/// points and contours necessary to export all points/borders from the stroked outline/path.
		/// </summary>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="pointsCount">The number of points.</param>
		/// <param name="contoursCount">The number of contours.</param>
		[CLSCompliant(false)]
		public static void StrokerGetCounts(Stroker stroker, out uint pointsCount, out uint contoursCount)
		{
			Error err = FT_Stroker_GetCounts(stroker.Reference, out pointsCount, out contoursCount);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Call this function after <see cref="FT.StrokerGetBorderCounts"/> to export all borders to your own
		/// <see cref="Outline"/> structure.
		/// </para><para>
		/// Note that this function appends the border points and contours to your outline, but does not try to resize
		/// its arrays.
		/// </para></summary>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="outline">The target outline handle.</param>
		public static void StrokerExport(Stroker stroker, Outline outline)
		{
			FT_Stroker_Export(stroker.Reference, outline.Reference);
		}

		/// <summary>
		/// Destroy a stroker object.
		/// </summary>
		/// <param name="stroker">A stroker handle. Can be NULL.</param>
		public static void StrokerDone(Stroker stroker)
		{
			FT_Stroker_Done(stroker.Reference);
		}

		/// <summary>
		/// Stroke a given outline glyph object with a given stroker.
		/// </summary>
		/// <remarks>
		/// The source glyph is untouched in case of error.
		/// </remarks>
		/// <param name="glyph">Source glyph handle.</param>
		/// <param name="stroker">A stroker handle.</param>
		/// <param name="destroy">A Boolean. If 1, the source glyph object is destroyed on success.</param>
		/// <returns>New glyph handle.</returns>
		public static Glyph GlyphStroke(Glyph glyph, Stroker stroker, bool destroy)
		{
			IntPtr sourceRef = glyph.Reference;

			Error err = FT_Glyph_Stroke(ref sourceRef, stroker.Reference, destroy);

			if (destroy)
			{
				//TODO when Glyph implements IDisposable, dispose the glyph.
			}

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			if (sourceRef == glyph.Reference)
				return glyph;
			else
				return new Glyph(sourceRef, glyph.Library);
		}

		/// <summary>
		/// Stroke a given outline glyph object with a given stroker, but only return either its inside or outside
		/// border.
		/// </summary>
		/// <remarks>
		/// The source glyph is untouched in case of error.
		/// </remarks>
		/// <param name="glyph">Source glyph handle.</param>
		/// <param name="stroker">A stroker handle.</param>
		/// <param name="inside">A Boolean. If 1, return the inside border, otherwise the outside border.</param>
		/// <param name="destroy">A Boolean. If 1, the source glyph object is destroyed on success.</param>
		/// <returns>New glyph handle.</returns>
		public static Glyph GlyphStrokeBorder(Glyph glyph, Stroker stroker, bool inside, bool destroy)
		{
			IntPtr sourceRef = glyph.Reference;

			Error err = FT_Glyph_StrokeBorder(ref sourceRef, stroker.Reference, inside, destroy);

			if (destroy)
			{
				//TODO when Glyph implements IDisposable, dispose the glyph.
			}

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			if (sourceRef == glyph.Reference)
				return glyph;
			else
				return new Glyph(sourceRef, glyph.Library);
		}

		#endregion

		#region Module Management

		/// <summary>
		/// Add a new module to a given library instance.
		/// </summary>
		/// <remarks>
		/// An error will be returned if a module already exists by that name, or if the module requires a version of
		/// FreeType that is too great.
		/// </remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="clazz">A pointer to class descriptor for the module.</param>
		public static void AddModule(Library library, ModuleClass clazz)
		{
			Error err = FT_Add_Module(library.Reference, clazz.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Find a module by its name.
		/// </summary>
		/// <remarks>
		/// FreeType's internal modules aren't documented very well, and you should look up the source code for
		/// details.
		/// </remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="moduleName">The module's name (as an ASCII string).</param>
		/// <returns>A module handle. 0 if none was found.</returns>
		public static Module GetModule(Library library, string moduleName)
		{
			return new Module(FT_Get_Module(library.Reference, moduleName));
		}

		/// <summary>
		/// Remove a given module from a library instance.
		/// </summary>
		/// <remarks>
		/// The module object is destroyed by the function in case of success.
		/// </remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="module">A handle to a module object.</param>
		public static void RemoveModule(Library library, Module module)
		{
			Error err = FT_Remove_Module(library.Reference, module.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// A counter gets initialized to 1 at the time a <see cref="Library"/> structure is created. This function
		/// increments the counter. <see cref="FT.DoneLibrary"/> then only destroys a library if the counter is 1,
		/// otherwise it simply decrements the counter.
		/// </para><para>
		/// This function helps in managing life-cycles of structures which reference <see cref="Library"/> objects.
		/// </para></summary>
		/// <param name="library">A handle to a target library object.</param>
		internal static void ReferenceLibrary(Library library)
		{
			//marked as internal because the Library class wraps this funcitonality.
			Error err = FT_Reference_Library(library.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// This function is used to create a new FreeType library instance from a given memory object. It is thus
		/// possible to use libraries with distinct memory allocators within the same program.
		/// </para><para>
		/// Normally, you would call this function (followed by a call to <see cref="FT.AddDefaultModules"/> or a
		/// series of calls to <see cref="FT.AddModule"/>) instead of <see cref="FT.InitFreeType"/> to initialize the
		/// FreeType library.
		/// </para><para>
		/// Don't use <see cref="FT.DoneFreeType"/> but <see cref="FT.DoneLibrary"/> to destroy a library instance.
		/// </para></summary>
		/// <remarks>
		/// See the discussion of reference counters in the description of <see cref="FT.ReferenceLibrary"/>.
		/// </remarks>
		/// <param name="memory">A handle to the original memory object.</param>
		/// <returns>A pointer to handle of a new library object.</returns>
		public static Library NewLibrary(Memory memory)
		{
			IntPtr libraryRef;
			Error err = FT_New_Library(memory.Reference, out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Library(libraryRef, false);
		}

		/// <summary>
		/// Discard a given library object. This closes all drivers and discards all resource objects.
		/// </summary>
		/// <remarks>
		/// See the discussion of reference counters in the description of <see cref="FT.ReferenceLibrary"/>.
		/// </remarks>
		/// <param name="library">A handle to the target library.</param>
		public static void DoneLibrary(Library library)
		{
			library.Dispose();
		}

		/// <summary>
		/// Set a debug hook function for debugging the interpreter of a font format.
		/// </summary>
		/// <remarks><para>
		/// Currently, four debug hook slots are available, but only two (for the TrueType and the Type 1 interpreter)
		/// are defined.
		/// </para><para>
		/// Since the internal headers of FreeType are no longer installed, the symbol ‘FT_DEBUG_HOOK_TRUETYPE’ isn't
		/// available publicly. This is a bug and will be fixed in a forthcoming release.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="hookIndex">The index of the debug hook. You should use the values defined in ‘ftobjs.h’, e.g., ‘FT_DEBUG_HOOK_TRUETYPE’.</param>
		/// <param name="debugHook">The function used to debug the interpreter.</param>
		[CLSCompliant(false)]
		public static void SetDebugHook(Library library, uint hookIndex, IntPtr debugHook)
		{
			FT_Set_Debug_Hook(library.Reference, hookIndex, debugHook);
		}

		/// <summary>
		/// Add the set of default drivers to a given library object. This is only useful when you create a library
		/// object with <see cref="FT.NewLibrary"/> (usually to plug a custom memory manager).
		/// </summary>
		/// <param name="library">A handle to a new library object.</param>
		public static void AddDefaultModules(Library library)
		{
			FT_Add_Default_Modules(library.Reference);
		}

		/// <summary>
		/// Retrieve the current renderer for a given glyph format.
		/// </summary>
		/// <remarks><para>
		/// An error will be returned if a module already exists by that name, or if the module requires a version of
		/// FreeType that is too great.
		/// </para><para>
		/// To add a new renderer, simply use <see cref="FT.AddModule"/>. To retrieve a renderer by its name, use
		/// <see cref="FT.GetModule"/>.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="format">The glyph format.</param>
		/// <returns>A renderer handle. 0 if none found.</returns>
		[CLSCompliant(false)]
		public static Renderer GetRenderer(Library library, GlyphFormat format)
		{
			return new Renderer(FT_Get_Renderer(library.Reference, format));
		}

		/// <summary>
		/// Set the current renderer to use, and set additional mode.
		/// </summary>
		/// <remarks><para>
		/// In case of success, the renderer will be used to convert glyph images in the renderer's known format into
		/// bitmaps.
		/// </para><para>
		/// This doesn't change the current renderer for other formats.
		/// </para><para>
		/// Currently, only the B/W renderer, if compiled with FT_RASTER_OPTION_ANTI_ALIASING (providing a 5-levels
		/// anti-aliasing mode; this option must be set directly in ‘ftraster.c’ and is undefined by default) accepts a
		/// single tag ‘pal5’ to set its gray palette as a character string with 5 elements. Consequently, the third
		/// and fourth argument are zero normally.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="renderer">A handle to the renderer object.</param>
		/// <param name="numParams">The number of additional parameters.</param>
		/// <param name="parameters">Additional parameters.</param>
		[CLSCompliant(false)]
		public unsafe static void SetRenderer(Library library, Renderer renderer, uint numParams, Parameter[] parameters)
		{
			ParameterRec[] paramRecs = Array.ConvertAll<Parameter, ParameterRec>(parameters, (p => p.Record));
			fixed (void* ptr = paramRecs)
			{
				Error err = FT_Set_Renderer(library.Reference, renderer.Reference, numParams, (IntPtr)ptr);
			}
		}

		#endregion

		#region GZIP Streams

		/// <summary>
		/// Open a new stream to parse gzip-compressed font files. This is mainly used to support the compressed
		/// ‘*.pcf.gz’ fonts that come with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream will not call ‘FT_Stream_Close’ on the
		/// source stream. None of the stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the decompression process each time seeking backwards is
		/// needed within the stream.
		/// </para><para>
		/// In certain builds of the library, gzip compression recognition is automatically handled when calling
		/// <see cref="FT.NewFace"/> or <see cref="FT.OpenFace"/>. This means that if no font driver is capable of
		/// handling the raw compressed file, the library will try to open a gzipped stream from it and re-open the
		/// face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/> if your build of FreeType was not
		/// compiled with zlib support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenGzip(FTStream stream, FTStream source)
		{
			Error err = FT_Stream_OpenGzip(stream.Reference, source.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LZW Streams

		/// <summary>
		/// Open a new stream to parse LZW-compressed font files. This is mainly used to support the compressed
		/// ‘*.pcf.Z’ fonts that come with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream will not call ‘FT_Stream_Close’ on the
		/// source stream. None of the stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the decompression process each time seeking backwards is
		/// needed within the stream.
		/// </para><para>
		/// In certain builds of the library, LZW compression recognition is automatically handled when calling
		/// <see cref="FT.NewFace"/> or <see cref="FT.OpenFace"/>. This means that if no font driver is capable of
		/// handling the raw compressed file, the library will try to open a LZW stream from it and re-open the face
		/// with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/> if your build of FreeType was not
		/// compiled with LZW support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenLZW(FTStream stream, FTStream source)
		{
			Error err = FT_Stream_OpenLZW(stream.Reference, source.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region BZIP2 Streams

		/// <summary>
		/// Open a new stream to parse bzip2-compressed font files. This is mainly used to support the compressed
		/// ‘*.pcf.bz2’ fonts that come with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream will not call ‘FT_Stream_Close’ on the
		/// source stream. None of the stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the decompression process each time seeking backwards is
		/// needed within the stream.
		/// </para><para>
		/// In certain builds of the library, bzip2 compression recognition is automatically handled when calling
		/// <see cref="FT.NewFace"/> or <see cref="FT.OpenFace"/>. This means that if no font driver is capable of
		/// handling the raw compressed file, the library will try to open a bzip2 stream from it and re-open the face
		/// with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/> if your build of FreeType was not
		/// compiled with bzip2 support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenBzip2(FTStream stream, FTStream source)
		{
			Error err = FT_Stream_OpenBzip2(stream.Reference, source.Reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LCD Filtering

		/// <summary>
		/// This function is used to apply color filtering to LCD decimated bitmaps, like the ones used when calling
		/// <see cref="FT.RenderGlyph"/> with <see cref="RenderMode.LCD"/> or <see cref="RenderMode.VerticalLCD"/>.
		/// </summary>
		/// <remarks><para>
		/// This feature is always disabled by default. Clients must make an explicit call to this function with a
		/// ‘filter’ value other than <see cref="LcdFilter.None"/> in order to enable it.
		/// </para><para>
		/// Due to <b>PATENTS</b> covering subpixel rendering, this function doesn't do anything except returning
		/// <see cref="Error.UnimplementedFeature"/> if the configuration macro FT_CONFIG_OPTION_SUBPIXEL_RENDERING is
		/// not defined in your build of the library, which should correspond to all default builds of FreeType.
		/// </para><para>
		/// The filter affects glyph bitmaps rendered through <see cref="FT.RenderGlyph"/>,
		/// <see cref="FT.OutlineGetBitmap"/>, <see cref="FT.LoadGlyph"/>, and <see cref="FT.LoadChar"/>.
		/// </para><para>
		/// It does not affect the output of <see cref="FT.OutlineRender"/> and <see cref="FT.OutlineGetBitmap"/>.
		/// </para><para>
		/// If this feature is activated, the dimensions of LCD glyph bitmaps are either larger or taller than the
		/// dimensions of the corresponding outline with regards to the pixel grid. For example, for
		/// <see cref="RenderMode.LCD"/>, the filter adds up to 3 pixels to the left, and up to 3 pixels to the right.
		/// </para><para>
		/// The bitmap offset values are adjusted correctly, so clients shouldn't need to modify their layout and glyph
		/// positioning code when enabling the filter.
		/// </para></remarks>
		/// <param name="library">A handle to the target library instance.</param>
		/// <param name="filter"><para>
		/// The filter type.
		/// </para><para>
		/// You can use <see cref="LcdFilter.None"/> here to disable this feature, or <see cref="LcdFilter.Default"/>
		/// to use a default filter that should work well on most LCD screens.
		/// </para></param>
		public static void LibrarySetLcdFilter(Library library, LcdFilter filter)
		{
			Error err = FT_Library_SetLcdFilter(library.Reference, filter);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Use this function to override the filter weights selected by <see cref="FT.LibrarySetLcdFilter"/>. By
		/// default, FreeType uses the quintuple (0x00, 0x55, 0x56, 0x55, 0x00) for <see cref="LcdFilter.Light"/>, and
		/// (0x10, 0x40, 0x70, 0x40, 0x10) for <see cref="LcdFilter.Default"/> and <see cref="LcdFilter.Legacy"/>.
		/// </summary>
		/// <remarks><para>
		/// Due to <b>PATENTS</b> covering subpixel rendering, this function doesn't do anything except returning
		/// <see cref="Error.UnimplementedFeature"/> if the configuration macro FT_CONFIG_OPTION_SUBPIXEL_RENDERING is
		/// not defined in your build of the library, which should correspond to all default builds of FreeType.
		/// </para><para>
		/// This function must be called after <see cref="FT.LibrarySetLcdFilter"/> to have any effect.
		/// </para></remarks>
		/// <param name="library">A handle to the target library instance.</param>
		/// <param name="weights">
		/// A pointer to an array; the function copies the first five bytes and uses them to specify the filter
		/// weights.
		/// </param>
		public static void LibrarySetLcdFilterWeights(Library library, byte[] weights)
		{
			Error err = FT_Library_SetLcdFilterWeights(library.Reference, weights);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion
	}
}
