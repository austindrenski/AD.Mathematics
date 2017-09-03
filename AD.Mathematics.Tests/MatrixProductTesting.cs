using System.Diagnostics;
using System.Threading.Tasks;
using AD.Mathematics.Matrix;
using JetBrains.Annotations;
using Xunit;

namespace AD.Mathematics.Tests
{
    [PublicAPI]
    public class MatrixProductTesting
    {
        [Theory(Skip = "Use this for benchmarking -- not unit testing.")]
        [InlineData(2000)]
        public void TestMethod0(int size)
        {
            double[][] a = new double[size][];
            double[][] b = new double[size][];

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new double[size];
                b[i] = new double[size];
            }

            ParallelOptions o = new ParallelOptions();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // ReSharper disable once UnusedVariable
            double[][] c = a.MatrixProduct(b, o);

            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            Assert.True(true);
        }
    }
}
