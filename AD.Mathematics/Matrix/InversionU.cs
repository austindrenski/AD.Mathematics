using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    [PublicAPI]
    public static class InversionU
    {
        public static double[][] InvertU(this double[][] upper)
        {
            if (upper is null)
            {
                throw new ArgumentNullException(nameof(upper));
            }

            double[][] identity = new double[upper.Length][];
            double[][] diagonalInverse = new double[upper.Length][];
            double[][] strictUpper = new double[upper.Length][];

            for (int i = 0; i < identity.Length; i++)
            {
                identity[i] = new double[upper[i].Length];
                diagonalInverse[i] = new double[upper[i].Length];
                strictUpper[i] = new double[upper[i].Length];

                for (int j = 0; j < identity[i].Length; j++)
                {
                    if (i == j)
                    {
                        identity[i][j] = 1.0;
                        diagonalInverse[i][j] = 1.0 / upper[i][j];
                    }
                    else if (i < j)
                    {
                        strictUpper[i][j] = upper[i][j];
                    }
                }
            }

            return identity.Add(strictUpper).InvertLu().CrossProduct(diagonalInverse);
        }
    }
}