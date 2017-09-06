using System;
using System.Collections.Generic;

namespace AD.Mathematics.Tests
{
    public class UnitTestEqualityComparer : IEqualityComparer<double>
    {
        public int Precision { get; }

        private readonly double _tolerance;

        public UnitTestEqualityComparer(int precision)
        {
            Precision = precision;
            _tolerance = Math.Pow(1, -precision);
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">
        /// The first object of type T to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type T to compare.
        /// </param>
        /// <returns>
        /// True if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) < _tolerance;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="object"/> for which a hash code is to be returned.
        /// </param>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        public int GetHashCode(double obj)
        {
            return _tolerance.GetHashCode();
        }
    }
}