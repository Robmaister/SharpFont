//Copyright (c) 2014-2015 Robert Rouhani <robert.rouhani@gmail.com> and other contributors (see CONTRIBUTORS file).
//Licensed under the MIT License - https://raw.github.com/Robmaister/SharpFont.HarfBuzz/master/LICENSE

namespace SharpFont.HarfBuzz
{
	public enum Direction
	{
		Invalid = 0,
		LeftToRight = 4,
		RightToLeft,
		TopToBottom,
		BottomToTop
	}

	public static class DirectionExtensions
	{
		public static string ToString(this Direction dir)
		{
			return "";
		}

		public static bool IsHorizontal(this Direction dir)
		{
			return ((int)dir & ~1) == 4;
		}

		public static bool IsVertical(this Direction dir)
		{
			return ((int)dir & ~1) == 6;
		}

		public static bool IsForward(this Direction dir)
		{
			return ((int)dir & ~2) == 4;
		}

		public static bool IsBackward(this Direction dir)
		{
			return ((int)dir & ~2) == 5;
		}

		public static bool IsValid(this Direction dir)
		{
			return ((int)dir & ~3) == 4;
		}

		public static Direction Reverse(this Direction dir)
		{
			return (Direction)((int)dir ^ 1);
		}
	}
}
