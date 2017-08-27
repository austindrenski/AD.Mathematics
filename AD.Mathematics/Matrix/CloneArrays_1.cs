using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Generic extension methods for cloning arrays.
    /// </summary>
    [PublicAPI]
    public static class CloneArrays
    {
        /// <summary>
        /// Clones a jagged array.
        /// </summary>
        /// <param name="a">
        /// The array to clone.
        /// </param>
        /// <returns>
        /// A deep-clone of the input array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] CloneArray<T>([NotNull][ItemNotNull] this T[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            T[][] result = new T[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new T[a[i].Length];
                Array.Copy(a[i], result[i], a[i].Length);
            }

            return result;
        }

        /// <summary>
        /// Clones a jagged array.
        /// </summary>
        /// <param name="a">
        /// The array to clone.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// A deep-clone of the input array.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static T[][] CloneArray<T>([NotNull][ItemNotNull] this T[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            T[][] result = new T[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new T[a[i].Length];
                Array.Copy(a[i], result[i], a[i].Length);
            });

            return result;
        }
    }
}