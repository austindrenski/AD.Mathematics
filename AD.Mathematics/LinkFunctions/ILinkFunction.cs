using JetBrains.Annotations;

namespace AD.Mathematics.LinkFunctions
{
    /// <summary>
    /// Represents a link function providing methods for evaluation and inverse evaluation.
    /// </summary>
    [PublicAPI]
    public interface ILinkFunction
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
        [Pure]
        double Evaluate(double x);

        /// <summary>
        /// Evaluates the inverse link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The function value at the argument.
        /// </returns>
        [Pure]
        double Inverse(double x);

        /// <summary>
        /// Evaluates the first derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The first derivative value at the argument.
        /// </returns>
        [Pure]
        double FirstDerivative(double x);

        /// <summary>
        /// Evaluates the second derivative of the link function given the argument.
        /// </summary>
        /// <param name="x">
        /// The function argument.
        /// </param>
        /// <returns>
        /// The second derivative value at the argument.
        /// </returns>
        [Pure]
        double SecondDerivative(double x);
    }
}