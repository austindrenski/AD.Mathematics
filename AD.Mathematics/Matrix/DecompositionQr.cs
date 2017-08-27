using System;
using System.Linq;
using AD.Mathematics;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Represents the QR decomposition of an array.
    /// </summary>
    [PublicAPI]
    public static class DecompositionQr
    {
        private static readonly double Root2 = Math.Sqrt(2);
        
        /// <summary>
        /// Performs a QR decomposition on the <paramref name="design"/> array.
        /// </summary>
        /// <param name="design">
        /// The array to decompose.
        /// </param>
        /// <returns>
        /// A tuple of the orthogonal matrix and the upper-triangular matrix.
        /// </returns>
        [Pure]
        public static (double[][] Orthogonal, double[][] Upper) DecomposeQr(this double[][] design)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (design.Length < design[0].Length)
            {
                throw new ArrayConformabilityException<double>(design, design);
            }

            double[][] q = new double[design.Length][];

            for (int i = 0; i < q.Length; i++)
            {
                q[i] = new double[q.Length];
                q[i][i] = 1.0;
            }

            double[][] workingSpace = new double[Math.Min(design.Length, design[0].Length)][];

            double[][] r = design.CloneArray();

            for (int i = 0; i < workingSpace.Length; i++)
            {
                workingSpace[i] = QrGenerateColumn(r, i, i);

                QrHelper(r, workingSpace[i], i, r.Length, i + 1, r[0].Length);
            }

            for (int i = workingSpace.Length - 1; i >= 0; i--)
            {
                QrHelper(q, workingSpace[i], i, r.Length, i, r.Length);
            }
            
            return (q, r);
        }

        /// <summary>
        /// Prepare a column from source array for the work array
        /// </summary>
        /// <param name="source">
        /// The source array.
        /// </param>
        /// <param name="row">
        /// The row index.
        /// </param>
        /// <param name="column">
        /// The column index.
        /// </param>
        /// <returns>
        /// A modified source array vector.
        /// </returns>
        [Pure]
        [NotNull]
        private static double[] QrGenerateColumn([NotNull][ItemNotNull] double[][] source, int row, int column)
        {
            double[] result = new double[source.Length - row];

            for (int i = row; i < source.Length; i++)
            {
                result[i - row] = source[i][row];

                source[i][row] = 0.0;
            }

            double norm = result.Aggregate(0.0, (current, next) => current + next * next, Math.Sqrt);

            if (row.Equals(source.Length - 1) || norm.Equals(0.0))
            {
                source[row][column] = -result[0];

                result[0] = Root2;

                return result;
            }

            double scale0 = result[0] < 0.0 ? -1.0 / norm : 1.0 / norm;
            
            source[row][column] = -1.0 / scale0;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] *= scale0;
            }

            result[0]++;

            double scale1 = Math.Sqrt(1.0 / result[0]);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] *= scale1;
            }

            return result;
        }

        /// <summary>
        /// Performs a unit of work for the QR algorithm.
        /// </summary>
        /// <param name="active">
        /// The mutable active array. (e.g. Q or R)
        /// </param>
        /// <param name="workingSpace">
        /// The working space array.
        /// </param>
        /// <param name="rowStart">
        /// The row index at which to start.
        /// </param>
        /// <param name="rowEnd">
        /// The row to stop before.
        /// </param>
        /// <param name="columnStart">
        /// The column index at which to start.
        /// </param>
        /// <param name="columnEnd">
        /// The column to stop before.
        /// </param>
        private static void QrHelper([NotNull][ItemNotNull] double[][] active, [NotNull] double[] workingSpace, int rowStart, int rowEnd, int columnStart, int columnEnd)
        {
            for (int i = columnStart; i < columnEnd; i++)
            {
                double scale = 0.0;

                for (int j = rowStart; j < rowEnd; j++)
                {
                    scale += workingSpace[j - rowStart] * active[j][i];
                }

                for (int j = rowStart; j < rowEnd; j++)
                {
                    active[j][i] -= workingSpace[j - rowStart] * scale;
                }
            }
        }
    }
}