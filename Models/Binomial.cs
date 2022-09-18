using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Models
{
    public class Binomial
    {
        public Binomial(long? mod = null)
        {
            this.mod = mod;
        }

        private IDictionary<(int, int), long> cache = new Dictionary<(int, int), long>();
        private long? mod;
        

        public long Choose(int n, int k)
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
            if (mod.HasValue)
            {
                result %= mod.Value;
            }
            cache[(n, k)] = result;
            return result;
        }
    }
}
