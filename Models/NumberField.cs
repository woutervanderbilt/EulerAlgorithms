using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public class NumberField
{
    public IList<long> Minimal { get; }
    public long? Characteristic { get; }



    public NumberField(IList<long> minimal, long? characteristic = null)
    {
        Minimal = minimal;
        Characteristic = characteristic;
    }
}