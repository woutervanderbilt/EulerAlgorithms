using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public static class QuadraticNumberExtensions
{
    public static double RealValue(this QuadraticNumber<long> q)
    {
        return (double)q.RationalPart + (double)q.QuadraticPart * Math.Sqrt(q.Square);
    }
}

public struct QuadraticNumber<T> where T : struct, INumber<T>
{
    public QuadraticNumber(T a, T b, T square)
    {
        RationalPart = a;
        QuadraticPart = b;
        Square = square;
    }

    public T RationalPart { get; }
    public T QuadraticPart { get; }
    public T Square { get; }

    public bool IsIntegral => QuadraticPart == T.Zero;
        
    public static QuadraticNumber<T> operator +(QuadraticNumber<T> x, QuadraticNumber<T> y)
    {
        return new QuadraticNumber<T>(x.RationalPart + y.RationalPart, x.QuadraticPart + y.QuadraticPart, x.Square);
    }

    public static QuadraticNumber<T> operator -(QuadraticNumber<T> x, QuadraticNumber<T> y)
    {
        return new QuadraticNumber<T>(x.RationalPart - y.RationalPart, x.QuadraticPart - y.QuadraticPart, x.Square);
    }

    public static QuadraticNumber<T> operator *(QuadraticNumber<T> x, QuadraticNumber<T> y)
    {
        return new QuadraticNumber<T>(x.RationalPart * y.RationalPart + x.Square * x.QuadraticPart * y.QuadraticPart, x.RationalPart * y.QuadraticPart + x.QuadraticPart * y.RationalPart, x.Square);
    }

    public override string ToString()
    {
        return $"{RationalPart} + {QuadraticPart}√{Square}";
    }
}