using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public struct Rational : IComparable<Rational>, INumber<Rational>
{
    public static readonly Rational zero = new Rational(0,1);

    public Rational(long numerator, long denominator)
    {
        var gcd = EulerMath.GCD(numerator, denominator);
        var denominatorNegativeFactor = denominator > 0 ? 1 : -1;
        Numerator = denominatorNegativeFactor*numerator / gcd;
        Denominator = denominatorNegativeFactor*denominator / gcd;
    }

    public long Numerator { get; }
    public long Denominator { get; }

    public bool IsInteger => Denominator == 1;

    public Rational Inverse => new Rational(Denominator, Numerator);

    public static Rational operator +(Rational l, Rational r)
    {
        return new Rational(l.Numerator * r.Denominator + l.Denominator * r.Numerator, l.Denominator * r.Denominator);
    }

    public static Rational operator *(Rational l, Rational r)
    {
        return new Rational(l.Numerator * r.Numerator, l.Denominator * r.Denominator);
    }

    public static Rational operator -(Rational l, Rational r)
    {
        return new Rational(l.Numerator * r.Denominator - l.Denominator * r.Numerator, l.Denominator * r.Denominator);
    }

    public static Rational operator /(Rational l, Rational r)
    {
        if (r.Denominator == 0)
        {
            throw new DivideByZeroException();
        }
        return new Rational(l.Numerator * r.Denominator, l.Denominator * r.Numerator);
    }

    public static Rational operator -(Rational r)
    {
        return new Rational(-r.Numerator, r.Denominator);
    }

    public static implicit operator Rational(byte i)
    {
        return new Rational(i, 1);
    }

    public static implicit operator Rational(short i)
    {
        return new Rational(i, 1);
    }

    public static implicit operator Rational(int i)
    {
        return new Rational(i, 1);
    }

    public static implicit operator Rational(long i)
    {
        return new Rational(i, 1);
    }

    public static explicit operator double(Rational r)
    {
        return (double) r.Numerator / r.Denominator;
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }

    public static Rational operator %(Rational left, Rational right)
    {
        throw new NotImplementedException();
    }

    public static Rational operator +(Rational value)
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        if (obj is Rational r)
        {
            return r.Denominator == Denominator && r.Numerator == Numerator;
        }

        return false;
    }

    public bool Equals(Rational other)
    {
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Numerator.GetHashCode() * 397) ^ Denominator.GetHashCode();
        }
    }

    public int CompareTo(Rational other)
    {
        return (Numerator * other.Denominator).CompareTo(Denominator * other.Numerator);
    }

    public static bool operator <(Rational left, Rational right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Rational left, Rational right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Rational left, Rational right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Rational left, Rational right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static Rational Parse(string s, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string s, IFormatProvider provider, out Rational result)
    {
        throw new NotImplementedException();
    }

    public static Rational Parse(ReadOnlySpan<char> s, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider provider, out Rational result)
    {
        throw new NotImplementedException();
    }

    public static Rational AdditiveIdentity => 0;
    public static bool operator ==(Rational left, Rational right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Rational left, Rational right)
    {
        return !left.Equals(right);
    }

    public static Rational operator --(Rational value)
    {
        throw new NotImplementedException();
    }

    public static Rational operator ++(Rational value)
    {
        throw new NotImplementedException();
    }

    public static Rational MultiplicativeIdentity => 1;
    public static Rational Abs(Rational value)
    {
        return value < 0 ? -value : value;
    }

    public static bool IsCanonical(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsComplexNumber(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsEvenInteger(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsFinite(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsImaginaryNumber(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsInfinity(Rational value)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<Rational>.IsInteger(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNaN(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegative(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNegativeInfinity(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsNormal(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsOddInteger(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositive(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsPositiveInfinity(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsRealNumber(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsSubnormal(Rational value)
    {
        throw new NotImplementedException();
    }

    public static bool IsZero(Rational value)
    {
        throw new NotImplementedException();
    }

    public static Rational MaxMagnitude(Rational x, Rational y)
    {
        throw new NotImplementedException();
    }

    public static Rational MaxMagnitudeNumber(Rational x, Rational y)
    {
        throw new NotImplementedException();
    }

    public static Rational MinMagnitude(Rational x, Rational y)
    {
        throw new NotImplementedException();
    }

    public static Rational MinMagnitudeNumber(Rational x, Rational y)
    {
        throw new NotImplementedException();
    }

    public static Rational Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static Rational Parse(string s, NumberStyles style, IFormatProvider provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromChecked<TOther>(TOther value, out Rational result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out Rational result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out Rational result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToChecked<TOther>(Rational value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToSaturating<TOther>(Rational value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryConvertToTruncating<TOther>(Rational value, out TOther result) where TOther : INumberBase<TOther>
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out Rational result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Rational result)
    {
        throw new NotImplementedException();
    }

    public static Rational One => 1;
    public static int Radix { get; }
    public static Rational Zero => zero;
}