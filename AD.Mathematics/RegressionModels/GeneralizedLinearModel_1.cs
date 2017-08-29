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
        public IReadOnlyList<double> StandardErrorsOLS { get; }

        /// <summary>
        /// HCO (White, 1980): the original White (1980) standard errors ≡ Xᵀ * [eᵢ²] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC0 { get; }

        /// <summary>
        /// HC1 (MacKinnon and White, 1985): the common White standard errors, equivalent to the 'robust' option in Stata ≡ Xᵀ * [eᵢ² * n ÷ (n - k)] * X.
        /// </summary>
        public IReadOnlyList<double> StandardErrorsHC1 { get; }

        /// <summary>
        /// The variance for the model ≡ σ² = Σ(xᵢ - x̄)².
        /// </summary>
        public IEnumerable<double> VarianceOLS => StandardErrorsOLS.Select(x => x * x);

        /// <summary>
        /// The variance for the model based on HC0 scaling.
        /// </summary>
        public IEnumerable<double> VarianceHC0 => StandardErrorsHC0.Select(x => x * x);

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
                    ? design.RegressOLS(response)
                    : IrlsAlt(100);
                    //: FitIrls().Results;

            double[] squaredErrors = design.SquaredError(response, Evaluate);
            
            SumSquaredErrors = squaredErrors.Sum();

            StandardErrorsOLS = design.StandardError(squaredErrors, HeteroscedasticityConsistent.OLS);
            
            StandardErrorsHC0 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC0);

            StandardErrorsHC1 = design.StandardError(squaredErrors, HeteroscedasticityConsistent.HC1);
        }

        /// <summary>
        /// Constructs a <see cref="GeneralizedLinearModel{T}"/> estimated with the given data using <see cref="RegressionOLS.RegressOLS(double[][], double[])"/>.
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

        public double[] IrlsAlt(int maxIterations)
        {
            return new double[VariableCount];
            //def _fit_irls(self, start_params=None, maxiter=100, tol=1e-8,
            //              scale=None, cov_type='nonrobust', cov_kwds=None,
            //              use_t=None, **kwargs):
            //    """
            //    Fits a generalized linear model for a given family using
            //    iteratively reweighted least squares (IRLS).
            //    """
            //    atol = kwargs.get('atol')
            //    rtol = kwargs.get('rtol', 0.)
            //    tol_criterion = kwargs.get('tol_criterion', 'deviance')
            //    atol = tol if atol is None else atol

            //    endog = self.endog
            //    wlsexog = self.exog
            //    if start_params is None:
            //        start_params = np.zeros(self.exog.shape[1], np.float)
            //        mu = self.family.starting_mu(self.endog)
            //        lin_pred = self.family.predict(mu)
            //    else:
            //        lin_pred = np.dot(wlsexog, start_params) + self._offset_exposure
            //        mu = self.family.fitted(lin_pred)
            //    dev = self.family.deviance(self.endog, mu, self.iweights)
            //    if np.isnan(dev):
            //        raise ValueError("The first guess on the deviance function "
            //                         "returned a nan.  This could be a boundary "
            //                         " problem and should be reported.")

            //    # first guess on the deviance is assumed to be scaled by 1.
            //    # params are none to start, so they line up with the deviance
            //    history = dict(params=[np.inf, start_params], deviance=[np.inf, dev])
            //    converged = False
            //    criterion = history[tol_criterion]
            //    # This special case is used to get the likelihood for a specific
            //    # params vector.
            //    if maxiter == 0:
            //        mu = self.family.fitted(lin_pred)
            //        self.scale = self.estimate_scale(mu)
            //        wls_results = lm.RegressionResults(self, start_params, None)
            //        iteration = 0
            //    for iteration in range(maxiter):
            //        self.weights = (self.iweights * self.n_trials *
            //                        self.family.weights(mu))
            //        wlsendog = (lin_pred + self.family.link.deriv(mu) * (self.endog-mu)
            //                    - self._offset_exposure)
            //        wls_results = reg_tools._MinimalWLS(wlsendog, wlsexog, self.weights).fit(method='lstsq')
            //        lin_pred = np.dot(self.exog, wls_results.params) + self._offset_exposure
            //        mu = self.family.fitted(lin_pred)
            //        history = self._update_history(wls_results, mu, history)
            //        self.scale = self.estimate_scale(mu)
            //        if endog.squeeze().ndim == 1 and np.allclose(mu - endog, 0):
            //            msg = "Perfect separation detected, results not available"
            //            raise PerfectSeparationError(msg)
            //        converged = _check_convergence(criterion, iteration + 1, atol,
            //                                       rtol)
            //        if converged:
            //            break
            //    self.mu = mu

            //    if maxiter > 0:  # Only if iterative used
            //        wls_results = lm.WLS(wlsendog, wlsexog, self.weights).fit()

            //    glm_results = GLMResults(self, wls_results.params,
            //                             wls_results.normalized_cov_params,
            //                             self.scale,
            //                             cov_type=cov_type, cov_kwds=cov_kwds,
            //                             use_t=use_t)

            //    glm_results.method = "IRLS"
            //    history['iteration'] = iteration + 1
            //    glm_results.fit_history = history
            //    glm_results.converged = converged
            //    return GLMResultsWrapper(glm_results)
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
                stringBuilder.AppendLine($"B[{i}]: {Coefficients[i]} (SE: {StandardErrorsOLS[i]}) (HC0: {StandardErrorsHC0[i]}) (HC1: {StandardErrorsHC1[i]})");
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