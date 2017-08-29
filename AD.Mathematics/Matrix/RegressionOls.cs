using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Defines parallel matrix operations.
    /// </summary>
    [PublicAPI]
    public static class RegressionOLS
    {
        /// <summary>
        /// Calculates a linear regression given a system of coefficients.
        /// </summary>
        /// <param name="design">
        /// The design variables.
        /// </param>
        /// <param name="response">
        /// The response variables.
        /// </param>
        /// <returns>
        /// A vector of coefficients.
        /// </returns>
        /// <remarks>
        /// Caclulates the standard linear regression via ordinary least squares (OLS):
        ///      
        ///     β = (Xᵀ * X)⁻¹ * Xᵀ * Y
        /// 
        /// Where:
        /// 
        ///     X = design
        ///     Y = response
        ///     β = coefficients
        /// </remarks>
        [Pure]
        [NotNull]
        public static double[] RegressOLS([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            double[][] transpose =
                design.Transpose();

            double[] result =
                transpose.MatrixProduct(design)
                         .InvertLu()
                         .MatrixProduct(transpose)
                         .MatrixProduct(response);

            return result;
        }

        /// <summary>
        /// Calculates a linear regression given a system of coefficients.
        /// </summary>
        /// <param name="design">
        /// The design variables.
        /// </param>
        /// <param name="response">
        /// The response variables.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// A vector of coefficients.
        /// </returns>
        /// <remarks>
        /// Caclulates the standard linear regression via ordinary least squares (OLS):
        ///      
        ///     β = (Xᵀ * X)⁻¹ * Xᵀ * Y
        /// 
        /// Where:
        /// 
        ///     X = design
        ///     Y = response
        ///     β = coefficients
        /// </remarks>
        [Pure]
        [NotNull]
        public static double[] RegressOLS([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] ParallelOptions options)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            double[][] transpose =
                design.Transpose(options);

            double[] result =
                transpose.MatrixProduct(design, options)
                         .InvertLu(options)
                         .MatrixProduct(transpose, options)
                         .MatrixProduct(response, options);

            return result;
        }

        /// <summary>
        /// Calculates a linear regression given a system of coefficients. The coefficient matrix is prepended by a vector of constants.
        /// </summary>
        /// <param name="design">
        /// The design variables.
        /// </param>
        /// <param name="response">
        /// The response variables.
        /// </param>
        /// <param name="constant">
        /// A constant value to prepend the response variable array.
        /// </param>
        /// <returns>
        /// A vector of coefficients.
        /// </returns>
        /// <remarks>
        /// Caclulates the standard linear regression via ordinary least squares (OLS):
        ///      
        ///     β = (Xᵀ * X)⁻¹ * Xᵀ * Y
        /// 
        /// Where:
        /// 
        ///     X = design
        ///     Y = response
        ///     β = coefficients
        /// </remarks>
        [Pure]
        [NotNull]
        public static double[] RegressOLS([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, double constant)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            return RegressOLS(design.Prepend(constant), response);
        }

        /// <summary>
        /// Calculates a linear regression given a system of coefficients. The coefficient matrix is prepended by a vector of constants.
        /// </summary>
        /// <param name="design">
        /// The design variables.
        /// </param>
        /// <param name="response">
        /// The response variables.
        /// </param>
        /// <param name="constant">
        /// A constant value to prepend the response variable array.
        /// </param>
        /// <param name="options">
        /// Options for running this method in parallel.
        /// </param>
        /// <returns>
        /// A vector of coefficients.
        /// </returns>
        /// <remarks>
        /// Caclulates the standard linear regression via ordinary least squares (OLS):
        ///      
        ///     β = (Xᵀ * X)⁻¹ * Xᵀ * Y
        /// 
        /// Where:
        /// 
        ///     X = design
        ///     Y = response
        ///     β = coefficients
        /// </remarks>
        [Pure]
        [NotNull]
        public static double[] RegressOLS([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, double constant, [NotNull] ParallelOptions options)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return RegressOLS(design.Prepend(constant, options), response, options);
        }
    }
}