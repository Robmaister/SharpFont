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

namespace SharpFont
{
	public enum Error
	{
		Ok = 0x00,
		CannotOpenResource = 0x01,
		UnknownFileFormat = 0x02,
		InvalidFileFormat = 0x03,
		InvalidVersion = 0x04,
		LowerModuleVersion = 0x05,
		InvalidArgument = 0x06,
		UnimplementedFeature = 0x07,
		InvalidTable = 0x08,
		InvalidOffset = 0x09,
		InvalidGlyphIndex = 0x10,
		InvalidCharacterCode = 0x11,
		InvalidGlyphFormat = 0x12,
		CannotRenderGlyph = 0x13,
		InvalidOutline = 0x14,
		InvalidComposite = 0x15,
		TooManyHints = 0x16,
		InvalidPixelSize = 0x17,
		InvalidHandle = 0x20,
		InvalidLibraryHandle = 0x21,
		InvalidDriverHandle = 0x22,
		InvalidFaceHandle = 0x23,
		InvalidSizeHandle = 0x24,
		InvalidSlotHandle = 0x25,
		InvalidCharMapHandle = 0x26,
		InvalidCacheHandle = 0x27,
		InvalidStreamHandle = 0x28,
		TooManyDrivers = 0x30,
		TooManyExtensions = 0x31,
		OutOfMemory = 0x40,
		UnlistedObject = 0x41,
		CannotOpenStream = 0x51,
		InvalidStreamSeek = 0x52,
		InvalidStreamSkip = 0x53,
		InvalidStreamRead = 0x54,
		InvalidStreamOperation = 0x55,
		InvalidFrameOperation = 0x56,
		NestedFrameAccess = 0x57,
		InvalidFrameRead = 0x58,
		RasterUninitialized = 0x60,
		RasterCorrupted = 0x61,
		RasterOverflow = 0x62,
		RasterNegativeHeight = 0x63,
		TooManyCaches = 0x70,
		InvalidOpcode = 0x80,
		TooFewArguments = 0x81,
		StackOverflow = 0x82,
		CodeOverflow = 0x83,
		BadArgument = 0x84,
		DivideByZero = 0x85,
		InvalidReference = 0x86,
		DebugOpCode = 0x87,
		ENDFInExecStream = 0x88,
		NestedDEFS = 0x89,
		InvalidCodeRange = 0x8A,
		ExecutionTooLong = 0x8B,
		TooManyFunctionDefs = 0x8C,
		TooManyInstructionDefs = 0x8D,
		TableMissing = 0x8E,
		HorizHeaderMissing = 0x8F,
		LocationsMissing = 0x90,
		NameTableMissing = 0x91,
		CMapTableMissing = 0x92,
		HmtxTableMissing = 0x93,
		PostTableMissing = 0x94,
		InvalidHorizMetrics = 0x95,
		InvalidCharMapFormat = 0x96,
		InvalidPPem = 0x97,
		InvalidVertMetrics = 0x98,
		CouldNotFindContext = 0x99,
		InvalidPostTableFormat = 0x9A,
		InvalidPostTable = 0x9B,
		SyntaxError = 0xA0,
		StackUnderflow = 0xA1,
		Ignore = 0xA2,
		MissingStartfontField = 0xB0,
		MissingFontField = 0xB1,
		MissingSizeField = 0xB2,
		MissingCharsField = 0xB3,
		MissingStartcharField = 0xB4,
		MissingEncodingField = 0xB5,
		MissingBbxField = 0xB6,
		Max
	}
}
