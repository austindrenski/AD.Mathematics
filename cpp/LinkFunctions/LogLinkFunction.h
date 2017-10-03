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

            std::transform(
                    x.begin(),
                    x.end(),
                    result.begin(),
                    log);

            return result;
        }

        const std::vector<double> Inverse(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());

            std::transform(
                    x.begin(),
                    x.end(),
                    result.begin(),
                    exp);

            return result;
        }

        const std::vector<double> FirstDerivative(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());

            std::transform(
                    x.begin(),
                    x.end(),
                    result.begin(),
                    [](double y) -> double { return 1.0 / y; });

            return result;
        }

        const std::vector<double> SecondDerivative(const std::vector<double> &x) const override
        {
            std::vector<double> result(x.size());

            std::transform(
                    x.begin(),
                    x.end(),
                    result.begin(),
                    [](double y) -> double { return -1.0 / pow(y, 2); });

            return result;
        }

        const double LogLikelihood(const std::vector<double> &response, const std::vector<double> &fitted, const std::vector<double> &weights, double scale) const override
        {
            if (response.size() != fitted.size() || response.size() != weights.size()) {
                throw std::out_of_range("Argument vectors differ in length.");
            }

            double result = 0.0;

            double common = log(M_PI * 2.0 * scale);

            for (auto r = response.begin(), f = fitted.begin(), w = weights.begin(); r != response.end(); ++r, ++f, ++w) {
                result += -0.5 * *w * (pow(*r - *f, 2) / scale + common);
            }

            return result;
        }

    private:

        const double _slope;

        const double _intercept;
    };
}

