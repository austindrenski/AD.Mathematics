using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Represents a type of heteroscedacticity-consistent correction used to construct the variance-covariance matrix.
    /// </summary>
    [Flags]
    [PublicAPI]
    public enum StandardErrorType : byte
    {      
        /// <summary>
        /// The default correction type in which no correction is made ≡ cov(β) = mse * (Xᵀ * X)⁻¹.
        /// </summary>
        Ols = 0,

        /// <summary>
        /// (White, 1980) ≡ cov(β) = (Xᵀ * X)⁻¹ * Xᵀ * { eᵢ² } * X * (Xᵀ * X)⁻¹.
        /// </summary>
        HC0 = 1 << 1,

        /// <summary>
        /// (White, 1985) ≡ cov(β) = (Xᵀ * X)⁻¹ * Xᵀ * { eᵢ² * n / (n - k - 1) } * X * (Xᵀ * X)⁻¹.
        /// </summary>
        HC1 = 1 << 2
    }
}