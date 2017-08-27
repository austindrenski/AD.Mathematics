using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates a vector of column means for an array.
    /// </summary>
    [PublicAPI]
    public static class ColumnMeans
    {
        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static float[] ColumnMean([NotNull][ItemNotNull] this float[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            float[] result = new float[a.Length];

            for (int i = 0; i < a[0].Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            }

            return result;
        }

        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] ColumnMean([NotNull][ItemNotNull] this double[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            double[] result = new double[a.Length];

            for (int i = 0; i < a[0].Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            }

            return result;
        }

        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static decimal[] ColumnMean([NotNull][ItemNotNull] this decimal[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            decimal[] result = new decimal[a.Length];

            for (int i = 0; i < a[0].Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            }

            return result;
        }

        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static float[] ColumnMean([NotNull][ItemNotNull] this float[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            float[] result = new float[a.Length];

            Parallel.For(0, a[0].Length, options, i =>
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            });

            return result;
        }       
        
        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] ColumnMean([NotNull][ItemNotNull] this double[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            double[] result = new double[a.Length];

            Parallel.For(0, a[0].Length, options, i =>
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            });

            return result;
        }       
        
        /// <summary>
        /// Calculates a vector of column means.
        /// </summary>
        /// <param name="a">
        /// The array from which the column means are calculated.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The array of column means. 
        /// </returns>
        [Pure]
        [NotNull]
        public static decimal[] ColumnMean([NotNull][ItemNotNull] this decimal[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            decimal[] result = new decimal[a.Length];

            Parallel.For(0, a[0].Length, options, i =>
            {
                for (int j = 0; j < a.Length; j++)
                {
                    result[i] += a[j][i];
                }

                result[i] /= a.Length;
            });

            return result;
        }
    }
}