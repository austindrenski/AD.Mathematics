using System.Linq;
using AD.Mathematics.Matrix;
using JetBrains.Annotations;
using Xunit;

namespace AD.Mathematics.Tests
{
    [PublicAPI]
    public static class IndicatorTesting
    {
        [Fact]
        public static void TestMethod0()
        {
            double[][] data =
                new double[][]
                {
                    new double[] { 1.0, 2.0, 3.0, 1.0 },
                    new double[] { 1.0, 2.0, 3.0, 1.5 },
                    new double[] { 1.0, 2.0, 3.0, 2.0 },
                    new double[] { 1.0, 2.0, 3.0, 2.5 },
                    new double[] { 1.0, 2.0, 3.0, 3.0 },
                    new double[] { 1.0, 2.0, 3.0, 3.5 },
                    new double[] { 1.0, 2.0, 3.0, 1.0 }
                };

            double[][] test = data.Indicate(3);
            
            Assert.Equal(data.Length, test.Length);
            Assert.Equal(data[0].Length + 5, test[0].Length);
            Assert.Equal(data.Select(x => x[3]).Distinct().Count(), 1);
            Assert.Equal(data.Select(x => x[4]).Distinct().Count(), 1);
            Assert.Equal(data.Select(x => x[5]).Distinct().Count(), 1);
            Assert.Equal(data.Select(x => x[6]).Distinct().Count(), 1);
            Assert.Equal(data.Select(x => x[7]).Distinct().Count(), 1);
            Assert.Equal(data.Select(x => x[8]).Distinct().Count(), 1);
        }
        
    }
}