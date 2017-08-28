﻿using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods to perform weighted least squares regression.
    /// </summary>
    [PublicAPI]
    public static class RegressionWLS
    {
        [Pure]
        public static (double[] Results, double[] Fitted, double[] Residuals, double Scale) RegressWLS([NotNull] [ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] double[] weights)
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

            double[][] x = design;

            double[][] xt = x.Transpose();

            double[][] xtwtw = new double[xt.Length][];

            for (int i = 0; i < xt.Length; i++)
            {
                xtwtw[i] = new double[xt[i].Length];

                for (int j = 0; j < xt[i].Length; j++)
                {
                    xtwtw[i][j] = weights[j] * weights[j] * xt[i][j];
                }
            }

            double[][] xtwtwxinvxt = xtwtw.CrossProduct(x).InvertLu().CrossProduct(xt);

            double[][] xtwtwxinvxtwtw = new double[xtwtwxinvxt.Length][];

            for (int i = 0; i < xt.Length; i++)
            {
                xtwtwxinvxtwtw[i] = new double[xtwtwxinvxt[i].Length];

                for (int j = 0; j < xt[i].Length; j++)
                {
                    xtwtwxinvxtwtw[i][j] = weights[j] * weights[j] * xtwtwxinvxt[i][j];
                }
            }

            return (xtwtwxinvxtwtw.CrossProduct(response), null, null, 0);

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

            //double[] fitted = design.CrossProduct(result);
            //double[] weightedFitted = weightedDesign.CrossProduct(result);

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