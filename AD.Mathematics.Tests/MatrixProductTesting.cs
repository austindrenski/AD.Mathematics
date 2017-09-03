using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AD.Mathematics.Matrix;
using Xunit;

namespace AD.Mathematics.Tests
{
    public class MatrixProductTesting
    {
        [Theory]
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

            double[][] c = a.MatrixProduct(b, o);

            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            Assert.True(true);
        }
    }
}
