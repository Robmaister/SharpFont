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
	/// <summary>
	/// Provides an API very similar to the original FreeType Cache API.
	/// </summary>
	/// <remarks>
	/// Useful for porting over C code that relies on the FreeType caching
	/// sub-system. For everything else, use the instance methods of the
	/// classes provided by SharpFont, they are designed to follow .NET naming
	/// and style conventions.
	/// </remarks>
	public static class FTC
	{
		#region Public Members

		/// <summary>
		/// Create a new cache manager.
		/// </summary>
		/// <param name="library">The parent FreeType library handle to use.</param>
		/// <param name="maxFaces">Maximum number of opened <see cref="Face"/> objects managed by this cache instance. Use 0 for defaults.</param>
		/// <param name="maxSizes">Maximum number of opened <see cref="FTSize"/> objects managed by this cache instance. Use 0 for defaults.</param>
		/// <param name="maxBytes">Maximum number of bytes to use for cached data nodes. Use 0 for defaults. Note that this value does not account for managed <see cref="Face"/> and <see cref="FTSize"/> objects.</param>
		/// <param name="requester">An application-provided callback used to translate face IDs into real <see cref="Face"/> objects.</param>
		/// <param name="requestData">A generic pointer that is passed to the requester each time it is called (see <see cref="FaceRequester"/>).</param>
		/// <returns>A handle to a new manager object. 0 in case of failure.</returns>
		[CLSCompliant(false)]
		public static Manager ManagerNew(Library library, uint maxFaces, uint maxSizes, ulong maxBytes, FaceRequester requester, IntPtr requestData)
		{
			IntPtr managerRef;
			Error err = FTC_Manager_New(library.reference, maxFaces, maxSizes, maxBytes, requester, requestData, out managerRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Manager(managerRef);
		}

		/// <summary>
		/// Empty a given cache manager. This simply gets rid of all the
		/// currently cached <see cref="Face"/> and <see cref="FTSize"/> objects
		/// within the manager.
		/// </summary>
		/// <param name="manager">A handle to the manager.</param>
		public static void ManagerReset(Manager manager)
		{
			FTC_Manager_Reset(manager.reference);
		}

		/// <summary>
		/// Destroy a given manager after emptying it.
		/// </summary>
		/// <param name="manager">A handle to the target cache manager object.</param>
		public static void ManagerDone(Manager manager)
		{
			FTC_Manager_Done(manager.reference);
			manager.reference = IntPtr.Zero;
		}

		/// <summary>
		/// Retrieve the FT_Face object that corresponds to a given face ID
		/// through a cache manager.
		/// </summary>
		/// <remarks><para>
		/// The returned FT_Face object is always owned by the manager. You
		/// should never try to discard it yourself.
		/// </para><para>
		/// The FT_Face object doesn't necessarily have a current size object
		/// (i.e., face->size can be 0). If you need a specific ‘font size’,
		/// use FTC_Manager_LookupSize instead.
		/// </para><para>
		/// Never change the face's transformation matrix (i.e., never call the
		/// FT_Set_Transform function) on a returned face! If you need to
		/// transform glyphs, do it yourself after glyph loading.
		/// </para><para>
		/// When you perform a lookup, out-of-memory errors are detected within
		/// the lookup and force incremental flushes of the cache until enough
		/// memory is released for the lookup to succeed.
		/// </para><para>
		/// If a lookup fails with ‘FT_Err_Out_Of_Memory’ the cache has already
		/// been completely flushed, and still no memory was available for the
		/// operation.
		/// </para></remarks>
		/// <param name="manager">A handle to the cache manager.</param>
		/// <param name="faceID">The ID of the face object.</param>
		/// <returns>A handle to the face object.</returns>
		public static Face ManagerLookupFace(Manager manager, IntPtr faceID)
		{
			IntPtr faceRef;
			Error err = FTC_Manager_LookupFace(manager.reference, faceID, out faceRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new Face(faceRef, true);
		}

		/// <summary>
		/// Retrieve the <see cref="FTSize"/> object that corresponds to a given
		/// <see cref="Scaler"/> pointer through a cache manager.
		/// </summary>
		/// <remarks><para>
		/// The returned <see cref="FTSize"/> object is always owned by the
		/// manager. You should never try to discard it by yourself.
		/// </para><para>
		/// You can access the parent <see cref="Face"/> object simply as
		/// ‘size->face’ if you need it. Note that this object is also owned by
		/// the manager.
		/// </para><para>
		/// When you perform a lookup, out-of-memory errors are detected within
		/// the lookup and force incremental flushes of the cache until enough
		/// memory is released for the lookup to succeed.
		/// </para><para>
		/// If a lookup fails with ‘FT_Err_Out_Of_Memory’ the cache has already
		/// been completely flushed, and still no memory is available for the
		/// operation.
		/// </para></remarks>
		/// <param name="manager">A handle to the cache manager.</param>
		/// <param name="scaler">A scaler handle.</param>
		/// <returns>A handle to the size object.</returns>
		public static FTSize ManagerLookupSize(Manager manager, Scaler scaler)
		{
			IntPtr sizeRef;

			Error err = FTC_Manager_LookupSize(manager.reference, scaler.reference, out sizeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new FTSize(sizeRef, false);
		}

		/// <summary>
		/// Decrement a cache node's internal reference count. When the count
		/// reaches 0, it is not destroyed but becomes eligible for subsequent
		/// cache flushes.
		/// </summary>
		/// <param name="node">The cache node handle.</param>
		/// <param name="manager">The cache manager handle.</param>
		public static void NodeUnref(Node node, Manager manager)
		{
			FTC_Node_Unref(node.reference, manager.reference);
		}

		/// <summary>
		/// A special function used to indicate to the cache manager that a
		/// given FTC_FaceID is no longer valid, either because its content
		/// changed, or because it was deallocated or uninstalled.
		/// </summary>
		/// <remarks><para>
		/// This function flushes all nodes from the cache corresponding to
		/// this ‘face_id’, with the exception of nodes with a non-null
		/// reference count.
		/// </para><para>
		/// Such nodes are however modified internally so as to never appear in
		/// later lookups with the same ‘face_id’ value, and to be immediately
		/// destroyed when released by all their users.
		/// </para></remarks>
		/// <param name="manager">The cache manager handle.</param>
		/// <param name="faceID">The FTC_FaceID to be removed.</param>
		public static void ManagerRemoveFaceID(Manager manager, IntPtr faceID)
		{
			FTC_Manager_RemoveFaceID(manager.reference, faceID);
		}

		/// <summary>
		/// Create a new charmap cache.
		/// </summary>
		/// <remarks>
		/// Like all other caches, this one will be destroyed with the cache
		/// manager.
		/// </remarks>
		/// <param name="manager">A handle to the cache manager.</param>
		/// <returns>A new cache handle. NULL in case of error.</returns>
		public static CMapCache CMapCacheNew(Manager manager)
		{
			IntPtr cacheRef;

			Error err = FTC_CMapCache_New(manager.reference, out cacheRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new CMapCache(cacheRef);
		}

		/// <summary>
		/// Translate a character code into a glyph index, using the charmap
		/// cache.
		/// </summary>
		/// <param name="cache">A charmap cache handle.</param>
		/// <param name="faceID">The source face ID.</param>
		/// <param name="cmapIndex">The index of the charmap in the source face. Any negative value means to use the cache <see cref="Face"/>'s default charmap.</param>
		/// <param name="charCode">The character code (in the corresponding charmap).</param>
		/// <returns>Glyph index. 0 means ‘no glyph’.</returns>
		[CLSCompliant(false)]
		public static uint CMapCacheLookup(CMapCache cache, IntPtr faceID, int cmapIndex, uint charCode)
		{
			return FTC_CMapCache_Lookup(cache.reference, faceID, cmapIndex, charCode);
		}

		/// <summary>
		/// Create a new glyph image cache.
		/// </summary>
		/// <param name="manager">The parent manager for the image cache.</param>
		/// <returns>A handle to the new glyph image cache object.</returns>
		public static ImageCache ImageCacheNew(Manager manager)
		{
			IntPtr cacheRef;

			Error err = FTC_ImageCache_New(manager.reference, out cacheRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new ImageCache(cacheRef);
		}

		/// <summary>
		/// Retrieve a given glyph image from a glyph image cache.
		/// </summary>
		/// <remarks><para>
		/// The returned glyph is owned and managed by the glyph image cache.
		/// Never try to transform or discard it manually! You can however
		/// create a copy with FT_Glyph_Copy and modify the new one.
		/// </para><para>
		/// If ‘anode’ is not NULL, it receives the address of the cache node
		/// containing the glyph image, after increasing its reference count.
		/// This ensures that the node (as well as the FT_Glyph) will always be
		/// kept in the cache until you call FTC_Node_Unref to ‘release’ it.
		/// </para><para>
		/// If ‘anode’ is NULL, the cache node is left unchanged, which means
		/// that the FT_Glyph could be flushed out of the cache on the next
		/// call to one of the caching sub-system APIs. Don't assume that it is
		/// persistent!
		/// </para></remarks>
		/// <param name="cache">A handle to the source glyph image cache.</param>
		/// <param name="type">A pointer to a glyph image type descriptor.</param>
		/// <param name="gIndex">The glyph index to retrieve.</param>
		/// <param name="node">Used to return the address of of the corresponding cache node after incrementing its reference count (see note below).</param>
		/// <returns>The corresponding FT_Glyph object. 0 in case of failure.</returns>
		[CLSCompliant(false)]
		public static Glyph ImageCacheLookup(ImageCache cache, ImageType type, uint gIndex, out Node node)
		{
			IntPtr glyphRef, nodeRef;

			Error err = FTC_ImageCache_Lookup(cache.reference, type.reference, gIndex, out glyphRef, out nodeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			node = new Node(nodeRef);
			return new Glyph(glyphRef);
		}

		/// <summary>
		/// A variant of <see cref="ImageCacheLookup"/> that uses an
		/// <see cref="Scaler"/> to specify the face ID and its size.
		/// </summary>
		/// <remarks><para>
		/// The returned glyph is owned and managed by the glyph image cache.
		/// Never try to transform or discard it manually! You can however
		/// create a copy with FT_Glyph_Copy and modify the new one.
		/// </para><para>
		/// If ‘anode’ is not NULL, it receives the address of the cache node
		/// containing the glyph image, after increasing its reference count.
		/// This ensures that the node (as well as the FT_Glyph) will always be
		/// kept in the cache until you call FTC_Node_Unref to ‘release’ it.
		/// </para><para>
		/// If ‘anode’ is NULL, the cache node is left unchanged, which means
		/// that the FT_Glyph could be flushed out of the cache on the next
		/// call to one of the caching sub-system APIs. Don't assume that it is
		/// persistent!
		/// </para><para>
		/// Calls to FT_Set_Char_Size and friends have no effect on cached
		/// glyphs; you should always use the FreeType cache API instead.
		/// </para></remarks>
		/// <param name="cache">A handle to the source glyph image cache.</param>
		/// <param name="scaler">A pointer to a scaler descriptor.</param>
		/// <param name="loadFlags">The corresponding load flags.</param>
		/// <param name="gIndex">The glyph index to retrieve.</param>
		/// <param name="node">Used to return the address of of the corresponding cache node after incrementing its reference count (see note below).</param>
		/// <returns>The corresponding <see cref="Glyph"/> object. 0 in case of failure.</returns>
		[CLSCompliant(false)]
		public static Glyph ImageCacheLookupScaler(ImageCache cache, Scaler scaler, LoadFlags loadFlags, uint gIndex, out Node node)
		{
			IntPtr glyphRef, nodeRef;

			Error err = FTC_ImageCache_LookupScaler(cache.reference, scaler.reference, loadFlags, gIndex, out glyphRef, out nodeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			node = new Node(nodeRef);
			return new Glyph(glyphRef);
		}

		/// <summary>
		/// Create a new cache to store small glyph bitmaps.
		/// </summary>
		/// <param name="manager">A handle to the source cache manager.</param>
		/// <returns>A handle to the new sbit cache. NULL in case of error.</returns>
		public static SBitCache SBitCacheNew(Manager manager)
		{
			IntPtr cacheRef;

			Error err = FTC_SBitCache_New(manager.reference, out cacheRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new SBitCache(cacheRef);
		}

		/// <summary>
		/// Look up a given small glyph bitmap in a given sbit cache and ‘lock’
		/// it to prevent its flushing from the cache until needed.
		/// </summary>
		/// <remarks><para>
		/// The small bitmap descriptor and its bit buffer are owned by the
		/// cache and should never be freed by the application. They might as
		/// well disappear from memory on the next cache lookup, so don't treat
		/// them as persistent data.
		/// </para><para>
		/// The descriptor's ‘buffer’ field is set to 0 to indicate a missing
		/// glyph bitmap.
		/// </para><para>
		/// If ‘anode’ is not NULL, it receives the address of the cache node
		/// containing the bitmap, after increasing its reference count. This
		/// ensures that the node (as well as the image) will always be kept in
		/// the cache until you call FTC_Node_Unref to ‘release’ it.
		/// </para><para>
		/// If ‘anode’ is NULL, the cache node is left unchanged, which means
		/// that the bitmap could be flushed out of the cache on the next call
		/// to one of the caching sub-system APIs. Don't assume that it is
		/// persistent!
		/// </para></remarks>
		/// <param name="cache">A handle to the source sbit cache.</param>
		/// <param name="type">A pointer to the glyph image type descriptor.</param>
		/// <param name="gIndex">The glyph index.</param>
		/// <param name="node">Used to return the address of of the corresponding cache node after incrementing its reference count (see note below).</param>
		/// <returns>A handle to a small bitmap descriptor.</returns>
		[CLSCompliant(false)]
		public static SBit SBitCacheLookup(SBitCache cache, ImageType type, uint gIndex, out Node node)
		{
			IntPtr sbitRef, nodeRef;

			Error err = FTC_SBitCache_Lookup(cache.reference, type.reference, gIndex, out sbitRef, out nodeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			node = new Node(nodeRef);
			return new SBit(sbitRef);
		}

		/// <summary>
		/// A variant of <see cref="SBitCacheLookup"/> that uses a
		/// <see cref="Scaler"/> to specify the face ID and its size.
		/// </summary>
		/// <remarks><para>
		/// The small bitmap descriptor and its bit buffer are owned by the
		/// cache and should never be freed by the application. They might as
		/// well disappear from memory on the next cache lookup, so don't treat
		/// them as persistent data.
		/// </para><para>
		/// The descriptor's ‘buffer’ field is set to 0 to indicate a missing
		/// glyph bitmap.
		/// </para><para>
		/// If ‘anode’ is not NULL, it receives the address of the cache node
		/// containing the bitmap, after increasing its reference count. This
		/// ensures that the node (as well as the image) will always be kept in
		/// the cache until you call FTC_Node_Unref to ‘release’ it.
		/// </para><para>
		/// If ‘anode’ is NULL, the cache node is left unchanged, which means
		/// that the bitmap could be flushed out of the cache on the next call
		/// to one of the caching sub-system APIs. Don't assume that it is
		/// persistent!
		/// </para></remarks>
		/// <param name="cache">A handle to the source sbit cache.</param>
		/// <param name="scaler">A pointer to the scaler descriptor.</param>
		/// <param name="loadFlags">The corresponding load flags.</param>
		/// <param name="gIndex">The glyph index.</param>
		/// <param name="node">Used to return the address of of the corresponding cache node after incrementing its reference count (see note below).</param>
		/// <returns>A handle to a small bitmap descriptor.</returns>
		[CLSCompliant(false)]
		public static SBit SBitCacheLookupScaler(SBitCache cache, Scaler scaler, LoadFlags loadFlags, uint gIndex, out Node node)
		{
			IntPtr sbitRef, nodeRef;

			Error err = FTC_SBitCache_LookupScaler(cache.reference, scaler.reference, loadFlags, gIndex, out sbitRef, out nodeRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			node = new Node(nodeRef);
			return new SBit(sbitRef);
		}

		#endregion

		#region Internal Members

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_New(IntPtr library, uint max_faces, uint max_sizes, ulong maxBytes, FaceRequester requester, IntPtr req_data, out IntPtr amanager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Manager_Reset(IntPtr manager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void FTC_Manager_Done(IntPtr manager);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_LookupFace(IntPtr manager, IntPtr face_id, out IntPtr aface);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_Manager_LookupSize(IntPtr manager, IntPtr scaler, out IntPtr asize);

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
		internal static extern Error FTC_SBitCache_Lookup(IntPtr cache, IntPtr type, uint gindex, out IntPtr sbit, out IntPtr anode);

		[DllImport("freetype.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern Error FTC_SBitCache_LookupScaler(IntPtr cache, IntPtr scaler, LoadFlags load_flags, uint gindex, out IntPtr sbit, out IntPtr anode);

		#endregion
	}
}
