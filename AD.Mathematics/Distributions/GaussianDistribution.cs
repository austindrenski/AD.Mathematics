using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AD.Mathematics.Distributions
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a Gaussian statistical distribution.
    /// </summary>
    [PublicAPI]
    public class GaussianDistribution : IDistribution<double>
    {
        /// <summary>
        /// The random number generator used by the distribution.
        /// </summary>
        [NotNull]
        private readonly Random _random;

        /// <inheritdoc />
        /// <summary>
        /// The link function that maps from the linear domains.
        /// </summary>
        public ILinkFunction LinkFunction { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the entropy of the distribution.
        /// </summary>
        public double Entropy => 0.5 * (1.0 + Math.Log(2.0 * Math.PI * Variance));

        /// <inheritdoc />
        /// <summary>
        /// Gets the maximum of the distribution.
        /// </summary>
        public double Maximum => double.PositiveInfinity;

        /// <inheritdoc />
        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public double Mean { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the median of the distribution.
        /// </summary>
        public double Median => Mean;

        /// <inheritdoc />
        /// <summary>
        /// Gets the minimum of the distribution.
        /// </summary>
        public double Minimum => double.NegativeInfinity;

        /// <inheritdoc />
        /// <summary>
        /// Gets the mode of the distribution.
        /// </summary>
        public double Mode => Mean;

        /// <inheritdoc />
        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public double Skewness => default;

        /// <inheritdoc />
        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        public double Kurtosis => default;

        /// <inheritdoc />
        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance => StandardDeviation * StandardDeviation;

        /// <summary>
        /// Constructs a <see cref="GaussianDistribution"/>.
        /// </summary>
        /// <param name="mean">
        /// The mean of the distribution. Defaults to zero.
        /// </param>
        /// <param name="standardDeviation">
        /// The standard deviation of the distribution. Defaults to 1.0.
        /// </param>
        /// <param name="linkFunction">
        /// The link function.
        /// </param>
        /// <param name="random">
        /// A random number generator for the distribution. Defaults to <see cref="Random"/>.
        /// </param>
        public GaussianDistribution([NotNull] ILinkFunction linkFunction, double mean = 0.0, double standardDeviation = 1.0, [CanBeNull] Random random = null)
        {
            Mean = mean;
            StandardDeviation = standardDeviation;
            LinkFunction = linkFunction;
            _random = random ?? new Random();
        }

        /// <inheritdoc />
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
        public double Probability(double x)
        {
            return 1.0 / Math.Sqrt(Math.PI * 2.0) * Math.Exp(-0.5 * x * x);
        }

        /// <inheritdoc />
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
        public double LogProbability(double x)
        {
            return Math.Log(Probability(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// The log-likelihood function.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="meanResponse">
        /// An array of fitted mean response values.
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
        public double LogLikelihood(double[] response, double[] meanResponse, double[] weights, double scale = 1.0)
        {
            return LinkFunction.LogLikelihood(response, meanResponse, weights, scale);
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates the deviance for the given arguments.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="meanResponse">
        /// An array of mean response values.
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
        public double Deviance(double[] response, double[] meanResponse, double[] weights, double scale = 1.0)
        {
            double result = 0.0;

            for (int i = 0; i < response.Length; i++)
            {
                double error = response[i] - meanResponse[i];
                result += weights[i] * error * error;
            }

            return result / scale;
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates a linear prediction given a mean response value.
        /// </summary>
        /// <param name="meanResponse">
        /// A mean response value.
        /// </param>
        /// <returns>
        /// A linear prediction value.
        /// </returns>
        [Pure]
        public double[] Predict(double[] meanResponse)
        {
            return LinkFunction.Evaluate(meanResponse);
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates a mean response value given a linear prediction.
        /// </summary>
        /// <param name="linearPredicton">
        /// A linear predicton.
        /// </param>
        /// <returns>
        /// A mean response value.
        /// </returns>
        [Pure]
        public double[] Fit(double[] linearPredicton)
        {
            return LinkFunction.Inverse(linearPredicton);
        }

        /// <inheritdoc />
        /// <summary>
        /// Provides an initial mean response array for the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="response">
        /// An untransformed response array.
        /// </param>
        /// <returns>
        /// An initial mean response array.
        /// </returns>
        [Pure]
        public double[] InitialMean(double[] response)
        {
            double mean = 0.0;

            for (int i = 0; i < response.Length; i++)
            {
                mean += response[i];
            }

            mean /= response.Length;

            double[] initialMean = new double[response.Length];
            
            for (int i = 0; i < response.Length; i++)
            {
                initialMean[i] = 0.5 * (response[i] + mean);
            }

            return initialMean;
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates the weight for a step of the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="meanResponse">
        /// A mean response value.
        /// </param>
        /// <returns>
        /// A weight based on the mean response.
        /// </returns>
        [Pure]
        public double[] Weight(double[] meanResponse)
        {                   
            double[] weight = new double[meanResponse.Length];
            double[] derivative = LinkFunction.FirstDerivative(meanResponse);

            for (int i = 0; i < derivative.Length; i++)
            {
                weight[i] = 1.0 / (derivative[i] * derivative[i] * Variance);
            }

            return weight;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Randomly draws a value from the distribution.
        /// </summary>
        /// <returns>
        /// A randomly drawn value from the distribution.
        /// </returns>
        public double Draw()
        {
            return Mean + StandardDeviation * Math.Sqrt(-2.0 * Math.Log(1 - _random.NextDouble())) * Math.Cos(2.0 * Math.PI * (1 - _random.NextDouble()));
        }

        /// <inheritdoc />
        /// <summary>
        /// Randomly draws the specified count of values from the distribution.
        /// </summary>
        /// <param name="count">
        /// The number of random draws to make.
        /// </param>
        /// <returns>
        /// An enumerable collection of randomly drawn values from the distribution.
        /// </returns>
        public IEnumerable<double> Draw(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Draw();
            }
        }

        /// <inheritdoc cref="IDistribution{T}.ToString" />
        /// <summary>
        /// Returns a string that represents the current distribution.
        /// </summary>
        /// <returns>
        /// A string that represents the current distribution.
        /// </returns>
        [Pure]
        public override string ToString()
        {
            return $"{nameof(GaussianDistribution)}: {nameof(Mean)}: {Mean}, {nameof(StandardDeviation)}: {StandardDeviation}.";
        }
    }
}