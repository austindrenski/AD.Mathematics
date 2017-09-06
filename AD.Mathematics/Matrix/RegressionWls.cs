using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods to perform weighted least squares regression.
    /// </summary>
    [PublicAPI]
    public static class RegressionWls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="design"></param>
        /// <param name="response"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArrayConformabilityException{T}"></exception>
        [Pure]
        public static double[] RegressWls([NotNull] [ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] double[] weights)
        {
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (weights is null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            if (design.Length != response.Length)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            double[][] weightedDesign = design.CloneArray();
            
            double[] weightedResponse = response.ToArray();

            double sumOfWeights = weights.Sum();
            double[] scaledWeights = weights.Select(x => Math.Sqrt(x / sumOfWeights)).ToArray();
            
            for (int i = 0; i < weightedDesign.Length; i++)
            {
                weightedResponse[i] *= scaledWeights[i];
                
                for (int j = 0; j < weightedDesign[i].Length; j++)
                {
                    weightedDesign[i][j] *= scaledWeights[i] ;
                }
            }

            return weightedDesign.RegressOls(weightedResponse);

            //OLD

            //double[] rootWeights = new double[weights.Length];
            //double[] weightedResponse = new double[weights.Length];
            //double[][] weightedDesign = new double[weights.Length][];

            //for (int i = 0; i < rootWeights.Length; i++)
            //{
            //    rootWeights[i] = Math.Sqrt(weights[i]);

            //    weightedResponse[i] = rootWeights[i] * response[i];

            //    weightedDesign[i] = new double[design[i].Length];

            //    for (int j = 0; j < weightedDesign[i].Length; j++)
            //    {
            //        weightedDesign[i][j] = rootWeights[i] * design[i][j];
            //    }
            //}

            //double[] result = weightedDesign.SolveQr(response);

            //double[] fitted = design.MatrixProduct(result);
            //double[] weightedFitted = weightedDesign.MatrixProduct(result);

            //double[] residuals = new double[fitted.Length];
            //double[] weightedResiduals = new double[fitted.Length];

            //for (int i = 0; i < residuals.Length; i++)
            //{
            //    residuals[i] = response[i] - fitted[i];
            //    weightedResiduals[i] = weightedResponse[i] - weightedFitted[i];
            //}

            //int degreesOfFreedomResidual = weightedDesign.Length - weightedDesign[0].Length;

            //double scale = weightedResiduals.DotProduct(weightedResiduals, 0, weightedResiduals.Length) / degreesOfFreedomResidual;

            //return (result, fitted, residuals, scale);
        }
    }
}