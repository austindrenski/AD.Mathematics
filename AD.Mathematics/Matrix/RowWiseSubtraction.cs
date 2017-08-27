using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Performs row-wise subtraction on two arrays.
    /// </summary>
    [PublicAPI]
    public static class RowWiseSubtraction
    {
        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static int[][] RowSubtract([NotNull][ItemNotNull] this int[][] a, [NotNull] int[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new int[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Performs element-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static long[][] RowSubtract([NotNull][ItemNotNull] this long[][] a, [NotNull] long[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new long[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Performs element-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static float[][] RowSubtract([NotNull][ItemNotNull] this float[][] a, [NotNull] float[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new float[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Performs element-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] RowSubtract([NotNull][ItemNotNull] this double[][] a, [NotNull] double[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            double[][] result = new double[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new double[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Performs element-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static decimal[][] RowSubtract([NotNull][ItemNotNull] this decimal[][] a, [NotNull] decimal[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new decimal[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static int[][] RowSubtract([NotNull][ItemNotNull] this int[][] a, [NotNull] int[] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            Parallel.For(0, a.Length, i =>
            {
                result[i] = new int[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static long[][] RowSubtract([NotNull][ItemNotNull] this long[][] a, [NotNull] long[] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            Parallel.For(0, a.Length, i =>
            {
                result[i] = new long[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static float[][] RowSubtract([NotNull][ItemNotNull] this float[][] a, [NotNull] float[] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            Parallel.For(0, a.Length, i =>
            {
                result[i] = new float[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] RowSubtract([NotNull][ItemNotNull] this double[][] a, [NotNull] double[] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            double[][] result = new double[a.Length][];

            Parallel.For(0, a.Length, i =>
            {
                result[i] = new double[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs row-wise subtraction on the arrays.
        /// </summary>
        /// <param name="a">
        /// The left-hand side array.
        /// </param>
        /// <param name="b">
        /// The right-hand side array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static decimal[][] RowSubtract([NotNull][ItemNotNull] this decimal[][] a, [NotNull] decimal[] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            Parallel.For(0, a.Length, i =>
            {
                result[i] = new decimal[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[j];
                }
            });

            return result;
        }
    }
}