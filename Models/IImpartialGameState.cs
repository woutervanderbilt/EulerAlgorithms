using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public interface IImpartialGameState<T> where T : IImpartialGameState<T>
{
    IEnumerable<IEnumerable<T>> Moves();
    bool IsBaseLosingPosition();
}