using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// Read-only binary data represented as a pointer and a length.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Data
	{
		/// <summary>
		/// The data.
		/// </summary>
		public readonly IntPtr Pointer;

		/// <summary>
		/// The length of the data in bytes.
		/// </summary>
		public readonly int Length;
	}
}
