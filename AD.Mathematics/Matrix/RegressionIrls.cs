using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegressionIrls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static double[] RegressIrls([NotNull] [ItemNotNull] this double[][] design, [NotNull] double[] response)
        {
            // 
            // Input: A, b, x_1, k=1
            // while not meeting the stopping criterion do
            // {
            //     UpdateW: W_k_i = |x_k_i|^-1 for all W_k_i
            //     Update x: x_(k+1) = (W_k)^-1 * A_t * (A * W_k^-1 * A_t)^-1 * b
            //     Update k = k + 1
            // }
            return response;
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
    }
}