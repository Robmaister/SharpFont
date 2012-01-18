using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A structure used to model a size request.
	/// </summary>
	/// <remarks>
	/// If <see cref="Width"/> is zero, then the horizontal scaling value is set equal to the vertical scaling value, and vice versa.
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct SizeRequest
	{
		/// <summary>
		/// See <see cref="SizeRequestType"/>.
		/// </summary>
		SizeRequestType RequestType;

		/// <summary>
		/// The desired width.
		/// </summary>
		int Width;

		/// <summary>
		/// The desired height.
		/// </summary>
		int Height;

		/// <summary>
		/// The horizontal resolution. If set to zero, <see cref="Width"/> is treated as a 26.6 fractional pixel value.
		/// </summary>
		uint HorizontalResolution;

		/// <summary>
		/// The horizontal resolution. If set to zero, <see cref="Height"/> is treated as a 26.6 fractional pixel value.
		/// </summary>
		uint VerticalResolution;
	}
}
