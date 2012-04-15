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

using SharpFont.Internal;

namespace SharpFont
{
	/// <summary>
	/// FreeType root size class structure. A size object models a face object
	/// at a given size.
	/// </summary>
	public sealed class FTSize : IDisposable
	{
		private bool userAlloc;
		private bool disposed;

		internal IntPtr reference;
		internal SizeRec rec;

		private Face parentFace;

		internal FTSize(IntPtr reference, bool userAlloc, Face parentFace)
		{
			this.reference = reference;
			this.rec = PInvokeHelper.PtrToStructure<SizeRec>(reference);

			this.userAlloc = userAlloc;

			this.parentFace = parentFace;
			parentFace.AddChildSize(this);
		}

		/// <summary>
		/// Finalizes an instance of the FTSize class.
		/// </summary>
		~FTSize()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets a handle to the parent face object.
		/// </summary>
		public Face Face
		{
			get
			{
				return parentFace;
			}
		}

		/// <summary>
		/// Gets a typeless pointer, which is unused by the FreeType library or
		/// any of its drivers. It can be used by client applications to link
		/// their own data to each size object.
		/// </summary>
		public Generic Generic
		{
			get
			{
				return new Generic(rec.generic);
			}

			set
			{
				value.WriteToUnmanagedMemory(new IntPtr(reference.ToInt64() + Marshal.OffsetOf(typeof(FaceRec), "generic").ToInt64()));
				rec = (SizeRec)Marshal.PtrToStructure(reference, typeof(SizeRec));
			}
		}

		/// <summary>
		/// Gets metrics for this size object. This field is read-only.
		/// </summary>
		public SizeMetrics Metrics
		{
			get
			{
				return new SizeMetrics(rec.metrics);
			}
		}

		/// <summary><para>
		/// Even though it is possible to create several size objects for a
		/// given face (see <see cref="FT.NewSize"/> for details), functions
		/// like <see cref="FT.LoadGlyph"/> or <see cref="FT.LoadChar"/> only
		/// use the one which has been activated last to determine the ‘current
		/// character pixel size’.
		/// </para><para>
		/// This function can be used to ‘activate’ a previously created size
		/// object.
		/// </para></summary>
		/// <remarks><see cref="FT.ActivateSize"/>.</remarks>
		/// <param name="size">A handle to a target size object.</param>
		public void Activate(FTSize size)
		{
			FT.ActivateSize(this);
		}

		/// <summary>
		/// Diposes the FTSize.
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
				//only dispose the user allocated sizes.
				if (userAlloc)
					FT.DoneSize(this);

				disposed = true;
			}
		}
	}
}
