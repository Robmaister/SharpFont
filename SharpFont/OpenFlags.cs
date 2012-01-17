using System;

namespace SharpFont
{
	/// <summary>
	/// A list of bit-field constants used within the ‘flags’ field of the FT_Open_Args structure.
	/// </summary>
	/// <remarks>
	/// The ‘FT_OPEN_MEMORY’, ‘FT_OPEN_STREAM’, and ‘FT_OPEN_PATHNAME’ flags are mutually exclusive.
	/// </remarks>
	[Flags]
	public enum OpenFlags : uint
	{
		/// <summary>
		/// This is a memory-based stream.
		/// </summary>
		Memory =	0x01,

		/// <summary>
		/// Copy the stream from the ‘stream’ field.
		/// </summary>
		Stream =	0x02,

		/// <summary>
		/// Create a new input stream from a C path name.
		/// </summary>
		Pathname =	0x04,

		/// <summary>
		/// Use the ‘driver’ field.
		/// </summary>
		Driver =	0x08,

		/// <summary>
		/// Use the ‘num_params’ and ‘params’ fields.
		/// </summary>
		Params =	0x10
	}
}
