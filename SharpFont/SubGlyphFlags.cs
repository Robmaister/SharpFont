using System;

namespace SharpFont
{
	/// <summary>
	/// A list of constants used to describe subglyphs. Please refer to the
	/// TrueType specification for the meaning of the various flags.
	/// </summary>
	[Flags]
	public enum SubGlyphFlags
	{
		ArgsAreWords =		0x0001,
		ArgsAreXYValues =	0x0002,
		RoundXYToGrid =		0x0004,
		Scale =				0x0008,
		XYScale =			0x0040,
		TwoByTwo =			0x0080,
		UseMyMetrics =		0x0200
	}
}
