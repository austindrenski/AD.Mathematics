#include "Factorial.h"

namespace SpecialFunctions {

    const std::array<double, Factorial::Limit> Factorial::_cacheFactorial = FactorialTemplate<Factorial::Limit>::create_values();

    const std::array<double, Factorial::Limit> Factorial::_cacheLogFactorial = FactorialTemplate<Factorial::Limit>::create_logs();
}