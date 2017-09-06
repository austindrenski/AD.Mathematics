using System;
using JetBrains.Annotations;

namespace AD.Mathematics
{
    /// <summary>
    /// Represents a mismatch in expected array dimensions.
    /// </summary>
    /// <typeparam name="T">
    /// The type of entry in the array.
    /// </typeparam>
    [PublicAPI]
    public class ArrayConformabilityException<T> : RankException
    {
        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[] a, T[] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[] a, T[][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[] a, T[][][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][] a, T[] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][] a, T[][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][] a, T[][][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][][] a, T[] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][][] a, T[][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }

        /// <summary>
        /// Constructs an exception resulting from dimensonal mismatch.
        /// </summary>
        public ArrayConformabilityException(T[][][] a, T[][][] b)
            : base($"Conformability: {Format(nameof(a), a)}, {Format(nameof(b), b)},") { }
        
        private static string Format(string name, T[] array)
        {
            return $"{name}[{array.Length}]";
        }

        private static string Format(string name, T[][] array)
        {
            return $"{name}[{array.Length}][{array[0].Length}]";
        }

        private static string Format(string name, T[][][] array)
        {
            return $"{name}[{array.Length}][{array[0].Length}][{array[0][0].Length}]";
        }
    }
}