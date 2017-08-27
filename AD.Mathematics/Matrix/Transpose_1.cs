using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Generic extension methods for transposing arrays.
    /// </summary>
    [PublicAPI]
    public static class MatrixTranspose
    {
        /// <summary>
        /// Transposes the rows and columns of an array.
        /// </summary>
        /// <param name="a">
        /// The array to transpose.
        /// </param>
        /// <returns>
        /// The transposed array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] Transpose<T>([NotNull][ItemNotNull] this T[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            T[][] result = new T[a[0].Length][];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new T[a.Length];
            }

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[j][i] = a[i][j];
                }
            }

            return result;
        }

        /// <summary>
        /// Transposes the rows and columns of an array.
        /// </summary>
        /// <param name="a">
        /// The array to transpose.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The transposed array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] Transpose<T>([NotNull][ItemNotNull] this T[][] a, ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            T[][] result = new T[a[0].Length][];

            Parallel.For(0, result.Length, options, i =>
            {
                result[i] = new T[a.Length];
            });

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[j][i] = a[i][j];
                }
            });

            return result;
        }
    }
}