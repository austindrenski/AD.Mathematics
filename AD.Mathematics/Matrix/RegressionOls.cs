using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Defines parallel matrix operations.
    /// </summary>
    [PublicAPI]
    public static class RegressionOls
    {
        /// <summary>
        /// Specifies the solution technique.
        /// </summary>
        [PublicAPI]
        public enum OlsType
        {
            LuInversion,
            LuFactorization,
            QrFactorization
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
        public static float[] RegressOls([NotNull][ItemNotNull] this float[][] design, [NotNull] float[] response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            float[][] transpose =
                design.Transpose();
            
            float[] result =
                transpose.CrossProduct(design)
                         .InvertLu()
                         .CrossProduct(transpose)
                         .CrossProduct(response);

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
        public static double[] RegressOls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response)
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
                transpose.CrossProduct(design)
                         .InvertLu()
                         .CrossProduct(transpose)
                         .CrossProduct(response);

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
        public static decimal[] RegressOls([NotNull][ItemNotNull] this decimal[][] design, [NotNull] decimal[] response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            decimal[][] transpose =
                design.Transpose();

            decimal[] result =
                transpose.CrossProduct(design)
                         .InvertLu()
                         .CrossProduct(transpose)
                         .CrossProduct(response);

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
        public static float[] RegressOls([NotNull][ItemNotNull] this float[][] design, [NotNull] float[] response, [NotNull] ParallelOptions options)
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

            float[][] transpose =
                design.Transpose(options);

            float[] result =
                transpose.CrossProduct(design, options)
                         .InvertLu(options)
                         .CrossProduct(transpose, options)
                         .CrossProduct(response, options);

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
        public static double[] RegressOls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] ParallelOptions options)
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
                transpose.CrossProduct(design, options)
                         .InvertLu(options)
                         .CrossProduct(transpose, options)
                         .CrossProduct(response, options);

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
        public static decimal[] RegressOls([NotNull][ItemNotNull] this decimal[][] design, [NotNull] decimal[] response, [NotNull] ParallelOptions options)
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

            decimal[][] transpose =
                design.Transpose(options);
            
            decimal[] result =
                transpose.CrossProduct(design, options)
                         .InvertLu(options)
                         .CrossProduct(transpose, options)
                         .CrossProduct(response, options);

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
        public static float[] RegressOls([NotNull][ItemNotNull] this float[][] design, [NotNull] float[] response, float constant)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            return RegressOls(design.Prepend(constant), response);
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
        public static double[] RegressOls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, double constant)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            return RegressOls(design.Prepend(constant), response);
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
        public static decimal[] RegressOls([NotNull][ItemNotNull] this decimal[][] design, [NotNull] decimal[] response, decimal constant)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }

            return RegressOls(design.Prepend(constant), response);
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
        public static float[] RegressOls([NotNull][ItemNotNull] this float[][] design, [NotNull] float[] response, float constant, [NotNull] ParallelOptions options)
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

            return RegressOls(design.Prepend(constant, options), response, options);
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
        public static double[] RegressOls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, double constant, [NotNull] ParallelOptions options)
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

            return RegressOls(design.Prepend(constant, options), response, options);
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
        public static decimal[] RegressOls([NotNull][ItemNotNull] this decimal[][] design, [NotNull] decimal[] response, decimal constant, [NotNull] ParallelOptions options)
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

            return RegressOls(design.Prepend(constant, options), response, options);
        }
    }
}