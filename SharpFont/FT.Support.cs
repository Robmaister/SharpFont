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

namespace SharpFont
{
	public static partial class FT
	{
		#region Computations

		#endregion

		#region List Processing

		#endregion

		#region Outline Processing

		#endregion

		#region Quick retrieval of advance values

		#endregion

		#region Bitmap Handling

		#endregion

		#region Scanline Converter

		#endregion

		#region Glyph Stroker

		#endregion

		#region System Interface

		#endregion

		#region Module Management

		#endregion

		#region GZIP Streams

		/// <summary>
		/// Open a new stream to parse gzip-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.gz’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, gzip compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a gzipped stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with zlib support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenGzip(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenGzip(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LZW Streams

		/// <summary>
		/// Open a new stream to parse LZW-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.Z’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, LZW compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a LZW stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with LZW support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenLZW(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenLZW(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region BZIP2 Streams

		/// <summary>
		/// Open a new stream to parse bzip2-compressed font files. This is
		/// mainly used to support the compressed ‘*.pcf.bz2’ fonts that come
		/// with XFree86.
		/// </summary>
		/// <remarks><para>
		/// The source stream must be opened before calling this function.
		/// </para><para>
		/// Calling the internal function ‘FT_Stream_Close’ on the new stream
		/// will not call ‘FT_Stream_Close’ on the source stream. None of the
		/// stream objects will be released to the heap.
		/// </para><para>
		/// The stream implementation is very basic and resets the
		/// decompression process each time seeking backwards is needed within
		/// the stream.
		/// </para><para>
		/// In certain builds of the library, bzip2 compression recognition is
		/// automatically handled when calling <see cref="NewFace"/> or
		/// <see cref="OpenFace"/>. This means that if no font driver is
		/// capable of handling the raw compressed file, the library will try
		/// to open a bzip2 stream from it and re-open the face with it.
		/// </para><para>
		/// This function may return <see cref="Error.UnimplementedFeature"/>
		/// if your build of FreeType was not compiled with bzip2 support.
		/// </para></remarks>
		/// <param name="stream">The target embedding stream.</param>
		/// <param name="source">The source stream.</param>
		public static void StreamOpenBzip2(Stream stream, Stream source)
		{
			Error err = FT_Stream_OpenBzip2(stream.reference, source.reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		#endregion

		#region LCD Filtering

		#endregion
	}
}
