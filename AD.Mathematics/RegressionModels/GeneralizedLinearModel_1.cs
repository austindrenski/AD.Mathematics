using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [NotNull]
        private readonly IDistribution<T> _family;

        [NotNull]
        [ItemNotNull]
        private readonly double[][] _design;

        [NotNull]
        private readonly double[] _response;

        [NotNull]
        private readonly double[] _weights;

        /// <inheritdoc />
        /// <summary>
        /// The number of observations used to train the model ≡ N.
        /// </summary>
        public int ObservationCount => _design.Length;

        /// <inheritdoc />
        /// <summary>
        /// The number of variables in the model ≡ K.
        /// </summary>
        public int VariableCount => _design[0].Length;

        /// <inheritdoc />
        /// <summary>
        /// The degrees of freedom for the model ≡ df = N - K.
        /// </summary>
        public int DegreesOfFreedom => ObservationCount - Coefficients.Count;

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
        /// The coefficients calculated by the model ≡ β = (Xᵀ * X)⁻¹ * Xᵀ * y.
        /// </summary>
        public IReadOnlyList<double> Coefficients { get; }

        /// <inheritdoc />
        /// <summary>
        /// The standard errors for the model intercept and coefficients ≡ SE = sqrt(σ²) = sqrt(Σ(xᵢ - x̄)²).
        /// </summary>
        public IReadOnlyList<double> StandardErrorsOls { get; }

        /// <inheritdoc />
        /// <summary>
        /// HCO (White, 1980): the original White (1980) standard errors ≡ Xᵀ * [eᵢ²] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC0 { get; }

        /// <inheritdoc />
        /// <summary>
        /// HC1 (MacKinnon and White, 1985): the common White standard errors, equivalent to the 'robust' option in Stata ≡ Xᵀ * [eᵢ² * n ÷ (n - k)] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC1 { get; }

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
        /// <param name="family">
        /// The distribution class used by the model.
        /// </param>
        public GeneralizedLinearModel([NotNull][ItemNotNull] double[][] design, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] IDistribution<T> family)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (family is null)
            {
                throw new ArgumentNullException(nameof(family));
            }
            if (design.Length != response.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            _family = family;
            _design = design;
            _response = response;
            _weights = weights;

            Coefficients =
                _family is GaussianDistribution && _family.LinkFunction is IdentityLinkFunction
                    ? design.RegressOls(response)
                    : design.RegressIrls(response);

            double[] squaredErrors = design.SquaredError(response, Evaluate);
            
            SumSquaredErrors = squaredErrors.Sum();

            StandardErrorsOls = design.StandardError(squaredErrors, HeteroscedasticityConsistent.Ols);
            
            StandardErrorsHC0 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC0);

            StandardErrorsHC1 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC1);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructs a <see cref="GeneralizedLinearModel{T}"/> estimated with the given data and the independent matrix prepended by the constant.
        /// </summary>
        /// <param name="independent">
        /// A collection of independent value vectors.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        /// <param name="weights">
        /// An array of importance weights.
        /// </param>
        /// <param name="family">
        /// The distribution class used by the model.
        /// </param>
        /// <param name="constant">
        /// The constant used by the model to prepend the design matrix.
        /// </param>
        public GeneralizedLinearModel([NotNull][ItemNotNull] double[][] independent, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] IDistribution<T> family, double constant)
            : this(independent.Prepend(constant), response, weights, family)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the regression for a given response vector.
        /// </summary>
        /// <param name="designVector">
        /// The design vector to which a transformation is applied.
        /// </param>
        /// <returns>
        /// The value of the transformation given independent values vector.
        /// </returns>
        public double Evaluate(IReadOnlyList<double> designVector)
        {
            if (designVector is null)
            {
                throw new ArgumentNullException(nameof(designVector));
            }

            double result = 0.0;

            for (int i = 0; i < designVector.Count; i++)
            {
                result += Coefficients[i] * designVector[i];
            }

            return result;
        }

        /// <summary>
        /// Calculates the log-likelihood for a given <paramref name="response"/>.
        /// </summary>
        /// <param name="response">
        /// The response values to test.
        /// </param>
        /// <param name="scale">
        /// An optional scaling factor.
        /// </param>
        /// <returns>
        /// The log-likelihood for the given response.
        /// </returns>
        public double LogLikelihood([NotNull] IReadOnlyList<double> response, double scale = 1.0)
        {
            double[] linearPrediction = _design.MatrixProduct(response); // + self.offset_exposure

            double[] meanResponse = _family.LinkFunction.Inverse(linearPrediction);

            if (scale is 1.0)
            {
                // Set some scale features based on the expected value.
                // Python: self.estimate_scale(expval)
            }

            return _family.LogLikelihood(response, meanResponse, _weights, scale);
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

        /// <summary>
        /// Private helper method to check whether two vectors are sufficiently close to indicate convergence.
        /// </summary>
        /// <param name="a">
        /// The first vector.
        /// </param>
        /// <param name="b">
        /// The second vector.
        /// </param>
        /// <param name="absoluteTolerance">
        /// The absolute tolerance for convergence.
        /// </param>
        /// <param name="relativeTolerance">
        /// The relative tolerance among the calues for convergence.
        /// </param>
        /// <returns>
        /// True if convergence is likely; otherwise false.
        /// </returns>
        private bool CheckConvergence(double[] a, double[] b, double absoluteTolerance, double relativeTolerance)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > absoluteTolerance + relativeTolerance * Math.Abs(b[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}