#pragma once

#include <memory>
#include "IDistribution.h"
#include "ILinkFunction.h"

namespace Distributions {
    class PoissonDistribution : public IDistribution {
    public:

        explicit PoissonDistribution(double mean = 1.0, std::unique_ptr<ILinkFunction> link = nullptr);

        const double Entropy() const override
        { return _entropy; }

        const double Maximum() const override
        { return _maximum; }

        const double Mean() const override
        { return _mean; }

        const double Median() const override
        { return _median; }

        const double Minimum() const override
        { return _minimum; }

        const double Mode() const override
        { return _mode; }

        const double Skewness() const override
        { return _skewness; }

        const double Kurtosis() const override
        { return _kurtosis; }

        const double StandardDeviation() const override
        { return _standardDeviation; }

        const double Variance() const override
        { return _variance; }

        /// <summary>
        /// Calculates the deviance for the given arguments.
        /// </summary>
        /// <param name="response">
        /// An array of response values.
        /// </param>
        /// <param name="meanResponse">
        /// An array of mean response values.
        /// </param>
        /// <param name="weights">
        /// An array of importance weights.
        /// </param>
        /// <param name="scale">
        /// An option scaling value.
        /// </param>
        /// <returns>
        /// The deviance function evaluated with the given inputs.
        /// </returns>
        const double Deviance(const std::vector<double> &response, const std::vector<double> &meanResponse, const std::vector<double> &weights, double scale) const override;

        /// <summary>
        /// Provides an initial mean response array for the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="response">
        /// An untransformed response array.
        /// </param>
        /// <returns>
        /// An initial mean response array.
        /// </returns>
        const std::vector<double> InitialMean(const std::vector<double> &response) const override;

        /// <summary>
        /// Calculates the weight for a step of the Iteratively Reweighted Least Squares (IRLS) algorithm.
        /// </summary>
        /// <param name="meanResponse">
        /// A mean response value.
        /// </param>
        /// <returns>
        /// A weight based on the mean response.
        /// </returns>
        const std::vector<double> Weight(const std::vector<double> &meanResponse) const override;

        /// <summary>
        /// Calculates a linear prediction given a mean response value.
        /// </summary>
        /// <param name="meanResponse">
        /// A mean response value.
        /// </param>
        /// <returns>
        /// A linear prediction value.
        /// </returns>
        const std::vector<double> Predict(const std::vector<double> &meanResponse) const override;

        /// <summary>
        /// Calculates a mean response value given a linear prediction.
        /// </summary>
        /// <param name="linearPrediction">
        /// A linear prediction.
        /// </param>
        /// <returns>
        /// A mean response value.
        /// </returns>
        const std::vector<double> Fit(const std::vector<double> &linearPrediction) const override;

        /// <summary>
        /// The probability function of the distribution (e.g. PMF for discrete distributions, PDF for continuous distributions).
        /// </summary>
        /// <param name="x">
        /// The domain location at which the probability is evaluated.
        /// </param>
        /// <returns>
        /// The probability at the given location.
        /// </returns>
        const double Probability(double x) const override;

        /// <summary>
        /// The logarithm of the probability function of the distribution.
        /// </summary>
        /// <param name="x">
        /// The domain location at which the log(Probability) is evaluated.
        /// </param>
        /// <returns>
        /// The logarithm of the probability at the given location.
        /// </returns>
        const double LogProbability(double x) const override;

    private:

        std::unique_ptr<ILinkFunction> _link;

        const double _entropy;

        const double _kurtosis;

        const double _maximum;

        const double _mean;

        const double _median;

        const double _minimum;

        const double _mode;

        const double _skewness;

        const double _standardDeviation;

        const double _variance;
    };
}