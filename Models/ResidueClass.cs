using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public struct ResidueClass<T> where T : struct, INumber<T>
{
    private static T two = T.One + T.One;

    public ResidueClass(T value, T modulus)
    {
        Value = value % modulus + (value < T.Zero ? modulus : T.Zero);
        Modulus = modulus;
    }
    public T Value { get; set; }
    public T Modulus { get; set; }

    public static ResidueClass<T> operator +(ResidueClass<T> l, ResidueClass<T> r)
    {
        if(l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new ResidueClass<T>(l.Value + r.Value, l.Modulus);
    }

    public static ResidueClass<T> operator *(ResidueClass<T> l, ResidueClass<T> r)
    {
        if (l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new ResidueClass<T>(l.Value * r.Value, l.Modulus);
    }

    public static ResidueClass<T> operator -(ResidueClass<T> r)
    {
        return new ResidueClass<T>(r.Modulus - r.Value, r.Modulus);
    }

    public static ResidueClass<T> operator -(ResidueClass<T> l, ResidueClass<T> r)
    {
        if (l.Modulus != r.Modulus)
        {
            throw new ArgumentException("Moduli komen niet overen", nameof(l));
        }
        return new ResidueClass<T>(l.Value -r.Value, l.Modulus);
    }

    public static ResidueClass<T> operator +(ResidueClass<T> l, T r)
    {
        return new ResidueClass<T>(l.Value + (r % l.Modulus), l.Modulus);
    }

    public static ResidueClass<T> operator -(ResidueClass<T> l, T r)
    {
        return new ResidueClass<T>(l.Value - (r % l.Modulus), l.Modulus);
    }
    public static ResidueClass<T> operator *(ResidueClass<T> l, T r)
    {
        return new ResidueClass<T>(l.Value * (r % l.Modulus), l.Modulus);
    }

    public static ResidueClass<T> operator ++(ResidueClass<T> l)
    {
        return new ResidueClass<T>(l.Value + T.One, l.Modulus);
    }

    public static ResidueClass<T> operator --(ResidueClass<T> l)
    {
        return new ResidueClass<T>(l.Value - T.One, l.Modulus);
    }

    public ResidueClass<T> ToThePower(T n)
    {
        T result = T.One;
        T currentPower = Value;
        while (n > T.Zero)
        {
            if (n % two == T.One)
            {
                result = result * currentPower % Modulus;
            }

            currentPower = currentPower * currentPower % Modulus;
            n /= two;
        }

        return new ResidueClass<T>(result, Modulus);
    }

    public ResidueClass<T> Inverse()
    {
        T a = Value;
        T m = Modulus;
        T t, q;
        T x0 = T.Zero;
        T x1 = T.One;
        while (a > T.One)
        {
            q = a / m;
            t = m;
            m = a % m;
            a = t;
            t = x0;
            x0 = x1 - q * x0;
            x1 = t;
        }

        return new ResidueClass<T>(x1 < T.Zero ? x1 + Modulus : x1, Modulus);
    }

    public ResidueClass<T> Chinese(ResidueClass<T> other)
    {
        var (u, _, gcd) = EulerMath.ExtendedEuclidean(Modulus, other.Modulus);
        var lcm = Modulus / gcd * other.Modulus;
        if (Value % gcd != other.Value % gcd)
        {
            throw new ArgumentException("Geen oplossing");
        }

        var l = (Value - other.Value) / gcd;
        return new ResidueClass<T>(Value - Modulus*u*l, lcm);
    }

    public bool IsQuadraticResidue()
    {
        // Modulus moet priem zijn
        return Value == T.Zero || ToThePower((Modulus - T.One) / two).Value == T.One;
    }
    
    public override string ToString()
    {
        return $"{Value} ({Modulus})";
    }
}