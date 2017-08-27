using System;
using System.Linq;
using AD.Mathematics;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods for Iteratively Reweighted Least Squares (IRLS) based on the Newton-Raphson algorithm.
    /// </summary>
    [PublicAPI]
    public static class SolverIrls
    {
        /// <summary>
        /// Solves an equation of the form A * x = b by Iteratively Reweighted Least Squares (IRLS) using the Newton-Raphson algorithm.
        /// </summary>
        /// <param name="design"></param>
        /// <param name="response"></param>
        /// <param name="function"></param>
        /// <param name="maxIterations"></param>
        /// <param name="expansions"></param>
        /// <param name="speedLimit"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [Pure]
        [NotNull]
        public static double[] SolveIrls([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, Func<double,double> function, int maxIterations = 100, int expansions = 100, double speedLimit = 1e+03, double tolerance = 1e-15)
        {
            // Use the Newton-Raphson technique to estimate logistic regression beta parameters
            // xMatrix is a design matrix of predictor variables where the first column is augmented with all 1.0 to represent dummy x values for the b0 constant
            // yVector is a column vector of binary (0.0 or 1.0) dependent variables
            // maxIterations is the maximum number of times to iterate in the algorithm. A value of 1000 is reasonable.
            // epsilon is a closeness parameter: if all new b[i] values after an iteration are within epsilon of
            // the old b[i] values, we assume the algorithm has converged and we return. A value like 0.001 is often reasonable.
            // jumpFactor stops the algorithm if any new beta value is jumpFactor times greater than the old value. A value of 1000.0 seems reasonable.
            // The return is a column vector of the beta estimates: b[0] is the constant, b[1] for x1, etc.
            // There is a lot that can go wrong here. The algorithm involves finding a matrx inverse (see MatrixInverse) which will throw
            // if the inverse cannot be computed. The Newton-Raphson algorithm can generate beta values that tend towards infinity. 
            // If anything bad happens the return is the best beta values known at the time (which could be all 0.0 values but not null).
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            if (design.Length != response.Length)
            {
                throw new ArrayConformabilityException<double>(design, response);
            }

            double[] beta = new double[design[0].Length];

            double[] bestBeta = new double[beta.Length];

            double[] probability = Enumerable.Repeat(1.0, response.Length).ToArray();//design.CrossProduct(beta).Select(function).ToArray(); // design.Probability(beta);

            double meanSquaredError = probability.SquaredError(response).Average();

            int worse = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                double[] newBeta = design.CalculateBeta(response, beta, probability);

                if (!beta.IsDifferent(newBeta, tolerance) || beta.SpeedCheck(newBeta, speedLimit))
                {
                    return bestBeta;
                }

                probability = design.CrossProduct(newBeta).Select(function).ToArray(); // design.Probability(newBeta);

                double newMeanSquaredError = probability.SquaredError(response).Average();

                Console.WriteLine(probability.Sum());
                Console.WriteLine(newMeanSquaredError);

                if (meanSquaredError < newMeanSquaredError)
                {
                    worse++;

                    if (worse > expansions)
                    {
                        return bestBeta;
                    }

                    Array.Copy(newBeta, beta, beta.Length);

                    for (int j = 0; j < beta.Length; j++)
                    {
                        beta[j] = 0.5 * (beta[j] + newBeta[j]);
                    }

                    meanSquaredError = newMeanSquaredError;

                    continue;
                }

                Array.Copy(newBeta, beta, beta.Length);
                Array.Copy(beta, bestBeta, bestBeta.Length);
                meanSquaredError = newMeanSquaredError;
                worse = 0;
            }

            return bestBeta;
        }

        /// <summary>
        /// Calculates a new beta array.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="response">
        /// The response array.
        /// </param>
        /// <param name="beta">
        /// The beta array.
        /// </param>
        /// <param name="probability">
        /// The probability array.
        /// </param>
        /// <returns>
        /// A new beta array.
        /// </returns>
        [Pure]
        [NotNull]
        private static double[] CalculateBeta([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] response, [NotNull] double[] beta, [NotNull] double[] probability)
        {
            // this is the heart of the Newton-Raphson technique
            // b[t] = b[t-1] + inv(X'W[t-1]X)X'(y - p[t-1])
            //
            // b[t] is the new (time t) b column vector
            // b[t-1] is the old (time t-1) vector
            // X' is the transpose of the X matrix of x data (1.0, age, sex, chol)
            // W[t-1] is the old weight matrix
            // y is the column vector of binary dependent variable data
            // p[t-1] is the old column probability vector (computed as 1.0 / (1.0 + exp(-z) where z = b0x0 + b1x1 + . . .)

            // note: W[t-1] is nxn which could be huge so instead of computing b[t] = b[t-1] + inv(X'W[t-1]X)X'(y - p[t-1])
            // compute b[t] = b[t-1] + inv(X'X~)X'(y - p[t-1]) where X~ is W[t-1]X computed directly
            // the idea is that the vast majority of W[t-1] cells are 0.0 and so can be ignored

            // Xᵀ
            double[][] xt = 
                design.Transpose();
            
            // W * X
            double[][] wx = 
                design.ComputeTildeX(probability);

            // Xᵀ * W * X
            double[][] xtwx =
                xt.CrossProduct(wx);

            // (Xᵀ * W * X)⁻¹
            double[][] invxtwx =
                xtwx.InvertLu();

            // (Xᵀ * W * X)⁻¹ * Xᵀ
            double[][] invxtwxxt =
                invxtwx.CrossProduct(xt);

            // Y - P
            double[] yp =
                response.Subtract(probability);

            // (Xᵀ * W * X)⁻¹ * Xᵀ * (Y - P)
            double[] b =
                invxtwxxt.CrossProduct(yp);

            // B + (Xᵀ * W * X)⁻¹ * Xᵀ * (Y - P)
            return beta.Add(b);

            //.SolveQr(designTranspose.CrossProduct(responseMinusProbability));

            //double[] b =
            //    designTranspose.CrossProduct(tildeX)
            //                   .InvertLu()
            //                   .CrossProduct(designTranspose)
            //                   .CrossProduct(responseMinusProbability);

        }

        [Pure]
        [NotNull]
        [ItemNotNull]
        private static double[][] ComputeTildeX([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] probability)
        {
            // note: W[t-1] is nxn which could be huge so instead of computing b[t] = b[t-1] + inv(X'W[t-1]X)X'(y - p[t-1]) directly
            // we compute the W[t-1]X part, without the use of W.

            // Since W is derived from the prob vector and W has non-0.0 elements only on the diagonal we can avoid a ton of work
            // by using the prob vector directly and not computing W at all.

            // Some of the research papers refer to the product W[t-1]X as X~ hence the name of this method.
            // ex: if xMatrix is 10x4 then W would be 10x10 so WX would be 10x4 -- the same size as X

            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (probability is null)
            {
                throw new ArgumentNullException(nameof(probability));
            }
            if (probability.Length != design.Length)
            {
                throw new ArrayConformabilityException<double>(probability, design);
            }

            double[][] result = new double[probability.Length][];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[design[0].Length];

                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = probability[i] * (1.0 - probability[i]) * design[i][j];
                }
            }

            return result;
        }

        [Pure]
        private static bool IsDifferent([NotNull] this double[] current, [NotNull] double[] next, double tolerance)
        {
            for (int i = 0; i < current.Length; ++i)
            {
                if (tolerance < Math.Abs(current[i] - next[i]))
                {
                    return true;
                }
            }

            return false;
        }

        [Pure]
        private static bool SpeedCheck([NotNull] this double[] current, [NotNull] double[] next, double speedLimit)
        {
            for (int i = 0; i < current.Length; i++)
            {
                if (!current[i].Equals(0.0) && speedLimit < Math.Abs(current[i] - next[i]) / Math.Abs(current[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}