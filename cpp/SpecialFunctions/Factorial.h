#pragma once

#include <array>
#include <stdexcept>
#include "FactorialTemplate.h"

namespace SpecialFunctions {

    /// <summary>
    /// Represents a static cache of factorial values that are template constructed.
    /// </summary>
    class Factorial {
    public:
        /// <summary>
        /// The recommended limit for factorial calculation.
        /// </summary>
        static constexpr int Limit = 170;

        /// <summary>
        /// Returns the factorial value for x in [0, 170]
        /// </summary>
        /// <param name="x">
        /// The number for which the factorial result is returned.
        /// </param>
        /// <returns>
        /// The factorial of <see cref="x"/>.
        /// </returns>
        template<typename T>
        static const double Get(const T x)
        {
            static_assert(std::is_arithmetic<T>::value, "Numeric type required.");

            if (x < 0 || x > Limit) {
                throw std::out_of_range("Argument range: [0, 170].");
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
        template<typename T>
        static const double GetLog(const T x)
        {
            static_assert(std::is_arithmetic<T>::value, "Numeric type required.");

            if (x <= 0 || x > Limit) {
                throw std::out_of_range("Argument range: (0, 170].");
            }

            return _cacheLogFactorial[x];
        }

    private:
        /// <summary>
        /// The cache of factorial values.
        /// </summary>
        static const std::array<double, Limit> _cacheFactorial;

        /// <summary>
        /// The cache of log factorial values;
        /// </summary>
        static const std::array<double, Limit> _cacheLogFactorial;
    };
}