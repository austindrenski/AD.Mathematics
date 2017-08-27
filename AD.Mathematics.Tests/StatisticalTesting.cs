using System;
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
        [PublicAPI]
        public class Observation
        {
            public string Importer { get; set; }

            public string Exporter { get; set; }

            public int Id { get; set; }

            public string Year { get; set; }

            public double Distance { get; set; }

            public double Trade { get; set; }

            public int CommonBorder { get; set; }

            public int CommonLanguage { get; set; }

            public int ColonialRelationship { get; set; }
        }

        private static Observation[] GravityCourseData { get; }

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
        /// Test that the <see cref="GeneralizedLinearRegressionModel{T}"/> with a <see cref="GaussianDistribution"/> replicates the <see cref="MultipleLinearRegressionModel"/>.
        /// </summary>
        [Fact]
        public static void Test0()
        {
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
            
            double[] weights =
                Enumerable.Repeat(1.0, response.Length)
                          .ToArray();

            MultipleLinearRegressionModel standard =
                new MultipleLinearRegressionModel(input, response, weights, 1.0);

            GeneralizedLinearRegressionModel<double> generalized = 
                new GeneralizedLinearRegressionModel<double>(input, response, weights, new GaussianDistribution(), 1.0);

            Assert.Equal(standard.ObservationCount, generalized.ObservationCount);
            Assert.Equal(standard.VariableCount, generalized.VariableCount);
            Assert.Equal(standard.DegreesOfFreedom, generalized.DegreesOfFreedom);

            Assert.Equal(standard.Coefficients, generalized.Coefficients);

            Assert.Equal(standard.MeanSquaredError, generalized.MeanSquaredError);
            Assert.Equal(standard.SumSquaredErrors, generalized.SumSquaredErrors);

            Assert.Equal(standard.StandardErrors, generalized.StandardErrors);
            Assert.Equal(standard.StandardErrorsHc0, generalized.StandardErrorsHc0);
            Assert.Equal(standard.StandardErrorsHc1, generalized.StandardErrorsHc1);

            Assert.Equal(standard.Variance, generalized.Variance);
            Assert.Equal(standard.VarianceHc0, generalized.VarianceHc0);
            Assert.Equal(standard.VarianceHc1, generalized.VarianceHc1);
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

            GeneralizedLinearRegressionModel<int> generalized =
                new GeneralizedLinearRegressionModel<int>(input, response, weights, new PoissonDistribution(), 1.0);

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