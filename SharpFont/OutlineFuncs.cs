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
	/// <summary><para>
	/// A function pointer type used to describe the signature of a ‘move to’ function during outline
	/// walking/decomposition.
	/// </para><para>
	/// A ‘move to’ is emitted to start a new contour in an outline.
	/// </para></summary>
	/// <param name="to">A pointer to the target point of the ‘move to’.</param>
	/// <param name="user">A typeless pointer which is passed from the caller of the decomposition function.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int MoveToFunc(FTVector to, IntPtr user);

	/// <summary><para>
	/// A function pointer type used to describe the signature of a ‘line to’ function during outline
	/// walking/decomposition.
	/// </para><para>
	/// A ‘line to’ is emitted to indicate a segment in the outline.
	/// </para></summary>
	/// <param name="to">A pointer to the target point of the ‘line to’.</param>
	/// <param name="user">A typeless pointer which is passed from the caller of the decomposition function.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int LineToFunc(FTVector to, IntPtr user);

	/// <summary><para>
	/// A function pointer type used to describe the signature of a ‘conic to’ function during outline walking or
	/// decomposition.
	/// </para><para>
	/// A ‘conic to’ is emitted to indicate a second-order Bézier arc in the outline.
	/// </para></summary>
	/// <param name="control">
	/// An intermediate control point between the last position and the new target in ‘to’.
	/// </param>
	/// <param name="to">A pointer to the target end point of the conic arc.</param>
	/// <param name="user">A typeless pointer which is passed from the caller of the decomposition function.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int ConicToFunc(FTVector control, FTVector to, IntPtr user);

	/// <summary><para>
	/// A function pointer type used to describe the signature of a ‘cubic to’ function during outline walking or
	/// decomposition.
	/// </para><para>
	/// A ‘cubic to’ is emitted to indicate a third-order Bézier arc.
	/// </para></summary>
	/// <param name="control1">A pointer to the first Bézier control point.</param>
	/// <param name="control2">A pointer to the second Bézier control point.</param>
	/// <param name="to">A pointer to the target end point.</param>
	/// <param name="user">A typeless pointer which is passed from the caller of the decomposition function.</param>
	/// <returns>Error code. 0 means success.</returns>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int CubicToFunc(FTVector control1, FTVector control2, FTVector to, IntPtr user);

	/// <summary>
	/// A structure to hold various function pointers used during outline decomposition in order to emit segments,
	/// conic, and cubic Béziers.
	/// </summary>
	/// <remarks>
	/// The point coordinates sent to the emitters are the transformed version of the original coordinates (this is
	/// important for high accuracy during scan-conversion). The transformation is simple:
	/// <code>
	/// x' = (x &lt;&lt; shift) - delta
	/// y' = (x &lt;&lt; shift) - delta
	/// </code>
	/// Set the values of ‘shift’ and ‘delta’ to 0 to get the original point coordinates.
	/// </remarks>
	public class OutlineFuncs
	{
		internal OutlineFuncsRec rec;

		/// <summary>
		/// Initializes a new instance of the OutlineFuncs class.
		/// </summary>
		public OutlineFuncs()
		{
		}

		/// <summary>
		/// Initializes a new instance of the OutlineFuncs class.
		/// </summary>
		/// <param name="moveTo">The move to delegate.</param>
		/// <param name="lineTo">The line to delegate.</param>
		/// <param name="conicTo">The conic to delegate.</param>
		/// <param name="cubicTo">The cubic to delegate.</param>
		/// <param name="shift">A value to shift by.</param>
		/// <param name="delta">A delta to transform by.</param>
		public OutlineFuncs(MoveToFunc moveTo, LineToFunc lineTo, ConicToFunc conicTo, CubicToFunc cubicTo, int shift, int delta)
		{
			rec.moveTo = moveTo;
			rec.lineTo = lineTo;
			rec.conicTo = conicTo;
			rec.cubicTo = cubicTo;
			rec.shift = shift;

			#if WIN64
			rec.delta = delta;
			#else
			rec.delta = (IntPtr)delta;
			#endif
		}

		/// <summary>
		/// Gets or sets the ‘move to’ emitter.
		/// </summary>
		public MoveToFunc MoveFunction
		{
			get
			{
				return rec.moveTo;
			}

			set
			{
				rec.moveTo = value;
			}
		}

		/// <summary>
		/// Gets or sets the segment emitter.
		/// </summary>
		public LineToFunc LineFuction
		{
			get
			{
				return rec.lineTo;
			}

			set
			{
				rec.lineTo = value;
			}
		}

		/// <summary>
		/// Gets or sets the second-order Bézier arc emitter.
		/// </summary>
		public ConicToFunc ConicFunction
		{
			get
			{
				return rec.conicTo;
			}

			set
			{
				rec.conicTo = value;
			}
		}

		/// <summary>
		/// Gets or sets the third-order Bézier arc emitter.
		/// </summary>
		public CubicToFunc CubicFunction
		{
			get
			{
				return rec.cubicTo;
			}

			set
			{
				rec.cubicTo = value;
			}
		}

		/// <summary>
		/// Gets or sets the shift that is applied to coordinates before they are sent to the emitter.
		/// </summary>
		public int Shift
		{
			get
			{
				return rec.shift;
			}

			set
			{
				rec.shift = value;
			}
		}

		/// <summary>
		/// Gets or sets the delta that is applied to coordinates before they are sent to the emitter, but after the
		/// shift.
		/// </summary>
		public int Delta
		{
			get
			{
				return (int)rec.delta;
			}

			/*set
			{
				funcsInt.delta = (IntPtr)value;
			}*/
		}
	}
}
