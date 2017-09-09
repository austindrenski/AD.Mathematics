using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public static class Indicators
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="indicators"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static double[][] Indicate(this double[][] source, params int[] indicators) 
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<int, Dictionary<double, int>> indicatorLookup =
                new Dictionary<int, Dictionary<double, int>>();

            Dictionary<int, int> forwardMap = new Dictionary<int, int>();

            for (int i = 0; i < source[0].Length; i++)
            {
                if (!indicators.Contains(i))
                {
                    forwardMap.Add(i, forwardMap.Count);
                }
            }

            int columnPointer = source[0].Length - indicators.Length;

            for (int i = 0; i < indicators.Length; i++)
            {
                int loopIndicator = indicators[i];
                int loopColumnPointer = columnPointer;

                Dictionary<double, int> indicatorMap =
                    source.Select((x, j) => x[loopIndicator])
                          .Distinct()
                          .Select((x, j) => (key: j, value: x))
                          .ToDictionary(x => x.value, x => x.key + loopColumnPointer);

                columnPointer += indicatorMap.Count;

                indicatorLookup.Add(indicators[i], indicatorMap);
            }

            double[][] result = new double[source.Length][];

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = new double[columnPointer];
                
                for (int j = 0; j < source[0].Length; j++)
                {
                    if (indicatorLookup.TryGetValue(j, out Dictionary<double, int> appendMap))
                    {
                        result[i][appendMap[source[i][j]]] = 1.0;
                    }
                    else
                    {
                        result[i][forwardMap[j]] = source[i][j];
                    }
                }
            }

            return result;
        }
    }
}