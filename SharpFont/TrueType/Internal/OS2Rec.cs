#region MIT License
/*Copyright (c) 2012-2014 Robert Rouhani <robert.rouhani@gmail.com>

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

using FT_26Dot6 = System.IntPtr;
using FT_Fixed = System.IntPtr;
using FT_Long = System.IntPtr;
using FT_Pos = System.IntPtr;
using FT_ULong = System.UIntPtr;

namespace SharpFont.TrueType.Internal
{
	[StructLayout(LayoutKind.Sequential)]
	internal class OS2Rec
	{
		internal ushort version;
		internal short xAvgCharWidth;
		internal ushort usWeightClass;
		internal ushort usWidthClass;
		internal EmbeddingTypes fsType;
		internal short ySubscriptXSize;
		internal short ySubscriptYSize;
		internal short ySubscriptXOffset;
		internal short ySubscriptYOffset;
		internal short ySuperscriptXSize;
		internal short ySuperscriptYSize;
		internal short ySuperscriptXOffset;
		internal short ySuperscriptYOffset;
		internal short yStrikeoutSize;
		internal short yStrikeoutPosition;
		internal short sFamilyClass;

		[MarshalAs(UnmanagedType.LPArray, SizeConst = 10)]
		internal byte[] panose;

		internal FT_ULong ulUnicodeRange1;
		internal FT_ULong ulUnicodeRange2;
		internal FT_ULong ulUnicodeRange3;
		internal FT_ULong ulUnicodeRange4;

		[MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
		internal byte[] achVendID;

		internal ushort fsSelection;
		internal ushort usFirstCharIndex;
		internal ushort usLastCharIndex;
		internal short sTypoAscender;
		internal short sTypoDescender;
		internal short sTypoLineGap;
		internal ushort usWinAscent;
		internal ushort usWinDescent;

		internal FT_ULong ulCodePageRange1;
		internal FT_ULong ulCodePageRange2;

		internal short sxHeight;
		internal short sCapHeight;
		internal ushort usDefaultChar;
		internal ushort usBreakChar;
		internal ushort usMaxContext;

		internal ushort usLowerOpticalPointSize;
		internal ushort usUpperOpticalPointSize;
	}
}
