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
    /// <summary>
    /// Represents a generalized linear regression model.
    /// </summary>
    [PublicAPI]
    public class GeneralizedLinearRegressionModel<T> : IRegressionModel
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
        /// <param name="weights">
        /// An array of importance weights.
        /// </param>
        /// <param name="family">
        /// The distribution class used by the model.
        /// </param>
        public GeneralizedLinearRegressionModel([NotNull][ItemNotNull] double[][] design, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] IDistribution<T> family)
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

            Coefficients = _family is GaussianDistribution && _family.LinkFunction is IdentityLinkFunction ? design.RegressOls(response) : FitIrls().Results;

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
        /// An array of importance weights.
        /// </param>
        /// <param name="family">
        /// The distribution class used by the model.
        /// </param>
        /// <param name="constant">
        /// The constant used by the model to prepend the design matrix.
        /// </param>
        public GeneralizedLinearRegressionModel([NotNull][ItemNotNull] double[][] independent, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] IDistribution<T> family, double constant)
            : this(independent.Prepend(constant), response, weights, family)
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

        public (double[] Results, double[] Fitted, double[] Residuals, double Scale) FitIrls(double tol = 1e-15, double absoluteTolerance = 1e-15, double relativeTolerance = 0, double tolCriterion = 1e-15, int maxIterations = 100, double[] initial = null)
        {
            double[] meanResponse;
            double[] linearPrediction;

            if (initial is null)
            {
                meanResponse = _response.Select(_family.Fit).ToArray();
                linearPrediction = meanResponse.Select(x => _family.Predict(x)).ToArray();
            }
            else
            {
                linearPrediction = _design.CrossProduct(initial); // + self.offset_exposure
                meanResponse = linearPrediction.Select(x => _family.Fit(x)).ToArray();
            }

            double deviance = _family.Deviance(_response, meanResponse, _weights);

            double[] previousResiduals = new double[_design[0].Length];

            double[] wlsResponse = new double[_weights.Length];

            double[] weights = new double[_weights.Length];

            for (int i = 0; i < maxIterations; i++)
            {
                for (int j = 0; j < weights.Length; j++)
                {
                    weights[j] = _weights[j] * _family.Weight(meanResponse[j]);
                }

                for (int j = 0; j < wlsResponse.Length; j++)
                {
                    wlsResponse[j] = linearPrediction[j] + _family.LinkFunction.FirstDerivative(meanResponse[j]) * (_response[j] - meanResponse[j]); // - self.offset_exposure
                }

                (double[] wlsResults, double[] wlsFitted, double[] wlsResiduals, double wlsScale) = _design.RegressWls(wlsResponse, weights);

                linearPrediction = _design.CrossProduct(wlsResults); // + self.offset_exposure

                for (int j = 0; j < wlsResponse.Length; j++)
                {
                    meanResponse[j] = _family.Fit(linearPrediction[j]);
                }

                if (CheckConvergence(previousResiduals, wlsResiduals, absoluteTolerance, relativeTolerance))
                {
                    break;
                }

                previousResiduals = wlsResiduals.ToArray();
            }

            return _design.RegressWls(wlsResponse, weights);
        }

        public double LogLikelihood([NotNull] IReadOnlyList<double> response, double scale = 1.0)
        {
            double[] linearPrediction = _design.CrossProduct(response); // + self.offset_exposure

            double[] meanResponse = linearPrediction.Select(x => _family.LinkFunction.Inverse(x)).ToArray();

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
                stringBuilder.AppendLine($"B[{i}]: {Coefficients[i]} (SE: {StandardErrors[i]}) (HC0: {StandardErrorsHc0[i]}) (HC1: {StandardErrorsHc1[i]})");
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