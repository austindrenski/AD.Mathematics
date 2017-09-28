#include <cmath>
#include <limits>
#include <random>
#include <utility>
#include "PoissonDistribution.h"
#include "LogLinkFunction.h"
#include "Factorial.h"

namespace Distributions {
    PoissonDistribution::PoissonDistribution(const double mean, std::unique_ptr<ILinkFunction> link)
            : _entropy(0.5 * log(2 * M_PI * M_E * mean)
                       - 1.0 / (12.0 * mean)
                       - 1.0 / (24.0 * mean * mean)
                       - 19.0 / (360.0 * mean * mean * mean)),
              _kurtosis(1.0 / mean),
              _maximum(std::numeric_limits<double>::max()),
              _mean(mean),
              _median(floor(_mean + 1.0 / 3.0 - 0.02 / mean)),
              _minimum(0),
              _mode(floor(mean)),
              _skewness(1.0 / sqrt(mean)),
              _standardDeviation(sqrt(mean)),
              _variance(mean)
    {
        _link = link == nullptr ? std::make_unique<LinkFunctions::LogLinkFunction>() : std::move(link);
    }

    const double PoissonDistribution::Deviance(const std::vector<double> &response, const std::vector<double> &meanResponse, const std::vector<double> &weights, double scale) const
    {
        if (response.size() != meanResponse.size() || response.size() != weights.size()) {
            throw std::out_of_range("Argument vectors differ in length.");
        }

        double result = 0.0;

        for (auto w = weights.begin(), r = response.begin(), m = meanResponse.begin(); w < weights.end(); w++, r++, m++) {
            const double d = log(*r <= 0 ? std::numeric_limits<double>::epsilon() : *r / *m);

            result += *w * (*r * d - *r - *m);
        }

        return 2.0 * result / scale;
    }

    const std::vector<double> PoissonDistribution::Fit(const std::vector<double> &linearPrediction) const
    {
        return _link->Inverse(linearPrediction);
    }

    const std::vector<double> PoissonDistribution::InitialMean(const std::vector<double> &response) const
    {
        if (response.empty()) {
            throw std::out_of_range("Argument vector is empty.");
        }

        double mean = std::accumulate(response.begin(), response.end(), 0.0) / response.size();

        std::vector<double> initialMean(response.size());

        for (auto i = initialMean.begin(), r = static_cast<std::vector<double>>(response).begin(); i < initialMean.end(); i++, r++) {
            *i = 0.5 * (*r + mean);
        }

        return initialMean;
    }

    const double PoissonDistribution::LogProbability(double x) const
    {
        if (x < 0 || x > 170) {
            throw std::out_of_range("Argument range: [0, 170].");
        }

        return x * log(_mean) - SpecialFunctions::StaticFactorial.GetLog(static_cast<unsigned int>(x)) - _mean;
    }

    const std::vector<double> PoissonDistribution::Predict(const std::vector<double> &meanResponse) const
    {
        return _link->Evaluate(meanResponse);
    }

    const double PoissonDistribution::Probability(double x) const
    {
        if (x < 0.0 || x > 170.0) {
            throw std::out_of_range("Argument range: [0, 170].");
        }

        return exp(LogProbability(x));
    }

    const std::vector<double> PoissonDistribution::Weight(const std::vector<double> &meanResponse) const
    {
        std::vector<double> weight(meanResponse.size());

        std::transform(meanResponse.begin(), meanResponse.end(), weight.begin(), [](double x) -> double { return std::abs(x); });

        std::vector<double> derivative = _link->FirstDerivative(weight);

        for (auto w = weight.begin(), d = derivative.begin(); w < weight.end(); w++, d++) {
            *w = 1.0 / (*d * *d * *w);
        }

        return weight;
    }
}