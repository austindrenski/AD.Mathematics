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

            for (int i = 0; i < indicators.Length; i++)
            {
                SortedSet<double> set = new SortedSet<double>();
                
                Dictionary<double, int> indicatorMap = new Dictionary<double, int>();
                
                for (int j = 0; j < source.Length; j++)
                {
                    double value = source[j][indicators[i]];
                    
                    if (set.Add(value))
                    {
                        indicatorMap.Add(value, i);
                    }
                }
                
                indicatorLookup.Add(indicators[i], indicatorMap);
            }

            double[][] result = new double[source.Length][];

            int sum = indicatorLookup.Sum(x => x.Value.Count);

            int resultColumns = source.Length - indicators.Length + sum;
            
            for (int i = 0; i < source.Length; i++)
            {
                result[i] = new double[resultColumns];
                
                for (int j = 0; j < source[0].Length; j++)
                {
                    if (!indicators.Contains(j))
                    {
                        result[i][j] = source[i][j];
                    }
                    else
                    {
                        result[i][indicatorLookup[j][source[i][j]]] = 1.0;
                    }
                }
            }

            return result;
        }
    }
}