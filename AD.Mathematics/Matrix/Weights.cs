using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    [PublicAPI]
    public static class Weights
    {
        [Pure]
        [NotNull]
        public static double[] Weight([NotNull] this double[] a, [NotNull] double[] weights)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            if (a.Length != weights.Length)
            {
                throw new ArgumentException($"Conformability error: {nameof(a)}[{a.Length}], {nameof(weights)}[{a.Length}]");
            }

            double[] squareRootOfWeights = new double[weights.Length];

            for (int i = 0; i < squareRootOfWeights.Length; i++)
            {
                squareRootOfWeights[i] = Math.Sqrt(weights[i]);
            }

            double[] result = new double[a.Length];

            Array.Copy(a, result, result.Length);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] *= squareRootOfWeights[i];
            }

            return result;
        }

        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] Weight([NotNull][ItemNotNull] this double[][] a, [NotNull] double[] weights)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            if (a.Length != weights.Length)
            {
                throw new ArgumentException($"Conformability error: {nameof(a)}[{a.Length}][{a[0].Length}], {nameof(weights)}[{weights.Length}]");
            }

            double[] squareRootOfWeights = new double[weights.Length];

            for (int i = 0; i < squareRootOfWeights.Length; i++)
            {
                squareRootOfWeights[i] = Math.Sqrt(weights[i]);
            }

            double[][] result = new double[a.Length][];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[a[i].Length];

                Array.Copy(a[i], result[i], result[i].Length);

                for (int j = 0; j < result[0].Length; j++)
                {
                    result[i][j] *= squareRootOfWeights[i];
                }

            }

            return result;
        }
    }
}
