#pragma once

#include <vector>
#include <algorithm>
#include "ILinkFunction.h"

namespace LinkFunctions {
    class LogLinkFunction : public ILinkFunction {
    public:

        explicit LogLinkFunction(const double slope = 1.0, const double intercept = 0.0)
                : _slope(slope),
                  _intercept(intercept)
        {
        }

        const double Slope() const
        { return _slope; }

        const double Intercept() const
        { return _intercept; }

        const std::vector<double> Evaluate(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());
            std::transform(x.begin(), x.end(), result.begin(), [](double y) -> double { return log(y); });
            return result;
        }

        const std::vector<double> Inverse(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());
            std::transform(x.begin(), x.end(), result.begin(), [](double y) -> double { return exp(y); });
            return result;
        }

        const std::vector<double> FirstDerivative(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());
            std::transform(x.begin(), x.end(), result.begin(), [](double y) -> double { return 1.0 / y; });
            return result;
        }

        const std::vector<double> SecondDerivative(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());
            std::transform(x.begin(), x.end(), result.begin(), [](double y) -> double { return -1.0 / (y * y); });
            return result;
        }

        const double LogLikelihood(const std::vector<double> &response, const std::vector<double> &fitted, const std::vector<double> &weights, double scale) const override
        {
            double result = 0.0;

            double common = log(M_PI * 2.0 * scale);

            for (int i = 0; i < response.size(); i++) {
                double error = response[i] - fitted[i];

                result += -0.5 * weights[i] * (error * error / scale + common);
            }

            return result;
        }

    private:

        const double _slope;

        const double _intercept;
    };
}

