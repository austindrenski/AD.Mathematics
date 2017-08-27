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
    public static class StatisticalTesting
    {
        [NotNull]
        [ItemNotNull]
        private static IReadOnlyCollection<Observation> GravityCourseData { get; }

        static StatisticalTesting()
        {
            GravityCourseData =
                File.ReadLines("\\users\\adren\\desktop\\grav_data_course.csv")
                    .SplitDelimitedLine(',')
                    .Skip(1)
                    .Select(x => x.Select(y => y.Trim()).ToArray())
                    .Select(
                        x => new Observation
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
        /// Test that the <see cref="GeneralizedLinearModel{T}"/> with a <see cref="GaussianDistribution"/> replicates a known regression.
        /// </summary>
        [Fact]
        public static void Test0()
        {
            const int precision = 8;
            ArrayEqualityComparer comparer = new ArrayEqualityComparer(precision);

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
                GeneralizedLinearModel.OlsRegression(input, response);

            int n = input.Length;
            int k = input[0].Length + 1;
            int df = n - k;
            const double sse = 1039248.23542009;
            double mse = sse / df;

            double[] coefficients = new double[] { 13.420712, -1.241669, 1.553772, -0.3691012, 2.8724586 };

            double[] varianceOLS = new double[] { 0.01178342, 0.00015513, 0.00564040, 0.00119503, 0.00521226 };
            double[] varianceHC0 = new double[] { 0.01091862, 0.00014569, 0.00437145, 0.00120774, 0.00297996 };
            double[] varianceHC1 = new double[] { 0.01091922, 0.00014570, 0.00437169, 0.00120781, 0.00298013 };

            double[] standardErrorsOLS = varianceOLS.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC0 = varianceHC0.Select(Math.Sqrt).ToArray();
            double[] standardErrorsHC1 = varianceHC1.Select(Math.Sqrt).ToArray();
            
            Assert.Equal(n, generalized.ObservationCount);
            Assert.Equal(k, generalized.VariableCount);
            Assert.Equal(df, generalized.DegreesOfFreedom);

            Assert.Equal(mse, generalized.MeanSquaredError, precision);
            Assert.Equal(sse, generalized.SumSquaredErrors, precision);

            Assert.Equal(coefficients, generalized.Coefficients, comparer);

            Assert.Equal(varianceOLS, generalized.VarianceOLS, comparer);
            Assert.Equal(varianceHC0, generalized.VarianceHC0, comparer);
            Assert.Equal(varianceHC1, generalized.VarianceHC1, comparer);

            Assert.Equal(standardErrorsOLS, generalized.StandardErrorsOLS, comparer);
            Assert.Equal(standardErrorsHC0, generalized.StandardErrorsHC0, comparer);
            Assert.Equal(standardErrorsHC1, generalized.StandardErrorsHC1, comparer);
        }

        [Fact]
        public static void Test1()
        {
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

            GeneralizedLinearModel<int> generalized =
                new GeneralizedLinearModel<int>(input, response, weights, new PoissonDistribution(), 1.0);

            Assert.Equal(99981, generalized.ObservationCount);
            Assert.Equal(5, generalized.VariableCount);
            Assert.Equal(99976, generalized.DegreesOfFreedom);

            Assert.Equal(
                new double[] { 14.3401, -0.7727, 0.1800, -0.8762, -0.0784 }, 
                generalized.Coefficients.Select(x => Math.Round(x, 4)));

            //Assert.Equal(, generalized.MeanSquaredError);
            //Assert.Equal(, generalized.SumSquaredErrors);

            //Assert.Equal(, generalized.StandardErrors);
            //Assert.Equal(, generalized.StandardErrorsHc0);
            //Assert.Equal(, generalized.StandardErrorsHc1);

            //Assert.Equal(, generalized.Variance);
            //Assert.Equal(, generalized.VarianceHc0);
            //Assert.Equal(, generalized.VarianceHc1);
        }
    }
}