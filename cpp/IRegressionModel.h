#pragma once

#include <vector>

class IRegressionModel {
public:

    virtual const unsigned long ObservationCount() const = 0;

    virtual const unsigned long VariableCount() const = 0;

    virtual const long DegreesOfFreedom() const = 0;

    virtual const std::vector<double> Coefficients() const = 0;

    virtual const double SumSquaredErrors() const = 0;

    virtual const double MeanSquaredError() const = 0;

    virtual const double RootMeanSquaredError() const = 0;

    virtual const std::vector<double> StandardErrorsOls() const = 0;

    virtual const std::vector<double> StandardErrorsHC0() const = 0;

    virtual const std::vector<double> StandardErrorsHC1() const = 0;

    virtual const std::vector<double> VarianceOls() const = 0;

    virtual const std::vector<double> VarianceHC0() const = 0;

    virtual const std::vector<double> VarianceHC1() const = 0;

    virtual const double Evaluate(const std::vector<double> &observation) const = 0;
};