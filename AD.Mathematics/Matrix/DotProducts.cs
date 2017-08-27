using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the dot product of two arrays.
    /// </summary>
    [PublicAPI]
    public static class DotProducts
    {
        /// <summary>
        /// Calculates the dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="lower">
        /// The inclusive lower bound.
        /// </param>
        /// <param name="upper">
        /// The exclusive upper bound.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        [Pure]
        public static int DotProduct([NotNull] this int[] a, [NotNull] int[] b, int lower, int upper)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            int result = default(int);

            for (int i = lower; i < upper; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="lower">
        /// The inclusive lower bound.
        /// </param>
        /// <param name="upper">
        /// The exclusive upper bound.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        [Pure]
        public static long DotProduct([NotNull] this long[] a, [NotNull] long[] b, int lower, int upper)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            long result = default(long);

            for (int i = lower; i < upper; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="lower">
        /// The inclusive lower bound.
        /// </param>
        /// <param name="upper">
        /// The exclusive upper bound.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        [Pure]
        public static float DotProduct([NotNull] this float[] a, [NotNull] float[] b, int lower, int upper)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            float result = default(float);

            for (int i = lower; i < upper; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="lower">
        /// The inclusive lower bound.
        /// </param>
        /// <param name="upper">
        /// The exclusive upper bound.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        [Pure]
        public static double DotProduct([NotNull] this double[] a, [NotNull] double[] b, int lower, int upper)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            double result = default(double);

            for (int i = lower; i < upper; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="lower">
        /// The inclusive lower bound.
        /// </param>
        /// <param name="upper">
        /// The exclusive upper bound.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        [Pure]
        public static decimal DotProduct([NotNull] this decimal[] a, [NotNull] decimal[] b, int lower, int upper)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            decimal result = default(decimal);

            for (int i = lower; i < upper; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }
    }
}