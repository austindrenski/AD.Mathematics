using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Functions for in-place matrix manipulation.
    /// </summary>
    [PublicAPI]
    public static class Matrix
    {
        /// <summary>
        /// Swaps rows in an array.
        /// </summary>
        /// <param name="a">
        /// The array to mutate.
        /// </param>
        /// <param name="i">
        /// The index of the first row to swap.
        /// </param>
        /// <param name="j">
        /// The index of the other row to swap.
        /// </param>
        public static void Swap<T>([NotNull] T[] a, int i, int j)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(i));
            }

            T temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        /// <summary>
        /// Swaps rows in an array.
        /// </summary>
        /// <param name="a">
        /// The array to mutate.
        /// </param>
        /// <param name="i">
        /// The index of the first row to swap.
        /// </param>
        /// <param name="j">
        /// The index of the other row to swap.
        /// </param>
        public static void Swap<T>(T[][] a, int i, int j)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(i));
            }

            T[] temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }
}