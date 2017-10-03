#include <vector>
#include <numeric>
#include "GeneralizedLinearModel.h"
#include "GaussianDistribution.h"
#include "Prepend.h"

namespace RegressionModels {
    GeneralizedLinearModel::GeneralizedLinearModel(const std::vector<std::vector<double>> &design, const std::vector<double> &response, const std::vector<double> &weights, std::unique_ptr<IDistribution> distribution, const bool addConstant)
    {
        if (design.size() != response.size() || design.size() != weights.size() || design.empty()) {
            throw std::out_of_range("Argument vectors differ in length.");
        }

        _distribution = distribution == nullptr ? std::make_unique<Distributions::GaussianDistribution>() : std::move(distribution);
        _observationCount = design[0].size();
        _variableCount = design.size();
        _sumSquaredErrors = 0;

        const std::vector<std::vector<double>> designArray = addConstant ? prepend(design, 1.0) : design;

        _coefficients = std::vector<double>();
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
        return std::inner_product(
                _coefficients.begin(),
                _coefficients.end(),
                observation.begin(),
                0.0);
    }
}
