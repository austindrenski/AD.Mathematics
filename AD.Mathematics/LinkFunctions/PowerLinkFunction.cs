using System;
using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <inheritdoc />
    [PublicAPI]
    public class PowerLinkFunction : ILinkFunction
    {
        public double Power { get; }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            if (Power is 1.0)
            {
                double[] fitted = Inverse(meanResponse);
                
                double sumSquaredErrors = 0.0;

                for (int i = 0; i < response.Length; i++)
                {
                    double error = response[i] - fitted[i];

                    sumSquaredErrors += error * error;
                }

                double halfObs = 0.5 * response.Length;

                return -halfObs * (Math.Log(sumSquaredErrors) + (1.0 + Math.Log(Math.PI / halfObs)));
            }
            
            double result = 0.0;

            double common = Math.Log(Math.PI * 2.0 * scale);

            for (int i = 0; i < response.Length; i++)
            {
                double error = response[i] - meanResponse[i];

                result += -0.5 * weights[i] * (error * error / scale + common);
            }

            return result;
        }
    }
}