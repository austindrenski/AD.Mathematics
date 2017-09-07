using System.Collections.Generic;
using System.IO;
using System.Linq;
using AD.IO;
using AD.Mathematics.Matrix;
using JetBrains.Annotations;
using Xunit;

namespace AD.Mathematics.Tests
{
    public static class RegressionTesting
    {      
        [NotNull]
        [ItemNotNull]
        private static IReadOnlyCollection<WeightedRegressionObservation> WeightedRegressionData { get; }

        static RegressionTesting()
        {
            WeightedRegressionData =
                File.ReadLines("\\users\\austin.drenski\\desktop\\wls_example_data.csv")
                    .SplitDelimitedLine(',')
                    .Skip(1)
                    .Select(x => x.Select(y => y.Trim()).ToArray())
                    .Select(
                        x => new WeightedRegressionObservation
                        {
                            Experience = double.Parse(x[0]),
                            Age = int.Parse(x[1]),
                            Income = double.Parse(x[2]),
                            OwnRent= int.Parse(x[3]) > 0,
                            SelfEmployed = int.Parse(x[4]) > 0
                        })
                    .ToArray();
        }

        /// <summary>
        /// Test if the <see cref="RegressionWls"/> replicates a known model.
        /// </summary>
        [Fact(DisplayName = "Least squares test: Weighted")]
        public static void RegressionWls0()
        {
            UnitTestEqualityComparer comparer = new UnitTestEqualityComparer(8);

            double[][] input =
                WeightedRegressionData.Select(
                                          x => new[]
                                          {
                                              x.Age,
                                              x.OwnRent ? 1 : 0,
                                              x.Income,
                                              x.IncomeSquared
                                          })
                                      .ToArray();

            double[] response = WeightedRegressionData.Select(x => x.Experience).ToArray();

            double[] weights = WeightedRegressionData.Select(x => x.Income).ToArray();

            double[] results = input.Prepend(1).RegressWls(response, weights);

            double[] coefficients = new double[] { -260.72086, -3.5707488, -3.8085199, 254.82168, -16.405243 };
            
            Assert.Equal(coefficients, results, comparer);
        }
    }
}