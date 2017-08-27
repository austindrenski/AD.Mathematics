using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the cross product of two arrays.
    /// </summary>
    [PublicAPI]
    public static class CrossProducts
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
        public static int[] CrossProduct([NotNull] this int[] a, [NotNull][ItemNotNull] int[][] b)
        {
            return CrossProduct(b, a);
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
        public static long[] CrossProduct([NotNull] this long[] a, [NotNull][ItemNotNull] long[][] b)
        {
            return CrossProduct(b, a);
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
        public static float[] CrossProduct([NotNull] this float[] a, [NotNull][ItemNotNull] float[][] b)
        {
            return CrossProduct(b, a);
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
        public static double[] CrossProduct([NotNull] this double[] a, [NotNull][ItemNotNull] double[][] b)
        {
            return CrossProduct(b, a);
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
        public static decimal[] CrossProduct([NotNull] this decimal[] a, [NotNull][ItemNotNull] decimal[][] b)
        {
            return CrossProduct(b, a);
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
        public static int[] CrossProduct([NotNull] this int[] a, [NotNull][ItemNotNull] int[][] b, [NotNull] ParallelOptions options)
        {
            return CrossProduct(b, a, options);
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
        public static long[] CrossProduct([NotNull] this long[] a, [NotNull][ItemNotNull] long[][] b, [NotNull] ParallelOptions options)
        {
            return CrossProduct(b, a, options);
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
        public static float[] CrossProduct([NotNull] this float[] a, [NotNull][ItemNotNull] float[][] b, [NotNull] ParallelOptions options)
        {
            return CrossProduct(b, a, options);
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
        public static double[] CrossProduct([NotNull] this double[] a, [NotNull][ItemNotNull] double[][] b, [NotNull] ParallelOptions options)
        {
            return CrossProduct(b, a, options);
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
        public static decimal[] CrossProduct([NotNull] this decimal[] a, [NotNull][ItemNotNull] decimal[][] b, [NotNull] ParallelOptions options)
        {
            return CrossProduct(b, a, options);
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
        public static int[] CrossProduct([NotNull][ItemNotNull] this int[][] a, [NotNull] int[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            int[] result = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static long[] CrossProduct([NotNull][ItemNotNull] this long[][] a, [NotNull] long[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            long[] result = new long[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static float[] CrossProduct([NotNull][ItemNotNull] this float[][] a, [NotNull] float[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            float[] result = new float[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static double[] CrossProduct([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> a, [NotNull] IEnumerable<double> b)
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
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            double[] result = new double[arrayA.Length];

            for (int i = 0; i < arrayA.Length; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < arrayA[0].Length; j++)
                {
                    sum += arrayA[i][j] * arrayB[j];
                }

                result[i] = sum;
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
        public static decimal[] CrossProduct([NotNull][ItemNotNull] this decimal[][] a, [NotNull] decimal[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            decimal[] result = new decimal[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static int[] CrossProduct([NotNull][ItemNotNull] this int[][] a, [NotNull] int[] b, [NotNull] ParallelOptions options)
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

            int[] result = new int[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static long[] CrossProduct([NotNull][ItemNotNull] this long[][] a, [NotNull] long[] b, [NotNull] ParallelOptions options)
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

            long[] result = new long[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static float[] CrossProduct([NotNull][ItemNotNull] this float[][] a, [NotNull] float[] b, [NotNull]  ParallelOptions options)
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

            float[] result = new float[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static double[] CrossProduct([NotNull][ItemNotNull] this double[][] a, [NotNull] double[] b, [NotNull] ParallelOptions options)
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

            double[] result = new double[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        public static decimal[] CrossProduct([NotNull][ItemNotNull] this decimal[][] a, [NotNull] decimal[] b, [NotNull] ParallelOptions options)
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

            decimal[] result = new decimal[a.Length];

            Parallel.For(0, a.Length, options, i =>
            {
                for (int j = 0; j < a[0].Length; j++)
                {
                    result[i] += a[i][j] * b[j];
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
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static int[][] CrossProduct([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            int[][] result = new int[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new int[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (int* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
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
        public static long[][] CrossProduct([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            long[][] result = new long[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new long[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (long* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
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
        public static float[][] CrossProduct([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            float[][] result = new float[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new float[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (float* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
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
        public static double[][] CrossProduct([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
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
        public static decimal[][] CrossProduct([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a[0].Length != b.Length)
            {
                throw new InvalidOperationException("Operation requires inner dimmensions to match.");
            }

            decimal[][] result = new decimal[a.Length][];

            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = new decimal[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (decimal* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
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
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// The product of the arrays.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static int[][] CrossProduct([NotNull][ItemNotNull] this int[][] a, [NotNull][ItemNotNull] int[][] b, [NotNull] ParallelOptions options)
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

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new int[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (int* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
                        {
                            for (int k = 0; k < bColumns; k++)
                            {
                                pointerToResult[k] += pointerToA[j] * pointerToB[k];
                            }
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
        public static long[][] CrossProduct([NotNull][ItemNotNull] this long[][] a, [NotNull][ItemNotNull] long[][] b, [NotNull] ParallelOptions options)
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

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new long[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (long* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
                        {
                            for (int k = 0; k < bColumns; k++)
                            {
                                pointerToResult[k] += pointerToA[j] * pointerToB[k];
                            }
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
        public static float[][] CrossProduct([NotNull][ItemNotNull] this float[][] a, [NotNull][ItemNotNull] float[][] b, [NotNull] ParallelOptions options)
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

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new float[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (float* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
                        {
                            for (int k = 0; k < bColumns; k++)
                            {
                                pointerToResult[k] += pointerToA[j] * pointerToB[k];
                            }
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
        public static double[][] CrossProduct([NotNull][ItemNotNull] this double[][] a, [NotNull][ItemNotNull] double[][] b, [NotNull] ParallelOptions options)
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

            double[][] result = new double[a.Length][];
            
            int aColumns = a[0].Length;
            int bColumns = b[0].Length;

            Parallel.For(0, a.Length, options, i =>
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
        public static decimal[][] CrossProduct([NotNull][ItemNotNull] this decimal[][] a, [NotNull][ItemNotNull] decimal[][] b, [NotNull] ParallelOptions options)
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

            Parallel.For(0, a.Length, options, i =>
            {
                result[i] = new decimal[bColumns];

                for (int j = 0; j < aColumns; j++)
                {
                    unsafe
                    {
                        fixed (decimal* pointerToResult = &result[i][0], pointerToA = &a[i][0], pointerToB = &b[j][0])
                        {
                            for (int k = 0; k < bColumns; k++)
                            {
                                pointerToResult[k] += pointerToA[j] * pointerToB[k];
                            }
                        }
                    }
                }
            });

            return result;
        }
    }
}