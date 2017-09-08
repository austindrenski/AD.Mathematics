using System;
using System.Threading.Tasks;
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
            
            double[] weightedResponse = new double[response.Length];
            Array.Copy(response, weightedResponse, weightedResponse.Length);

            double sumOfWeights = 0.0;

            for (int i = 0; i < weights.Length; i++)
            {
                sumOfWeights += weights[i];
            }
            
            for (int i = 0; i < weightedDesign.Length; i++)
            {
                double scaledWeight = Math.Sqrt(weights[i] / sumOfWeights);

                weightedResponse[i] *= scaledWeight;
                
                for (int j = 0; j < weightedDesign[i].Length; j++)
                {
                    weightedDesign[i][j] *= scaledWeight;
                }
            }

            return weightedDesign.RegressOls(weightedResponse);
        }

        /// <summary>
        /// </summary>
        /// <param name="design"></param>
        /// <param name="response"></param>
        /// <param name="weights"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArrayConformabilityException{T}"></exception>
        [Pure]
        public static double[] RegressWls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] double[] weights, [NotNull] ParallelOptions options)
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
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (design.Length != response.Length)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            double[][] weightedDesign = design.CloneArray();
            
            double[] weightedResponse = new double[response.Length];
            Array.Copy(response, weightedResponse, weightedResponse.Length);

            double sumOfWeights = 0.0;

            for (int i = 0; i < weights.Length; i++)
            {
                sumOfWeights += weights[i];
            }

            Parallel.For(0, weightedDesign.Length, options, i =>
            {
                double scaledWeight = Math.Sqrt(weights[i] / sumOfWeights);

                weightedResponse[i] *= scaledWeight;

                for (int j = 0; j < weightedDesign[i].Length; j++)
                {
                    weightedDesign[i][j] *= scaledWeight;
                }
            });
            
            return weightedDesign.RegressOls(weightedResponse, options);
        }
    }
}