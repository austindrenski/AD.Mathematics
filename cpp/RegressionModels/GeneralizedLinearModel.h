#pragma once

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

        const unsigned long ObservationCount() const override;

        const unsigned long VariableCount() const override;

        const long DegreesOfFreedom() const override;

        const std::vector<double> Coefficients() const override;

        const double SumSquaredErrors() const override;

        const double MeanSquaredError() const override;

        const double RootMeanSquaredError() const override;

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

        long _degreesOfFreedom;

        std::vector<double> _coefficients;

        double _sumSquaredErrors;

        double _meanSquaredError;

        double _rootMeanSquaredError;
    };
}