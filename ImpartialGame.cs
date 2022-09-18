using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Models;

namespace Algorithms
{
    public class ImpartialGame<TState, TEqualityComparer> 
        where TState : IImpartialGameState<TState>
        where TEqualityComparer : IEqualityComparer<TState>, new()
    {
        private readonly IDictionary<TState, int> nimbers = new Dictionary<TState, int>(new TEqualityComparer());

        public int GetNimber(TState state)
        {
            if (state.IsBaseLosingPosition())
            {
                return 0;
            }

            if (nimbers.TryGetValue(state, out var n))
            {
                return n;
            }
            HashSet<int> smallerNimbers = new HashSet<int>();
            foreach (var impartialGameState in state.Moves())
            {
                smallerNimbers.Add(impartialGameState.Aggregate(0, (a,s) => a ^ GetNimber(s)));
            }

            for (int nimber = 0;; nimber++)
            {
                if (!smallerNimbers.Contains(nimber))
                {
                    nimbers[state] = nimber;
                    return nimber;
                }
            }
        }
    }
}
