using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods to calculate standard errors.
    /// </summary>
    [PublicAPI]
    public static class StandardErrors
    {
        /// <summary>
        /// Calculates the standard error of each column in the array. Conformability: rows(design) == rows(squaredErrors).
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
        /// The standard errors of the column entries.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] StandardError([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] squaredErrors, StandardErrorType heteroscedasticityConsistent)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (squaredErrors is null)
            {
                throw new ArgumentNullException(nameof(squaredErrors));
            }
            if (design.Length != squaredErrors.Length)
            {
                throw new ArrayConformabilityException<double>(design, squaredErrors);
            }

            double[][] covariance = design.Covariance(squaredErrors, heteroscedasticityConsistent);

            double[] result = new double[covariance.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Sqrt(covariance[i][i]);
            }

            return result;
        }
    }
}