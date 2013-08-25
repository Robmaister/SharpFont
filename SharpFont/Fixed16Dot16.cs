#region MIT License
/*Copyright (c) 2012-2013 Robert Rouhani <robert.rouhani@gmail.com>

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

namespace SharpFont
{
	/// <summary>
	/// Represents a fixed-point decimal value with 16 bits of decimal precision.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Fixed16Dot16 : IEquatable<Fixed16Dot16>, IComparable<Fixed16Dot16>
	{
		#region Fields
		
		/// <summary>
		/// The raw 16.16 integer.
		/// </summary>
		private int value;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Fixed16Dot16"/> struct.
		/// </summary>
		/// <param name="value">An integer value.</param>
		public Fixed16Dot16(int value)
		{
			this.value = value << 16;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fixed16Dot16"/> struct.
		/// </summary>
		/// <param name="value">A floating point value.</param>
		public Fixed16Dot16(float value)
		{
			this.value = (int)(value * 65536);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fixed16Dot16"/> struct.
		/// </summary>
		/// <param name="value">A floating point value.</param>
		public Fixed16Dot16(double value)
		{
			this.value = (int)(value * 65536);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fixed16Dot16"/> struct.
		/// </summary>
		/// <param name="value">A floating point value.</param>
		public Fixed16Dot16(decimal value)
		{
			this.value = (int)(value * 65536);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the raw 16.16 integer.
		/// </summary>
		public int Value
		{
			get
			{
				return value;
			}
		}

		#endregion

		#region Methods

		#region Static

		/// <summary>
		/// Creates a <see cref="Fixed16Dot16"/> from an int containing a 16.16 value.
		/// </summary>
		/// <param name="value">A 16.16 value.</param>
		/// <returns>An instance of <see cref="Fixed16Dot16"/>.</returns>
		public static Fixed16Dot16 FromRawValue(int value)
		{
			Fixed16Dot16 f = new Fixed16Dot16();
			f.value = value;
			return f;
		}

		/// <summary>
		/// Creates a new <see cref="Fixed16Dot16"/> from a <see cref="System.Int32"/>
		/// </summary>
		/// <param name="value">A <see cref="System.Int32"/> value.</param>
		/// <returns>The equivalent <see cref="Fixed16Dot16"/> value.</returns>
		public static Fixed16Dot16 FromInt32(int value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Creates a new <see cref="Fixed16Dot16"/> from <see cref="System.Single"/>.
		/// </summary>
		/// <param name="value">A floating-point value.</param>
		/// <returns>A fixed 16.16 value.</returns>
		public static Fixed16Dot16 FromSingle(float value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Creates a new <see cref="Fixed16Dot16"/> from a <see cref="System.Double"/>.
		/// </summary>
		/// <param name="value">A floating-point value.</param>
		/// <returns>A fixed 16.16 value.</returns>
		public static Fixed16Dot16 FromDouble(double value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Creates a new <see cref="Fixed16Dot16"/> from a <see cref="System.Decimal"/>.
		/// </summary>
		/// <param name="value">A floating-point value.</param>
		/// <returns>A fixed 16.16 value.</returns>
		public static Fixed16Dot16 FromDecimal(decimal value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Adds two 16.16 values together.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static Fixed16Dot16 Add(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return new Fixed16Dot16(left.value + right.value);
		}

		/// <summary>
		/// Subtacts one 16.16 values from another.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fixed16Dot16 Subtract(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return new Fixed16Dot16(left.value - right.value);
		}

		/// <summary>
		/// Multiplies two 16.16 values together.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fixed16Dot16 Multiply(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			long mul = (long)left.value * (long)right.value;
			Fixed16Dot16 ans = new Fixed16Dot16();
			ans.value = (int)(mul >> 16);
			return ans;
		}

		/// <summary>
		/// Divides one 16.16 values from another.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the division.</returns>
		public static Fixed16Dot16 Divide(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			long div = ((long)left.Value << 16) / right.value;
			Fixed16Dot16 ans = new Fixed16Dot16();
			ans.value = (int)div;
			return ans;
		}

		#endregion

		#region Operators

		/// <summary>
		/// Casts a <see cref="System.Single"/> to a <see cref="Fixed16Dot16"/>.
		/// </summary>
		/// <param name="value">A <see cref="System.Single"/> value.</param>
		/// <returns>The equivalent <see cref="Fixed16Dot16"/> value.</returns>
		public static explicit operator Fixed16Dot16(float value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Casts a <see cref="System.Double"/> to a <see cref="Fixed16Dot16"/>.
		/// </summary>
		/// <param name="value">A <see cref="System.Double"/> value.</param>
		/// <returns>The equivalent <see cref="Fixed16Dot16"/> value.</returns>
		public static explicit operator Fixed16Dot16(double value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Casts a <see cref="System.Single"/> to a <see cref="Fixed16Dot16"/>.
		/// </summary>
		/// <param name="value">A <see cref="System.Decimal"/> value.</param>
		/// <returns>The equivalent <see cref="Fixed16Dot16"/> value.</returns>
		public static explicit operator Fixed16Dot16(decimal value)
		{
			return new Fixed16Dot16(value);
		}

		/// <summary>
		/// Casts a <see cref="Fixed16Dot16"/> to a <see cref="System.Single"/>.
		/// </summary>
		/// <remarks>
		/// This operation can result in a loss of data.
		/// </remarks>
		/// <param name="value">A <see cref="Fixed16Dot16"/> value.</param>
		/// <returns>The equivalent <see cref="System.Single"/> value.</returns>
		public static explicit operator float(Fixed16Dot16 value)
		{
			return value.ToSingle();
		}

		/// <summary>
		/// Casts a <see cref="Fixed16Dot16"/> to a <see cref="System.Double"/>.
		/// </summary>
		/// <param name="value">A <see cref="Fixed16Dot16"/> value.</param>
		/// <returns>The equivalent <see cref="System.Double"/> value.</returns>
		public static implicit operator double(Fixed16Dot16 value)
		{
			return value.ToDouble();
		}

		/// <summary>
		/// Casts a <see cref="Fixed16Dot16"/> to a <see cref="System.Decimal"/>.
		/// </summary>
		/// <param name="value">A <see cref="Fixed16Dot16"/> value.</param>
		/// <returns>The equivalent <see cref="System.Single"/> value.</returns>
		public static implicit operator decimal(Fixed16Dot16 value)
		{
			return value.ToDecimal();
		}

		/// <summary>
		/// Adds two 16.16 values together.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the addition.</returns>
		public static Fixed16Dot16 operator +(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return Add(left, right);
		}

		/// <summary>
		/// Subtacts one 16.16 values from another.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the subtraction.</returns>
		public static Fixed16Dot16 operator -(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return Subtract(left, right);
		}

		/// <summary>
		/// Multiplies two 16.16 values together.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the multiplication.</returns>
		public static Fixed16Dot16 operator *(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return Multiply(left, right);
		}

		/// <summary>
		/// Divides one 16.16 values from another.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>The result of the division.</returns>
		public static Fixed16Dot16 operator /(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return Divide(left, right);
		}

		/// <summary>
		/// Compares two instances of <see cref="Fixed16Dot16"/> for equality.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether the two instances are equal.</returns>
		public static bool operator ==(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares two instances of <see cref="Fixed16Dot16"/> for inequality.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether the two instances are not equal.</returns>
		public static bool operator !=(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Checks if the left operand is less than the right operand.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether left is less than right.</returns>
		public static bool operator <(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Checks if the left operand is less than or equal to the right operand.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether left is less than or equal to right.</returns>
		public static bool operator <=(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Checks if the left operand is greater than the right operand.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether left is greater than right.</returns>
		public static bool operator >(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Checks if the left operand is greater than or equal to the right operand.
		/// </summary>
		/// <param name="left">The left operand.</param>
		/// <param name="right">The right operand.</param>
		/// <returns>A value indicating whether left is greater than or equal to right.</returns>
		public static bool operator >=(Fixed16Dot16 left, Fixed16Dot16 right)
		{
			return left.CompareTo(right) >= 0;
		}

		#endregion

		#region Instance

		/// <summary>
		/// Removes the decimal part of the value.
		/// </summary>
		/// <returns>The truncated number.</returns>
		public int Floor()
		{
			return value >> 16;
		}

		/// <summary>
		/// Removes the decimal part of the value.
		/// </summary>
		/// <returns>The truncated number in 16.16 format.</returns>
		public Fixed16Dot16 FloorFix()
		{
			//TODO does the P/Invoke overhead make this slower than re-implementing in C#? Test it
			return FT.FT_FloorFix(this);
		}

		/// <summary>
		/// Rounds to the nearest whole number.
		/// </summary>
		/// <returns>The nearest whole number.</returns>
		public int Round()
		{
			//add 2^15, rounds the integer part up if the decimal value is >= 0.5
			return (value + 32768) >> 16;
		}

		/// <summary>
		/// Rounds to the nearest whole number.
		/// </summary>
		/// <returns>The truncated number in 16.16 format.</returns>
		public Fixed16Dot16 RoundFix()
		{
			return FT.FT_RoundFix(this);
		}

		/// <summary>
		/// Rounds up to the next whole number.
		/// </summary>
		/// <returns>The next whole number.</returns>
		public int Ceiling()
		{
			//add 2^16 - 1, rounds the integer part up if there's any decimal value
			return (value + 65535) >> 16;
		}

		/// <summary>
		/// Rounds up to the next whole number.
		/// </summary>
		/// <returns>The next whole number in 16.16 format.</returns>
		public Fixed16Dot16 CeilingFix()
		{
			return FT.FT_CeilFix(this);
		}

		/// <summary>
		/// Converts the value to a <see cref="System.Int32"/>. The value is floored.
		/// </summary>
		/// <returns>An integer value.</returns>
		public int ToInt32()
		{
			return Floor();
		}

		/// <summary>
		/// Converts the value to a <see cref="System.Single"/>.
		/// </summary>
		/// <returns>A floating-point value.</returns>
		public float ToSingle()
		{
			return value / 65536f;
		}

		/// <summary>
		/// Converts the value to a <see cref="System.Double"/>.
		/// </summary>
		/// <returns>A floating-point value.</returns>
		public double ToDouble()
		{
			return value / 65536d;
		}

		/// <summary>
		/// Converts the value to a <see cref="System.Decimal"/>.
		/// </summary>
		/// <returns>A decimal value.</returns>
		public decimal ToDecimal()
		{
			return value / 65536m;
		}

		/// <summary>
		/// Compares this instance to another <see cref="Fixed16Dot16"/> for equality.
		/// </summary>
		/// <param name="other">A <see cref="Fixed16Dot16"/>.</param>
		/// <returns>A value indicating whether the two instances are equal.</returns>
		public bool Equals(Fixed16Dot16 other)
		{
			return value == other.value;
		}

		/// <summary>
		/// Compares this instnace with another <see cref="Fixed16Dot16"/> and returns an integer that indicates
		/// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
		/// other <see cref="Fixed16Dot16"/>.
		/// </summary>
		/// <param name="other">A <see cref="Fixed16Dot16"/>.</param>
		/// <returns>A value indicating the relative order of the instances.</returns>
		public int CompareTo(Fixed16Dot16 other)
		{
			return value.CompareTo(other.value);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			//TODO overloads with string formatting.
			return ToDouble().ToString();
		}

		/// <summary>
		/// Calculates a hash code for the current object.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		/// <summary>
		/// Determines whether the specified object isequal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>A value indicating equality between the two objects.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Fixed16Dot16)
				return this.Equals((Fixed16Dot16)obj);
			else if (obj is int)
				return value == ((Fixed16Dot16)obj).value;
			else
				return false;
		}

		#endregion

		#endregion
	}
}
