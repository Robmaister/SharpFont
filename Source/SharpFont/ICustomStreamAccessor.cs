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

namespace SharpFont
{
	/// <summary>
	/// Interface for providing access to a custom stream representing a font file.
	/// </summary>
	public interface ICustomStreamAccessor
	{
		/// <summary>
		/// Seeks to a specified position within a stream.
		/// </summary>
		/// <param name="position">Position from the beginning of the stream to seek to.</param>
		/// <returns>The new position within the stream.</returns>
		int Seek(int position);

		/// <summary>
		/// Reads bytes from the stream. The maximum number of bytes to read is specified by the buffer length.
		/// </summary>
		/// <param name="buffer">Buffer to receive read bytes.</param>
		/// <returns>The actual number of bytes read from the stream.</returns>
		int Read(byte[] buffer);

		/// <summary>
		/// Closes the stream. Called by freetype once the font face is destroyed.
		/// </summary>
		void Close();

		/// <summary>
		/// Returns total length of the stream in bytes.
		/// </summary>
		int Length { get; }
	}
}
