using System;
using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <summary>
    /// Represents the logit link function where the argument represents a probability and the result is the logarithm of the odds.
    /// </summary>
    /// <remarks>
    /// f(x) = log(x/(1-x))
    /// </remarks>
    [PublicAPI]
    public class LogitLinkFunction : ILinkFunction
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
        /// Constructs a <see cref="LogitLinkFunction"/> for the given <paramref name="slope"/> and <paramref name="intercept"/>.
        /// </summary>
        /// <param name="slope">
        /// The slope value. Defaults to 1.0.
        /// </param>
        /// <param name="intercept">
        /// The intercept value. Defaults to 0.0.
        /// </param>
        public LogitLinkFunction(double slope = 1.0, double intercept = 0.0)
        {
            Slope = slope;
            Intercept = intercept;
        }

        /// <summary>
        /// The logit link function.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        /// <remarks>
        /// f(x) = (Log(x / (1.0 - x)) - <see cref="Intercept"/>) / <see cref="Slope"/>.
        /// </remarks>
        [Pure]
        public double Evaluate(double x)
        {
            return (Math.Log(x / (1.0 - x)) - Intercept) / Slope;
        }

        /// <summary>
        /// The inverse of the logit link function.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        /// <remarks>
        /// f(x) = 1.0 / [1.0 + exp(-y)] where y = <see cref="Slope"/> * <paramref name="x"/> + <see cref="Intercept"/>.
        /// </remarks>
        [Pure]
        public double Inverse(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-(Slope * x + Intercept)));
        }

        /// <summary>
        /// Evaluates the first derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The first derivative value at the argument.
        /// </returns>
        public double FirstDerivative(double x)
        {
            double a = Slope * x + Intercept;

            double b = 1.0 / (1.0 + Math.Exp(-a));

            return b - b * b;
        }

        /// <summary>
        /// Evaluates the second derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The second derivative value at the argument.
        /// </returns>
        public double SecondDerivative(double x)
        {
            return x - x * x;
        }
    }
}