using System;
using System.Runtime.InteropServices;

namespace SharpFont
{
	/// <summary>
	/// Describe a function used to destroy the ‘client’ data of any FreeType
	/// object. See the description of the <see cref="Generic"/> type for 
	/// details of usage.
	/// </summary>
	/// <param name="object">The address of the FreeType object which is under finalization. Its client data is accessed through its ‘generic’ field.</param>
	public delegate void GenericFinalizer(IntPtr @object);

	/// <summary>
	/// Client applications often need to associate their own data to a variety
	/// of FreeType core objects. For example, a text layout API might want to
	/// associate a glyph cache to a given size object.
	/// 
	/// Most FreeType object contains a ‘generic’ field, of type FT_Generic, 
	/// which usage is left to client applications and font servers.
	/// 
	/// It can be used to store a pointer to client-specific data, as well as
	/// the address of a ‘finalizer’ function, which will be called by FreeType
	/// when the object is destroyed (for example, the previous client example
	/// would put the address of the glyph cache destructor in the ‘finalizer’
	/// field).
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct Generic
	{
		/// <summary>
		/// A typeless pointer to any client-specified data. This field is 
		/// completely ignored by the FreeType library.
		/// </summary>
		public IntPtr Data;

		/// <summary>
		/// A pointer to a ‘generic finalizer’ function, which will be called
		/// when the object is destroyed. If this field is set to NULL, no code
		/// will be called.
		/// </summary>
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public IntPtr Finalizer;

		public Generic(IntPtr data, GenericFinalizer finalizer)
			: this()
		{
			Data = data;
			Finalizer = Marshal.GetFunctionPointerForDelegate(finalizer);
		}
	}
}
