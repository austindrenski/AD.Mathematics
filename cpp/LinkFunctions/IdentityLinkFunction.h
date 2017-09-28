#pragma once

#include <vector>
#include <algorithm>
#include "ILinkFunction.h"

namespace LinkFunctions {
    class IdentityLinkFunction : public ILinkFunction {
    public:

        const std::vector<double> Evaluate(const std::vector<double> &x) const override
        {
            return std::vector<double>(x);
        }

        const std::vector<double> Inverse(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());
            std::transform(x.begin(), x.end(), result.begin(), [](double y) -> double { return 1.0 / y; });
            return result;
        }

        const std::vector<double> FirstDerivative(const std::vector<double> &x) const override
        {
            return std::vector<double>(x.size(), 1.0);
        }

        const std::vector<double> SecondDerivative(const std::vector<double> &x) const override
        {
            return std::vector<double>(x.size(), 0.0);
        }

        const double LogLikelihood(const std::vector<double> &response, const std::vector<double> &fitted, const std::vector<double> &weights, double scale) const override
        {
            double sumSquaredErrors = 0.0;

            for (int i = 0; i < response.size(); i++) {
                double error = response[i] - fitted[i];

                sumSquaredErrors += error * error;
            }

            double halfObs = 0.5 * response.size();

            return -halfObs * (log(sumSquaredErrors) + (1.0 + log(M_PI / halfObs)));
        }
    };
}