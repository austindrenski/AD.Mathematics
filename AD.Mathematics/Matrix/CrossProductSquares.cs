using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the cross product of two arrays that are square and share the same dimensions.
    /// </summary>
    [PublicAPI]
    public static class CrossProductSquares
    {
        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static int[][] CrossProductSquare([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            int[][] result = new int[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            int[][] bTranspose = b.Transpose();

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new int[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    int sum = default(int);

                    unsafe
                    {
                        fixed (int* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static long[][] CrossProductSquare([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            long[][] result = new long[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            long[][] bTranspose = b.Transpose();

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new long[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    long sum = default(long);

                    unsafe
                    {
                        fixed (long* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static float[][] CrossProductSquare([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            float[][] result = new float[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            float[][] bTranspose = b.Transpose();

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new float[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    float sum = default(float);

                    unsafe
                    {
                        fixed (float* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static double[][] CrossProductSquare([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            double[][] result = new double[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new double[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (double* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
                        {
                            for (int k = 0; k < bColumns; k++)
                            {
                                pointerToResult[k] += pointerToA[j] * pointerToB[k];
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static decimal[][] CrossProductSquare([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            decimal[][] result = new decimal[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            decimal[][] bTranspose = b.Transpose();

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new decimal[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    decimal sum = default(decimal);

                    unsafe
                    {
                        fixed (decimal* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static int[][] CrossProductSquare([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b, [NotNull] ParallelOptions options)
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
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            int[][] bTranspose = b.Transpose();

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new int[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    int sum = default(int);

                    unsafe
                    {
                        fixed (int* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static long[][] CrossProductSquare([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b, [NotNull] ParallelOptions options)
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
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            long[][] bTranspose = b.Transpose();

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new long[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    long sum = default(long);

                    unsafe
                    {
                        fixed (long* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static float[][] CrossProductSquare([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b, [NotNull] ParallelOptions options)
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
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            float[][] bTranspose = b.Transpose();

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new float[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    float sum = default(float);

                    unsafe
                    {
                        fixed (float* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static double[][] CrossProductSquare([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b, [NotNull] ParallelOptions options)
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
            if (a[0].Length != b.Length || a.Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires square inputs.");
            }

            double[][] result = new double[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            double[][] bTranspose = b.Transpose();

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new double[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    double sum = default(double);

                    unsafe
                    {
                        fixed (double* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Calculates the cross product of two arrays.
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
        public static decimal[][] CrossProductSquare([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b, [NotNull] ParallelOptions options)
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
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            decimal[][] bTranspose = b.Transpose();

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new decimal[bColumns];

                for (int j = 0; j < bColumns; j++)
                {
                    decimal sum = default(decimal);

                    unsafe
                    {
                        fixed (decimal* pointerToA = &a[i][0], pointerToB = &bTranspose[j][0], pointerToResult = &result[i][0])
                        {
                            for (int k = 0; k < aColumns; k++)
                            {
                                sum += pointerToA[k] * pointerToB[k];
                            }

                            pointerToResult[j] = sum;
                        }
                    }
                }
            });

            return result;
        }
    }
}