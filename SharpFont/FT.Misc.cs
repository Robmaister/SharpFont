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

using SharpFont.TrueType;

namespace SharpFont
{
	public static partial class FT
	{
		#region OpenType Validation

		/// <summary>
		/// Validate various OpenType tables to assure that all offsets and
		/// indices are valid. The idea is that a higher-level library which
		/// actually does the text layout can access those tables without error
		/// checking (which can be quite time consuming).
		/// </summary>
		/// <remarks><para>
		/// This function only works with OpenType fonts, returning an error
		/// otherwise.
		/// </para><para>
		/// After use, the application should deallocate the five tables with
		/// <see cref="OpenTypeFree"/>. A NULL value indicates that the table
		/// either doesn't exist in the font, or the application hasn't asked
		/// for validation.
		/// </para></remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="flags">A bit field which specifies the tables to be validated.</param>
		/// <param name="baseTable">A pointer to the BASE table.</param>
		/// <param name="gdefTable">A pointer to the GDEF table.</param>
		/// <param name="gposTable">A pointer to the GPOS table.</param>
		/// <param name="gsubTable">A pointer to the GSUB table.</param>
		/// <param name="jstfTable">A pointer to the JSTF table.</param>
		[CLSCompliant(false)]
		public static void OpenTypeValidate(Face face, OpenTypeValidationFlags flags, out IntPtr baseTable, out IntPtr gdefTable, out IntPtr gposTable, out IntPtr gsubTable, out IntPtr jstfTable)
		{
			Error err = FT_OpenType_Validate(face.reference, flags, out baseTable, out gdefTable, out gposTable, out gsubTable, out jstfTable);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Free the buffer allocated by OpenType validator.
		/// </summary>
		/// <remarks>
		/// This function must be used to free the buffer allocated by
		/// <see cref="OpenTypeValidate"/> only.
		/// </remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="table">The pointer to the buffer that is allocated by <see cref="OpenTypeValidate"/>.</param>
		public static void OpenTypeFree(Face face, IntPtr table)
		{
			FT_OpenType_Free(face.reference, table);
		}

		#endregion

		#region The TrueType Engine

		/// <summary>
		/// Return an <see cref="EngineType"/> value to indicate which level of
		/// the TrueType virtual machine a given library instance supports.
		/// </summary>
		/// <param name="library">A library instance.</param>
		/// <returns>A value indicating which level is supported.</returns>
		public static EngineType GetTrueTypeEngineType(Library library)
		{
			return FT_Get_TrueType_Engine_Type(library.reference);
		}

		#endregion

		#region TrueTypeGX/AAT Validation

		#endregion
	}
}
