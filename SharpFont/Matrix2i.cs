using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to store a 2x2 matrix. Coefficients are in 16.16 fixed float format. The computation performed is:
	///     <code>
	///     x' = x*xx + y*xy
	///     y' = x*yx + y*yy
	///     </code>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix2i
	{
		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XX;

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int XY;

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YX;

		/// <summary>
		/// Matrix coefficient.
		/// </summary>
		public int YY;
	}
}
