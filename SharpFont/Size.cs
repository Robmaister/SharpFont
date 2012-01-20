using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// FreeType root size class structure. A size object models a face object
	/// at a given size.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Size
	{
		/// <summary>
		/// Handle to the parent face object.
		/// </summary>
		public Face *Face;

		/// <summary>
		/// A typeless pointer, which is unused by the FreeType library or any
		/// of its drivers. It can be used by client applications to link their
		/// own data to each size object.
		/// </summary>
		public Generic Generic;

		/// <summary>
		/// Metrics for this size object. This field is read-only.
		/// </summary>
		public SizeMetrics Metrics;

		private IntPtr Internal;
	}
}
