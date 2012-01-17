using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to store a 2D vector.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2i
	{
		/// <summary>
		/// The horizontal coordinate.
		/// </summary>
		public int X;

		/// <summary>
		/// The vertical coordinate.
		/// </summary>
		public int Y;
	}
}
