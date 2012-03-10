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
	/// A union type used to store either a long or a pointer. This is used to
	/// store a file descriptor or a ‘FILE*’ in an input stream.
	/// </summary>
	public class StreamDesc
	{
		internal IntPtr reference;
		internal StreamDescRec rec;

		internal StreamDesc(IntPtr reference)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<StreamDescRec>(reference);
		}

		internal StreamDesc(IntPtr reference, IntPtr offset)
			: this(new IntPtr(reference.ToInt64() + offset.ToInt64()))
		{
		}

		public int Value
		{
			get
			{
				return (int)rec.value;
			}
		}

		public IntPtr Pointer
		{
			get
			{
				return rec.pointer;
			}
		}
	}
}
