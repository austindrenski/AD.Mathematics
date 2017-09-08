using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods to calculate variance-covariance matrices.
    /// </summary>
    [PublicAPI]
    public static class Covariances
    {
        /// <summary>
        /// Calculates the variance-covariance matrix for the array. Conformability: rows(design) == rows(squaredErrors).
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="squaredErrors">
        /// The squared errors associated with the design array.
        /// </param>
        /// <param name="heteroscedasticityConsistent">
        /// The type of heteroscedasticity-consistent correction for the results.
        /// </param>
        /// <returns>
        /// The variance-covariance matrix.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        public static double[][] Covariance([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] squaredErrors, StandardErrorType heteroscedasticityConsistent)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (squaredErrors is null)
            {
                throw new ArgumentNullException(nameof(squaredErrors));
            }
            if (design.Length != squaredErrors.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, squaredErrors);
            }

            switch (heteroscedasticityConsistent)
            {
                case StandardErrorType.HC0:
                {
                    return design.Covariance(squaredErrors, 1.0);
                }
                case StandardErrorType.HC1:
                {
                    return design.Covariance(squaredErrors, (double)design.Length / (design.Length - design[0].Length));
                }
                case StandardErrorType.Ols:
                {
                    return design.Covariance(squaredErrors);
                }
                default:
                {
                    throw new ArgumentException(nameof(StandardErrorType));
                }
            }
        }

        /// <summary>
        /// Calculates the variance-covariance matrix for the array with OLS scaling.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="squaredErrors">
        /// The squared errors associated with the design array.
        /// </param>
        /// <returns>
        /// The variance-covariance matrix.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        private static double[][] Covariance([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] squaredErrors)
        {
            double[][] informationMatrix = design.Transpose().MatrixProduct(design).InvertLu();

            double mean = 0.0;

            for (int i = 0; i < squaredErrors.Length; i++)
            {
                mean += squaredErrors[i];
            }
            
            mean /= design.Length - design[0].Length;
            
            double[][] covariance = new double[informationMatrix.Length][];

            int informationMatrixInner = informationMatrix[0].Length;
            
            for (int i = 0; i < informationMatrix.Length; i++)
            {
                covariance[i] = new double[informationMatrixInner];

                for (int j = 0; j < informationMatrixInner; j++)
                {
                    covariance[i][j] = mean * informationMatrix[i][j];
                }
            }

            return covariance;
        }

        /// <summary>
        /// Calculates the variance-covariance matrix for the array with HC scaling.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="squaredErrors">
        /// The squared errors associated with the design array.
        /// </param>
        /// <param name="scalar">
        /// The HC scalar.
        /// </param>
        /// <returns>
        /// The variance-covariance matrix.
        /// </returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        private static double[][] Covariance([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] squaredErrors, double scalar)
        {
            double[][] designTranspose = design.Transpose();

            double[][] temp = new double[designTranspose.Length][];

            int designTransposeInner = designTranspose[0].Length;

            for (int i = 0; i < designTranspose.Length; i++)
            {
                temp[i] = new double[designTransposeInner];

                for (int j = 0; j < designTransposeInner; j++)
                {
                    temp[i][j] = scalar * designTranspose[i][j] * squaredErrors[j];
                }
            }

            double[][] inner = temp.MatrixProduct(design);

            double[][] informationMatrix = designTranspose.MatrixProduct(design).InvertLu();

            return informationMatrix.MatrixProduct(inner).MatrixProduct(informationMatrix);
        }
    }
}