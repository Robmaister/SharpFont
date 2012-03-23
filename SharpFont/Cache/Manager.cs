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

namespace SharpFont.Cache
{
	/// <summary><para>
	/// This object corresponds to one instance of the cache-subsystem. It is
	/// used to cache one or more <see cref="Face"/> objects, along with
	/// corresponding <see cref="FTSize"/> objects.
	/// </para><para>
	/// The manager intentionally limits the total number of opened
	/// <see cref="Face"/> and <see cref="FTSize"/> objects to control memory
	/// usage. See the ‘max_faces’ and ‘max_sizes’ parameters of
	/// <see cref="FTC.ManagerNew"/>.
	/// </para><para>
	/// The manager is also used to cache ‘nodes’ of various types while
	/// limiting their total memory usage.
	/// </para><para>
	/// All limitations are enforced by keeping lists of managed objects in
	/// most-recently-used order, and flushing old nodes to make room for new
	/// ones.
	/// </para></summary>
	public sealed class Manager : IDisposable
	{
		#region Fields

		internal IntPtr reference;

		private bool disposed;

		#endregion

		#region Constructors

		internal Manager(IntPtr reference)
		{
			this.reference = reference;
		}

		/// <summary>
		/// Initializes a new instance of the Manager class.
		/// </summary>
		/// <param name="library">
		/// The parent FreeType library handle to use.
		/// </param>
		/// <param name="maxFaces">
		/// Maximum number of opened <see cref="Face"/> objects managed by this
		/// cache instance. Use 0 for defaults.
		/// </param>
		/// <param name="maxSizes">
		/// Maximum number of opened <see cref="FTSize"/> objects managed by
		/// this cache instance. Use 0 for defaults.
		/// </param>
		/// <param name="maxBytes">
		/// Maximum number of bytes to use for cached data nodes. Use 0 for
		/// defaults. Note that this value does not account for managed
		/// <see cref="Face"/> and <see cref="FTSize"/> objects.
		/// </param>
		/// <param name="requester">
		/// An application-provided callback used to translate face IDs into
		/// real <see cref="Face"/> objects.
		/// </param>
		/// <param name="requestData">
		/// A generic pointer that is passed to the requester each time it is
		/// called (see <see cref="FaceRequester"/>).
		/// </param>
		[CLSCompliant(false)]
		public Manager(Library library, uint maxFaces, uint maxSizes, ulong maxBytes, FaceRequester requester, IntPtr requestData)
		{
			Error err = FTC.FTC_Manager_New(library.reference, maxFaces, maxSizes, maxBytes, requester, requestData, out reference);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Finalizes an instance of the Manager class.
		/// </summary>
		~Manager()
		{
			Dispose(false);
		}

		#endregion

		#region Public Members

		/// <summary>
		/// Empty a given cache manager. This simply gets rid of all the
		/// currently cached <see cref="Face"/> and <see cref="FTSize"/>
		/// objects within the manager.
		/// </summary>
		public void Reset()
		{
			FTC.ManagerReset(this);
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
		/// <param name="faceID">The ID of the face object.</param>
		/// <returns>A handle to the face object.</returns>
		public Face LookupFace(IntPtr faceID)
		{
			return FTC.ManagerLookupFace(this, faceID);
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
		/// <param name="scaler">A scaler handle.</param>
		/// <returns>A handle to the size object.</returns>
		public FTSize LookupSize(Scaler scaler)
		{
			return FTC.ManagerLookupSize(this, scaler);
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
		/// <param name="faceID">The FTC_FaceID to be removed.</param>
		public void RemoveFaceID(IntPtr faceID)
		{
			FTC.ManagerRemoveFaceID(this, faceID);
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Disposes the Manager.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				}

				//TODO what happens when user calls FTC.ManagerDone from their code? fix this.
				FTC.ManagerDone(this);

				disposed = true;
			}
		}

		#endregion
	}
}
