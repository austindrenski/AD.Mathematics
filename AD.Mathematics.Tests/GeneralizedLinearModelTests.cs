using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AD.IO;
using AD.Mathematics.Distributions;
using AD.Mathematics.RegressionModels;
using JetBrains.Annotations;
using Xunit;

namespace AD.Mathematics.Tests
{
    [PublicAPI]
    public static class GeneralizedLinearModelTests
    {
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyCollection<GravityCourseObservation> GravityCourseData { get; }
        
        static GeneralizedLinearModelTests()
        {
            GravityCourseData =
                File.ReadLines("\\users\\austin.drenski\\desktop\\grav_data_course.csv")
                    .SplitDelimitedLine(',')
                    .Skip(1)
                    .Select(x => x.Select(y => y.Trim()).ToArray())
                    .Select(
                        x => new GravityCourseObservation
                        {
                            Importer = x[0],
                            Exporter = x[1],
                            Year = x[2],
                            Id = int.Parse(x[3]),
                            Distance = double.Parse(x[4]),
                            Trade = double.Parse(x[5]),
                            CommonBorder = int.Parse(x[6]),
                            CommonLanguage = int.Parse(x[7]),
                            ColonialRelationship = int.Parse(x[8])
                        })
                    .ToArray();
        }

        /// <summary>
        /// Test if the <see cref="GeneralizedLinearModel{T}"/> with a <see cref="GaussianDistribution"/> replicates a known regression.
        /// </summary>
        [Fact(DisplayName = "GLM test: Gaussian")]
        public static void GlmGaussian0()
        {
            UnitTestEqualityComparer comparer = new UnitTestEqualityComparer(8);

            double[][] input =
                GravityCourseData.Where(x => x.Trade > 0)
                                 .Select(
                                     x => new double[]
                                     {
                                         Math.Log(x.Distance),
                                         x.CommonBorder,
                                         x.CommonLanguage,
                                         x.ColonialRelationship
                                     })
                                 .ToArray();

            double[] response =
                GravityCourseData.Where(x => x.Trade > 0)
                                 .Select(x => Math.Log(x.Trade))
                                 .ToArray();

            GeneralizedLinearModel<double> generalized =
                GeneralizedLinearModel.OrdinaryLeastSquares(input, response);

            int n = input.Length;
            int k = input[0].Length + 1;
            int df = n - k;
            const double sse = 1039248.23542009;
            double mse = sse / df;

            double[] coefficients = new double[] { 13.420712, -1.241669, 1.553772, -0.3691012, 2.8724586 };

            double[] varianceOls = new double[] { 0.01178342, 0.00015513, 0.00564040, 0.00119503, 0.00521226 };
            double[] varianceHC0 = new double[] { 0.01091862, 0.00014569, 0.00437145, 0.00120774, 0.00297996 };
            double[] varianceHC1 = new double[] { 0.01091922, 0.00014570, 0.00437169, 0.00120781, 0.00298013 };

            double[] standardErrorsOls = varianceOls.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC0 = varianceHC0.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC1 = varianceHC1.Select(Math.Sqrt).ToArray();
            
            Assert.Equal(n, generalized.ObservationCount);
            Assert.Equal(k, generalized.VariableCount);
            Assert.Equal(df, generalized.DegreesOfFreedom);

            Assert.Equal(mse, generalized.MeanSquaredError, comparer.Precision);
            Assert.Equal(sse, generalized.SumSquaredErrors, comparer.Precision);

            Assert.Equal(coefficients, generalized.Coefficients, comparer);

            Assert.Equal(varianceOls, generalized.VarianceOls, comparer);
            Assert.Equal(varianceHC0, generalized.VarianceHC0, comparer);
            Assert.Equal(varianceHC1, generalized.VarianceHC1, comparer);

            Assert.Equal(standardErrorsOls, generalized.StandardErrorsOls, comparer);
            Assert.Equal(standardErrorsHC0, generalized.StandardErrorsHC0, comparer);
            Assert.Equal(standardErrorsHC1, generalized.StandardErrorsHC1, comparer);
        }

        /// <summary>
        /// Test if the <see cref="GeneralizedLinearModel{T}"/> with a <see cref="PoissonDistribution"/> replicates a known regression.
        /// </summary>
        [Fact(DisplayName = "GLM test: Poisson")]
        public static void GlmPoisson0()
        {
            UnitTestEqualityComparer comparer = new UnitTestEqualityComparer(8);

            double[][] input  =
                GravityCourseData.Select(
                                     x => new double[]
                                     {
                                         Math.Log(x.Distance),
                                         x.CommonBorder,
                                         x.CommonLanguage,
                                         x.ColonialRelationship
                                     })
                                 .ToArray();

            double[] response =
                GravityCourseData.Select(x => x.Trade)
                                 .ToArray();

            double[] weights =
                Enumerable.Repeat(1.0, response.Length)
                          .ToArray();

            GeneralizedLinearModel<double> generalized = 
                GeneralizedLinearModel.PoissonRegression(input, response, weights);
            
            int n = input.Length;
            int k = input[0].Length + 1;
            int df = n - k;

            double[] coefficients = new double[] { 14.34011355,  -0.77265135,   0.18003745,  -0.87616156,  -0.07842665 };
            
            double[] varianceOls = new double[] { 2.47622006e-08, 4.80378670e-10, 5.14379692e-08, 5.98550930e-08, 1.97414603e-07 };
            double[] varianceHC0 = new double[] { 0.01842182,  0.00027302,  0.0054894 ,  0.00458959,  0.00451205 };
            double[] varianceHC1 = new double[] { 0.01842182,  0.00027302,  0.0054894 ,  0.00458959,  0.00451205 };

            double[] standardErrorsOls = varianceOls.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC0 = varianceHC0.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC1 = varianceHC1.Select(Math.Sqrt).ToArray();

            Assert.Equal(n, generalized.ObservationCount);
            Assert.Equal(k, generalized.VariableCount);
            Assert.Equal(df, generalized.DegreesOfFreedom);

            Assert.Equal(coefficients, generalized.Coefficients, comparer);

            Assert.Equal(varianceOls, generalized.VarianceOls, comparer);
            Assert.Equal(varianceHC0, generalized.VarianceHC0, comparer);
            Assert.Equal(varianceHC1, generalized.VarianceHC1, comparer);

            Assert.Equal(standardErrorsOls, generalized.StandardErrorsOls, comparer);
            Assert.Equal(standardErrorsHC0, generalized.StandardErrorsHC0, comparer);
            Assert.Equal(standardErrorsHC1, generalized.StandardErrorsHC1, comparer);
        }
        
        /// <summary>
        /// Test if the <see cref="GeneralizedLinearModel{T}"/> with a <see cref="PoissonDistribution"/> replicates a known regression.
        /// </summary>
        [Fact(DisplayName = "GLM test: Poisson with fixed effects")]
        public static void GlmPoisson1()
        {
            UnitTestEqualityComparer comparer = new UnitTestEqualityComparer(8);

            double[][] groupMeans =
                GravityCourseData.GroupBy(
                                     x => new
                                     {
                                         x.Importer,
                                         x.Exporter,
                                         x.Year
                                     })
                                 .Select(
                                     x => new
                                     {
                                         x,
                                         dist = x.Average(y => y.Distance),
                                         trade = x.Average(y => y.Trade)
                                     })
                                 .Select(
                                     x =>
                                         x.x.SelectMany(
                                              y => new double[]
                                              {
                                                  x.dist,
                                                  x.trade
                                              })
                                          .ToArray())
                                 .ToArray();

            double[] grandMeans =
                new double[]
                {
                    groupMeans.Average(x => x[0]),
                    groupMeans.Average(x => x[1])
                };

            double[][] input =
                GravityCourseData.Select(
                                     (x, i) => new double[]
                                     {
                                         Math.Log(x.Distance) - Math.Log(groupMeans[i][0]) + Math.Log(grandMeans[0]),
                                         x.CommonBorder,
                                         x.CommonLanguage,
                                         x.ColonialRelationship
                                     })
                                 .ToArray();

            double[] response =
                GravityCourseData.Select(
                                     (x, i) => x.Trade - groupMeans[i][1] + grandMeans[1])
                                 .ToArray();

            double[] weights =
                Enumerable.Repeat(1.0, response.Length)
                          .ToArray();
            
            GeneralizedLinearModel<double> generalized = 
                GeneralizedLinearModel.PoissonRegression(input, response, weights, new System.Threading.Tasks.ParallelOptions());
            
            int n = input.Length;
            int k = input[0].Length + 1;
            int df = n - k;

            double[] coefficients = new double[] { 13.728590, -0.772651, 0.180037, -0.876162, -0.078427 };
//            
//            double[] varianceOls = new double[] { 4.110142e-07, 5.480129e-07, 1.331497e-06 };
//            double[] varianceHC0 = new double[] { 0.005335,  0.011331,  0.028898 };
//            double[] varianceHC1 = new double[] { 0.005335, 0.011331,  0.028898 };
//
//            double[] standardErrorsOls = varianceOls.Select(Math.Sqrt).ToArray();
//            double[] standardErrorsHC0 = varianceHC0.Select(Math.Sqrt).ToArray();
//            double[] standardErrorsHC1 = varianceHC1.Select(Math.Sqrt).ToArray();

            Assert.Equal(n, generalized.ObservationCount);
            Assert.Equal(k, generalized.VariableCount);
            Assert.Equal(df, generalized.DegreesOfFreedom);

            Assert.Equal(coefficients, generalized.Coefficients, comparer);
//
//            Assert.Equal(varianceOls, generalized.VarianceOls, comparer);
//            Assert.Equal(varianceHC0, generalized.VarianceHC0, comparer);
//            Assert.Equal(varianceHC1, generalized.VarianceHC1, comparer);
//
//            Assert.Equal(standardErrorsOls, generalized.StandardErrorsOls, comparer);
//            Assert.Equal(standardErrorsHC0, generalized.StandardErrorsHC0, comparer);
//            Assert.Equal(standardErrorsHC1, generalized.StandardErrorsHC1, comparer);
        }    
    }
}