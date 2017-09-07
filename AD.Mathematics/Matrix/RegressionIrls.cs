using System;
using AD.Mathematics.Distributions;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class RegressionIrls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        /// <param name="response"></param>
        /// <param name="weights"></param>
        /// <param name="distribution"></param>
        /// <param name="maxIterations"></param>
        /// <param name="absoluteTolerance"></param>
        /// <param name="relativeTolerance"></param>
        /// <returns></returns>
        public static (double[] Coefficients, double[] WeightedResponse) RegressIrls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] IDistribution distribution, int maxIterations = 100, double absoluteTolerance = 1e-15, double relativeTolerance = 0)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }            
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            
            double[] oldResiduals = new double[design[0].Length];
            double[] wlsCoefficients = new double[design[0].Length];
            double[] wlsResponse = new double[weights.Length];
            double[] wlsWeights = new double[weights.Length];
            
            Array.Copy(weights, wlsWeights, wlsWeights.Length);
            
            double[] meanResponse = distribution.InitialMean(response);
            double[] linearResponse = distribution.Predict(meanResponse);
            
            for (int i = 0; i < maxIterations; i++)
            {              
                wlsWeights = 
                    distribution.Weight(meanResponse)
                                .Multiply(weights);

                wlsResponse = 
                    distribution.LinkFunction
                                .FirstDerivative(meanResponse)
                                .Multiply(response.Subtract(meanResponse))
                                .Add(linearResponse);
               
                wlsCoefficients =
                    design.RegressWls(wlsResponse, wlsWeights);

                linearResponse =
                    design.MatrixProduct(wlsCoefficients);
                
                meanResponse =
                    distribution.Fit(linearResponse);

                double[] residuals = 
                    response.Subtract(linearResponse);
                
                if (!CheckConvergence(oldResiduals, residuals, absoluteTolerance, relativeTolerance))
                {
                    Array.Copy(residuals, oldResiduals, oldResiduals.Length);
                }
            }

            return (wlsCoefficients, wlsResponse);
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
        private static bool CheckConvergence(double[] a, double[] b, double absoluteTolerance, double relativeTolerance)
        {
            double tolerance = absoluteTolerance + relativeTolerance;
            
            for (int i = 0; i < a.Length; i++)
            {
                if (tolerance * Math.Abs(b[i]) < Math.Abs(a[i] - b[i]))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}