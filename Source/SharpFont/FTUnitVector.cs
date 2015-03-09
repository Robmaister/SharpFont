using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpFont
{
	/// <summary>
	/// A simple structure used to store a 2D vector unit vector. Uses <see cref="Fixed2Dot14"/> types.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct FTUnitVector
	{
		/// <summary>
		/// Horizontal coordinate.
		/// </summary>
		public Fixed2Dot14 X;

		/// <summary>
		/// Vertical coordinate.
		/// </summary>
		public Fixed2Dot14 Y;
	}
}
