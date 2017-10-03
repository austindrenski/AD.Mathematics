#include <iostream>
#include <vector>
#include "GeneralizedLinearModel.h"
#include "PoissonDistribution.h"
#include "Factorial.h"

using RegressionModels::GeneralizedLinearModel;
using Distributions::PoissonDistribution;

int main()
{
    const std::vector<std::vector<double>> design =
            {
                    std::vector<double> {1, 2, 3},
                    std::vector<double> {4, 5, 6},
                    std::vector<double> {7, 8, 9}
            };

    const std::vector<double> response =
            {
                    7,
                    8,
                    9
            };

    const std::vector<double> weights =
            {
                    1,
                    1,
                    1
            };

    GeneralizedLinearModel linearModel(design, response, weights);

    GeneralizedLinearModel poissonModel(design, response, weights, std::make_unique<PoissonDistribution>(), true);

    for (auto item : SpecialFunctions::FactorialTemplate<170>{}.create_values()) {
        std::cout << item << std::endl;
    }

    for (auto item : SpecialFunctions::FactorialTemplate<170>{}.create_logs()) {
        std::cout << item << std::endl;
    }

    for (int i = 0; i < 170; i++) {
        std::cout << SpecialFunctions::Factorial::Get(i) << std::endl;
    }

    for (int i = 1; i < 170; i++) {
        std::cout << SpecialFunctions::Factorial::GetLog(i) << std::endl;
    }

    std::cout << "Hello, World!" << std::endl;
    return 0;
}