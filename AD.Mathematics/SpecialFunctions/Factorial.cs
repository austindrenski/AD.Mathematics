using System;
using JetBrains.Annotations;

namespace AD.Mathematics.SpecialFunctions
{
    /// <summary>
    /// Represents a static cache of factorial values that are lazily constructed.
    /// </summary>
    [PublicAPI]
    public static class Factorial
    {
        /// <summary>
        /// The cache of factorial values.
        /// </summary>
        [NotNull]
        private static double[] _cacheFactorial;

        /// <summary>
        /// The cache of log factorial values;
        /// </summary>
        [NotNull]
        private static double[] _cacheLogFactorial;

        /// <summary>
        /// Constructs the value cache.
        /// </summary>
        static Factorial()
        {
            _cacheFactorial = new double[] { 1.0 };
            _cacheLogFactorial = new double[] { 0.0 };
        }

        /// <summary>
        /// Returns the factorial value for x in [0, 170]
        /// </summary>
        /// <param name="x">
        /// The number for which the factorial result is returned.
        /// </param>
        /// <returns>
        /// The factorial of <see cref="x"/>.
        /// </returns>
        [Pure]
        public static double Get(int x)
        {
            if (x < 0 || x > 170)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Argument range: [0, 170].");
            }

            if (x < _cacheFactorial.Length)
            {
                return x == 0 ? default : _cacheFactorial[x];
            }

            int start = _cacheFactorial.Length;

            Array.Resize(ref _cacheFactorial, x + 1);

            for (int i = start; i < _cacheFactorial.Length; i++)
            {
                _cacheFactorial[i] = _cacheFactorial[i - 1] * i;
            }

            return _cacheFactorial[x];
        }

        /// <summary>
        /// Returns the log of the factorial value for x in (0, 170]
        /// </summary>
        /// <param name="x">
        /// The number for which the factorial result is returned.
        /// </param>
        /// <returns>
        /// The log of the factorial of <see cref="x"/>.
        /// </returns>
        [Pure]
        public static double GetLog(int x)
        {
            if (x <= 0 || x > 170)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Argument range: (0, 170].");
            }

            if (x < _cacheLogFactorial.Length)
            {
                return x == 0 ? default : _cacheLogFactorial[x];
            }

            int start = _cacheLogFactorial.Length;

            Array.Resize(ref _cacheLogFactorial, x + 1);

            for (int i = start; i < _cacheLogFactorial.Length; i++)
            {
                _cacheLogFactorial[i] = Math.Log(Get(x));
            }

            return _cacheLogFactorial[x];
        }
    }
}