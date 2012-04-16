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
	/// A function used to seek and read data from a given input stream.
	/// </summary>
	/// <remarks>
	/// This function might be called to perform a seek or skip operation with
	/// a ‘count’ of 0. A non-zero return value then indicates an error.
	/// </remarks>
	/// <param name="stream">A handle to the source stream.</param>
	/// <param name="offset">The offset of read in stream (always from start).</param>
	/// <param name="buffer">The address of the read buffer.</param>
	/// <param name="count">The number of bytes to read from the stream.</param>
	/// <returns>The number of bytes effectively read by the stream.</returns>
	[CLSCompliant(false)]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate uint StreamIOFunc(IntPtr stream, uint offset, IntPtr buffer, uint count);

	/// <summary>
	/// A function used to close a given input stream.
	/// </summary>
	/// <param name="stream">A handle to the target stream.</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void StreamCloseFunc(IntPtr stream);

	/// <summary>
	/// A handle to an input stream.
	/// </summary>
	public sealed class FTStream
	{
		#region Fields

		private IntPtr reference;
		private StreamRec rec;

		#endregion

		#region Constructors

		internal FTStream(IntPtr reference)
		{
			Reference = reference;
		}

		#endregion

		#region Properties

		/// <summary>
		/// For memory-based streams, this is the address of the first stream
		/// byte in memory. This field should always be set to NULL for
		/// disk-based streams.
		/// </summary>
		public IntPtr Base
		{
			get
			{
				return rec.@base;
			}
		}
		
		/// <summary>
		/// The stream size in bytes.
		/// </summary>
		[CLSCompliant(false)]
		public uint Size
		{
			get
			{
				return (uint)rec.size;
			}
		}

		/// <summary>
		/// The current position within the stream.
		/// </summary>
		[CLSCompliant(false)]
		public uint Position
		{
			get
			{
				return (uint)rec.pos;
			}
		}

		/// <summary>
		/// This field is a union that can hold an integer or a pointer. It is
		/// used by stream implementations to store file descriptors or ‘FILE*’
		/// pointers.
		/// </summary>
		public StreamDesc Descriptor
		{
			get
			{
				return new StreamDesc(reference, Marshal.OffsetOf(typeof(StreamRec), "descriptor"));
			}
		}

		/// <summary>
		/// This field is completely ignored by FreeType. However, it is often
		/// useful during debugging to use it to store the stream's filename
		/// (where available).
		/// </summary>
		public StreamDesc PathName
		{
			get
			{
				return new StreamDesc(reference, Marshal.OffsetOf(typeof(StreamRec), "pathname"));
			}
		}

		/// <summary>
		/// The stream's input function.
		/// </summary>
		[CLSCompliant(false)]
		public StreamIOFunc Read
		{
			get
			{
				return rec.read;
			}
		}

		/// <summary>
		/// The stream's close function.
		/// </summary>
		public StreamCloseFunc Close
		{
			get
			{
				return rec.close;
			}
		}

		/// <summary>
		/// The memory manager to use to preload frames. This is set internally
		/// by FreeType and shouldn't be touched by stream implementations.
		/// </summary>
		public Memory Memory
		{
			get
			{
				return new Memory(reference, Marshal.OffsetOf(typeof(StreamRec), "memory"));
			}
		}

		/// <summary>
		/// This field is set and used internally by FreeType when parsing
		/// frames.
		/// </summary>
		public IntPtr Cursor
		{
			get
			{
				return rec.cursor;
			}
		}

		/// <summary>
		/// This field is set and used internally by FreeType when parsing
		/// frames.
		/// </summary>
		public IntPtr Limit
		{
			get
			{
				return rec.limit;
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
				rec = PInvokeHelper.PtrToStructure<StreamRec>(reference);
			}
		}

		#endregion
	}
}
