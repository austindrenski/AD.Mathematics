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
        if (scale <= 0.0) {
            throw std::out_of_range("Scale must be greater than zero.");
        }

        double result = 0.0;

        for (auto r = response.begin(), m = meanResponse.begin(); r != response.end(); ++r, ++m) {
            result += *r * pow(*r - *m, 2);
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

        std::transform(
                response.begin(),
                response.end(),
                initialMean.begin(),
                [mean](double x) -> double { return 0.5 * (x + mean); });

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
        return 1.0 / sqrt(M_PI * 2.0) * exp(-0.5 * pow(x, 2));
    }

    const std::vector<double> GaussianDistribution::Weight(const std::vector<double> &meanResponse) const
    {
        std::vector<double> weight(meanResponse.size());

        std::vector<double> derivative = _link->FirstDerivative(meanResponse);

        std::transform(
                derivative.begin(),
                derivative.end(),
                weight.begin(),
                [inverseVariance = 1.0 / Variance()](double x) -> double { return inverseVariance * pow(x, 2); });

        return weight;
    }
}