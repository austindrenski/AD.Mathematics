using AD.Mathematics.LinkFunctions;
using JetBrains.Annotations;

namespace AD.Mathematics.Distributions
{
    /// <summary>
    /// Represents a statistical distribution.
    /// </summary>
    [PublicAPI]
    public interface IDistribution
    {
        /// <summary>
        /// The link function that maps from the linear domains.
        /// </summary>
        [NotNull]
        ILinkFunction LinkFunction { get; }

        /// <summary>
        /// Gets the entropy of the distribution.
        /// </summary>
        double Entropy { get; }
        
        /// <summary>
        /// Gets the maximum of the distribution.
        /// </summary>
        double Maximum { get; }

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        double Mean { get; }

        /// <summary>
        /// Gets the median of the distribution.
        /// </summary>
        double Median { get; }

        /// <summary>
        /// Gets the minimum of the distribution.
        /// </summary>
        double Minimum { get; }

        /// <summary>
        /// Gets the mode of the distribution.
        /// </summary>
        double Mode { get; }

        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        double Skewness { get; }        
        
        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        double Kurtosis { get; }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        double StandardDeviation { get; }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        double Variance { get; }

        /// <summary>
        /// Calculates the deviance for the given arguments.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="mean">
        /// An array of mean values.
        /// </param>
        /// <param name="weights">
        /// An array of importance weights.
        /// </param>
        /// <param name="scale">
        /// An option scaling value.
        /// </param>
        /// <returns>
        /// The deviance function evaluated with the given inputs.
        /// </returns>
        double Deviance([NotNull] double[] response, [NotNull] double[] mean, [NotNull] double[] weights, double scale = 1.0);

        /// <summary>
        /// Calculates a mean value given a linear prediction.
        /// </summary>
        /// <param name="linearPredicton">
        /// A linear predicton.
        /// </param>
        /// <returns>
        /// A mean value.
        /// </returns>
        [Pure]
        double[] Fit(double[] linearPredicton);

        /// <summary>
        /// Provides an initial mean array for the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="response">
        /// An untransformed response array.
        /// </param>
        /// <returns>
        /// An initial mean array.
        /// </returns>
        [Pure]
        [NotNull]
        double[] InitialMean([NotNull] double[] response);
        
        /// <summary>
        /// The log-likelihood function.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="mean">
        /// An array of fitted mean values.
        /// </param>
        /// <param name="weights">
        /// An optional array of importance weights.
        /// </param>
        /// <param name="scale">
        /// An optional value that scales the log-likelihood function.
        /// </param>
        /// <returns>
        /// The value of the log-likelihood function evaluated with the given inputs.
        /// </returns>
        [Pure]
        double LogLikelihood([NotNull] double[] response, [NotNull] double[] mean, [NotNull] double[] weights, double scale = 1.0);

        /// <summary>
        /// The logarithm of the probability function of the distribution.
        /// </summary>
        /// <param name="x">
        /// The domain location at which the log(Probability) is evaluated.
        /// </param>
        /// <returns>
        /// The logarithm of the probability at the given location.
        /// </returns>
        [Pure]
        double LogProbability(double x);
        
        /// <summary>
        /// Calculates a linear prediction given a mean value.
        /// </summary>
        /// <param name="mean">
        /// A mean value.
        /// </param>
        /// <returns>
        /// A linear prediction value.
        /// </returns>
        [Pure]
        double[] Predict(double[] mean);
        
        /// <summary>
        /// The probability function of the distribution (e.g. PMF for discrete distributions, PDF for continuous distributions).
        /// </summary>
        /// <param name="x">
        /// The domain location at which the probability is evaluated.
        /// </param>
        /// <returns>
        /// The probability at the given location.
        /// </returns>
        [Pure]
        double Probability(double x);
        
        /// <summary>
        /// Calculates the weight for a step of the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="mean">
        /// A mean value.
        /// </param>
        /// <returns>
        /// A weight based on the mean.
        /// </returns>
        [Pure]
        double[] Weight(double[] mean);
        
        /// <summary>
        /// Returns a string that represents the current distribution.
        /// </summary>
        /// <returns>
        /// A string that represents the current distribution.
        /// </returns>
        [Pure]
        [NotNull]
        string ToString();
    }
}