#include <vector>
#include <cmath>
#include "GeneralizedLinearModel.h"
#include "GaussianDistribution.h"
#include "Prepend.h"

namespace RegressionModels {
    GeneralizedLinearModel::GeneralizedLinearModel(const std::vector<std::vector<double>> &design, const std::vector<double> &response, const std::vector<double> &weights, std::unique_ptr<IDistribution> distribution, const bool addConstant)
    {
        if (design.size() != response.size() || design.empty()) {
            throw std::out_of_range("Argument vectors differ in length.");
        }


        _distribution = distribution == nullptr ? std::make_unique<Distributions::GaussianDistribution>() : std::move(distribution);
        _observationCount = design[0].size();
        _variableCount = design.size();
        _degreesOfFreedom = _observationCount - _variableCount;
        _sumSquaredErrors = 0;
        _meanSquaredError = _sumSquaredErrors / _degreesOfFreedom;
        _rootMeanSquaredError = std::sqrt(_meanSquaredError);

        const std::vector<std::vector<double>> designArray = addConstant ? prepend(design, 1.0) : design;


        _coefficients = std::vector<double>();
    }

    const double GeneralizedLinearModel::MeanSquaredError() const
    {
        return _meanSquaredError;
    }

    const double GeneralizedLinearModel::RootMeanSquaredError() const
    {
        return _rootMeanSquaredError;
    }

    const unsigned long GeneralizedLinearModel::ObservationCount() const
    {
        return _observationCount;
    }

    const unsigned long GeneralizedLinearModel::VariableCount() const
    {
        return _variableCount;
    }

    const long GeneralizedLinearModel::DegreesOfFreedom() const
    {
        return _degreesOfFreedom;
    }

    const std::vector<double> GeneralizedLinearModel::Coefficients() const
    {
        return _coefficients;
    }

    const double GeneralizedLinearModel::SumSquaredErrors() const
    {
        return _sumSquaredErrors;
    }

    const std::vector<double> GeneralizedLinearModel::StandardErrorsOls() const
    {
        return std::vector<double>();
    }

    const std::vector<double> GeneralizedLinearModel::StandardErrorsHC0() const
    {
        return std::vector<double>();
    }

    const std::vector<double> GeneralizedLinearModel::StandardErrorsHC1() const
    {
        return std::vector<double>();
    }

    const std::vector<double> GeneralizedLinearModel::VarianceOls() const
    {
        return std::vector<double>();
    }

    const std::vector<double> GeneralizedLinearModel::VarianceHC0() const
    {
        return std::vector<double>();
    }

    const std::vector<double> GeneralizedLinearModel::VarianceHC1() const
    {
        return std::vector<double>();
    }

    const double GeneralizedLinearModel::Evaluate(const std::vector<double> &observation) const
    {
        double result = 0.0;

        for (unsigned long i = 0; i < observation.size(); i++) {
            result += _coefficients[i] * observation[i];
        }

        return result;
    }
}
