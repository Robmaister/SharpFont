using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A structure used to model a single span of gray (or black) pixels when rendering a monochrome or anti-aliased bitmap.
	/// </summary>
	/// <remarks>
	/// This structure is used by the span drawing callback type named FT_SpanFunc which takes the y coordinate of the span as a a parameter.
	/// The coverage value is always between 0 and 255. If you want less gray values, the callback function has to reduce them.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct Span
	{
		/// <summary>
		/// The span's horizontal start position.
		/// </summary>
		public short x;

		/// <summary>
		/// The span's length in pixels.
		/// </summary>
		public ushort len;

		/// <summary>
		/// The span color/coverage, ranging from 0 (background) to 255 (foreground). Only used for anti-aliased rendering.
		/// </summary>
		public byte coverage;
	}
}
