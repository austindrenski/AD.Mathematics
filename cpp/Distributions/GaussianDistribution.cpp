#include <limits>
#include <random>
#include "GaussianDistribution.h"
#include "IdentityLinkFunction.h"

namespace Distributions {
    GaussianDistribution::GaussianDistribution(const double mean, const double standardDeviation, std::unique_ptr<ILinkFunction> link)
            : _entropy(0.5 * (1.0 + log(2.0 * M_PI * standardDeviation * standardDeviation))),
              _kurtosis(0),
              _maximum(std::numeric_limits<double>::max()),
              _mean(mean),
              _median(mean),
              _minimum(std::numeric_limits<double>::min()),
              _mode(mean),
              _skewness(0),
              _standardDeviation(standardDeviation),
              _variance(standardDeviation * standardDeviation)
    {
        _link = link == nullptr ? std::make_unique<LinkFunctions::IdentityLinkFunction>() : std::move(link);
    }

    const double GaussianDistribution::Deviance(const std::vector<double> &response, const std::vector<double> &meanResponse, const std::vector<double> &weights, const double scale) const
    {
        if (response.size() != meanResponse.size() || response.size() != weights.size()) {
            throw std::out_of_range("Argument vectors differ in length.");
        }

        double result = 0.0;

        for (auto r = response.begin(), m = meanResponse.begin(); r < response.end(); r++, m++) {
            const double error = *r - *m;
            result += *r * error * error;
        }

        return result / scale;
    }

    const std::vector<double> GaussianDistribution::Fit(const std::vector<double> &linearPrediction) const
    {
        return _link->Inverse(linearPrediction);
    }

    const std::vector<double> GaussianDistribution::InitialMean(const std::vector<double> &response) const
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

    const double GaussianDistribution::LogProbability(const double x) const
    {
        return log(Probability(x));
    }

    const std::vector<double> GaussianDistribution::Predict(const std::vector<double> &meanResponse) const
    {
        return _link->Evaluate(meanResponse);
    }

    const double GaussianDistribution::Probability(const double x) const
    {
        return 1.0 / sqrt(M_PI * 2.0) * exp(-0.5 * x * x);
    }

    const std::vector<double> GaussianDistribution::Weight(const std::vector<double> &meanResponse) const
    {
        std::vector<double> derivative = _link->FirstDerivative(meanResponse);

        std::vector<double> weight(meanResponse.size());

        for (auto w = weight.begin(), d = derivative.begin(); w < weight.end(); w++, d++) {
            *w = 1.0 / (*d * *d * Variance());
        }

        return weight;
    }
}