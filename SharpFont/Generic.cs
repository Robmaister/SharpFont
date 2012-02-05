#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

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
	public sealed class Generic
	{
		internal IntPtr reference;

		internal Generic(IntPtr reference)
		{
			this.reference = reference;
		}

		internal Generic(IntPtr reference, int offset)
		{
			this.reference = new IntPtr(reference.ToInt64() + offset);
		}

		/// <summary>
		/// Gets the size of a Generic, in bytes.
		/// </summary>
		public static int SizeInBytes
		{
			get
			{
				return IntPtr.Size * 2;
			}
		}

		/// <summary>
		/// A typeless pointer to any client-specified data. This field is 
		/// completely ignored by the FreeType library.
		/// </summary>
		public IntPtr Data
		{
			get
			{
				return Marshal.ReadIntPtr(reference, 0);
			}

			set
			{
				Marshal.WriteIntPtr(reference, 0, value);
			}
		}

		/// <summary>
		/// A pointer to a <see cref="GenericFinalizer"/> function, which will
		/// be called when the object is destroyed. If this field is set to
		/// NULL, no code will be called.
		/// </summary>
		public IntPtr Finalizer
		{
			get
			{
				return Marshal.ReadIntPtr(reference,  IntPtr.Size);
			}

			set
			{
				Marshal.WriteIntPtr(reference,  IntPtr.Size, value);
			}
		}

		//TODO overload constructor for different data types.

		/// <summary>
		/// Initializes a new instance of the Generic struct. Useful for
		/// creating proper <see cref="Finalizer"/> function pointers.
		/// </summary>
		/// <param name="data">A typeless pointer to any client-specified data.</param>
		/// <param name="finalizer">A function pointer to be called when the containing object is destroyed.</param>
		public Generic(IntPtr data, GenericFinalizer finalizer)
		{
			Data = data;
			Finalizer = Marshal.GetFunctionPointerForDelegate(finalizer);
		}
	}
}
