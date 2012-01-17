using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// This structure is used to describe an outline to the scan-line converter.
	/// </summary>
	/// <remarks>
	/// The B/W rasterizer only checks bit 2 in the ‘tags’ array for the first point of each contour. The drop-out mode as given with FT_OUTLINE_IGNORE_DROPOUTS, FT_OUTLINE_SMART_DROPOUTS, and FT_OUTLINE_INCLUDE_STUBS in ‘flags’ is then overridden.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct Outline
	{
		/// <summary>
		/// The number of contours in the outline.
		/// </summary>
		public short ContoursCount;

		/// <summary>
		/// The number of points in the outline.
		/// </summary>
		public short PointsCount;

		/// <summary>
		/// A pointer to an array of ‘PointsCount’ FT_Vector elements, giving the outline's point coordinates.
		/// </summary>
		public IntPtr Points;

		/// <summary>
		/// A pointer to an array of ‘PointsCount’ chars, giving each outline point's type.
		/// If bit 0 is unset, the point is ‘off’ the curve, i.e., a Bézier control point, while it is ‘on’ if set.
		/// Bit 1 is meaningful for ‘off’ points only. If set, it indicates a third-order Bézier arc control point; and a second-order control point if unset.
		/// If bit 2 is set, bits 5-7 contain the drop-out mode (as defined in the OpenType specification; the value is the same as the argument to the SCANMODE instruction).
		/// Bits 3 and 4 are reserved for internal purposes.
		/// </summary>
		public IntPtr Tags;

		/// <summary>
		/// An array of ‘ContoursCount’ shorts, giving the end point of each contour within the outline. For example, the first contour is defined by the points ‘0’ to ‘Contours[0]’, the second one is defined by the points ‘Contours[0]+1’ to ‘Contours[1]’, etc.
		/// </summary>
		public IntPtr Contours;

		/// <summary>
		/// A set of bit flags used to characterize the outline and give hints to the scan-converter and hinter on how to convert/grid-fit it. See FT_OUTLINE_FLAGS.
		/// </summary>
		public OutlineFlags Flags;
	}
}
