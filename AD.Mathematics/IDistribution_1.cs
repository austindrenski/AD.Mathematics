using System.Collections.Generic;
using JetBrains.Annotations;

namespace AD.Mathematics
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a statistical distribution from which values can be drawn.
    /// </summary>
    [PublicAPI]
    public interface IDistribution<out T> : IDistribution
    {
        /// <summary>
        /// Randomly draws a value from the distribution.
        /// </summary>
        /// <returns>
        /// A randomly drawn value from the distribution.
        /// </returns>
        [NotNull]
        T Draw();

        /// <summary>
        /// Randomly draws the specified count of values from the distribution.
        /// </summary>
        /// <param name="count">
        /// The number of random draws to make.
        /// </param>
        /// <returns>
        /// An enumerable collection of randomly drawn values from the distribution.
        /// </returns>
        [NotNull]
        [ItemNotNull]
        IEnumerable<T> Draw(int count);
    }
}