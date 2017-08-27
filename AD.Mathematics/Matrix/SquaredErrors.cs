using System;
using JetBrains.Annotations;

namespace AD.Mathematics.Matrix
{
    /// <summary>
    /// Extension methods to calculate squared errors from design and response arrays.
    /// </summary>
    [PublicAPI]
    public static class SquaredErrors
    {
        /// <summary>
        /// Calculates the squared errors for two arrays.
        /// </summary>
        /// <param name="a">
        /// The first array.
        /// </param>
        /// <param name="b">
        /// The second array.
        /// </param>
        /// <returns>
        /// The vector of squared errors.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SquaredError([NotNull] this double[] a, [NotNull] double[] b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }
            if (a.Length != b.Length)
            {
                throw new ArrayConformabilityException<double>(a, b);
            }

            double[] squaredErrors = new double[a.Length];

            for (int i = 0; i < squaredErrors.Length; i++)
            {
                double error = a[i] - b[i];

                squaredErrors[i] = error * error;
            }

            return squaredErrors;
        }


        /// <summary>
        /// Calculates the squared errors for the design and response arrays.
        /// </summary>
        /// <param name="design">
        /// The design array.
        /// </param>
        /// <param name="dependent">
        /// The vector of dependent variable values.
        /// </param>
        /// <param name="function">
        /// The function delegate to evaluate.
        /// </param>
        /// <returns>
        /// The vector of squared errors.
        /// </returns>
        [Pure]
        [NotNull]
        public static double[] SquaredError([NotNull][ItemNotNull] this double[][] design, [NotNull] double[] dependent, [NotNull] Func<double[], double> function)
        {
            if (dependent is null)
            {
                throw new ArgumentNullException(nameof(dependent));
            }
            if (design is null)
            {
                throw new ArgumentNullException(nameof(design));
            }
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            if (design.Length != dependent.Length)
            {
                throw new ArrayConformabilityException<double>(design, dependent);
            }

            double[] squaredErrors = new double[design.Length];

            for (int i = 0; i < dependent.Length; i++)
            {
                double error = dependent[i] - function(design[i]);

                squaredErrors[i] = error * error;
            }

            return squaredErrors;
        }
    }
}