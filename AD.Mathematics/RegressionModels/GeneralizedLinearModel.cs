using System;
using System.Linq;
using AD.Mathematics.Distributions;
using JetBrains.Annotations;

namespace AD.Mathematics.RegressionModels
{
    /// <summary>
    /// Factory class to construct well-known generalized linear models.
    /// </summary>
    [PublicAPI]
    public static class GeneralizedLinearModel
    {
        /// <summary>
        /// Constructs a standard Ordinary Least Squares (OLS) regression model based on a <see cref="GaussianDistribution"/> 
        /// and with a constant prepended to the design array.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="response">
        /// The response values.
        /// </param>
        /// <returns>
        /// A GLM based on the OLS regression model.
        /// </returns>
        public static GeneralizedLinearModel<double> OrdinaryLeastSquares([NotNull][ItemNotNull] double[][] design, [NotNull] double[] response)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design.Length != response.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            return WeightedLeastSquares(design, response, Enumerable.Repeat(1.0, response.Length).ToArray());
        }

        /// <summary>
        /// Constructs a weighted Ordinary Least Squares (OLS) regression model based on a <see cref="GaussianDistribution"/> 
        /// and with a constant prepended to the design array.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="response">
        /// The response values.
        /// </param>
        /// <param name="weights">
        /// The observation weights.
        /// </param>
        /// <returns>
        /// A GLM based on the OLS regression model.
        /// </returns>
        public static GeneralizedLinearModel<double> WeightedLeastSquares(double[][] design, double[] response, double[] weights)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design.Length != response.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            if (weights.Length != response.Length)
            {
                throw new ArrayConformabilityException<double>(weights, response);
            }

            return new GeneralizedLinearModel<double>(design, response, weights, new GaussianDistribution(), true);
        }
    }
}