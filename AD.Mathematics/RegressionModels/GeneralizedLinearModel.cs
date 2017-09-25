using System;
using System.Linq;
using System.Threading.Tasks;
using AD.Mathematics.Distributions;
using AD.Mathematics.LinkFunctions;
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
        [Pure]
        [NotNull]
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
        [Pure]
        [NotNull]   public static GeneralizedLinearModel<double> WeightedLeastSquares(double[][] design, double[] response, double[] weights)
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

            return new GeneralizedLinearModel<double>(design, response, weights, new GaussianDistribution(new IdentityLinkFunction()), true);
        }
        
        /// <summary>
        /// Constructs a Poisson regression model based on a <see cref="PoissonDistribution"/>, the <see cref="LogLinkFunction"/> 
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
        /// A GLM based on the Poisson regression model.
        /// </returns>
        [Pure]
        [NotNull]   public static GeneralizedLinearModel<double> PoissonRegression(double[][] design, double[] response, double[] weights)
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

            return new GeneralizedLinearModel<double>(design, response, weights, new PoissonDistribution(new LogLinkFunction()), true);
        }

        /// <summary>
        /// Constructs a Poisson regression model based on a <see cref="PoissonDistribution"/>, the <see cref="LogLinkFunction"/> 
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
        /// <param name="options">
        /// Parallel options.
        /// </param>
        /// <returns>
        /// A GLM based on the Poisson regression model.
        /// </returns>
        [Pure]
        [NotNull]     public static GeneralizedLinearModel<double> PoissonRegression([NotNull][ItemNotNull] double[][] design, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] ParallelOptions options)
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
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (weights.Length != response.Length)
            {
                throw new ArrayConformabilityException<double>(weights, response);
            }

            return new GeneralizedLinearModel<double>(design, response, weights, new PoissonDistribution(new LogLinkFunction()), true, options);
        }
    }
}