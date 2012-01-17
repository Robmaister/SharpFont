#region MIT License
/*Copyright (c) 2012 Robert Rouhani, robert.rouhani@gmail.com

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
	public enum EncodingID : ushort
	{
		AppleDefault = 0,
		AppleUnicode11 = 1,
		AppleUnicode20 = 3,
		AppleUnicode32 = 4,
		AppleVariantSelector = 5,

		MacRoman = 0,
		MacJapanese = 1,
		MacTraditionalChinese = 2,
		MacKorean = 3,
		MacArabic = 4,
		MacHebrew = 5,
		MacGreek = 6,
		MacRussian = 7,
		MacRSymbol = 8,
		MacDevanagari = 9,
		MacGurmukhi = 10,
		MacGujarati = 11,
		MacOriya = 12,
		MacBengali = 13,
		MacTamil = 14,
		MacTelugu = 15,
		MacKannada = 16,
		MacMalayalam = 17,
		MacSinhalese = 18,
		MacBurmese = 19,
		MacKhmer = 20,
		MacThai = 21,
		MacLaotian = 22,
		MacGeorgian = 23,
		MacArmenian = 24,
		MacMaldivian = 25,
		MacSimplifiedChinese = 25,
		MacTibetan = 26,
		MacMongolian = 27,
		MacGeez = 28,
		MacSlavic = 29,
		MacVietnamese = 30,
		MacSindhi = 31,
		MacUninterp = 32,

		MicrosoftSymbol = 0,
		MicrosoftUnicode = 1,
		MicrosoftSJIS = 2,
		MicrosoftGB2312 = 3,
		MicrosoftBig5 = 4,
		MicrosoftWansung = 5,
		MicrosoftJohab = 6,
		MicrosoftUCS4 = 10,

		AdobeStandard = 0,
		AdobeExpert = 1,
		AdobeCustom = 2,
		AdobeLatin1 = 3
	}
}
