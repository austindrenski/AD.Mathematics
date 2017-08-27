using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the inverse of a square array with LU decomposition.
    /// </summary>
    [PublicAPI]
    public static class InversionLu
    {
        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static float[][] InvertLu([NotNull][ItemNotNull] this float[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            float[][] result = a.CloneArray();

            (float[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu();

            for (int i = 0; i < a.Length; i++)
            {
                float[] b = new float[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                float[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] InvertLu([NotNull][ItemNotNull] this double[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            double[][] result = a.CloneArray();

            (double[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu();

            for (int i = 0; i < a.Length; i++)
            {
                double[] b = new double[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                double[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static decimal[][] InvertLu([NotNull][ItemNotNull] this decimal[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            decimal[][] result = a.CloneArray();

            (decimal[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu();

            for (int i = 0; i < a.Length; i++)
            {
                decimal[] b = new decimal[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                decimal[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static float[][] InvertLu([NotNull][ItemNotNull] this float[][] a, [NotNull] ParallelOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            float[][] result = a.CloneArray(options);

            (float[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu(options);

            Parallel.For(0, a.Length, options, i =>
            {
                float[] b = new float[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                float[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] InvertLu([NotNull][ItemNotNull] this double[][] a, [NotNull] ParallelOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            double[][] result = a.CloneArray(options);

            (double[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu(options);

            Parallel.For(0, a.Length, options, i =>
            {
                double[] b = new double[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                double[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the inverse array.
        /// </summary>
        /// <param name="a">
        /// The array to invert.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The inverse of the array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static decimal[][] InvertLu([NotNull][ItemNotNull] this decimal[][] a, [NotNull] ParallelOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            decimal[][] result = a.CloneArray(options);

            (decimal[][] lowerUpper, int[] permutation, int _) = a.DecomposeLu(options);

            Parallel.For(0, a.Length, options, i =>
            {
                decimal[] b = new decimal[a.Length];

                for (int j = 0; j < permutation.Length; j++)
                {
                    b[j] = i == permutation[j] ? 1 : 0;
                }

                decimal[] x = InversionHelper(lowerUpper, b);

                for (int j = 0; j < a.Length; j++)
                {
                    result[j][i] = x[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Helps to solve the regression equation.
        /// </summary>
        /// <param name="lowerUpper">
        /// The lower-upper decomposed matrix.
        /// </param>
        /// <param name="b">
        /// A vector permuted by the permutation array generated by the lower-upper decomposition.
        /// </param>
        /// <returns>
        /// An array.
        /// </returns>
        [Pure]
        [NotNull]
        private static float[] InversionHelper([NotNull][ItemNotNull] this float[][] lowerUpper, [NotNull] float[] b)
        {
            if (lowerUpper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            float[] result = new float[lowerUpper.Length];

            Array.Copy(b, result, result.Length);

            for (int i = 1; i < lowerUpper.Length; i++)
            {
                result[i] -= result.DotProduct(lowerUpper[i], 0, i);
            }

            int index = lowerUpper.Length - 1;

            result[index] /= lowerUpper[index][index];

            for (int i = lowerUpper.Length - 2; i >= 0; i--)
            {
                result[i] -= result.DotProduct(lowerUpper[i], i + 1, lowerUpper.Length);

                result[i] /= lowerUpper[i][i];
            }

            return result;
        }

        /// <summary>
        /// Helps to solve the regression equation.
        /// </summary>
        /// <param name="lowerUpper">
        /// The lower-upper decomposed matrix.
        /// </param>
        /// <param name="b">
        /// A vector permuted by the permutation array generated by the lower-upper decomposition.
        /// </param>
        /// <returns>
        /// An array.
        /// </returns>
        [Pure]
        [NotNull]
        private static double[] InversionHelper([NotNull][ItemNotNull] this double[][] lowerUpper, [NotNull] double[] b)
        {
            if (lowerUpper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            double[] result = new double[lowerUpper.Length];

            Array.Copy(b, result, result.Length);

            for (int i = 1; i < lowerUpper.Length; i++)
            {
                result[i] -= result.DotProduct(lowerUpper[i], 0, i);
            }

            int index = lowerUpper.Length - 1;

            result[index] /= lowerUpper[index][index];

            for (int i = lowerUpper.Length - 2; i >= 0; i--)
            {
                result[i] -= result.DotProduct(lowerUpper[i], i + 1, lowerUpper.Length);

                result[i] /= lowerUpper[i][i];
            }

            return result;
        }

        /// <summary>
        /// Helps to solve the regression equation.
        /// </summary>
        /// <param name="lowerUpper">
        /// The lower-upper decomposed matrix.
        /// </param>
        /// <param name="b">
        /// A vector permuted by the permutation array generated by the lower-upper decomposition.
        /// </param>
        /// <returns>
        /// An array.
        /// </returns>
        [Pure]
        [NotNull]
        private static decimal[] InversionHelper([NotNull][ItemNotNull] this decimal[][] lowerUpper, [NotNull] decimal[] b)
        {
            if (lowerUpper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            decimal[] result = new decimal[lowerUpper.Length];

            Array.Copy(b, result, result.Length);

            for (int i = 1; i < lowerUpper.Length; i++)
            {
                result[i] -= result.DotProduct(lowerUpper[i], 0, i);
            }

            int index = lowerUpper.Length - 1;

            result[index] /= lowerUpper[index][index];

            for (int i = lowerUpper.Length - 2; i >= 0; i--)
            {
                result[i] -= result.DotProduct(lowerUpper[i], i + 1, lowerUpper.Length);

                result[i] /= lowerUpper[i][i];
            }

            return result;
        }
    }
}