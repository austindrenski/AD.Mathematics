using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Calculates the inverse of an array with QR decomposition.
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
    /// To solve this with QR factorization, let:
    /// 
    ///     3) S⁻¹ = (Xᵀ * X)⁻¹
    ///     
    /// Then factor S into Q * R such that:
    /// 
    ///     4) (Q * R)⁻¹ = S⁻¹
    /// 
    /// Which can be rewritten as:
    /// 
    ///     5) R⁻¹ * Q⁻¹ = (Q * R)⁻¹
    /// 
    /// Then substituting into (2):
    ///     
    ///     6) β = R⁻¹ * Q⁻¹ * Xᵀ * Y
    /// 
    /// Applying the fact that Q is orthogonal means that:
    /// 
    ///     7) Q⁻¹ = Qᵀ
    /// 
    /// Then let:
    ///
    ///     8) T = Qᵀ * Xᵀ * Y
    /// 
    /// Finally, simplify to:
    /// 
    ///     9) β = R⁻¹ * T
    /// 
    /// Finally, (9) can be solved by backward substitution:
    /// 
    ///     10) β = backwardSubstitution(R, T)
    /// </remarks>
    [PublicAPI]
    public static class SolverQr
    {
        /// <summary>
        /// Solves an equation of the form A * x = b where A = Q * R.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="response">
        /// The response array.
        /// </param>
        /// <returns>
        /// An array of coefficients.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SolveQr([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            double[][] transpose = 
                design.Transpose();

            double[][] designTransposeCrossDesign = transpose.MatrixProduct(design);

            (double[][] orthogonal, double[][] upper) = designTransposeCrossDesign.DecomposeQr();

            double[] t = orthogonal.Transpose().MatrixProduct(transpose).MatrixProduct(response);

            return upper.SolveLu(t);
        }

        [Pure]
        [NotNull]
        public static double[] SolveQr([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] ParallelOptions options)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            double[][] transpose =
                design.Transpose(options);

            double[][] designTransposeCrossDesign = transpose.MatrixProduct(design, options);

            (double[][] orthogonal, double[][] upper) = designTransposeCrossDesign.DecomposeQr();

            double[] t = orthogonal.Transpose(options).MatrixProduct(transpose, options).MatrixProduct(response, options);

            return upper.SolveLu(t);
        }
    }
}