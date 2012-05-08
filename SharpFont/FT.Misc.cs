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
		/// Validate various OpenType tables to assure that all offsets and indices are valid. The idea is that a
		/// higher-level library which actually does the text layout can access those tables without error checking
		/// (which can be quite time consuming).
		/// </summary>
		/// <remarks><para>
		/// This function only works with OpenType fonts, returning an error otherwise.
		/// </para><para>
		/// After use, the application should deallocate the five tables with <see cref="OpenTypeFree"/>. A NULL value
		/// indicates that the table either doesn't exist in the font, or the application hasn't asked for validation.
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
			Error err = FT_OpenType_Validate(face.Reference, flags, out baseTable, out gdefTable, out gposTable, out gsubTable, out jstfTable);

			if (err != Error.Ok)
				throw new FreeTypeException(err);
		}

		/// <summary>
		/// Free the buffer allocated by OpenType validator.
		/// </summary>
		/// <remarks>
		/// This function must be used to free the buffer allocated by <see cref="OpenTypeValidate"/> only.
		/// </remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="table">The pointer to the buffer that is allocated by <see cref="OpenTypeValidate"/>.</param>
		public static void OpenTypeFree(Face face, IntPtr table)
		{
			FT_OpenType_Free(face.Reference, table);
		}

		#endregion

		#region The TrueType Engine

		/// <summary>
		/// Return an <see cref="EngineType"/> value to indicate which level of the TrueType virtual machine a given
		/// library instance supports.
		/// </summary>
		/// <param name="library">A library instance.</param>
		/// <returns>A value indicating which level is supported.</returns>
		public static EngineType GetTrueTypeEngineType(Library library)
		{
			return FT_Get_TrueType_Engine_Type(library.Reference);
		}

		#endregion

		#region TrueTypeGX/AAT Validation

		/// <summary>
		/// Validate various TrueTypeGX tables to assure that all offsets and indices are valid. The idea is that a
		/// higher-level library which actually does the text layout can access those tables without error checking
		/// (which can be quite time consuming).
		/// </summary>
		/// <remarks><para>
		/// This function only works with TrueTypeGX fonts, returning an error otherwise.
		/// </para><para>
		/// After use, the application should deallocate the buffers pointed to by each ‘tables’ element, by calling
		/// <see cref="FT.TrueTypeGXFree"/>. A NULL value indicates that the table either doesn't exist in the font,
		/// the application hasn't asked for validation, or the validator doesn't have the ability to validate the sfnt
		/// table.
		/// </para></remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="flags">A bit field which specifies the tables to be validated.</param>
		/// <param name="tables">
		/// The array where all validated sfnt tables are stored. The array itself must be allocated by a client.
		/// </param>
		/// <param name="tableLength">
		/// The size of the ‘tables’ array. Normally, FT_VALIDATE_GX_LENGTH should be passed.
		/// </param>
		[CLSCompliant(false)]
		public static void TrueTypeGXValidate(Face face, TrueTypeValidationFlags flags, byte[][] tables, uint tableLength)
		{
			FT_TrueTypeGX_Validate(face.Reference, flags, tables, tableLength);
		}

		/// <summary>
		/// Free the buffer allocated by TrueTypeGX validator.
		/// </summary>
		/// <remarks>
		/// This function must be used to free the buffer allocated by <see cref="FT.TrueTypeGXValidate"/> only.
		/// </remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="table">The pointer to the buffer allocated by <see cref="FT.TrueTypeGXValidate"/>.</param>
		public static void TrueTypeGXFree(Face face, IntPtr table)
		{
			FT_TrueTypeGX_Free(face.Reference, table);
		}

		/// <summary><para>
		/// Validate classic (16-bit format) kern table to assure that the offsets and indices are valid. The idea is
		/// that a higher-level library which actually does the text layout can access those tables without error
		/// checking (which can be quite time consuming).
		/// </para><para>
		/// The ‘kern’ table validator in <see cref="FT.TrueTypeGXValidate"/> deals with both the new 32-bit format and
		/// the classic 16-bit format, while <see cref="FT.ClassicKernValidate"/> only supports the classic 16-bit
		/// format.
		/// </para></summary>
		/// <remarks>
		/// After use, the application should deallocate the buffers pointed to by ‘ckern_table’, by calling
		/// <see cref="FT.ClassicKernFree"/>. A NULL value indicates that the table doesn't exist in the font.
		/// </remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="flags">A bit field which specifies the dialect to be validated.</param>
		/// <returns>A pointer to the kern table.</returns>
		[CLSCompliant(false)]
		public static IntPtr ClassicKernValidate(Face face, ClassicKernValidationFlags flags)
		{
			IntPtr ckernRef;
			FT_ClassicKern_Validate(face.Reference, flags, out ckernRef);
			return ckernRef;
		}

		/// <summary>
		/// Free the buffer allocated by classic Kern validator.
		/// </summary>
		/// <remarks>
		/// This function must be used to free the buffer allocated by <see cref="FT.ClassicKernValidate"/> only.
		/// </remarks>
		/// <param name="face">A handle to the input face.</param>
		/// <param name="table">
		/// The pointer to the buffer that is allocated by <see cref="FT.ClassicKernValidate"/>.
		/// </param>
		public static void ClassicKernFree(Face face, IntPtr table)
		{
			FT_ClassicKern_Free(face.Reference, table);
		}

		#endregion
	}
}
