using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Algorithms.Models;

public class QuadraticIntegerResidueClass<T> where T : struct, INumber<T>
{
    public QuadraticIntegerResidueClass(QuadraticNumber<T> value, T modulus)
    {
        Value = new QuadraticNumber<T>(value.RationalPart % modulus + (value.RationalPart < T.Zero ? modulus : T.Zero),
            value.QuadraticPart % modulus + (value.QuadraticPart < T.Zero ? modulus : T.Zero), value.Square);
        Modulus = modulus;
        Square = value.Square;
    }
    public QuadraticNumber<T> Value { get; set; }
    public T Modulus { get; }
    public T Square { get; }

    public static QuadraticIntegerResidueClass<T> operator +(QuadraticIntegerResidueClass<T> l, QuadraticIntegerResidueClass<T> r)
    {
        if (l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new QuadraticIntegerResidueClass<T>(l.Value + r.Value, l.Modulus);
    }

    public static QuadraticIntegerResidueClass<T> operator *(QuadraticIntegerResidueClass<T> l, QuadraticIntegerResidueClass<T> r)
    {
        if (l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new QuadraticIntegerResidueClass<T>(l.Value * r.Value, l.Modulus);
    }
        
    public static QuadraticIntegerResidueClass<T> operator -(QuadraticIntegerResidueClass<T> l, QuadraticIntegerResidueClass<T> r)
    {
        if (l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new QuadraticIntegerResidueClass<T>(l.Value - r.Value, l.Modulus);
    }

    public QuadraticIntegerResidueClass<T> ToThePower(long n)
    {
        var result = new QuadraticIntegerResidueClass<T>(new QuadraticNumber<T>(T.One, T.Zero, Square), Modulus);
        var currentPower = this;
        while (n > 0)
        {
            if (n % 2 == 1)
            {
                result = result * currentPower;
            }

            currentPower = currentPower * currentPower;
            n /= 2;
        }

        return result;
    }

    public override string ToString()
    {
        return $"{Value} ({Modulus})";
    }
}