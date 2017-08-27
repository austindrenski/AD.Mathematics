using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    [PublicAPI]
    public static class SeparationLu
    {
        public static (double[][] Lower, double[][] Upper) SeparateLu(this (double[][] LowerUpper, int[] Permutation, int RowSwap) lowerUpperResult)
        {
            double[][] result = lowerUpperResult.LowerUpper.CloneArray();

            for (int i = lowerUpperResult.Permutation.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < result[0].Length; j++)
                {
                    double temp = result[i][j];
                    result[i][j] = result[lowerUpperResult.Permutation[i]][j];
                    result[lowerUpperResult.Permutation[i]][j] = temp;
                }
            }

            double[][] lower = new double[result.Length][];
            double[][] upper = new double[result.Length][];
            for (int i = 0; i < lower.Length; i++)
            {
                lower[i] = new double[result[i].Length];
                upper[i] = new double[result[i].Length];
            }

            for (int i = 0; i < lower.Length; i++)
            {
                for (int j = 0; j < lower[i].Length; j++)
                {
                    if (i <= j)
                    {
                        upper[i][j] = result[i][j];
                    }
                    if (i >= j)
                    {
                        lower[i][j] = i == j ? 1.0 : result[i][j];
                    }
                }
            }

            return (lower, upper);
        }
    }
}