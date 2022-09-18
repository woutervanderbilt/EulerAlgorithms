using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models
{
    public class FenwickTree
    {
        private int Size { get; }
        private long Mod { get; }
        private long[] Tree { get; }
        private int Depth { get; }


        public FenwickTree(int size, long mod)
        {
            Size = size;
            Mod = mod;
            Tree = new long[size+1];
            int depth = 1;
            while (depth < size / 2)
            {
                depth *= 2;
            }

            Depth = depth;
        }

        public void Add(int index, long value)
        {
            while (index < Size)
            {
                Tree[index] = (Tree[index] + value) % Mod;
                index += HighestPowerOfTwo(index);

                int HighestPowerOfTwo(int i)
                {
                    int power = 1;
                    while (i % power == 0)
                    {
                        power *= 2;
                    }

                    return power / 2;
                }
            }
        }

        public long Range(int min, int max)
        {
            return (Sum(max) - Sum(min - 1) + Mod) % Mod;
        }

        public long Sum(int index)
        {
            int depth = Depth;
            long sum = 0;
            int currentIndex = 0;
            while (depth > 0)
            {
                if ((index & depth) != 0)
                {
                    currentIndex += depth;
                    sum = (sum + Tree[currentIndex]) % Mod;
                }
                depth /= 2;
            }

            return sum;
        }
    }
}
