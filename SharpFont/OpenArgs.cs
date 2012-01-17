using System;
using System.Runtime.InteropServices;

//TODO replace IntPtr with proper struct references.

namespace SharpFont
{
	[StructLayout(LayoutKind.Sequential)]
	public struct OpenArgs
	{
		public OpenFlags Flags;
		public IntPtr MemoryBase;
		public int MemorySize;
		public string Pathname;
		public IntPtr Stream;
		public IntPtr Driver;
		public int ParamsCount;
		public IntPtr Params;
	}
}
