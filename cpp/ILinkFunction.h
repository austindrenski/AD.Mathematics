#pragma once

#include <vector>

class ILinkFunction {
public:
    virtual const std::vector<double> Evaluate(const std::vector<double> &x) const = 0;

    virtual const std::vector<double> Inverse(const std::vector<double> &x) const = 0;

    virtual const std::vector<double> FirstDerivative(const std::vector<double> &x) const = 0;

    virtual const std::vector<double> SecondDerivative(const std::vector<double> &x) const = 0;

    virtual const double LogLikelihood(const std::vector<double> &response, const std::vector<double> &fitted, const std::vector<double> &weights, double scale) const = 0;
};