using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    [PublicAPI]
    public static class Determinants
    {
        [Pure]
        public static float Determinant([NotNull][ItemNotNull] this float[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            (float[][] lowerUpper, int[] _, float result) = a.DecomposeLu();

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }

        [Pure]
        public static double Determinant([NotNull][ItemNotNull] this double[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            (double[][] lowerUpper, int[] _, double result) = a.DecomposeLu();

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }

        [Pure]
        public static decimal Determinant([NotNull][ItemNotNull] this decimal[][] a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            (decimal[][] lowerUpper, int[] _, decimal result) = a.DecomposeLu();

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }

        [Pure]
        public static float Determinant([NotNull][ItemNotNull] this float[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            (float[][] lowerUpper, int[] _, float result) = a.DecomposeLu(options);

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }

        [Pure]
        public static double Determinant([NotNull][ItemNotNull] this double[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            (double[][] lowerUpper, int[] _, double result) = a.DecomposeLu(options);

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }

        [Pure]
        public static decimal Determinant([NotNull][ItemNotNull] this decimal[][] a, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            (decimal[][] lowerUpper, int[] _, decimal result) = a.DecomposeLu(options);

            for (int i = 0; i < lowerUpper.Length; i++)
            {
                result *= lowerUpper[i][i];
            }

            return result;
        }
    }
}