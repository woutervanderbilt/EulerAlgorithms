using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Models;

namespace Algorithms;

public class BernoulliGenerator
{
    private long Modulus { get; }
    private BinomialMod BinomialMod { get; }

    public BernoulliGenerator(long modulus)
    {
        Modulus = modulus;
        BinomialMod = new BinomialMod(modulus);
    }

    public IEnumerable<ResidueClass<long>> GenerateBernoulliNumbers()
    {
        int m = 0;
        while (true)
        {
            if (m % 2 == 1)
            {
                yield return m == 1 ? new ResidueClass<long>(2, Modulus).Inverse() : new ResidueClass<long>(0, Modulus);
            }
            else
            {
                var result = new ResidueClass<long>(0, Modulus);
                for (int k = 0; k <= m; k++)
                {
                    var kPlus1Inverse = new ResidueClass<long>(k + 1, Modulus).Inverse();
                    long sign = 1;
                    for (int v = 0; v <= k; v++)
                    {
                        result += new ResidueClass<long>(v, Modulus).ToThePower(m)
                                  * kPlus1Inverse
                                  * BinomialMod.Binomial(k, v)
                                  * sign;
                        sign = -sign;
                    }
                }

                yield return result;
            }

            m++;
        }
    }
}