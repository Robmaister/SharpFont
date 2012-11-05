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

namespace SharpFont.Internal
{
	internal sealed class SpanMarshaler : ICustomMarshaler
	{
		private static readonly SpanMarshaler Instance = new SpanMarshaler();

		private SpanMarshaler()
		{
		}

		public static ICustomMarshaler GetInstance(string cookie)
		{
			return Instance;
		}

		public void CleanUpManagedData(object managedObj)
		{
			if (managedObj == null)
				throw new ArgumentNullException("ManagedObj");

			if (managedObj.GetType() != typeof(Span))
				throw new ArgumentException("Managed object is not a Span.");
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
			//Do nothing.
		}

		public int GetNativeDataSize()
		{
			return SpanRec.SizeInBytes;
		}

		public IntPtr MarshalManagedToNative(object managedObj)
		{
			if (managedObj == null)
				throw new ArgumentNullException("ManagedObj");

			if (managedObj.GetType() != typeof(Span))
				throw new ArgumentException("Managed object is not a Span.");

			//TODO if we have any setters in ListNode, marshal them.
			return ((Span)managedObj).Reference;
		}

		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return new Span(pNativeData);
		}
	}
}
