using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AD.Mathematics.Matrix;
using JetBrains.Annotations;

namespace AD.Mathematics.RegressionModels
{
    /// <summary>
    /// Represents a linear regression model of multiple variables.
    /// </summary>
    [PublicAPI]
    public class MultipleLinearRegressionModel : IRegressionModel
    {
        [NotNull]
        [ItemNotNull]
        private readonly double[][] _design;

        [NotNull]
        private readonly double[] _response;

        /// <summary>
        /// The number of observations used to train the model ≡ N.
        /// </summary>
        public int ObservationCount => _design.Length;

        /// <summary>
        /// The number of variables in the model ≡ K.
        /// </summary>
        public int VariableCount => _design[0].Length;

        /// <summary>
        /// The degrees of freedom for the model ≡ df = N - K.
        /// </summary>
        public int DegreesOfFreedom => ObservationCount - Coefficients.Count;
        
        /// <summary>
        /// The sum of squared errors for the model ≡ SSE = Σ(Ŷᵢ - Yᵢ)².
        /// </summary>
        public double SumSquaredErrors { get; }

        /// <summary>
        /// The mean of the squared errors for the model ≡ MSE = SSE ÷ (N - K).
        /// </summary>
        public double MeanSquaredError => SumSquaredErrors / DegreesOfFreedom;

        /// <summary>
        /// The square root of the mean squared error for the model ≡ RootMSE = sqrt(MSE).
        /// </summary>
        public double RootMeanSquaredError => Math.Sqrt(MeanSquaredError);
        
        /// <summary>
        /// The coefficients calculated by the model ≡ β = (Xᵀ * X)⁻¹ * Xᵀ * y.
        /// </summary>
        public IReadOnlyList<double> Coefficients { get; }

        /// <summary>
        /// The standard errors for the model intercept and coefficients ≡ SE = sqrt(σ²) = sqrt(Σ(xᵢ - x̄)²).
        /// </summary>
        public IReadOnlyList<double> StandardErrors { get; }

        /// <summary>
        /// HCO (White, 1980): the original White (1980) standard errors ≡ Xᵀ * [eᵢ²] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHc0 { get; }

        /// <summary>
        /// HC1 (MacKinnon and White, 1985): the common White standard errors, equivalent to the 'robust' option in Stata ≡ Xᵀ * [eᵢ² * n ÷ (n - k)] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHc1 { get; }

        /// <summary>
        /// The variance for the model ≡ σ² = Σ(xᵢ - x̄)².
        /// </summary>
        public IEnumerable<double> Variance => StandardErrors.Select(x => x * x);

        /// <summary>
        /// The variance for the model based on HC0 scaling.
        /// </summary>
        public IEnumerable<double> VarianceHc0 => StandardErrorsHc0.Select(x => x * x);

        /// <summary>
        /// The variance for the model based on HC1 scaling.
        /// </summary>
        public IEnumerable<double> VarianceHc1 => StandardErrorsHc1.Select(x => x * x);

        /// <summary>
        /// Constructs a <see cref="MultipleLinearRegressionModel"/> estimated with the given data.
        /// </summary>
        /// <param name="design">
        /// The design matrix of independent variables.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        public MultipleLinearRegressionModel([NotNull][ItemNotNull] double[][] design, [NotNull] double[] response)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design.Length != response.Length || design.Length == 0)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            _design = design;
            _response = response;

            Coefficients = design.RegressOls(response);

            double[] squaredErrors = design.SquaredError(response, Evaluate);

            SumSquaredErrors = squaredErrors.Sum();

            StandardErrors = design.StandardError(squaredErrors, HeteroscedasticityConsistent.OLS);

            StandardErrorsHc0 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC0);

            StandardErrorsHc1 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC1);
        }

        /// <summary>
        /// Constructs a <see cref="MultipleLinearRegressionModel"/> estimated with the given data using <see cref="RegressionOls.RegressOls(double[][], double[])"/>.
        /// </summary>
        /// <param name="independent">
        /// A collection of independent value vectors.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        /// <param name="weights">
        /// The observation weights applied by the model.
        /// </param>
        /// <param name="constant">
        /// The constant used by the model to prepend the design matrix.
        /// </param>
        public MultipleLinearRegressionModel([NotNull][ItemNotNull] double[][] independent, [NotNull] double[] response, [NotNull] double[] weights, double constant)
            : this(independent.Prepend(constant).Weight(weights), response.Weight(weights))
        {
        }

        /// <summary>
        /// Constructs a <see cref="MultipleLinearRegressionModel"/> estimated with the given data using <see cref="RegressionOls.RegressOls(double[][], double[])"/>.
        /// </summary>
        /// <param name="independent">
        /// A collection of independent value vectors.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        /// <param name="constant">
        /// The constant used by the model to prepend the design matrix.
        /// </param>
        public MultipleLinearRegressionModel([NotNull][ItemNotNull] double[][] independent, [NotNull] double[] response, double constant)
            : this(independent.Prepend(constant), response)
        {
        }

        /// <summary>
        /// Constructs a <see cref="MultipleLinearRegressionModel"/> estimated with the given data using <see cref="RegressionOls.RegressOls(double[][], double[])"/>.
        /// </summary>
        /// <param name="design">
        /// The design matrix of independent variables.
        /// </param>
        /// <param name="response">
        /// A collection of response values.
        /// </param>
        /// <param name="weights">
        /// The observation weights.
        /// </param>
        public MultipleLinearRegressionModel([NotNull] [ItemNotNull] double[][] design, [NotNull] double[] response, [NotNull] double[] weights)
            : this(design.Weight(weights), response.Weight(weights))
        {
        }
        
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
                stringBuilder.AppendLine($"B[{i}]: {Coefficients[i]} (SE: {StandardErrors[i]}) (HC0: {StandardErrorsHc0[i]}) (HC1: {StandardErrorsHc1[i]})");
            }

            return stringBuilder.ToString();
        }
    }
}