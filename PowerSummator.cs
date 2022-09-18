using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Models;

namespace Algorithms
{
    public class PowerSummator
    {
        private long Modulus { get; }
        private IList<ResidueClass> BernoulliNumbers { get; }

        public PowerSummator(long modulus, IList<ResidueClass> bernoulliNumbers)
        {
            Modulus = modulus;
            BernoulliNumbers = bernoulliNumbers;
        }

        public ResidueClass SumPowers(int exponent, long limit)
        {
            var factorial = new ResidueClass(1, Modulus);
            IList<ResidueClass> factorials = new List<ResidueClass> { factorial };
            IList<ResidueClass> inverseFactorials = new List<ResidueClass> { factorial };

            for (int k = 1; k <= exponent; k++)
            {
                factorial *= k;
                factorials.Add(factorial);
                inverseFactorials.Add(factorial.Inverse());
            }
            var result = new ResidueClass(limit, Modulus).ToThePower(exponent + 1) *
                         new ResidueClass(exponent + 1, Modulus).Inverse();
            result += new ResidueClass(limit, Modulus).ToThePower(exponent) * ((Modulus + 1) / 2);
            
            for (int k = 2; k <= exponent; k++)
            {
                result += inverseFactorials[k] * BernoulliNumbers[k]
                                               * factorials[exponent] * inverseFactorials[exponent - k + 1]
                                               * new ResidueClass(limit, Modulus).ToThePower(exponent - k + 1);
            }

            return result;
        }
    }
}
