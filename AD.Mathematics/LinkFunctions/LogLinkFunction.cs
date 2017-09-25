using System;
using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the log link function where the argument represents a probability and the result is the logarithm of the odds.
    /// </summary>
    /// <remarks>
    /// f(x) = log(x)
    /// </remarks>
    [PublicAPI]
    public class LogLinkFunction : ILinkFunction
    {
        /// <summary>
        /// The function intercept.
        /// </summary>
        public double Intercept { get; }

        /// <summary>
        /// The function slope.
        /// </summary>
        public double Slope { get; }

        /// <summary>
        /// Constructs a <see cref="LogLinkFunction"/> for the given <paramref name="slope"/> and <paramref name="intercept"/>.
        /// </summary>
        /// <param name="slope">
        /// The slope value. Defaults to 1.0.
        /// </param>
        /// <param name="intercept">
        /// The intercept value. Defaults to 0.0.
        /// </param>
        public LogLinkFunction(double slope = 1.0, double intercept = default)
        {
            Slope = slope;
            Intercept = intercept;
        }

        /// <inheritdoc />
        /// <summary>
        /// The log link function.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        /// <remarks>
        /// f(x) = (Log(x) - <see cref="Intercept"/>) / <see cref="Slope"/>.
        /// </remarks>
        [Pure]
        public double[] Evaluate(double[] x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            
            double[] result = new double[x.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Log(Check(x[i]));
            }

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// The inverse of the log link function.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        /// <remarks>
        /// f(x) = exp(<see cref="Slope"/> * <paramref name="x"/> + <see cref="Intercept"/>).
        /// </remarks>
        [Pure]
        public double[] Inverse(double[] x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            
            double[] result = new double[x.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Exp(x[i]);
            }

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the first derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The first derivative value at the argument.
        /// </returns>
        public double[] FirstDerivative(double[] x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            
            double[] result = new double[x.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = 1.0 / Check(x[i]);
            }

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the second derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The second derivative value at the argument.
        /// </returns>
        public double[] SecondDerivative(double[] x)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            double[] result = new double[x.Length];

            for (int i = 0; i < result.Length; i++)
            {
                double temp = Check(x[i]);
                result[i] = -1.0 / (temp * temp);
            }

            return result;
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

                result += -0.5 * weights[i] * (error * error / scale + common);
            }

            return result;
        }

        private static double Check(double x)
        {
            return double.Epsilon < x ? x : double.Epsilon;
        }
    }
}
