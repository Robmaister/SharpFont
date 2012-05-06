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
	/// This object corresponds to one instance of the cache-subsystem. It is used to cache one or more
	/// <see cref="Face"/> objects, along with corresponding <see cref="FTSize"/> objects.
	/// </para><para>
	/// The manager intentionally limits the total number of opened <see cref="Face"/> and <see cref="FTSize"/> objects
	/// to control memory usage. See the ‘max_faces’ and ‘max_sizes’ parameters of <see cref="FTC.ManagerNew"/>.
	/// </para><para>
	/// The manager is also used to cache ‘nodes’ of various types while limiting their total memory usage.
	/// </para><para>
	/// All limitations are enforced by keeping lists of managed objects in most-recently-used order, and flushing old
	/// nodes to make room for new ones.
	/// </para></summary>
	public sealed class Manager : IDisposable
	{
		#region Fields

		private IntPtr reference;

		private bool disposed;

		#endregion

		#region Constructors

		internal Manager(IntPtr reference)
		{
			Reference = reference;
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
			Error err = FTC.FTC_Manager_New(library.Reference, maxFaces, maxSizes, maxBytes, requester, requestData, out reference);

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

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the object has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get
			{
				return disposed;
			}
		}

		internal IntPtr Reference
		{
			get
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				return reference;
			}

			set
			{
				if (disposed)
					throw new ObjectDisposedException("Reference", "Cannot access a disposed object.");

				reference = value;
			}
		}

		#endregion

		#region Public Members

		/// <summary>
		/// Empty a given cache manager. This simply gets rid of all the currently cached <see cref="Face"/> and
		/// <see cref="FTSize"/> objects within the manager.
		/// </summary>
		public void Reset()
		{
			FTC.ManagerReset(this);
		}

		/// <summary>
		/// Retrieve the <see cref="Face"/> object that corresponds to a given face ID through a cache manager.
		/// </summary>
		/// <remarks>See <see cref="FTC.ManagerLookupFace"/>.</remarks>
		/// <param name="faceID">The ID of the face object.</param>
		/// <returns>A handle to the face object.</returns>
		public Face LookupFace(IntPtr faceID)
		{
			return FTC.ManagerLookupFace(this, faceID);
		}

		/// <summary>
		/// Retrieve the <see cref="FTSize"/> object that corresponds to a given <see cref="Scaler"/> pointer through a
		/// cache manager.
		/// </summary>
		/// <remarks>See <see cref="FTC.ManagerLookupSize"/>.</remarks>
		/// <param name="scaler">A scaler handle.</param>
		/// <returns>A handle to the size object.</returns>
		public FTSize LookupSize(Scaler scaler)
		{
			return FTC.ManagerLookupSize(this, scaler);
		}

		/// <summary>
		/// A special function used to indicate to the cache manager that a given FTC_FaceID is no longer valid, either
		/// because its content changed, or because it was deallocated or uninstalled.
		/// </summary>
		/// <remarks>See <see cref="FTC.ManagerRemoveFaceID"/>.</remarks>
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
				disposed = true;

				FTC.FTC_Manager_Done(reference);
				reference = IntPtr.Zero;
			}
		}

		#endregion
	}
}
