using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Decomposes an array using Doolittle's algorithm.
    /// </summary>
    [PublicAPI]
    public static class LuDecomposition
    {
        /// <summary>
        /// Numerical tolerance for floating-point comparisons.
        /// </summary>
        private static readonly double Tolerance = 1e-15;

        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (float[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this float[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            float[][] result = a.CloneArray();

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            for (int i = 0; i < a.Length - 1; i++)
            {
                float columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                if (Math.Abs(result[i][i]) < Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            }

            return (result, permutation, rowSwap);
        }

        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (double[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this double[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            double[][] result = a.CloneArray();

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            for (int i = 0; i < a.Length - 1; i++)
            {
                double columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                if (Math.Abs(result[i][i]) < Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            }

            return (result, permutation, rowSwap);
        }

        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (decimal[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this decimal[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            decimal[][] result = a.CloneArray();

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            for (int i = 0; i < a.Length - 1; i++)
            {
                decimal columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                if (Math.Abs(result[i][i]) < (decimal) Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > (decimal) Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            }

            return (result, permutation, rowSwap);
        }
     
        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (float[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this float[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            float[][] result = a.CloneArray(options);

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            for (int i = 0; i < a.Length - 1; i++)
            {
                float columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                // => |result[i][i]| < Tolerance
                if (Math.Abs(result[i][i]) < Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            }

            return (result, permutation, rowSwap);
        }

        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (double[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this double[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            double[][] result = a.CloneArray(options);

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            //for (int i = 0; i < a.Length - 1; i++)
            Parallel.For(0, a.Length - 1, i =>
            {
                double columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                // => |result[i][i]| < Tolerance
                if (Math.Abs(result[i][i]) < Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            });

            return (result, permutation, rowSwap);
        }

        /// <summary>
        /// Decomposes an array using Doolittle's Lower-Upper-Permutation method.
        /// </summary>
        /// <param name="a">
        /// The input array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The lower array with 1's on the diagonal and the permutation vector.
        /// </returns>
        [Pure]
        public static (decimal[][] LowerUpper, int[] Permutation, int RowSwap) DecomposeLu([NotNull][ItemNotNull] this decimal[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (a.Length != a[0].Length)
            {
                throw new InvalidOperationException("Input array should be square.");
            }

            decimal[][] result = a.CloneArray(options);

            int[] permutation = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                permutation[i] = i;
            }

            int rowSwap = 1;

            for (int i = 0; i < a.Length - 1; i++)
            {
                decimal columnMax = Math.Abs(result[i][i]);

                int pivotRow = i;

                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Math.Abs(result[j][i]) <= columnMax)
                    {
                        continue;
                    }
                    columnMax = Math.Abs(result[j][i]);
                    pivotRow = j;
                }

                if (pivotRow != i)
                {
                    Matrix.Swap(result, pivotRow, i);
                    Matrix.Swap(permutation, pivotRow, i);

                    rowSwap = -rowSwap;
                }

                // => |result[i][i]| < Tolerance
                if (Math.Abs(result[i][i]) < (decimal) Tolerance)
                {
                    int swapCandidate = -1;

                    for (int j = i + 1; j < a.Length; j++)
                    {
                        if (Math.Abs(result[j][i]) > (decimal) Tolerance)
                        {
                            swapCandidate = j;
                        }
                    }

                    if (swapCandidate == -1)
                    {
                        throw new InvalidOperationException("No suitable candidate to swap. Doolittle's method was unsuccessful.");
                    }

                    Matrix.Swap(result, swapCandidate, i);
                    Matrix.Swap(permutation, swapCandidate, i);

                    rowSwap = -rowSwap;
                }

                for (int j = i + 1; j < a.Length; j++)
                {
                    result[j][i] /= result[i][i];

                    for (int k = i + 1; k < a.Length; k++)
                    {
                        result[j][k] -= result[j][i] * result[i][k];
                    }
                }
            }

            return (result, permutation, rowSwap);
        }
    }
}