using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Represents a type of heteroscedacticity-consistent correction used to construct the variance-covariance matrix.
    /// </summary>
    [PublicAPI]
    public enum HeteroscedasticityConsistent
    {
        /// <summary>
        /// The default correction type in which no correction is made ≡ cov(β) = mse * (Xᵀ * X)⁻¹.
        /// </summary>
        Ols,

        /// <summary>
        /// (White, 1980) ≡ cov(β) = (Xᵀ * X)⁻¹ * Xᵀ * { eᵢ² } * X * (Xᵀ * X)⁻¹.
        /// </summary>
        HC0,

        /// <summary>
        /// (White, 1985) ≡ cov(β) = (Xᵀ * X)⁻¹ * Xᵀ * { eᵢ² * n / (n - k - 1) } * X * (Xᵀ * X)⁻¹.
        /// </summary>
        HC1
    }
}