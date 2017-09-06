﻿using System;
using System.Linq;
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
        public LogLinkFunction(double slope = 1.0, double intercept = 0.0)
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
            return x.Select(Check).Select(y => Math.Log(y)).ToArray();
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
            return x.Select(Math.Exp).ToArray();
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
            return x.Select(Check).Select(y => 1.0 / y).ToArray();
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
            return x.Select(Check).Select(y => -1.0 / (y * y)).ToArray();
        }

        private static double Check(double x)
        {
            return double.Epsilon < x ? x : double.Epsilon;
        }
    }
}
