using System;
using System.Linq;
using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the identity link function.
    /// </summary>
    [PublicAPI]
    public class IdentityLinkFunction : ILinkFunction
    {
        /// <inheritdoc />
        /// <summary>
        /// Evaluates the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        public double[] Evaluate(double[] x)
        {
            return x.ToArray();
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the inverse link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        public double[] Inverse(double[] x)
        {
            return x.Select(y => 1.0 / y).ToArray();
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
            return Enumerable.Repeat(1.0, x.Length).ToArray();
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
            return new double[x.Length];
        }

        /// <inheritdoc />
        /// <summary>
        /// The log-likelihood function.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="fitted">
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
        public double LogLikelihood(double[] response, double[] fitted, double[] weights, double scale = 1.0)
        {
            double sumSquaredErrors = 0.0;

            for (int i = 0; i < response.Length; i++)
            {
                double error = response[i] - fitted[i];

                sumSquaredErrors += error * error;
            }

            double halfObs = 0.5 * response.Length;

            return -halfObs * (Math.Log(sumSquaredErrors) + (1.0 + Math.Log(Math.PI / halfObs)));
        }
    }
}