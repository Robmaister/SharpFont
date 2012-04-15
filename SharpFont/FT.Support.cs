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
		public static void VectorTransform(FTVector vec, FTMatrix matrix)
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
		public static void MatrixMultiply(FTMatrix a, FTMatrix b)
		{
			FT_Matrix_Multiply(a.reference, ref b.reference);

			//update the matrix record.
			b.rec = PInvokeHelper.PtrToStructure<MatrixRec>(b.reference);
		}

		/// <summary>
		/// Invert a 2x2 matrix. Return an error if it can't be inverted.
		/// </summary>
		/// <param name="matrix">A pointer to the target matrix. Remains untouched in case of error.</param>
		public static void MatrixInvert(FTMatrix matrix)
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
		public static FTVector VectorUnit(int angle)
		{
			IntPtr vecRef;
			FT_Vector_Unit(out vecRef, angle);

			return new FTVector(vecRef);
		}

		/// <summary>
		/// Rotate a vector by a given angle.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <param name="angle">The address of angle.</param>
		public static void VectorRotate(FTVector vec, int angle)
		{
			FT_Vector_Rotate(ref vec.reference, angle);
		}

		/// <summary>
		/// Return the length of a given vector.
		/// </summary>
		/// <param name="vec">The address of target vector.</param>
		/// <returns>The vector length, expressed in the same units that the original vector coordinates.</returns>
		public static int VectorLength(FTVector vec)
		{
			return FT_Vector_Length(vec.reference);
		}

		/// <summary>
		/// Compute both the length and angle of a given vector.
		/// </summary>
		/// <param name="vec">The address of source vector.</param>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		public static void VectorPolarize(FTVector vec, out int length, out int angle)
		{
			FT_Vector_Polarize(vec.reference, out length, out angle);
		}

		/// <summary>
		/// Compute vector coordinates from a length and angle.
		/// </summary>
		/// <param name="length">The vector length.</param>
		/// <param name="angle">The vector angle.</param>
		/// <returns>The address of source vector.</returns>
		public static FTVector VectorFromPolar(int length, int angle)
		{
			IntPtr vecRef;
			FT_Vector_From_Polar(out vecRef, length, angle);

			return new FTVector(vecRef);
		}

		#endregion

		#region List Processing

		/// <summary>
		/// Find the list node for a given listed object.
		/// </summary>
		/// <param name="list">Find the list node for a given listed object.</param>
		/// <param name="data">The address of the listed object.</param>
		/// <returns>List node. NULL if it wasn't found.</returns>
		public static ListNode ListFind(FTList list, IntPtr data)
		{
			return new ListNode(FT_List_Find(list.reference, data));
		}

		/// <summary>
		/// Append an element to the end of a list.
		/// </summary>
		/// <param name="list">A pointer to the parent list.</param>
		/// <param name="node">The node to append.</param>
		public static void ListAdd(FTList list, ListNode node)
		{
			FT_List_Add(list.reference, node.reference);
		}

		/// <summary>
		/// Insert an element at the head of a list.
		/// </summary>
		/// <param name="list">A pointer to parent list.</param>
		/// <param name="node">The node to insert.</param>
		public static void ListInsert(FTList list, ListNode node)
		{
			FT_List_Insert(list.reference, node.reference);
		}

		/// <summary>
		/// Remove a node from a list. This function doesn't check whether the
		/// node is in the list!
		/// </summary>
		/// <param name="list">A pointer to the parent list.</param>
		/// <param name="node">The node to remove.</param>
		public static void ListRemove(FTList list, ListNode node)
		{
			FT_List_Remove(list.reference, node.reference);
		}

		/// <summary>
		/// Move a node to the head/top of a list. Used to maintain LRU lists.
		/// </summary>
		/// <param name="list">A pointer to the parent list.</param>
		/// <param name="node">The node to move.</param>
		public static void ListUp(FTList list, ListNode node)
		{
			FT_List_Up(list.reference, node.reference);
		}

		/// <summary>
		/// Parse a list and calls a given iterator function on each element.
		/// Note that parsing is stopped as soon as one of the iterator calls
		/// returns a non-zero value.
		/// </summary>
		/// <param name="list">A handle to the list.</param>
		/// <param name="iterator">An iterator function, called on each node of the list.</param>
		/// <param name="user">A user-supplied field which is passed as the second argument to the iterator.</param>
		public static void ListIterate(FTList list, ListIterator iterator, IntPtr user)
		{
			Error err = FT_List_Iterate(list.reference, Marshal.GetFunctionPointerForDelegate(iterator), user);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Destroy all elements in the list as well as the list itself.
		/// </summary>
		/// <remarks>
		/// This function expects that all nodes added by
		/// <see cref="FT.ListAdd"/> or <see cref="FT.ListInsert"/> have been
		/// dynamically allocated.
		/// </remarks>
		/// <param name="list">A handle to the list.</param>
		/// <param name="destroy">A list destructor that will be applied to each element of the list.</param>
		/// <param name="memory">The current memory object which handles deallocation.</param>
		/// <param name="user">A user-supplied field which is passed as the last argument to the destructor.</param>
		public static void ListFinalize(FTList list, ListDestructor destroy, Memory memory, IntPtr user)
		{
			FT_List_Finalize(list.reference, Marshal.GetFunctionPointerForDelegate(destroy), memory.reference, user);
		}

		#endregion

		#region Outline Processing

		/// <summary>
		/// Create a new outline of a given size.
		/// </summary>
		/// <remarks>
		/// The reason why this function takes a ‘library’ parameter is simply
		/// to use the library's memory allocator.
		/// </remarks>
		/// <param name="library">A handle to the library object from where the outline is allocated. Note however that the new outline will not necessarily be freed, when destroying the library, by <see cref="FT.DoneFreeType"/>.</param>
		/// <param name="numPoints">The maximal number of points within the outline.</param>
		/// <param name="numContours">The maximal number of contours within the outline.</param>
		/// <returns>A handle to the new outline.</returns>
		[CLSCompliant(false)]
		public static Outline OutlineNew(Library library, uint numPoints, int numContours)
		{
			IntPtr outlineRef;
			Error err = FT_Outline_New(library.reference, numPoints, numContours, out outlineRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Outline(outlineRef);
		}

		/// <summary>
		/// Create a new outline of a given size.
		/// </summary>
		/// <param name="memory">A handle to a FreeType memory allocator.</param>
		/// <param name="numPoints">The maximal number of points within the outline.</param>
		/// <param name="numContours">The maximal number of contours within the outline.</param>
		/// <returns>A handle to the new outline.</returns>
		[CLSCompliant(false)]
		public static Outline OutlineNew(Memory memory, uint numPoints, int numContours)
		{
			IntPtr outlineRef;
			Error err = FT_Outline_New_Internal(memory.reference, numPoints, numContours, out outlineRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Outline(outlineRef);
		}

		/// <summary>
		/// Destroy an outline created with
		/// <see cref="OutlineNew(Library, uint, int)"/>.
		/// </summary>
		/// <remarks><para>
		/// If the outline's ‘owner’ field is not set, only the outline
		/// descriptor will be released.
		/// </para><para>
		/// The reason why this function takes an ‘library’ parameter is simply
		/// to use ft_mem_free().
		/// </para></remarks>
		/// <param name="library">A handle of the library object used to allocate the outline.</param>
		/// <param name="outline">A pointer to the outline object to be discarded.</param>
		public static void OutlineDone(Library library, Outline outline)
		{
			Error err = FT_Outline_Done(library.reference, outline.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Destroy an outline created with
		/// <see cref="OutlineNew(Library, uint, int)"/>.
		/// </summary>
		/// <remarks>
		/// If the outline's ‘owner’ field is not set, only the outline
		/// descriptor will be released.
		/// </remarks>
		/// <param name="memory">A handle of the library object used to allocate the outline.</param>
		/// <param name="outline">A pointer to the outline object to be discarded.</param>
		public static void OutlineDone(Memory memory, Outline outline)
		{
			Error err = FT_Outline_Done_Internal(memory.reference, outline.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Copy an outline into another one. Both objects must have the same
		/// sizes (number of points &amp; number of contours) when this function is
		/// called.
		/// </summary>
		/// <param name="source">A handle to the source outline.</param>
		/// <param name="target">A handle to the target outline.</param>
		public static void OutlineCopy(Outline source, ref Outline target)
		{
			Error err = FT_Outline_Copy(source.reference, ref target.reference);
			target.rec = PInvokeHelper.PtrToStructure<OutlineRec>(target.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Apply a simple translation to the points of an outline.
		/// </summary>
		/// <param name="outline">A pointer to the target outline descriptor.</param>
		/// <param name="xOffset">The horizontal offset.</param>
		/// <param name="yOffset">The vertical offset.</param>
		public static void OutlineTranslate(Outline outline, int xOffset, int yOffset)
		{
			FT_Outline_Translate(outline.reference, xOffset, yOffset);
		}

		/// <summary>
		/// Apply a simple 2x2 matrix to all of an outline's points. Useful for
		/// applying rotations, slanting, flipping, etc.
		/// </summary>
		/// <remarks>
		/// You can use <see cref="FT.OutlineTranslate"/> if you need to
		/// translate the outline's points.
		/// </remarks>
		/// <param name="outline">A pointer to the target outline descriptor.</param>
		/// <param name="matrix">A pointer to the transformation matrix.</param>
		public static void OutlineTransform(Outline outline, FTMatrix matrix)
		{
			FT_Outline_Transform(outline.reference, matrix.reference);
		}

		/// <summary><para>
		/// Embolden an outline. The new outline will be at most 4 times
		/// ‘strength’ pixels wider and higher. You may think of the left and
		/// bottom borders as unchanged.
		/// </para><para>
		/// Negative ‘strength’ values to reduce the outline thickness are
		/// possible also.
		/// </para></summary>
		/// <remarks><para>
		/// The used algorithm to increase or decrease the thickness of the
		/// glyph doesn't change the number of points; this means that certain
		/// situations like acute angles or intersections are sometimes handled
		/// incorrectly.
		/// </para><para>
		/// If you need ‘better’ metrics values you should call
		/// <see cref="FT.OutlineGetCBox"/> or <see cref="FT.OutlineGetBBox"/>.
		/// </para></remarks>
		/// <example>
		/// FT_Load_Glyph( face, index, FT_LOAD_DEFAULT );
		/// if ( face-&gt;slot-&gt;format == FT_GLYPH_FORMAT_OUTLINE )
		/// 	FT_Outline_Embolden( &amp;face-&gt;slot-&gt;outline, strength );
		/// </example>
		/// <param name="outline">A handle to the target outline.</param>
		/// <param name="strength">How strong the glyph is emboldened. Expressed in 26.6 pixel format.</param>
		public static void OutlineEmbolden(Outline outline, int strength)
		{
			Error err = FT_Outline_Embolden(outline.reference, strength);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Reverse the drawing direction of an outline. This is used to ensure
		/// consistent fill conventions for mirrored glyphs.
		/// </summary>
		/// <remarks><para>
		/// This function toggles the bit flag
		/// <see cref="OutlineFlags.ReverseFill"/> in the outline's ‘flags’
		/// field.
		/// </para><para>
		/// It shouldn't be used by a normal client application, unless it
		/// knows what it is doing.
		/// </para></remarks>
		/// <param name="outline">A pointer to the target outline descriptor.</param>
		public static void OutlineReverse(Outline outline)
		{
			FT_Outline_Reverse(outline.reference);
		}

		/// <summary>
		/// Check the contents of an outline descriptor.
		/// </summary>
		/// <param name="outline">A handle to a source outline.</param>
		public static void OutlineCheck(Outline outline)
		{
			Error err = FT_Outline_Check(outline.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Compute the exact bounding box of an outline. This is slower than
		/// computing the control box. However, it uses an advanced algorithm
		/// which returns very quickly when the two boxes coincide. Otherwise,
		/// the outline Bézier arcs are traversed to extract their extrema.
		/// </summary>
		/// <remarks>
		/// If the font is tricky and the glyph has been loaded with
		/// <see cref="LoadFlags.NoScale"/>, the resulting BBox is meaningless.
		/// To get reasonable values for the BBox it is necessary to load the
		/// glyph at a large ppem value (so that the hinting instructions can
		/// properly shift and scale the subglyphs), then extracting the BBox
		/// which can be eventually converted back to font units.
		/// </remarks>
		/// <param name="outline">A pointer to the source outline.</param>
		/// <returns>The outline's exact bounding box.</returns>
		public static BBox OutlineGetBBox(Outline outline)
		{
			IntPtr bboxRef;
			Error err = FT_Outline_Get_BBox(outline.reference, out bboxRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new BBox(bboxRef);
		}

		/// <summary>
		/// Walk over an outline's structure to decompose it into individual
		/// segments and Bézier arcs. This function also emits ‘move to’
		/// operations to indicate the start of new contours in the outline.
		/// </summary>
		/// <param name="outline">A pointer to the source target.</param>
		/// <param name="funcInterface">A table of ‘emitters’, i.e., function pointers called during decomposition to indicate path operations.</param>
		/// <param name="user">A typeless pointer which is passed to each emitter during the decomposition. It can be used to store the state during the decomposition.</param>
		public static void OutlineDecompose(Outline outline, OutlineFuncs funcInterface, IntPtr user)
		{
			//TODO cleanup/move to the outlinefuncs class?
			IntPtr funcInterfaceRef = Marshal.AllocHGlobal(OutlineFuncsRec.SizeInBytes);
			Marshal.WriteIntPtr(funcInterfaceRef, Marshal.GetFunctionPointerForDelegate(funcInterface.MoveFunction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "line_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.LineFuction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "conic_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.ConicFunction));
			Marshal.WriteIntPtr(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "cubic_to"), Marshal.GetFunctionPointerForDelegate(funcInterface.CubicFunction));

			Marshal.WriteInt32(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "shift"), funcInterface.Shift);
			Marshal.WriteInt32(funcInterfaceRef, (int)Marshal.OffsetOf(typeof(OutlineFuncsRec), "delta"), funcInterface.Delta);

			Error err = FT_Outline_Decompose(outline.reference, funcInterfaceRef, user);

			Marshal.FreeHGlobal(funcInterfaceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Return an outline's ‘control box’. The control box encloses all the
		/// outline's points, including Bézier control points. Though it
		/// coincides with the exact bounding box for most glyphs, it can be
		/// slightly larger in some situations (like when rotating an outline
		/// which contains Bézier outside arcs).
		/// </para><para>
		/// Computing the control box is very fast, while getting the bounding
		/// box can take much more time as it needs to walk over all segments
		/// and arcs in the outline. To get the latter, you can use the
		/// ‘ftbbox’ component which is dedicated to this single task.
		/// </para></summary>
		/// <remarks>
		/// See <see cref="FT.GlyphGetCBox"/> for a discussion of tricky fonts.
		/// </remarks>
		/// <param name="outline">A pointer to the source outline descriptor.</param>
		/// <returns>The outline's control box.</returns>
		public static BBox OutlineGetCBox(Outline outline)
		{
			IntPtr cboxRef;

			FT_Outline_Get_CBox(outline.reference, out cboxRef);

			return new BBox(cboxRef);
		}

		/// <summary>
		/// Render an outline within a bitmap. The outline's image is simply
		/// OR-ed to the target bitmap.
		/// </summary>
		/// <remarks><para>
		/// This function does NOT CREATE the bitmap, it only renders an
		/// outline image within the one you pass to it! Consequently, the
		/// various fields in ‘abitmap’ should be set accordingly.
		/// </para><para>
		/// It will use the raster corresponding to the default glyph format.
		/// </para><para>
		/// The value of the ‘num_grays’ field in ‘abitmap’ is ignored. If you
		/// select the gray-level rasterizer, and you want less than 256 gray
		/// levels, you have to use <see cref="FT.OutlineRender"/> directly.
		/// </para></remarks>
		/// <param name="library">A handle to a FreeType library object.</param>
		/// <param name="outline">A pointer to the source outline descriptor.</param>
		/// <param name="bitmap">A pointer to the target bitmap descriptor.</param>
		public static void OutlineGetBitmap(Library library, Outline outline, FTBitmap bitmap)
		{
			Error err = FT_Outline_Get_Bitmap(library.reference, outline.reference, bitmap.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Render an outline within a bitmap using the current scan-convert.
		/// This function uses an <see cref="RasterParams"/> structure as an
		/// argument, allowing advanced features like direct composition,
		/// translucency, etc.
		/// </summary>
		/// <remarks><para>
		/// You should know what you are doing and how
		/// <see cref="RasterParams"/> works to use this function.
		/// </para><para>
		/// The field ‘params.source’ will be set to ‘outline’ before the scan
		/// converter is called, which means that the value you give to it is
		/// actually ignored.
		/// </para><para>
		/// The gray-level rasterizer always uses 256 gray levels. If you want
		/// less gray levels, you have to provide your own span callback. See
		/// the <see cref="RasterFlags.Direct"/> value of the ‘flags’ field in
		/// the <see cref="RasterParams"/> structure for more details.
		/// </para></remarks>
		/// <param name="library">A handle to a FreeType library object.</param>
		/// <param name="outline">A pointer to the source outline descriptor.</param>
		/// <param name="params">A pointer to an <see cref="RasterParams"/> structure used to describe the rendering operation.</param>
		public static void OutlineRender(Library library, Outline outline, RasterParams @params)
		{
			Error err = FT_Outline_Render(library.reference, outline.reference, @params.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// This function analyzes a glyph outline and tries to compute its
		/// fill orientation (see <see cref="Orientation"/>). This is done by
		/// computing the direction of each global horizontal and/or vertical
		/// extrema within the outline.
		/// </para><para>
		/// Note that this will return <see cref="Orientation.TrueType"/> for
		/// empty outlines.
		/// </para></summary>
		/// <param name="outline">A handle to the source outline.</param>
		/// <returns>The orientation.</returns>
		public static Orientation OutlineGetOrientation(Outline outline)
		{
			return FT_Outline_Get_Orientation(outline.reference);
		}

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
			Error err = FT_Get_Advance(face.Reference, glyphIndex, flags, out padvance);

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
			Error err = FT_Get_Advances(face.Reference, start, count, flags, out advPtr);

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

		/// <summary>
		/// Initialize a pointer to an <see cref="FTBitmap"/> structure.
		/// </summary>
		/// <returns>A pointer to the bitmap structure.</returns>
		public static FTBitmap BitmapNew()
		{
			IntPtr bitmapRef;
			FT_Bitmap_New(out bitmapRef);

			return new FTBitmap(bitmapRef);
		}

		/// <summary>
		/// Copy a bitmap into another one.
		/// </summary>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="source">A handle to the source bitmap.</param>
		/// <returns>A handle to the target bitmap.</returns>
		public static FTBitmap BitmapCopy(Library library, FTBitmap source)
		{
			IntPtr bitmapRef;
			Error err = FT_Bitmap_Copy(library.reference, source.reference, out bitmapRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTBitmap(bitmapRef);
		}

		/// <summary>
		/// Embolden a bitmap. The new bitmap will be about ‘xStrength’ pixels
		/// wider and ‘yStrength’ pixels higher. The left and bottom borders
		/// are kept unchanged.
		/// </summary>
		/// <remarks><para>
		/// The current implementation restricts ‘xStrength’ to be less than or
		/// equal to 8 if bitmap is of pixel_mode <see cref="PixelMode.Mono"/>.
		/// </para><para>
		/// If you want to embolden the bitmap owned by a
		/// <see cref="GlyphSlot"/>, you should call
		/// <see cref="FT.GlyphSlotOwnBitmap"/> on the slot first.
		/// </para></remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="bitmap">A handle to the target bitmap.</param>
		/// <param name="xStrength">How strong the glyph is emboldened horizontally. Expressed in 26.6 pixel format.</param>
		/// <param name="yStrength">How strong the glyph is emboldened vertically. Expressed in 26.6 pixel format.</param>
		public static void BitmapEmbolden(Library library, FTBitmap bitmap, int xStrength, int yStrength)
		{
			Error err = FT_Bitmap_Embolden(library.reference, bitmap.reference, xStrength, yStrength);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Convert a bitmap object with depth 1bpp, 2bpp, 4bpp, or 8bpp to a
		/// bitmap object with depth 8bpp, making the number of used bytes per
		/// line (a.k.a. the ‘pitch’) a multiple of ‘alignment’.
		/// </summary>
		/// <remarks><para>
		/// It is possible to call <see cref="FT.BitmapConvert"/> multiple
		/// times without calling <see cref="FT.BitmapDone"/> (the memory is
		/// simply reallocated).
		/// </para><para>
		/// Use <see cref="BitmapDone"/> to finally remove the bitmap object.
		/// </para><para>
		/// The ‘library’ argument is taken to have access to FreeType's memory
		/// handling functions.
		/// </para></remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="source">The source bitmap.</param>
		/// <param name="alignment">The pitch of the bitmap is a multiple of this parameter. Common values are 1, 2, or 4.</param>
		/// <returns>The target bitmap.</returns>
		public static FTBitmap BitmapConvert(Library library, FTBitmap source, int alignment)
		{
			IntPtr bitmapRef;
			Error err = FT_Bitmap_Convert(library.reference, source.reference, out bitmapRef, alignment);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTBitmap(bitmapRef);
		}

		/// <summary>
		/// Make sure that a glyph slot owns ‘slot->bitmap’.
		/// </summary>
		/// <remarks>
		/// This function is to be used in combination with
		/// <see cref="FT.BitmapEmbolden"/>.
		/// </remarks>
		/// <param name="slot">The glyph slot.</param>
		public static void GlyphSlotOwnBitmap(GlyphSlot slot)
		{
			Error err = FT_GlyphSlot_Own_Bitmap(slot.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Destroy a bitmap object created with <see cref="FT.BitmapNew"/>.
		/// </summary>
		/// <remarks>
		/// The ‘library’ argument is taken to have access to FreeType's memory
		/// handling functions.
		/// </remarks>
		/// <param name="library">A handle to a library object.</param>
		/// <param name="bitmap">The bitmap object to be freed.</param>
		public static void BitmapDone(Library library, FTBitmap bitmap)
		{
			Error err = FT_Bitmap_Done(library.reference, bitmap.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region Glyph Stroker

		/// <summary>
		/// Retrieve the <see cref="StrokerBorder"/> value corresponding to the
		/// ‘inside’ borders of a given outline.
		/// </summary>
		/// <param name="outline">The source outline handle.</param>
		/// <returns>The border index. <see cref="StrokerBorder.Right"/> for empty or invalid outlines.</returns>
		public static StrokerBorder OutlineGetInsideBorder(Outline outline)
		{
			return FT_Outline_GetInsideBorder(outline.reference);
		}

		/// <summary>
		/// Retrieve the <see cref="StrokerBorder"/> value corresponding to the
		/// ‘outside’ borders of a given outline.
		/// </summary>
		/// <param name="outline">The source outline handle.</param>
		/// <returns>The border index. <see cref="StrokerBorder.Left"/> for empty or invalid outlines.</returns>
		public static StrokerBorder OutlineGetOutsideBorder(Outline outline)
		{
			return FT_Outline_GetOutsideBorder(outline.reference);
		}

		/// <summary>
		/// Create a new stroker object.
		/// </summary>
		/// <param name="library">FreeType library handle.</param>
		/// <returns>A new stroker object handle. NULL in case of error.</returns>
		public static Stroker StrokerNew(Library library)
		{
			IntPtr strokerRef;
			Error err = FT_Stroker_New(library.reference, out strokerRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Stroker(strokerRef);
		}

		/// <summary>
		/// Reset a stroker object's attributes.
		/// </summary>
		/// <remarks>
		/// The radius is expressed in the same units as the outline
		/// coordinates.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="radius">The border radius.</param>
		/// <param name="lineCap">The line cap style.</param>
		/// <param name="lineJoin">The line join style.</param>
		/// <param name="miterLimit">The miter limit for the <see cref="StrokerLineJoin.MiterFixed"/> and <see cref="StrokerLineJoin.MiterVariable"/> line join styles, expressed as 16.16 fixed point value.</param>
		public static void StrokerSet(Stroker stroker, int radius, StrokerLineCap lineCap, StrokerLineJoin lineJoin, int miterLimit)
		{
			FT_Stroker_Set(stroker.reference, radius, lineCap, lineJoin, miterLimit);
		}

		/// <summary>
		/// Reset a stroker object without changing its attributes. You should
		/// call this function before beginning a new series of calls to
		/// <see cref="StrokerBeginSubPath"/> or
		/// <see cref="StrokerEndSubPath"/>.
		/// </summary>
		/// <param name="stroker">The target stroker handle.</param>
		public static void StrokerRewind(Stroker stroker)
		{
			FT_Stroker_Rewind(stroker.reference);
		}
		
		/// <summary>
		/// A convenience function used to parse a whole outline with the
		/// stroker. The resulting outline(s) can be retrieved later by
		/// functions like <see cref="StrokerGetCounts"/> and
		/// <see cref="StrokerExport"/>.
		/// </summary>
		/// <remarks><para>
		/// If ‘opened’ is 0 (the default), the outline is treated as a closed
		/// path, and the stroker generates two distinct ‘border’ outlines.
		/// </para><para>
		/// If ‘opened’ is 1, the outline is processed as an open path, and the
		/// stroker generates a single ‘stroke’ outline.
		/// </para><para>
		/// This function calls <see cref="StrokerRewind"/> automatically.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="outline">The source outline.</param>
		/// <param name="opened">A boolean. If 1, the outline is treated as an open path instead of a closed one.</param>
		public static void StrokerParseOutline(Stroker stroker, Outline outline, bool opened)
		{
			Error err = FT_Stroker_ParseOutline(stroker.reference, outline.reference, opened);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Start a new sub-path in the stroker.
		/// </summary>
		/// <remarks>
		/// This function is useful when you need to stroke a path that is not
		/// stored as an <see cref="Outline"/> object.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="to">A pointer to the start vector.</param>
		/// <param name="open">A boolean. If 1, the sub-path is treated as an open one.</param>
		public static void StrokerBeginSubPath(Stroker stroker, FTVector to, bool open)
		{
			Error err = FT_Stroker_BeginSubPath(stroker.reference, to.reference, open);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Close the current sub-path in the stroker.
		/// </summary>
		/// <remarks>
		/// You should call this function after
		/// <see cref="StrokerBeginSubPath"/>. If the subpath was not ‘opened’,
		/// this function ‘draws’ a single line segment to the start position
		/// when needed.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		public static void StrokerEndSubPath(Stroker stroker)
		{
			Error err = FT_Stroker_EndSubPath(stroker.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single line segment in the stroker's current sub-path,
		/// from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between
		/// <see cref="StrokerBeginSubPath"/> and
		/// <see cref="StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerLineTo(Stroker stroker, FTVector to)
		{
			Error err = FT_Stroker_LineTo(stroker.reference, to.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single quadratic Bézier in the stroker's current sub-path,
		/// from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between
		/// <see cref="StrokerBeginSubPath"/> and
		/// <see cref="StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="control">A pointer to a Bézier control point.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerConicTo(Stroker stroker, FTVector control, FTVector to)
		{
			Error err = FT_Stroker_ConicTo(stroker.reference, control.reference, to.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// ‘Draw’ a single cubic Bézier in the stroker's current sub-path,
		/// from the last position.
		/// </summary>
		/// <remarks>
		/// You should call this function between
		/// <see cref="StrokerBeginSubPath"/> and
		/// <see cref="StrokerEndSubPath"/>.
		/// </remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="control1">A pointer to the first Bézier control point.</param>
		/// <param name="control2">A pointer to second Bézier control point.</param>
		/// <param name="to">A pointer to the destination point.</param>
		public static void StrokerCubicTo(Stroker stroker, FTVector control1, FTVector control2, FTVector to)
		{
			Error err = FT_Stroker_CubicTo(stroker.reference, control1.reference, control2.reference, to.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Call this function once you have finished parsing your paths with
		/// the stroker. It returns the number of points and contours necessary
		/// to export one of the ‘border’ or ‘stroke’ outlines generated by the
		/// stroker.
		/// </summary>
		/// <remarks><para>
		/// When an outline, or a sub-path, is ‘closed’, the stroker generates
		/// two independent ‘border’ outlines, named ‘left’ and ‘right’.
		/// </para><para>
		/// When the outline, or a sub-path, is ‘opened’, the stroker merges
		/// the ‘border’ outlines with caps. The ‘left’ border receives all
		/// points, while the ‘right’ border becomes empty.
		/// </para><para>
		/// Use the function <see cref="StrokerGetCounts"/> instead if you want
		/// to retrieve the counts associated to both borders.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="border">The border index.</param>
		/// <param name="pointsCount">The number of points.</param>
		/// <param name="contoursCount">The number of contours.</param>
		[CLSCompliant(false)]
		public static void StrokerGetBorderCounts(Stroker stroker, StrokerBorder border, out uint pointsCount, out uint contoursCount)
		{
			Error err = FT_Stroker_GetBorderCounts(stroker.reference, border, out pointsCount, out contoursCount);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Call this function after <see cref="StrokerGetBorderCounts"/> to
		/// export the corresponding border to your own <see cref="Outline"/>
		/// structure.
		/// </para><para>
		/// Note that this function appends the border points and contours to
		/// your outline, but does not try to resize its arrays.
		/// </para></summary>
		/// <remarks><para>
		/// Always call this function after
		/// <see cref="StrokerGetBorderCounts"/> to get sure that there is
		/// enough room in your <see cref="Outline"/> object to receive all new
		/// data.
		/// </para><para>
		/// When an outline, or a sub-path, is ‘closed’, the stroker generates
		/// two independent ‘border’ outlines, named ‘left’ and ‘right’
		/// </para><para>
		/// When the outline, or a sub-path, is ‘opened’, the stroker merges
		/// the ‘border’ outlines with caps. The ‘left’ border receives all
		/// points, while the ‘right’ border becomes empty.
		/// </para><para>
		/// Use the function <see cref="StrokerExport"/> instead if you want to
		/// retrieve all borders at once.
		/// </para></remarks>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="border">The border index.</param>
		/// <param name="outline">The target outline handle.</param>
		public static void StrokerExportBorder(Stroker stroker, StrokerBorder border, Outline outline)
		{
			FT_Stroker_ExportBorder(stroker.reference, border, outline.reference);
		}

		/// <summary>
		/// Call this function once you have finished parsing your paths with
		/// the stroker. It returns the number of points and contours necessary
		/// to export all points/borders from the stroked outline/path.
		/// </summary>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="pointsCount">The number of points.</param>
		/// <param name="contoursCount">The number of contours.</param>
		[CLSCompliant(false)]
		public static void StrokerGetCounts(Stroker stroker, out uint pointsCount, out uint contoursCount)
		{
			Error err = FT_Stroker_GetCounts(stroker.reference, out pointsCount, out contoursCount);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// Call this function after <see cref="StrokerGetBorderCounts"/> to
		/// export all borders to your own <see cref="Outline"/> structure.
		/// </para><para>
		/// Note that this function appends the border points and contours to
		/// your outline, but does not try to resize its arrays.
		/// </para></summary>
		/// <param name="stroker">The target stroker handle.</param>
		/// <param name="outline">The target outline handle.</param>
		public static void StrokerExport(Stroker stroker, Outline outline)
		{
			FT_Stroker_Export(stroker.reference, outline.reference);
		}

		/// <summary>
		/// Destroy a stroker object.
		/// </summary>
		/// <param name="stroker">A stroker handle. Can be NULL.</param>
		public static void StrokerDone(Stroker stroker)
		{
			FT_Stroker_Done(stroker.reference);
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
			IntPtr sourceRef = glyph.reference;

			Error err = FT_Glyph_Stroke(ref sourceRef, stroker.reference, destroy);

			if (destroy)
			{
				//TODO when Glyph implements IDisposable, dispose the glyph.
			}

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			if (sourceRef == glyph.reference)
				return glyph;
			else
				return new Glyph(sourceRef, glyph.Library);
		}

		/// <summary>
		/// Stroke a given outline glyph object with a given stroker, but only
		/// return either its inside or outside border.
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
			IntPtr sourceRef = glyph.reference;

			Error err = FT_Glyph_StrokeBorder(ref sourceRef, stroker.reference, inside, destroy);

			if (destroy)
			{
				//TODO when Glyph implements IDisposable, dispose the glyph.
			}

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			if (sourceRef == glyph.reference)
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
		/// An error will be returned if a module already exists by that name,
		/// or if the module requires a version of FreeType that is too great.
		/// </remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="clazz">A pointer to class descriptor for the module.</param>
		public static void AddModule(Library library, ModuleClass clazz)
		{
			Error err = FT_Add_Module(library.reference, clazz.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Find a module by its name.
		/// </summary>
		/// <remarks>
		/// FreeType's internal modules aren't documented very well, and you
		/// should look up the source code for details.
		/// </remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="moduleName">The module's name (as an ASCII string).</param>
		/// <returns>A module handle. 0 if none was found.</returns>
		public static Module GetModule(Library library, string moduleName)
		{
			return new Module(FT_Get_Module(library.reference, moduleName));
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
			Error err = FT_Remove_Module(library.reference, module.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// A counter gets initialized to 1 at the time a
		/// <see cref="Library"/> structure is created. This function
		/// increments the counter. <see cref="FT.DoneLibrary"/> then only
		/// destroys a library if the counter is 1, otherwise it simply
		/// decrements the counter.
		/// </para><para>
		/// This function helps in managing life-cycles of structures which
		/// reference <see cref="Library"/> objects.
		/// </para></summary>
		/// <param name="library">A handle to a target library object.</param>
		internal static void ReferenceLibrary(Library library)
		{
			//marked as internal because the Library class wraps this funcitonality.
			Error err = FT_Reference_Library(library.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary><para>
		/// This function is used to create a new FreeType library instance
		/// from a given memory object. It is thus possible to use libraries
		/// with distinct memory allocators within the same program.
		/// </para><para>
		/// Normally, you would call this function (followed by a call to
		/// <see cref="FT.AddDefaultModules"/> or a series of calls to
		/// <see cref="FT.AddModule"/>) instead of
		/// <see cref="FT.InitFreeType"/> to initialize the FreeType library.
		/// </para><para>
		/// Don't use <see cref="FT.DoneFreeType"/> but
		/// <see cref="FT.DoneLibrary"/> to destroy a library instance.
		/// </para></summary>
		/// <remarks>
		/// See the discussion of reference counters in the description of
		/// <see cref="FT.ReferenceLibrary"/>.
		/// </remarks>
		/// <param name="memory">A handle to the original memory object.</param>
		/// <returns>A pointer to handle of a new library object.</returns>
		public static Library NewLibrary(Memory memory)
		{
			IntPtr libraryRef;
			Error err = FT_New_Library(memory.reference, out libraryRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Library(libraryRef, false);
		}

		/// <summary>
		/// Discard a given library object. This closes all drivers and
		/// discards all resource objects.
		/// </summary>
		/// <remarks>
		/// See the discussion of reference counters in the description of
		/// <see cref="FT.ReferenceLibrary"/>.
		/// </remarks>
		/// <param name="library">A handle to the target library.</param>
		public static void DoneLibrary(Library library)
		{
			Error err = FT_Done_Library(library.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Set a debug hook function for debugging the interpreter of a font
		/// format.
		/// </summary>
		/// <remarks><para>
		/// Currently, four debug hook slots are available, but only two (for
		/// the TrueType and the Type 1 interpreter) are defined.
		/// </para><para>
		/// Since the internal headers of FreeType are no longer installed, the
		/// symbol ‘FT_DEBUG_HOOK_TRUETYPE’ isn't available publicly. This is a
		/// bug and will be fixed in a forthcoming release.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="hookIndex">The index of the debug hook. You should use the values defined in ‘ftobjs.h’, e.g., ‘FT_DEBUG_HOOK_TRUETYPE’.</param>
		/// <param name="debugHook">The function used to debug the interpreter.</param>
		[CLSCompliant(false)]
		public static void SetDebugHook(Library library, uint hookIndex, IntPtr debugHook)
		{
			FT_Set_Debug_Hook(library.reference, hookIndex, debugHook);
		}

		/// <summary>
		/// Add the set of default drivers to a given library object. This is
		/// only useful when you create a library object with
		/// <see cref="FT.NewLibrary"/> (usually to plug a custom memory
		/// manager).
		/// </summary>
		/// <param name="library">A handle to a new library object.</param>
		public static void AddDefaultModules(Library library)
		{
			FT_Add_Default_Modules(library.reference);
		}

		/// <summary>
		/// Retrieve the current renderer for a given glyph format.
		/// </summary>
		/// <remarks><para>
		/// An error will be returned if a module already exists by that name,
		/// or if the module requires a version of FreeType that is too great.
		/// </para><para>
		/// To add a new renderer, simply use <see cref="FT.AddModule"/>. To
		/// retrieve a renderer by its name, use <see cref="FT.GetModule"/>.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="format">The glyph format.</param>
		/// <returns>A renderer handle. 0 if none found.</returns>
		[CLSCompliant(false)]
		public static Renderer GetRenderer(Library library, GlyphFormat format)
		{
			return new Renderer(FT_Get_Renderer(library.reference, format));
		}

		/// <summary>
		/// Set the current renderer to use, and set additional mode.
		/// </summary>
		/// <remarks><para>
		/// In case of success, the renderer will be used to convert glyph
		/// images in the renderer's known format into bitmaps.
		/// </para><para>
		/// This doesn't change the current renderer for other formats.
		/// </para><para>
		/// Currently, only the B/W renderer, if compiled with
		/// FT_RASTER_OPTION_ANTI_ALIASING (providing a 5-levels anti-aliasing
		/// mode; this option must be set directly in ‘ftraster.c’ and is
		/// undefined by default) accepts a single tag ‘pal5’ to set its gray
		/// palette as a character string with 5 elements. Consequently, the
		/// third and fourth argument are zero normally.
		/// </para></remarks>
		/// <param name="library">A handle to the library object.</param>
		/// <param name="renderer">A handle to the renderer object.</param>
		/// <param name="numParams">The number of additional parameters.</param>
		/// <param name="parameters">Additional parameters.</param>
		[CLSCompliant(false)]
		public unsafe static void SetRenderer(Library library, Renderer renderer, uint numParams, Parameter[] parameters)
		{
			ParameterRec[] paramRecs = Array.ConvertAll<Parameter, ParameterRec>(parameters, (p => p.rec));
			fixed (void* ptr = paramRecs)
			{
				Error err = FT_Set_Renderer(library.reference, renderer.reference, numParams, (IntPtr)ptr);
			}
		}

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
		public static void StreamOpenGzip(FTStream stream, FTStream source)
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
		public static void StreamOpenLZW(FTStream stream, FTStream source)
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
		public static void StreamOpenBzip2(FTStream stream, FTStream source)
		{
			Error err = FT_Stream_OpenBzip2(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LCD Filtering

		/// <summary>
		/// This function is used to apply color filtering to LCD decimated
		/// bitmaps, like the ones used when calling
		/// <see cref="FT.RenderGlyph"/> with <see cref="RenderMode.LCD"/> or
		/// <see cref="RenderMode.VerticalLCD"/>.
		/// </summary>
		/// <remarks><para>
		/// This feature is always disabled by default. Clients must make an
		/// explicit call to this function with a ‘filter’ value other than
		/// <see cref="LcdFilter.None"/> in order to enable it.
		/// </para><para>
		/// Due to <b>PATENTS</b> covering subpixel rendering, this function
		/// doesn't do anything except returning
		/// <see cref="Error.UnimplementedFeature"/> if the configuration macro
		/// FT_CONFIG_OPTION_SUBPIXEL_RENDERING is not defined in your build of
		/// the library, which should correspond to all default builds of
		/// FreeType.
		/// </para><para>
		/// The filter affects glyph bitmaps rendered through
		/// <see cref="FT.RenderGlyph"/>, <see cref="FT.OutlineGetBitmap"/>,
		/// <see cref="FT.LoadGlyph"/>, and <see cref="FT.LoadChar"/>.
		/// </para><para>
		/// It does not affect the output of <see cref="FT.OutlineRender"/> and
		/// <see cref="FT.OutlineGetBitmap"/>.
		/// </para><para>
		/// If this feature is activated, the dimensions of LCD glyph bitmaps
		/// are either larger or taller than the dimensions of the
		/// corresponding outline with regards to the pixel grid. For example,
		/// for <see cref="RenderMode.LCD"/>, the filter adds up to 3 pixels to
		/// the left, and up to 3 pixels to the right.
		/// </para><para>
		/// The bitmap offset values are adjusted correctly, so clients
		/// shouldn't need to modify their layout and glyph positioning code
		/// when enabling the filter.
		/// </para></remarks>
		/// <param name="library">A handle to the target library instance.</param>
		/// <param name="filter"><para>The filter type.
		/// </para><para>
		/// You can use <see cref="LcdFilter.None"/> here to disable this feature, or <see cref="LcdFilter.Default"/> to use a default filter that should work well on most LCD screens.</para></param>
		public static void LibrarySetLcdFilter(Library library, LcdFilter filter)
		{
			Error err = FT_Library_SetLcdFilter(library.reference, filter);

			//TODO since LCD Filtering isn't enabled by default, catch the EntryPointNotFoundException and throw an exception telling the user to recompile freetype with the proper #define.

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Use this function to override the filter weights selected by
		/// <see cref="FT.LibrarySetLcdFilter"/>. By default, FreeType uses the
		/// quintuple (0x00, 0x55, 0x56, 0x55, 0x00) for
		/// <see cref="LcdFilter.Light"/>, and (0x10, 0x40, 0x70, 0x40, 0x10)
		/// for <see cref="LcdFilter.Default"/> and
		/// <see cref="LcdFilter.Legacy"/>.
		/// </summary>
		/// <remarks><para>
		/// Due to <b>PATENTS</b> covering subpixel rendering, this function
		/// doesn't do anything except returning
		/// <see cref="Error.UnimplementedFeature"/> if the configuration macro
		/// FT_CONFIG_OPTION_SUBPIXEL_RENDERING is not defined in your build of
		/// the library, which should correspond to all default builds of
		/// FreeType.
		/// </para><para>
		/// This function must be called after
		/// <see cref="FT.LibrarySetLcdFilter"/> to have any effect.
		/// </para></remarks>
		/// <param name="library">A handle to the target library instance.</param>
		/// <param name="weights">A pointer to an array; the function copies the first five bytes and uses them to specify the filter weights.</param>
		public static void LibrarySetLcdFilterWeights(Library library, byte[] weights)
		{
			Error err = FT_Library_SetLcdFilterWeights(library.reference, weights);

			//TODO since LCD Filtering isn't enabled by default, catch the EntryPointNotFoundException and throw an exception telling the user to recompile freetype with the proper #define.

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion
	}
}
