using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A structure used to hold an outline's bounding box, i.e., the coordinates of its extrema in the horizontal and vertical directions.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct BBox
	{
		/// <summary>
		/// The horizontal minimum (left-most).
		/// </summary>
		public int Left;

		/// <summary>
		/// The vertical minimum (bottom-most).
		/// </summary>
		public int Bottom;

		/// <summary>
		/// The horizontal maximum (right-most).
		/// </summary>
		public int Right;

		/// <summary>
		/// The vertical maximum (top-most).
		/// </summary>
		public int Top;
	}
}
