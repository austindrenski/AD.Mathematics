#include <iostream>
#include <vector>
#include "GeneralizedLinearModel.h"
#include "PoissonDistribution.h"

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

    std::cout << "Hello, World!" << std::endl;
    int result;
    std::cin >> result;
    std::cout << result;
    return result;
}