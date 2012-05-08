#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

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

using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// An <see cref="FTList"/> iterator function which is called during a list parse by <see cref="FT.ListIterate"/>.
	/// </summary>
	/// <param name="node">The current iteration list node.</param>
	/// <param name="user">
	/// A typeless pointer passed to <see cref="ListIterator"/>. Can be used to point to the iteration's state.
	/// </param>
	/// <returns>Error code.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate Error ListIterator([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(ListNodeMarshaler))]ListNode node, IntPtr user);

	/// <summary>
	/// An <see cref="FTList"/> iterator function which is called during a list finalization by
	/// <see cref="FT.ListFinalize"/> to destroy all elements in a given list.
	/// </summary>
	/// <param name="memory">The current system object.</param>
	/// <param name="data">The current object to destroy.</param>
	/// <param name="user">
	/// A typeless pointer passed to <see cref="FT.ListIterate"/>. It can be used to point to the iteration's state.
	/// </param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ListDestructor([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryMarshaler))]Memory memory, IntPtr data, IntPtr user);

	/// <summary>
	/// A structure used to hold a simple doubly-linked list. These are used in many parts of FreeType.
	/// </summary>
	public class FTList
	{
		#region Fields

		private IntPtr reference;
		private ListRec rec;

		#endregion

		#region Constructors

		internal FTList(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The head (first element) of doubly-linked list.
		/// </summary>
		public ListNode Head
		{
			get
			{
				return new ListNode(rec.head);
			}
		}

		/// <summary>
		/// The tail (last element) of doubly-linked list.
		/// </summary>
		public ListNode Tail
		{
			get
			{
				return new ListNode(rec.tail);
			}
		}

		internal IntPtr Reference
		{
			get
			{
				return reference;
			}

			set
			{
				reference = value;
				rec = PInvokeHelper.PtrToStructure<ListRec>(reference);
			}
		}

		#endregion
	}
}
