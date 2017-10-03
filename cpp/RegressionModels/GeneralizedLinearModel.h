#pragma once

#include <cmath>
#include <memory>
#include <vector>
#include "IDistribution.h"
#include "IRegressionModel.h"

namespace RegressionModels {
    class GeneralizedLinearModel : public IRegressionModel {
    public:

        GeneralizedLinearModel(
                const std::vector<std::vector<double>> &design,
                const std::vector<double> &response,
                const std::vector<double> &weights,
                std::unique_ptr<IDistribution> distribution = nullptr,
                bool addConstant = false);

        const unsigned long ObservationCount() const override
        { return _observationCount; }

        const unsigned long VariableCount() const override
        { return _variableCount; }

        const long DegreesOfFreedom() const override
        { return _observationCount - _variableCount; }

        const std::vector<double> Coefficients() const override
        { return _coefficients; }

        const double SumSquaredErrors() const override
        { return _sumSquaredErrors; }

        const double MeanSquaredError() const override
        { return _sumSquaredErrors / DegreesOfFreedom(); }

        const double RootMeanSquaredError() const override
        { return sqrt(MeanSquaredError()); }

        const std::vector<double> StandardErrorsOls() const override;

        const std::vector<double> StandardErrorsHC0() const override;

        const std::vector<double> StandardErrorsHC1() const override;

        const std::vector<double> VarianceOls() const override;

        const std::vector<double> VarianceHC0() const override;

        const std::vector<double> VarianceHC1() const override;

        const double Evaluate(const std::vector<double> &observation) const override;

    private:

        std::unique_ptr<IDistribution> _distribution;

        unsigned long _observationCount;

        unsigned long _variableCount;

        std::vector<double> _coefficients;

        double _sumSquaredErrors;
    };
}