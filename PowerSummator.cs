using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Models;

namespace Algorithms;

public class PowerSummator
{
    private long Modulus { get; }
    private IList<ResidueClass<long>> BernoulliNumbers { get; }

    public PowerSummator(long modulus, IList<ResidueClass<long>> bernoulliNumbers)
    {
        Modulus = modulus;
        BernoulliNumbers = bernoulliNumbers;
    }

    public ResidueClass<long> SumPowers(int exponent, long limit)
    {
        var factorial = new ResidueClass<long>(1, Modulus);
        IList<ResidueClass<long>> factorials = new List<ResidueClass<long>> { factorial };
        IList<ResidueClass<long>> inverseFactorials = new List<ResidueClass<long>> { factorial };

        for (int k = 1; k <= exponent; k++)
        {
            factorial *= k;
            factorials.Add(factorial);
            inverseFactorials.Add(factorial.Inverse());
        }
        var result = new ResidueClass<long>(limit, Modulus).ToThePower(exponent + 1) *
                     new ResidueClass<long>(exponent + 1, Modulus).Inverse();
        result += new ResidueClass<long>(limit, Modulus).ToThePower(exponent) * ((Modulus + 1) / 2);
            
        for (int k = 2; k <= exponent; k++)
        {
            result += inverseFactorials[k] * BernoulliNumbers[k]
                                           * factorials[exponent] * inverseFactorials[exponent - k + 1]
                                           * new ResidueClass<long>(limit, Modulus).ToThePower(exponent - k + 1);
        }

        return result;
    }
}