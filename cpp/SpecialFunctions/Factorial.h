#pragma once

#include <stdexcept>
#include <vector>

namespace SpecialFunctions {
    /// <summary>
    /// Represents a static cache of factorial values that are lazily constructed.
    /// </summary>
    class Factorial {
    public:
        /// <summary>
        /// Constructs the value cache.
        /// </summary>
        Factorial()
        {
            _cacheFactorial = {1.0};
            _cacheLogFactorial = {0.0};
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
        double Get(const unsigned int x)
        {
            if (x < 0 || x > 170) {
                throw std::out_of_range("Argument range: [0, 170].");
            }

            if (x < _cacheFactorial.size()) {
                return x == 0 ? 0.0 : _cacheFactorial[x];
            }

            const unsigned long start = _cacheFactorial.size();

            for (auto i = start; i < start + x; i++) {
                _cacheFactorial.push_back(_cacheFactorial[i - 1] * i);
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
        double GetLog(const unsigned int x)
        {
            if (x <= 0 || x > 170) {
                throw std::out_of_range("Argument range: (0, 170].");
            }

            if (x < _cacheLogFactorial.size()) {
                return x == 0 ? 0.0 : _cacheLogFactorial[x];
            }

            const unsigned long start = _cacheLogFactorial.size();

            for (unsigned long i = start; i < start + x; i++) {
                _cacheLogFactorial.push_back(log(Get(x)));
            }

            return _cacheLogFactorial[x];
        }

    private:
        /// <summary>
        /// The cache of factorial values.
        /// </summary>
        std::vector<double> _cacheFactorial;

        /// <summary>
        /// The cache of log factorial values;
        /// </summary>
        std::vector<double> _cacheLogFactorial;

    };

    /// <summary>
    /// Static copy of the factorial class.
    /// </summary>
    Factorial StaticFactorial;
}