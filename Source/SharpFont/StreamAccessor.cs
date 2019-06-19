#region MIT License
/*Copyright (c) 2015-2016 Robert Rouhani <robert.rouhani@gmail.com>

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
using System.IO;

namespace SharpFont
{
	/// <summary>
	/// Custom font stream accessor based on a generic Stream object.
	/// </summary>
	internal class StreamAccessor : ICustomStreamAccessor
	{
		private Stream _stream;
		private bool _takeStreamOwnership;

		public StreamAccessor(Stream stream, bool takeStreamOwnership)
		{
			if (stream == null)
				throw new ArgumentException("Stream cannot be null", "stream");

			if (!stream.CanRead || !stream.CanSeek)
				throw new ArgumentException("Stream must support reading and seeking", "stream");

			_stream = stream;
			_takeStreamOwnership = takeStreamOwnership;
		}

		public void Close()
		{
			if (_takeStreamOwnership)
			{
				_stream.Dispose();
			}
		}

		public int Read(byte[] buffer)
		{
			return _stream.Read(buffer, 0, buffer.Length);
		}

		public int Seek(int position)
		{
			return (int)_stream.Seek(position, SeekOrigin.Begin);
		}

		public int Length
		{
			get
			{
				return (int)_stream.Length;
			}
		}
	}
}
