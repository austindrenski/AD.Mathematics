using System;
using System.Linq;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Solves an equation of the form A * x = b where A = L * U.
    /// </summary>
    /// <remarks>
    /// Recall that OLS regression has the matrix equation: 
    ///     
    ///     1) X * β = Y
    /// 
    /// This equation has the following closed-form solution:
    /// 
    ///     2) β = (Xᵀ * X)⁻¹ * Xᵀ * Y
    /// 
    /// To solve this with LU factorization, let:
    /// 
    ///     3) S = (Xᵀ * X)
    ///     
    /// Then factor S into (2) such that:
    /// 
    ///     4) β = S⁻¹ * Xᵀ * Y
    /// 
    /// Then let T = Xᵀ * Y and rewrite (2) as:
    /// 
    ///     5) β = S⁻¹ * T
    /// 
    /// Finally, (5) can be solved by backward substitution:
    /// 
    ///     6) β = backwardSubstitution(S, T)
    /// </remarks>
    [PublicAPI]
    public static class SolverLu
    {
        /// <summary>
        /// Solves an equation of the form A * x = b where A = L * U.
        /// </summary>
        /// <param name="lowerUpper">
        /// The lower and upper triangular arrays.
        /// </param>
        /// <param name="response">
        /// The response vector.
        /// </param>
        /// <returns>
        /// An array of coefficients.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SolveLu(this (double[][] Lower, double[][] Upper) lowerUpper, [NotNull] double[] response)
        {
            if (lowerUpper.Lower is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper.Lower));
            }
            if (lowerUpper.Upper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper.Upper));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return SolveLu(lowerUpper.Lower, lowerUpper.Upper, response);
        }

        /// <summary>
        /// Solves an equation of the form A * x = b where A = L * U.
        /// </summary>
        /// <param name="lower">
        /// The lower triangular array.
        /// </param>
        /// <param name="upper">
        /// The upper triangular array.
        /// </param>
        /// <param name="response">
        /// The response vector.
        /// </param>
        /// <returns>
        /// An array of coefficients.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SolveLu([NotNull] [ItemNotNull] this double[][] lower, [NotNull] [ItemNotNull] double[][] upper, [NotNull] double[] response)
        {
            if (lower is null)
            {
                throw new ArgumentNullException(nameof(lower));
            }
            if (upper is null)
            {
                throw new ArgumentNullException(nameof(upper));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            
            double[] x = new double[response.Length];
            double[] y = new double[response.Length];

            for (int i = 0; i < response.Length; i++)
            {
                y[i] = response[i];

                for (int j = 0; j < i; j++)
                {
                    y[i] -= lower[i][j] * y[j];
                }

                y[i] /= lower[i][i];
            }

            for (int i = response.Length - 1; i >= 0; i--)
            {
                x[i] = y[i];

                for (int j = i + 1; j < response.Length; j++)
                {
                    x[i] -= upper[i][j] * x[j];
                }

                x[i] /= upper[i][i];
            }

            return x;
        }

        /// <summary>
        /// Solves an equation of the form A * x = b where A = L * U.
        /// </summary>
        /// <param name="lowerUpper">
        /// The combined lower and upper arrays where the lower diagonal are implicitly equal to 1.0.
        /// </param>
        /// <param name="response">
        /// The response vector.
        /// </param>
        /// <returns>
        /// An array of coefficients.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SolveLu([NotNull][ItemNotNull] this double[][] lowerUpper, [NotNull] double[] response)
        {
            if (lowerUpper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return SolveLu(lowerUpper, response, Enumerable.Range(0, lowerUpper.Length).ToArray());
        }

        /// <summary>
        /// Solves an equation of the form A * x = b where A = P(L * U) and P(...) is a permutation function.
        /// </summary>
        /// <param name="lowerUpper">
        /// The combined lower and upper arrays where the lower diagonal are implicitly equal to 1.0.
        /// </param>
        /// <param name="response">
        /// The response vector.
        /// </param>
        /// <param name="permutation">
        /// The array mapping the row order of the combined lower and upper arrays to the original.
        /// </param>
        /// <returns>
        /// An array of coefficients.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SolveLu([NotNull][ItemNotNull] this double[][] lowerUpper, [NotNull] double[] response, [NotNull] int[] permutation)
        {
            if (lowerUpper is null)
            {
                throw new ArgumentNullException(nameof(lowerUpper));
            }
            if (permutation is null)
            {
                throw new ArgumentNullException(nameof(permutation));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            double[] y = new double[lowerUpper.Length];
            double[] x = new double[lowerUpper.Length];
            
            for (int i = 0; i <= lowerUpper.Length - 1; i++)
            {
                double sum = 0.0;

                for (int j = 0; j <= i - 1; j++)
                {
                    sum += i == j ? y[j] : y[j]  * lowerUpper[i][j];
                }

                y[i] = response[permutation[i]] - sum;
            }

            for (int i = lowerUpper.Length - 1; i >= 0; i--)
            {
                double sum = 0.0;

                for (int j = i + 1; j <= lowerUpper.Length - 1; j++)
                {
                    sum += lowerUpper[i][j] * x[j];
                }

                x[i] = (y[i] - sum) / lowerUpper[i][i];
            }

            return x;
        }
    }
}