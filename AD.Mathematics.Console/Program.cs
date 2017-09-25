using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AD.Mathematics.Distributions;
using AD.Mathematics.LinkFunctions;
using AD.Mathematics.RegressionModels;
using AD.Mathematics.Tests;

namespace AD.Mathematics.Console
{
    public static class Program
    {
        public static void Main()
        {
            double[][] input =
                GeneralizedLinearModelTests.GravityCourseData
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
                GeneralizedLinearModelTests.GravityCourseData
                                           .Select(x => x.Trade)
                                           .ToArray();

            double[] weights =
                Enumerable.Repeat(1.0, response.Length)
                          .ToArray();

            GeneralizedLinearModel<double> generalized = 
                GeneralizedLinearModel.PoissonRegression(input, response, weights);

            const int count = 10;
            
            IDistribution<double> distribution = new PoissonDistribution(new LogLinkFunction());
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                generalized = new GeneralizedLinearModel<double>(input, response, weights, distribution, true);
            }
            sw.Stop();
            System.Console.WriteLine(generalized);
            System.Console.WriteLine((double)sw.ElapsedMilliseconds / count);
             
            ParallelOptions options = new ParallelOptions();
            sw.Restart();
            for (int i = 0; i < count; i++)
            {
                generalized = new GeneralizedLinearModel<double>(input, response, weights, distribution, true, options);
            }
            sw.Stop();
            System.Console.WriteLine(generalized);
            System.Console.WriteLine((double)sw.ElapsedMilliseconds / count);
        }
    }
}