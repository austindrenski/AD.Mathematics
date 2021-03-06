﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AD.Mathematics.Distributions;
using AD.Mathematics.LinkFunctions;
using AD.Mathematics.Matrix;
using JetBrains.Annotations;

namespace AD.Mathematics.RegressionModels
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a generalized linear regression model.
    /// </summary>
    [PublicAPI]
    public class GeneralizedLinearModel<T> : IRegressionModel
    {      
        private readonly Lazy<double[]> _standardErrorsOls;
        private readonly Lazy<double[]> _standardErrorsHC0;
        private readonly Lazy<double[]> _standardErrorsHC1;

        /// <summary>
        /// The distribution used to estimate the model.
        /// </summary>
        private readonly IDistribution<T> _distribution;

        /// <inheritdoc />
        /// <summary>
        /// The number of observations used to train the model ≡ N.
        /// </summary>
        public int ObservationCount { get; }

        /// <inheritdoc />
        /// <summary>
        /// The number of variables in the model ≡ K.
        /// </summary>
        public int VariableCount { get; }
        
        /// <inheritdoc />
        /// <summary>
        /// The degrees of freedom for the model ≡ df = N - K.
        /// </summary>
        public int DegreesOfFreedom => ObservationCount - VariableCount;
        
        /// <inheritdoc />
        /// <summary>
        /// The coefficients calculated by the model ≡ β = (Xᵀ * X)⁻¹ * Xᵀ * y.
        /// </summary>
        public IReadOnlyList<double> Coefficients { get; }

        /// <inheritdoc />
        /// <summary>
        /// The sum of squared errors for the model ≡ SSE = Σ(Ŷᵢ - Yᵢ)².
        /// </summary>
        public double SumSquaredErrors { get; }

        /// <inheritdoc />
        /// <summary>
        /// The mean of the squared errors for the model ≡ MSE = SSE ÷ (N - K).
        /// </summary>
        public double MeanSquaredError => SumSquaredErrors / DegreesOfFreedom;

        /// <inheritdoc />
        /// <summary>
        /// The square root of the mean squared error for the model ≡ RootMSE = sqrt(MSE).
        /// </summary>
        public double RootMeanSquaredError => Math.Sqrt(MeanSquaredError);

        /// <inheritdoc />
        /// <summary>
        /// The standard errors for the model intercept and coefficients ≡ SE = sqrt(σ²) = sqrt(Σ(xᵢ - x̄)²).
        /// </summary>
        public IReadOnlyList<double> StandardErrorsOls => _standardErrorsOls.Value;

        /// <inheritdoc />
        /// <summary>
        /// HCO (White, 1980): the original White (1980) standard errors ≡ Xᵀ * [eᵢ²] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC0 => _standardErrorsHC0.Value;

        /// <inheritdoc />
        /// <summary>
        /// HC1 (MacKinnon and White, 1985): the common White standard errors, equivalent to the 'robust' option in Stata ≡ Xᵀ * [eᵢ² * n ÷ (n - k)] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC1 => _standardErrorsHC1.Value;

        /// <inheritdoc />
        /// <summary>
        /// The variance for the model ≡ σ² = Σ(xᵢ - x̄)².
        /// </summary>
        public IEnumerable<double> VarianceOls => StandardErrorsOls.Select(x => x * x);

        /// <inheritdoc />
        /// <summary>
        /// The variance for the model based on HC0 scaling.
        /// </summary>
        public IEnumerable<double> VarianceHC0 => StandardErrorsHC0.Select(x => x * x);

        /// <inheritdoc />
        /// <summary>
        /// The variance for the model based on HC1 scaling.
        /// </summary>
        public IEnumerable<double> VarianceHC1 => StandardErrorsHC1.Select(x => x * x);

        /// <summary>
        /// Constructs a <see cref="GeneralizedLinearModel{T}"/> estimated with the given data.
        /// </summary>
        /// <param name="design">
        /// The design matrix of independent variables.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        /// <param name="weights">
        /// An array of importance weights.
        /// </param>
        /// <param name="distribution">
        /// The distribution class used by the model.
        /// </param>
        /// <param name="addConstant">
        /// True to prepend the design matrix with a unit vector; otherwise false. 
        /// </param>
        /// <param name="options">
        /// Executes select methods in parallel if provided.
        /// </param>
        public GeneralizedLinearModel(
            [NotNull][ItemNotNull] double[][] design, 
            [NotNull] double[] response,
            [NotNull] double[] weights, 
            [NotNull] IDistribution<T> distribution, 
            bool addConstant = false,
            [CanBeNull] ParallelOptions options = default)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (distribution is null)
            {
                throw new ArgumentNullException(nameof(distribution));
            }
            if (design.Length != response.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            double[][] designArray = addConstant ? design.Prepend(1.0) : design;

            _distribution  = distribution;

            ObservationCount = designArray.Length;
            
            VariableCount = designArray[0].Length;

            double[] resultResponse;
            
            if (distribution is GaussianDistribution && distribution.LinkFunction is IdentityLinkFunction)
            {
                Coefficients = 
                    options is null
                        ? designArray.RegressOls(response) 
                        : designArray.RegressOls(response, options);
                
                resultResponse = response;
            }
            else
            {
                (Coefficients, resultResponse) =
                    options is null
                        ? designArray.RegressIrls(response, weights, distribution)
                        : designArray.RegressIrls(response, weights, distribution, options);
            }
            
            double[] squaredErrors = designArray.SquaredError(resultResponse, Evaluate);

            SumSquaredErrors = squaredErrors.Sum();

            _standardErrorsOls = new Lazy<double[]>(() => designArray.StandardError(squaredErrors, StandardErrorType.Ols));
            _standardErrorsHC0 = new Lazy<double[]>(() => designArray.StandardError(squaredErrors, StandardErrorType.HC0));
            _standardErrorsHC1 = new Lazy<double[]>(() => designArray.StandardError(squaredErrors, StandardErrorType.HC1));
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the regression for a given observation vector.
        /// </summary>
        /// <param name="observation">
        /// The observation vector to which a transformation is applied.
        /// </param>
        /// <returns>
        /// The value of the transformation given observation vector.
        /// </returns>
        public double Evaluate(IReadOnlyList<double> observation)
        {
            if (observation is null)
            {
                throw new ArgumentNullException(nameof(observation));
            }

            double result = 0.0;

            for (int i = 0; i < observation.Count; i++)
            {
                result += Coefficients[i] * observation[i];
            }

            return result;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"N: {ObservationCount}");
            stringBuilder.AppendLine($"K: {VariableCount}");
            stringBuilder.AppendLine($"df: {DegreesOfFreedom}");
            stringBuilder.AppendLine($"SSE: {SumSquaredErrors}");
            stringBuilder.AppendLine($"MSE: {MeanSquaredError}");
            stringBuilder.AppendLine($"Root MSE: {RootMeanSquaredError}");

            for (int i = 0; i < Coefficients.Count; i++)
            {
                stringBuilder.AppendLine($"B[{i}]: {Coefficients[i]} (SE: {StandardErrorsOls[i]}) (HC0: {StandardErrorsHC0[i]}) (HC1: {StandardErrorsHC1[i]})");
            }

            return stringBuilder.ToString();
        }
    }
}