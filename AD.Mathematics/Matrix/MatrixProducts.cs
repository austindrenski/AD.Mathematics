using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the matrix product of two arrays.
    /// </summary>
    [PublicAPI]
    public static class MatrixProducts
    {
        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] MatrixProduct([NotNull] this IEnumerable<double> a, [NotNull][ItemNotNull] IEnumerable<IEnumerable<double>> b)
        {
            return MatrixProduct(b, a);
        }

        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] MatrixProduct([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> a, [NotNull] IEnumerable<double> b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            double[][] arrayA = a as double[][] ?? a.Select(x => x as double[] ?? x.ToArray()).ToArray();
            double[] arrayB = b as double[] ?? b.ToArray();
            
            if (arrayA[0].Length != arrayB.Length)
            {
                throw new ArrayConformabilityException<double>(arrayA, arrayB);
            }

            double[] result = new double[arrayA.Length];

            for (int i = 0; i < arrayA.Length; i++)
            {
                double sum = 0.0;

                double[] innerA = arrayA[i];

                for (int j = 0; j < arrayB.Length; j++)
                {
                    sum += innerA[j] * arrayB[j];
                }

                result[i] = sum;
            }

            return result;
        }

        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] MatrixProduct([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> a, [NotNull][ItemNotNull] IEnumerable<IEnumerable<double>> b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            double[][] arrayA = a as double[][] ?? a.Select(x => x as double[] ?? x.ToArray()).ToArray();
            double[][] arrayB = b as double[][] ?? b.Select(x => x as double[] ?? x.ToArray()).ToArray();

            if (arrayA[0].Length != arrayB.Length)
            {
                throw new ArrayConformabilityException<double>(arrayA, arrayB);
            }

            double[][] result = new double[arrayA.Length][];

            for (int i = 0; i < arrayA.Length; i++)
            {
                result[i] = new double[arrayB[0].Length];

                double[] innerA = arrayA[i];
                double[] innerResult = result[i];

                for (int k = 0; k < innerA.Length; k++)
                {
                    double valueInnerA = innerA[k];

                    double[] innerB = arrayB[k];
                    
                    for (int j = 0; j < innerB.Length; j++)
                    {
                        innerResult[j] += valueInnerA * innerB[j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] MatrixProduct([NotNull] this IEnumerable<double> a, [NotNull][ItemNotNull] IEnumerable<IEnumerable<double>> b, [NotNull] ParallelOptions options)
        {
            return MatrixProduct(b, a, options);
        }

        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] MatrixProduct([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> a, [NotNull] IEnumerable<double> b, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            double[][] arrayA = a as double[][] ?? a.Select(x => x as double[] ?? x.ToArray()).ToArray();
            double[] arrayB = b as double[] ?? b.ToArray();

            if (arrayA[0].Length != arrayB.Length)
            {
                throw new ArrayConformabilityException<double>(arrayA, arrayB);
            }

            double[] result = new double[arrayA.Length];

            Parallel.For(0, arrayA.Length, options, i =>
            {
                double sum = 0.0;

                double[] innerA = arrayA[i];

                for (int j = 0; j < arrayB.Length; j++)
                {
                    sum += innerA[j] * arrayB[j];
                }

                result[i] = sum;
            });

            return result;
        }

        /// <summary>
        /// Calculates the matrix product of two arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand array.
        /// </param>
        /// <param name="b">
        /// The right-hand array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] MatrixProduct([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> a, [NotNull][ItemNotNull] IEnumerable<IEnumerable<double>> b, [NotNull] ParallelOptions options)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            double[][] arrayA = a as double[][] ?? a.Select(x => x as double[] ?? x.ToArray()).ToArray();
            double[][] arrayB = b as double[][] ?? b.Select(x => x as double[] ?? x.ToArray()).ToArray();

            if (arrayA[0].Length != arrayB.Length)
            {
                throw new ArrayConformabilityException<double>(arrayA, arrayB);
            }

            double[][] result = new double[arrayA.Length][];
            
            Parallel.For(0, arrayA.Length, options, i =>
            {
                result[i] = new double[arrayB[0].Length];

                double[] innerA = arrayA[i];
                double[] innerResult = result[i];

                for (int k = 0; k < innerA.Length; k++)
                {
                    double valueInnerA = innerA[k];

                    double[] innerB = arrayB[k];

                    for (int j = 0; j < innerB.Length; j++)
                    {
                        innerResult[j] += valueInnerA * innerB[j];
                    }
                }
            });

            return result;
        }
    }
}