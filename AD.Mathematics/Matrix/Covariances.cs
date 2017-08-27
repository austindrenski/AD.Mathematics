using System;
using System.Collections.Generic;
using System.Linq;
using AD.Mathematics;
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
        public static double[][] Covariance([NotNull][ItemNotNull] this IEnumerable<IEnumerable<double>> design, [NotNull] IEnumerable<double> squaredErrors, HeteroscedasticityConsistent heteroscedasticityConsistent)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (squaredErrors is null)
            {
                throw new ArgumentNullException(nameof(squaredErrors));
            }

            double[][] designArray = design as double[][] ?? (design as IEnumerable<double[]>)?.ToArray() ?? design.Select(x => x.ToArray()).ToArray();

            double[] squaredErrorsArray = squaredErrors as double[] ?? squaredErrors.ToArray();

            if (designArray.Length != squaredErrorsArray.Length || designArray.Length == 0)
            {
                throw new ArrayConformabilityException<double>(designArray, squaredErrorsArray);
            }

            switch (heteroscedasticityConsistent)
            {
                case HeteroscedasticityConsistent.HC0:
                {
                    return designArray.Covariance(squaredErrorsArray, 1.0);
                }
                case HeteroscedasticityConsistent.HC1:
                {
                    return designArray.Covariance(squaredErrorsArray, (double)designArray.Length / (designArray.Length - designArray[0].Length));
                }
                case HeteroscedasticityConsistent.OLS:
                {
                    return designArray.Covariance(squaredErrorsArray);
                }
                default:
                {
                    throw new ArgumentException(nameof(HeteroscedasticityConsistent));
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
            double[][] informationMatrix = design.Transpose().CrossProduct(design).InvertLu();

            double mean = squaredErrors.Sum() / (design.Length - design[0].Length);
            
            double[][] covariance = new double[informationMatrix.Length][];

            for (int i = 0; i < informationMatrix.Length; i++)
            {
                covariance[i] = new double[informationMatrix[i].Length];

                for (int j = 0; j < informationMatrix[i].Length; j++)
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

            for (int i = 0; i < designTranspose.Length; i++)
            {
                temp[i] = new double[designTranspose[i].Length];

                for (int j = 0; j < designTranspose[i].Length; j++)
                {
                    temp[i][j] = scalar * designTranspose[i][j] * squaredErrors[j];
                }
            }

            double[][] inner = temp.CrossProduct(design);

            double[][] informationMatrix = designTranspose.CrossProduct(design).InvertLu();

            return informationMatrix.CrossProduct(inner).CrossProduct(informationMatrix);
        }
    }
}