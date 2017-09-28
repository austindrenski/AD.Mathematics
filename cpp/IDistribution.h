#pragma once

#include <vector>

class IDistribution {
public:

    virtual const double Entropy() const = 0;

    virtual const double Maximum() const = 0;

    virtual const double Mean() const = 0;

    virtual const double Median() const = 0;

    virtual const double Minimum() const = 0;

    virtual const double Mode() const = 0;

    virtual const double Skewness() const = 0;

    virtual const double Kurtosis() const = 0;

    virtual const double StandardDeviation() const = 0;

    virtual const double Variance() const = 0;

    virtual const double Probability(double x) const = 0;

    virtual const double LogProbability(double x) const = 0;

    virtual const double Deviance(const std::vector<double> &response, const std::vector<double> &meanResponse, const std::vector<double> &weights, double scale) const = 0;

    virtual const std::vector<double> InitialMean(const std::vector<double> &response) const = 0;

    virtual const std::vector<double> Weight(const std::vector<double> &meanResponse) const = 0;

    virtual const std::vector<double> Fit(const std::vector<double> &linearPrediction) const = 0;

    virtual const std::vector<double> Predict(const std::vector<double> &meanResponse) const = 0;
};