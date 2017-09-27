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

            Dictionary<int, int> sourceMap = 
                new Dictionary<int, int>();

            for (int i = 0; i < source[0].Length; i++)
            {
                if (!indicators.Contains(i))
                {
                    sourceMap.Add(i, sourceMap.Count);
                }
            }

            int columnCounter = source[0].Length - indicators.Length;

            for (int i = 0; i < indicators.Length; i++)
            {
                int loopIndicator = indicators[i];
                int loopColumnOffset = columnCounter;

                Dictionary<double, int> indicatorMap =
                    source.Select(x => x[loopIndicator])
                          .Distinct()
                          .Select(
                              (x, j) => new
                              {
                                  categoryValue = x,
                                  column = j + loopColumnOffset
                              })
                          .ToDictionary(
                              x => x.categoryValue,
                              x => x.column);

                columnCounter += indicatorMap.Count;

                indicatorLookup.Add(loopIndicator, indicatorMap);
            }

            return ConstructResults(source, sourceMap, indicatorLookup);
        }

        private static double[][] ConstructResults([NotNull][ItemNotNull] double[][] source, [NotNull] Dictionary<int, int> sourceLookup, [NotNull] Dictionary<int, Dictionary<double, int>> indicatorLookup)
        {
            double[][] result = new double[source.Length][];
            
            int columns = sourceLookup.Count + indicatorLookup.Sum(x => x.Value.Count);

            for (int i = 0; i < source.Length; i++)
            {
                result[i] = new double[columns];
                
                for (int j = 0; j < source[0].Length; j++)
                {
                    if (indicatorLookup.TryGetValue(j, out Dictionary<double, int> indicatorColumnMap))
                    {
                        result[i][indicatorColumnMap[source[i][j]]] = 1.0;
                    }
                    else
                    {
                        result[i][sourceLookup[j]] = source[i][j];
                    }
                }
            }

            return result;
        }
    }
}