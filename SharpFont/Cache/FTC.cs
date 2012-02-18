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

namespace SharpFont.Cache
{
	public class FTC
	{
		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_New(IntPtr library, uint max_faces, uint max_sizes, uint maxBytes, FaceRequester requester, IntPtr req_data, out IntPtr amanager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Manager_Reset(IntPtr manager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Manager_Done(IntPtr manager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_LookupFace(IntPtr manager, IntPtr face_id, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_LookupSize(IntPtr manager, IntPtr scaler, IntPtr asize);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Node_Unref(IntPtr node, IntPtr manager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Manager_RemoveFaceID(IntPtr manager, IntPtr face_id);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_CMapCache_New(IntPtr manager, out IntPtr acache);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint FTC_CMapCache_Lookup(IntPtr cache, IntPtr face_id, int cmap_index, uint char_code);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_ImageCache_New(IntPtr manager, out IntPtr acache);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_ImageCache_Lookup(IntPtr cache, IntPtr type, uint gindex, out IntPtr aglyph, out IntPtr anode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_ImageCache_LookupScaler(IntPtr cache, IntPtr scaler, LoadFlags load_flags, uint gindex, out IntPtr aglyph, out IntPtr anode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_SBitCache_New(IntPtr manager, out IntPtr acache);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_Reset(IntPtr cache, IntPtr type, uint gindex, out IntPtr sbit, out IntPtr anode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_SBitCache_LookupScaler(IntPtr cache, IntPtr scaler, LoadFlags load_flags, uint gindex, out IntPtr sbit, out IntPtr anode);
	}
}
