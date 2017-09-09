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
                    new double[] { 1.0 },
                    new double[] { 1.5 },
                    new double[] { 2.0 },
                    new double[] { 2.5 },
                    new double[] { 3.0 },
                    new double[] { 3.5 },
                    new double[] { 1.0 }
                };

            double[][] test = data.Indicate(0);
            
            Assert.Equal(data.Length, test.Length);
            Assert.Equal(6, test[0].Length);
            Assert.Equal(2, test.Count(x => x[0] is 1.0));
            Assert.Equal(1, test.Count(x => x[1] is 1.0));
            Assert.Equal(1, test.Count(x => x[2] is 1.0));
            Assert.Equal(1, test.Count(x => x[3] is 1.0));
            Assert.Equal(1, test.Count(x => x[4] is 1.0));
            Assert.Equal(1, test.Count(x => x[5] is 1.0));
        }

        [Fact]
        public static void TestMethod1()
        {
            double[][] data =
                new double[][]
                {
                    new double[] { default, default, default, 0.0 },
                    new double[] { default, default, default, 1.0 },
                    new double[] { default, default, default, 2.0 },
                    new double[] { default, default, default, 3.0 },
                    new double[] { default, default, default, 4.0 },
                    new double[] { default, default, default, 5.0 },
                    new double[] { default, default, default, 0.0 }
                };

            double[][] test = data.Indicate(3);

            Assert.Equal(data.Length, test.Length);
            Assert.Equal(data[0].Length + 5, test[0].Length);
            Assert.Equal(2, test.Count(x => x[3] is 1.0));
            Assert.Equal(1, test.Count(x => x[4] is 1.0));
            Assert.Equal(1, test.Count(x => x[5] is 1.0));
            Assert.Equal(1, test.Count(x => x[6] is 1.0));
            Assert.Equal(1, test.Count(x => x[7] is 1.0));
            Assert.Equal(1, test.Count(x => x[8] is 1.0));
        }

        [Fact]
        public static void TestMethod2()
        {
            double[][] data =
                new double[][]
                {
                    new double[] { default, default, default, 0.0, 6.0 },
                    new double[] { default, default, default, 1.0, 5.0 },
                    new double[] { default, default, default, 2.0, 4.0 },
                    new double[] { default, default, default, 3.0, 3.0 },
                    new double[] { default, default, default, 4.0, 2.0 },
                    new double[] { default, default, default, 5.0, 1.0 },
                    new double[] { default, default, default, 0.0, 3.0 }
                };

            double[][] test = data.Indicate(3, 4);

            Assert.Equal(data.Length, test.Length);
            Assert.Equal(data[0].Length + 10, test[0].Length);

            Assert.Equal(2, test.Count(x => x[3] is 1.0));
            Assert.Equal(1, test.Count(x => x[4] is 1.0));
            Assert.Equal(1, test.Count(x => x[5] is 1.0));
            Assert.Equal(1, test.Count(x => x[6] is 1.0));
            Assert.Equal(1, test.Count(x => x[7] is 1.0));
            Assert.Equal(1, test.Count(x => x[8] is 1.0));

            Assert.Equal(1, test.Count(x => x[9] is 1.0));
            Assert.Equal(1, test.Count(x => x[10] is 1.0));
            Assert.Equal(1, test.Count(x => x[11] is 1.0));
            Assert.Equal(2, test.Count(x => x[12] is 1.0));
            Assert.Equal(1, test.Count(x => x[13] is 1.0));
            Assert.Equal(1, test.Count(x => x[14] is 1.0));
        }
    }
}