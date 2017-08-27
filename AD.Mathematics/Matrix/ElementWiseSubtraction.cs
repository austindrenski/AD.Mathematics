using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Performs element-wise subtraction on two arrays.
    /// </summary>
    [PublicAPI]
    public static class ElementWiseSubtraction
    {
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
        public static int[] Subtract([NotNull] this int[] a, [NotNull] int[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            int[] result = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
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
        public static long[] Subtract([NotNull] this long[] a, [NotNull] long[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            long[] result = new long[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
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
        public static float[] Subtract([NotNull] this float[] a, [NotNull] float[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            float[] result = new float[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
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
        public static double[] Subtract([NotNull] this double[] a, [NotNull] double[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            double[] result = new double[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
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
        public static decimal[] Subtract([NotNull] this decimal[] a, [NotNull] decimal[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            decimal[] result = new decimal[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        public static int[] Subtract([NotNull] this int[] a, [NotNull] int[] b, [NotNull] ParallelOptions options)
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

            int[] result = new int[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = a[i] - b[i];
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        public static long[] Subtract([NotNull] this long[] a, [NotNull] long[] b, [NotNull] ParallelOptions options)
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

            long[] result = new long[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = a[i] - b[i];
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        public static float[] Subtract([NotNull] this float[] a, [NotNull] float[] b, [NotNull] ParallelOptions options)
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

            float[] result = new float[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = a[i] - b[i];
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] Subtract([NotNull] this double[] a, [NotNull] double[] b, [NotNull] ParallelOptions options)
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

            double[] result = new double[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = a[i] - b[i];
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        public static decimal[] Subtract([NotNull] this decimal[] a, [NotNull] decimal[] b, [NotNull] ParallelOptions options)
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

            decimal[] result = new decimal[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = a[i] - b[i];
            });

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
        public static int[][] Subtract([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new int[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
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
        public static long[][] Subtract([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new long[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
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
        public static float[][] Subtract([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new float[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
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
        public static double[][] Subtract([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            double[][] result = new double[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new double[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
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
        public static decimal[][] Subtract([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new decimal[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static int[][] Subtract([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new int[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
                }
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static long[][] Subtract([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new long[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
                }
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static float[][] Subtract([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new float[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
                }
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] Subtract([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            double[][] result = new double[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new double[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
                }
            });

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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The element-wise result.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static decimal[][] Subtract([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b, [NotNull] ParallelOptions options)
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
            if (a.Length != b.Length || a[0].Length != b[0].Length)
            {
                throw new InvalidOperationException("Operation requires dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new decimal[a[0].Length];

                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i][j] = a[i][j] - b[i][j];
                }
            });

            return result;
        }
    }
}