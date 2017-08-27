using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Generic extension methods for appending a column of constants to an array.
    /// </summary>
    [PublicAPI]
    public static class AppendColumn
    {
        /// <summary>
        /// Resizes an array such that each row is appended by the specified value.
        /// </summary>
        /// <param name="a">
        /// An array to resize.
        /// </param>
        /// <param name="value">
        /// The value to prepend to each row.
        /// </param>
        /// <returns>
        /// An array whose rows are prepended by the specified value.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] Append<T>([NotNull][ItemNotNull] this T[][] a, T value)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            T[][] result = new T[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new T[a[i].Length + 1];
                result[i][result.Length] = value;
                Array.Copy(a[i], 0, result[i], 0, a[i].Length);
            }

            return result;
        }

        /// <summary>
        /// Resizes an array such that each row is appended by the specified value.
        /// </summary>
        /// <param name="a">
        /// An array to resize.
        /// </param>
        /// <param name="value">
        /// The value to prepend to each row.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// An array whose rows are prepended by the specified value.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] Append<T>([NotNull][ItemNotNull] this T[][] a, T value, ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            T[][] result = new T[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new T[a[i].Length + 1];
                result[i][result.Length] = value;
                Array.Copy(a[i], 0, result[i], 0, a[i].Length);
            });

            return result;
        }
    }
}