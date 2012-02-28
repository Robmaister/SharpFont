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

using SharpFont.MultipleMasters;
using SharpFont.TrueType;

namespace SharpFont
{
	public partial class FT
	{
		#region Multiple Masters

		/// <summary><para>
		/// Retrieve the Multiple Master descriptor of a given font.
		/// </para><para>
		/// This function can't be used with GX fonts.
		/// </para></summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The Multiple Masters descriptor.</returns>
		public static MultiMaster GetMultiMaster(Face face)
		{
			IntPtr masterRef;

			Error err = FT_Get_Multi_Master(face.reference, out masterRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new MultiMaster(masterRef);
		}

		/// <summary>
		/// Retrieve the Multiple Master/GX var descriptor of a given font.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The Multiple Masters/GX var descriptor. Allocates a data structure, which the user must free (a single call to FT_FREE will do it).</returns>
		public static MMVar GetMMVar(Face face)
		{
			IntPtr varRef;

			Error err = FT_Get_MM_Var(face.reference, out varRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new MMVar(varRef);
		}

		/// <summary><para>
		/// For Multiple Masters fonts, choose an interpolated font design through design coordinates.
		/// </para><para>
		/// This function can't be used with GX fonts.
		/// </para></summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">An array of design coordinates.</param>
		public unsafe static void SetMMDesignCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_MM_Design_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// For Multiple Master or GX Var fonts, choose an interpolated font
		/// design through design coordinates.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">An array of design coordinates.</param>
		public unsafe static void SetVarDesignCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_Var_Design_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// For Multiple Masters and GX var fonts, choose an interpolated font
		/// design through normalized blend coordinates.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">The design coordinates array (each element must be between 0 and 1.0).</param>
		public unsafe static void SetMMBlendCoordinates(Face face, long[] coords)
		{
			fixed (void* ptr = coords)
			{
				IntPtr coordsPtr = (IntPtr)ptr;
				Error err = FT_Set_MM_Blend_Coordinates(face.reference, (uint)coords.Length, coordsPtr);

				if (err != Error.Ok)
					throw new FreeTypeException(err);
			}
		}

		/// <summary>
		/// This is another name of <see cref="SetMMBlendCoordinates"/>.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="coords">The design coordinates array (each element must be between 0 and 1.0).</param>
		public unsafe static void SetVarBlendCoordinates(Face face, long[] coords)
		{
			SetMMBlendCoordinates(face, coords);
		}

		#endregion

		#region TrueType Tables

		#endregion

		#region Type 1 Tables

		#endregion

		#region SFNT Names

		/// <summary>
		/// Retrieve the number of name strings in the SFNT ‘name’ table.
		/// </summary>
		/// <param name="face">A handle to the source face.</param>
		/// <returns>The number of strings in the ‘name’ table.</returns>
		public static uint GetSfntNameCount(Face face)
		{
			return FT_Get_Sfnt_Name_Count(face.reference);
		}

		/// <summary>
		/// Retrieve a string of the SFNT ‘name’ table for a given index.
		/// </summary>
		/// <remarks><para>
		/// The ‘string’ array returned in the ‘aname’ structure is not
		/// null-terminated. The application should deallocate it if it is no
		/// longer in use.
		/// </para><para>
		/// Use FT_Get_Sfnt_Name_Count to get the total number of available
		/// ‘name’ table entries, then do a loop until you get the right
		/// platform, encoding, and name ID.
		/// </para></remarks>
		/// <param name="face">A handle to the source face.</param>
		/// <param name="idx">The index of the ‘name’ string.</param>
		/// <returns>The indexed FT_SfntName structure.</returns>
		public static SfntName GetSfntName(Face face, uint idx)
		{
			IntPtr nameRef;

			Error err = FT_Get_Sfnt_Name(face.reference, idx, out nameRef);

			if (err != Error.Ok)
				throw new FreeTypeException(err);

			return new SfntName(nameRef);
		}

		#endregion

		#region BDF and PCF Files

		#endregion

		#region CID Fonts

		#endregion

		#region PFR Fonts

		#endregion

		#region Windows FNT Files

		#endregion

		#region Font Formats

		#endregion

		#region Gasp Table

		#endregion
	}
}
