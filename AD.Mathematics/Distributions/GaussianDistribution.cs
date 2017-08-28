using System;
using System.Collections.Generic;
using System.Linq;
using AD.Mathematics.LinkFunctions;
using JetBrains.Annotations;

namespace AD.Mathematics.Distributions
{
    [PublicAPI]
    public class GaussianDistribution : IDistribution<double>
    {
        /// <summary>
        /// The random number generator used by the distribution.
        /// </summary>
        [NotNull]
        private readonly Random _random;

        /// <summary>
        /// The link function that maps from the linear domains.
        /// </summary>
        public ILinkFunction LinkFunction { get; }

        /// <summary>
        /// Gets the entropy of the distribution.
        /// </summary>
        public double Entropy => 0.5 * (1.0 + Math.Log(2.0 * Math.PI * Variance));

        /// <summary>
        /// Gets the maximum of the distribution.
        /// </summary>
        public double Maximum => double.PositiveInfinity;

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public double Mean { get; }

        /// <summary>
        /// Gets the median of the distribution.
        /// </summary>
        public double Median => Mean;

        /// <summary>
        /// Gets the minimum of the distribution.
        /// </summary>
        public double Minimum => double.NegativeInfinity;

        /// <summary>
        /// Gets the mode of the distribution.
        /// </summary>
        public double Mode => Mean;

        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public double Skewness => default(double);

        /// <summary>
        /// Gets the kurtosis of the distribution.
        /// </summary>
        public double Kurtosis => default(double);

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation { get; }

        /// <summary>
        /// Gets the varianve of the distribution.
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
        /// The link function. Defaults to <see cref="IdentityLinkFunction"/>.
        /// </param>
        /// <param name="random">
        /// A random number generator for the distribution. Defaults to <see cref="Random"/>.
        /// </param>
        public GaussianDistribution(double mean = 0.0, double standardDeviation = 1.0, [CanBeNull] ILinkFunction linkFunction = null, [CanBeNull] Random random = null)
        {
            Mean = mean;
            StandardDeviation = standardDeviation;
            LinkFunction = linkFunction ?? new IdentityLinkFunction();
            _random = random ?? new Random();
        }

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
        public double LogLikelihood(IReadOnlyList<double> response, IReadOnlyList<double> meanResponse, IReadOnlyList<double> weights, double scale = 1.0)
        {
            if (LinkFunction is IdentityLinkFunction || LinkFunction is PowerLinkFunction power && power.Power is 1.0)
            {
                double[] fitted = Fit(meanResponse.ToArray());

                double sumSquaredErrors = 0.0;

                for (int i = 0; i < response.Count; i++)
                {
                    double error = response[i] - fitted[i];

                    sumSquaredErrors += error * error;
                }

                double halfObs = response.Count / 2.0;

                return -halfObs * (Math.Log(sumSquaredErrors) + (1.0 + Math.Log(Math.PI / halfObs)));
            }

            double result = 0.0;

            double common = Math.Log(Math.PI * 2.0 * scale);

            for (int i = 0; i < response.Count; i++)
            {
                double error = response[i] - meanResponse[i];

                result += -0.5 * weights[i] * (error * error / scale + common);
            }

            return result;
        }

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
        public double Deviance(IReadOnlyList<double> response, IReadOnlyList<double> meanResponse, IReadOnlyList<double> weights, double scale = 1.0)
        {
            double result = 0.0;

            for (int i = 0; i < response.Count; i++)
            {
                double error = response[i] - meanResponse[i];
                result += weights[i] * error * error;
            }

            return result / scale;
        }

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
        public double[] InitialMeanResponse(double[] response)
        {
            double mean = response.Average();

            return response.Select(x => (x + mean) / 2.0).ToArray();
        }

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
            return LinkFunction.FirstDerivative(meanResponse).Select(x => 1.0 / (x * x)).ToArray();
        }

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