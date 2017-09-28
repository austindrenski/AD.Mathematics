cmake_minimum_required(VERSION 3.6)

project(AD_Mathematics LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)

include_directories(
        ${PROJECT_SOURCE_DIR}
        LinkFunctions
        Distributions
        Matrix
        RegressionModels
        SpecialFunctions)

add_library(
        AD_Mathematics

        Matrix/Append.h
        Matrix/Prepend.h

        ILinkFunction.h
        LinkFunctions/IdentityLinkFunction.h
        LinkFunctions/LogLinkFunction.h

        IDistribution.h
        Distributions/GaussianDistribution.h
        Distributions/GaussianDistribution.cpp
        Distributions/PoissonDistribution.h
        Distributions/PoissonDistribution.cpp

        IRegressionModel.h
        RegressionModels/GeneralizedLinearModel.h
        RegressionModels/GeneralizedLinearModel.cpp

        SpecialFunctions/Factorial.h)

add_executable(app main.cpp)
target_link_libraries(app AD_Mathematics)