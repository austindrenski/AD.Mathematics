﻿using System;
using System.Collections.Generic;
using System.Linq;
using AD.Mathematics.SpecialFunctions;
using JetBrains.Annotations;

namespace AD.Mathematics.Distributions
{
    /// <inheritdoc cref="IDistribution{T}" />
    /// <summary>
    /// Represents a Poisson statistical distribution. 
    /// </summary>
    [PublicAPI]
    public class PoissonDistribution : IDistribution<double>, IDistribution<int>
    {
        /// <summary>
        /// The random number generator used by the distribution.
        /// </summary>
        [NotNull]
        private readonly Random _random;

        /// <summary>
        /// A sampling enumerator implemented with Knuth's method. See <see cref="SmallSamples"/>.
        /// </summary>
        [NotNull]
        private readonly IEnumerator<int> _smallSampleEnumerator;

        /// <summary>
        /// A sampling enumerator implemented with the PA method. See <see cref="LargeSamples"/>.
        /// </summary>
        [NotNull]
        private readonly IEnumerator<int> _largeSampleEnumerator;

        /// <inheritdoc />
        /// <summary>
        /// Gets the entropy of the distribution.
        /// </summary>
        public double Entropy =>
            0.5 * Math.Log(2 * Math.PI * Math.E * Mean)
            - 1.0 / (12.0 * Mean)
            - 1.0 / (24.0 * Mean * Mean)
            - 19.0 / (360.0 * Mean * Mean * Mean);

        /// <inheritdoc />
        /// <summary>
        /// The link function that maps from the linear domains.
        /// </summary>
        public ILinkFunction LinkFunction { get; }

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
        public double Median => Math.Floor(Mean + 1.0 / 3.0 - 0.02 / Mean);

        /// <inheritdoc />
        /// <summary>
        /// Gets the minimum of the distribution.
        /// </summary>
        public double Minimum => default;

        /// <inheritdoc />
        /// <summary>
        /// Gets the mode of the distribution.
        /// </summary>
        public double Mode => Math.Floor(Mean);

        /// <inheritdoc />
        /// <summary>
        /// Gets the skewness of the distribution.
        /// </summary>
        public double Skewness => 1.0 / StandardDeviation;

        /// <inheritdoc />
        /// <summary>
        /// Gets the excess kurtosis of the distribution.
        /// </summary>
        public double Kurtosis => 1.0 / Mean;

        /// <inheritdoc />
        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation => Math.Sqrt(Mean);

        /// <inheritdoc />
        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance => Mean;

        /// <summary>
        /// Constructs a <see cref="PoissonDistribution"/>.
        /// </summary>
        /// <param name="linkFunction">
        ///     The link function.
        /// </param>
        /// <param name="mean">
        ///     A <paramref name="mean"/> value for the distribution. Defaults to 1.0.
        /// </param>
        /// <param name="random">
        ///     A random number generator for the distribution. Defaults to <see cref="Random"/>.
        /// </param>
        public PoissonDistribution([NotNull] ILinkFunction linkFunction, double mean = 1.0, [CanBeNull] Random random = default)
        {
            if (mean <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(mean), "Argument must be greater than zero.");
            }

            Mean = mean;
            _random = random ?? new Random();
            LinkFunction = linkFunction;

            _smallSampleEnumerator = SmallSamples();
            _largeSampleEnumerator = LargeSamples();
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
            double result = 0.0;

            double common = Math.Log(Math.PI * 2.0 * scale);

            for (int i = 0; i < response.Length; i++)
            {
                double error = response[i] - meanResponse[i];

                result += weights[i] * (error * error / scale + common);
            }

            result *= -0.5;

            return result;
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
                double d = response[i] <= 0 ? double.Epsilon : response[i] / meanResponse[i];
                
                result += weights[i] * (response[i] * Math.Log(d) - response[i] - meanResponse[i]) / scale;
            }

            return 2 * result;
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
            
            for (int i = 0; i < meanResponse.Length; i++)
            {
                weight[i] = Math.Abs(meanResponse[i]);
            }
         
            double[] derivative = LinkFunction.FirstDerivative(weight);

            for (int i = 0; i < derivative.Length; i++)
            {
                weight[i] = 1.0 / (derivative[i] * derivative[i] * weight[i]);
            }

            return weight;
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
            if (x < 0.0 || x > 170.0)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Argument range: [0, 170].");
            }

            return Math.Exp(LogProbability(x));
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
            if (x < 0 || x > 170)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Argument range: [0, 170].");
            }

            return (int)x * Math.Log(Mean) - Factorial.GetLog((int)x) - Mean;
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
            if (Mean < 30.0)
            {
                _smallSampleEnumerator.MoveNext();
                return _smallSampleEnumerator.Current;
            }

            _largeSampleEnumerator.MoveNext();
            return _largeSampleEnumerator.Current;
        }

        int IDistribution<int>.Draw()
        {
            return (int)Draw();
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
        
        IEnumerable<int> IDistribution<int>.Draw(int count)
        {
            return Draw(count).Select(x => (int)x);
        }

        /// <summary>
        /// Generates samples from the Poisson distribution by Knuth's method. The runtime of this method is O(λ).
        /// This should only be called from the constructor and cached in <see cref="_smallSampleEnumerator"/>.
        /// </summary>
        /// <returns>
        /// A random sample from the Poisson distribution.
        /// </returns>
        [NotNull]
        private IEnumerator<int> SmallSamples()
        {
            double limit = Math.Exp(-Mean);

            while (true)
            {
                int count = 0;

                for (double product = _random.NextDouble(); product >= limit; product *= _random.NextDouble())
                {
                    count++;
                }

                yield return count;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        /// <summary>
        /// Generates samples from the Poisson distribution by PA method. The runtime of this method is independent of λ.
        /// This should only be called from the constructor and cached in <see cref="_largeSampleEnumerator"/>.
        /// </summary>
        /// <returns>
        /// A random sample from the Poisson distribution.
        /// </returns>
        /// <remarks>
        /// Adapted from https://www.johndcook.com/blog/2010/06/14/generating-poisson-random-values.
        /// </remarks>
        [NotNull]
        private IEnumerator<int> LargeSamples()
        {
            double beta = Math.PI / Math.Sqrt(3.0 * Mean);
            double alpha = beta * Mean;
            double k = Math.Log(0.767 - 3.36 / Mean) - Mean - Math.Log(beta);
            double logLambda = Math.Log(Mean);

            while (true)
            {
                double u = _random.NextDouble();

                double x = (alpha - Math.Log((1.0 - u) / u)) / beta;

                if (x < -0.5)
                {
                    continue;
                }

                int n = (int)Math.Floor(x + 0.5);

                double y = alpha - beta * x;

                double temp = 1.0 + Math.Exp(y);

                double left = y + Math.Log(_random.NextDouble() / (temp * temp));

                double right = k + n * logLambda - Factorial.GetLog(n);

                if (left <= right)
                {
                    yield return n;
                }
            }
            // ReSharper disable once IteratorNeverReturns
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
            return $"{nameof(PoissonDistribution)}: {nameof(Mean)}: {Mean}, {nameof(StandardDeviation)}: {StandardDeviation}.";
        }
    }
}