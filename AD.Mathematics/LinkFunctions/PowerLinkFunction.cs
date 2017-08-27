using System;
using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    [PublicAPI]
    public class PowerLinkFunction : ILinkFunction
    {
        public double Power { get; }

        /// <summary>
        /// Evaluates the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        public double Evaluate(double x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evaluates the inverse link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        public double Inverse(double x)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
