﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace AD.Mathematics.RegressionModels
{
    /// <summary>
    /// Represents a regression model.
    /// </summary>
    [PublicAPI]
    public interface IRegressionModel
    {
        /// <summary>
        /// The number of observations used to train the model ≡ N.
        /// </summary>
        int ObservationCount { get; }

        /// <summary>
        /// The number of variables in the model ≡ K.
        /// </summary>
        int VariableCount { get; }

        /// <summary>
        /// The degrees of freedom for the model ≡ df = N - K.
        /// </summary>
        int DegreesOfFreedom { get; }

        /// <summary>
        /// The sum of squared errors for the model ≡ SSE = Σ(Ŷᵢ - Yᵢ)².
        /// </summary>
        double SumSquaredErrors { get; }

        /// <summary>
        /// The mean of the squared errors for the model ≡ MSE = SSE ÷ (N - K).
        /// </summary>
        double MeanSquaredError { get; }

        /// <summary>
        /// The square root of the mean squared error for the model ≡ RootMSE = sqrt(MSE).
        /// </summary>
        double RootMeanSquaredError { get; }

        /// <summary>
        /// The coefficients calculated by the model ≡ β = (Xᵀ * X)⁻¹ * Xᵀ * y.
        /// </summary>
        [NotNull]
        IReadOnlyList<double> Coefficients { get; }

        /// <summary>
        /// The standard errors for the model intercept and coefficients ≡ SE = sqrt(σ²) = sqrt(Σ(xᵢ - x̄)²).
        /// </summary>
        [NotNull]
        IReadOnlyList<double> StandardErrors { get; }

        /// <summary>
        /// HCO (White, 1980): the original White (1980) standard errors ≡ Xᵀ * [eᵢ²] * X.
        /// </summary>
        [NotNull]
        IReadOnlyList<double> StandardErrorsHc0 { get; }

        /// <summary>
        /// HC1 (MacKinnon and White, 1985): the common White standard errors, equivalent to the 'robust' option in Stata ≡ Xᵀ * [eᵢ² * n ÷ (n - k)] * X.
        /// </summary>
        [NotNull]
        IReadOnlyList<double> StandardErrorsHc1 { get; }

        /// <summary>
        /// The variance for the model ≡ σ² = Σ(xᵢ - x̄)².
        /// </summary>
        [NotNull]
        IEnumerable<double> Variance { get; }

        /// <summary>
        /// The variance for the model based on HC0 scaling.
        /// </summary>
        [NotNull]
        IEnumerable<double> VarianceHc0 { get; }

        /// <summary>
        /// The variance for the model based on HC1 scaling.
        /// </summary>
        [NotNull]
        IEnumerable<double> VarianceHc1 { get; }

        /// <summary>
        /// Evaluates the regression for a given response vector.
        /// </summary>
        /// <param name="designVector">
        /// The design vector to which a transformation is applied.
        /// </param>
        /// <returns>
        /// The value of the transformation given independent values vector.
        /// </returns>
        [Pure]
        double Evaluate([NotNull] IReadOnlyList<double> designVector);
    }
}