#pragma

namespace SpecialFunctions {

    /// <summary>
    /// Template for a specific factorial with functions suitable for creating an array of factorial values at compile-time.
    /// </summary>
    template<std::size_t T>
    struct FactorialTemplate {
    public:
        /// <summary>
        /// The factorial value of this template.
        /// </summary>
        static constexpr double Value = T * FactorialTemplate<T - 1>::Value;

        /// <summary>
        /// Constructs an array of factorial values from this value to zero.
        /// </summary>
        static constexpr std::array<double, T> create_values() noexcept
        {
            return value_array(std::make_index_sequence<T> {});
        }

        /// <summary>
        /// Constructs an array of logged factorial values from this value to zero.
        /// </summary>
        static constexpr std::array<double, T> create_logs() noexcept
        {
            return log_array(std::make_index_sequence<T> {});
        }

    private:
        /// <summary>
        /// Constructs an array of factorial values from this value to zero.
        /// </summary>
        template<std::size_t...N>
        static constexpr std::array<double, sizeof...(N)> value_array(std::index_sequence<N...> sequence) noexcept
        {
            return std::array<double, sizeof...(N)> {FactorialTemplate<N>::Value ...};
        }

        /// <summary>
        /// Constructs an array of logged factorial values from this value to zero.
        /// </summary>
        template<std::size_t...N>
        static constexpr std::array<double, sizeof...(N)> log_array(std::index_sequence<N...> sequence) noexcept
        {
            return std::array<double, sizeof...(N)> {FactorialTemplate<N>::Value...};
        }
    };

    /// <summary>
    /// Template specialized for the base factorial.
    /// </summary>
    template<>
    struct FactorialTemplate<0> {
    public:
        /// <summary>
        /// The factorial value of this template.
        /// </summary>
        static constexpr double Value = 1.0;

        /// <summary>
        /// Constructs an array of factorial values from this value to zero.
        /// </summary>
        static constexpr std::array<double, 0> create_values() noexcept
        {
            return std::array<double, 0> {};
        }

        /// <summary>
        /// Constructs an array of logged factorial values from this value to zero.
        /// </summary>
        static constexpr std::array<double, 0> create_logs() noexcept
        {
            return std::array<double, 0> {};
        }
    };
}