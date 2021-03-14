using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Factorizer
    {
        private long Limit { get; }
        private long[] smallestOddPrimeFactor;
        private IList<long> primes = new List<long> { 2 };
        private bool isInitialized = false;
        private long LengthOfList;
        private IDictionary<long, byte> moebius = new Dictionary<long, byte>();
        private IDictionary<long, long> rads = new Dictionary<long, long>();

        public Factorizer(long limit)
        {
            Limit = limit;
            LengthOfList = (Limit - 1) / 2;
            smallestOddPrimeFactor = new long[LengthOfList];
        }

        private void Initialize()
        {
            for (long i = 0; i < LengthOfList; i++)
            {
                if (smallestOddPrimeFactor[i] != 0)
                {
                    continue;
                }
                var p = 2 * i + 3;
                primes.Add(p);
                smallestOddPrimeFactor[i] = p;
                for (long j = 2 * i * i + 6 * i + 3; j < LengthOfList; j += p)
                {
                    if (smallestOddPrimeFactor[j] == 0)
                    {
                        smallestOddPrimeFactor[j] = p;
                    }
                }
            }
            isInitialized = true;
        }

        public IEnumerable<Tuple<long, short>> Factor(long l)
        {
            if (!isInitialized)
            {
                Initialize();
            }
            short e = 0;
            while (l % 2 == 0)
            {
                e++;
                l /= 2;
            }

            if (e > 0)
            {
                yield return new Tuple<long, short>(2, e);
            }
            foreach (var tuple in FactorizeInternal(l))
            {
                yield return tuple;
            }
        }

        private IEnumerable<Tuple<long, short>> FactorizeInternal(long l, int startPrimeIndex = 0)
        {
            if (l == 1)
            {
                yield break;
            }

            if (l > Limit)
            {
                for (int i = startPrimeIndex; i < primes.Count; i++)
                {
                    var prime = primes[i];
                    if (prime * prime > l)
                    {
                        yield return new Tuple<long, short>(l, 1);
                        yield break;
                    }
                    short exp = 0;
                    while (l % prime == 0)
                    {
                        exp++;
                        l /= prime;
                    }

                    if (exp > 0)
                    {
                        yield return new Tuple<long, short>(prime, exp);
                        foreach (var tuple in FactorizeInternal(l, i))
                        {
                            yield return tuple;
                        }

                        yield break;
                    }
                }

            }

            var p = smallestOddPrimeFactor[(l - 3) / 2];
            short e = 1;
            while (l > 1)
            {
                l /= p;
                if (l == 1)
                {
                    yield return new Tuple<long, short>(p, e);
                    yield break;
                }

                var q = smallestOddPrimeFactor[(l - 3) / 2];
                if (q == p)
                {
                    e++;
                }
                else
                {
                    yield return new Tuple<long, short>(p, e);
                    p = q;
                    e = 1;
                }
            }
        }

        public int Moebius(long l)
        {
            if (moebius.ContainsKey(l))
            {
                return moebius[l] - 1;
            }

            byte result;
            var f = Factor(l).ToList();
            if (f.Any(p => p.Item2 > 1))
            {
                result = 1;
            }
            else
            {
                result = (byte)(f.Count % 2 == 0 ? 2 : 0);
            }

            moebius[l] = result;
            return result - 1;
        }

        public long DivisorSum(long l)
        {
            var factorization = Factor(l);
            long result = 1;
            foreach (var prime in factorization)
            {
                long power = prime.Item1;
                for (short s = 1; s <= prime.Item2; s++)
                {
                    power *= prime.Item1;
                }

                result *= (power - 1) / (prime.Item1 - 1);
            }

            return result;
        }

        public IEnumerable<long> Divisors(long l)
        {
            var factorization = Factor(l).ToList();
            return DivisorsInternal(1, factorization);
        }

        public long Totient(long l)
        {
            var factorization = Factor(l);
            long result = 1;
            foreach (var tuple in factorization)
            {
                result *= (tuple.Item1 - 1);
                for (int i = 1; i < tuple.Item2; i++)
                {
                    result *= tuple.Item1;
                }
            }

            return result;
        }



        public long Rad(long l)
        {
            if (rads.ContainsKey(l))
            {
                return rads[l];
            }
            var rad = Factor(l).Aggregate(1l, (prod, p) => prod * p.Item1);
            rads[l] = rad;
            return rad;
        }

        private IEnumerable<long> DivisorsInternal(long f, List<Tuple<long, short>> factors)
        {
            if (!factors.Any())
            {
                yield return f;
                yield break;
            }
            var firstFactor = factors.First();
            var remainingFactors = factors.Skip(1).ToList();
            for (int i = 0; i <= firstFactor.Item2; i++)
            {
                foreach (var d in DivisorsInternal(f, remainingFactors))
                {
                    yield return d;
                }

                if (i < firstFactor.Item2)
                {
                    f *= firstFactor.Item1;
                }
            }
        }
    }
}
