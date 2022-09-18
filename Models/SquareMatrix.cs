using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models
{
    public class SquareMatrix : Matrix
    {
        public SquareMatrix(int size, long? modulus = null) : base(size, size, modulus)
        {
        }

        public static SquareMatrix Identity(int size, long? modulus)
        {
            var result = new SquareMatrix(size, modulus);
            for (int i = 0; i < size; i++)
            {
                result[i, i] = 1;
            }

            return result;
        }

        public SquareMatrix PowerMod(long e)
        {
            if (e == 0)
            {
                return Identity(NumberOfColumns, Modulus);
            }
            SquareMatrix result = null;
            SquareMatrix currentPower = this;
            while (e > 0)
            {
                if (e % 2 == 1)
                {
                    if (result == null)
                    {
                        result = currentPower;
                    }
                    else
                    {
                        result = (SquareMatrix)result.Times(currentPower);
                    }
                }

                currentPower = (SquareMatrix) currentPower.Times(currentPower);
                e /= 2;
            }

            return result;
        }

        public SquareMatrix Inverse()
        {
            if (Modulus == null)
            {
                throw new Exception("Modulus is null");
            }
            IList<IList<long>> oldMatrix = new List<IList<long>>();
            IList<IList<long>> newMatrix = new List<IList<long>>();
            for (int r = 0; r < NumberOfColumns; r++)
            {
                IList<long> oldRow = new List<long>();
                IList<long> newRow = new List<long>();
                for (int c = 0; c < NumberOfColumns; c++)
                {
                    newRow.Add(c == r ? 1 : 0);
                    oldRow.Add(this[r, c]);
                }
                oldMatrix.Add(oldRow);
                newMatrix.Add(newRow);
            }

            for (int i = 0; i < NumberOfColumns; i++)
            {
                int j = i;
                while (oldMatrix[i][i] == 0)
                {
                    j++;
                }

                if (j != i)
                {
                    MoveRow(j, i);
                }
                ScalarMultiplyRow(i, new ResidueClass(oldMatrix[i][i], Modulus.Value).Inverse().Value);

                for (int k = i + 1; k < NumberOfColumns; k++)
                {
                    if (oldMatrix[k][i] != 0)
                    {
                        AddNthRowKTimesToMthRow(i, k, -oldMatrix[k][i]);
                    }
                }
            }


            for (int i = NumberOfColumns - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    if (oldMatrix[k][i] != 0)
                    {
                        AddNthRowKTimesToMthRow(i, k, -oldMatrix[k][i]);
                    }
                }
            }





            var result = new SquareMatrix(NumberOfColumns);
            for (int r = 0; r < NumberOfColumns; r++)
            {
                for (int c = 0; c < NumberOfColumns; c++)
                {
                    result[r, c] = newMatrix[r][c];
                }
            }

            return result;

            void MoveRow(int from, int to)
            {
                var oldRowToMove = oldMatrix[from];
                oldMatrix.Insert(to, oldRowToMove);
                oldMatrix.RemoveAt(from + (to <= from ? 1 : 0));

                var newRoTowMove = newMatrix[from];
                newMatrix.Insert(to, newRoTowMove);
                newMatrix.RemoveAt(from + (to <= from ? 1 : 0));
            }

            void AddNthRowKTimesToMthRow(int n, int m, long k)
            {
                var oldNthRow = oldMatrix[n];
                var oldMthRow = oldMatrix[m];
                var newNthRow = newMatrix[n];
                var newMthRow = newMatrix[m];
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    oldMthRow[i] = (oldMthRow[i] + k * oldNthRow[i]) % Modulus.Value;
                    newMthRow[i] = (newMthRow[i] + k * newNthRow[i]) % Modulus.Value;
                }
            }

            void ScalarMultiplyRow(int n, long k)
            {
                var oldRow = oldMatrix[n];
                var newRow = newMatrix[n];
                for (int c = 0; c < NumberOfColumns; c++)
                {
                    oldRow[c] = oldRow[c] * k % Modulus.Value;
                    newRow[c] = newRow[c] * k % Modulus.Value;
                }
            }

        }

    }
}
