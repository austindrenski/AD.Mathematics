using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <summary>
    /// Represents the identity link function.
    /// </summary>
    [PublicAPI]
    public class IdentityLinkFunction : ILinkFunction
    {
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
            return x;
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
            return 1.0 / x;
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
            return 1.0;
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
            return 0.0;
        }
    }
}