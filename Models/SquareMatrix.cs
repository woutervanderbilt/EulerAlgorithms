using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Models;

public class SquareMatrix<T> : Matrix<T> where T : struct, INumber<T>
{
    public SquareMatrix(int size, T? modulus = null) : base(size, size, modulus)
    {
    }

    public static SquareMatrix<T> Identity(int size, T? modulus = null)
    {
        var result = new SquareMatrix<T>(size, modulus);
        for (int i = 0; i < size; i++)
        {
            result[i, i] = T.One;
        }

        return result;
    }

    public SquareMatrix<T> ToThePower(long e)
    {
        if (e == 0)
        {
            return Identity(NumberOfColumns, Modulus);
        }
        SquareMatrix<T> result = null;
        SquareMatrix<T> currentPower = this;
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
                    result = (SquareMatrix<T>)result.Times(currentPower);
                }
            }

            currentPower = (SquareMatrix<T>) currentPower.Times(currentPower);
            e /= 2;
        }

        return result;
    }

    public SquareMatrix<T> Inverse()
    {
        if (Modulus == null)
        {
            throw new Exception("Modulus is null");
        }
        IList<IList<T>> oldMatrix = new List<IList<T>>();
        IList<IList<T>> newMatrix = new List<IList<T>>();
        for (int r = 0; r < NumberOfColumns; r++)
        {
            IList<T> oldRow = new List<T>();
            IList<T> newRow = new List<T>();
            for (int c = 0; c < NumberOfColumns; c++)
            {
                newRow.Add(c == r ? T.One : T.Zero);
                oldRow.Add(this[r, c]);
            }
            oldMatrix.Add(oldRow);
            newMatrix.Add(newRow);
        }

        for (int i = 0; i < NumberOfColumns; i++)
        {
            int j = i;
            while (oldMatrix[i][i] == T.Zero)
            {
                j++;
            }

            if (j != i)
            {
                MoveRow(j, i);
            }

            if (Modulus.HasValue)
            {
                ScalarMultiplyRow(i, new ResidueClass<T>(oldMatrix[i][i], Modulus.Value).Inverse().Value);
            }
            else
            {
                ScalarMultiplyRow(i, T.One/oldMatrix[i][i]);
            }

            for (int k = i + 1; k < NumberOfColumns; k++)
            {
                if (oldMatrix[k][i] != T.Zero)
                {
                    AddNthRowKTimesToMthRow(i, k, -oldMatrix[k][i]);
                }
            }
        }


        for (int i = NumberOfColumns - 1; i >= 0; i--)
        {
            for (int k = i - 1; k >= 0; k--)
            {
                if (oldMatrix[k][i] != T.Zero)
                {
                    AddNthRowKTimesToMthRow(i, k, -oldMatrix[k][i]);
                }
            }
        }





        var result = new SquareMatrix<T>(NumberOfColumns);
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

        void AddNthRowKTimesToMthRow(int n, int m, T k)
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

        void ScalarMultiplyRow(int n, T k)
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