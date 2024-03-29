﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Models;

public class BinomialDouble
{
    private IDictionary<(int, int), double> cache = new Dictionary<(int, int), double>();

    public double Choose(int n, int k)
    {
        if (k > n)
        {
            return 0;
        }
        if (k == 0 || k == n)
        {
            return 1;
        }

        if (cache.ContainsKey((n, k)))
        {
            return cache[(n, k)];
        }

        var result = Choose(n - 1, k) + Choose(n - 1, k - 1);
        cache[(n, k)] = result;
        return result;
    }
}