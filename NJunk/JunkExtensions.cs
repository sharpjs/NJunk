using System;
using System.ComponentModel;

namespace NJunk
{
    /// <summary>
    ///   Provides a set of extension methods that generate random data.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static partial class JunkExtensions
    {
        /// <summary>
        ///   Returns a new instance of <c>System.Random</c> with a random seed.
        /// </summary>
        /// <param name="junk">
        ///   Typically, the value returned by <see cref="Junk.Get"/>.
        ///   This parameter is not used.
        /// </param>
        /// <returns>
        ///   A new instance of <c>System.Random</c>, seeded with a random value.
        /// </returns>
        /// <remarks>
        ///   This method is thread-safe.
        /// </remarks>
        public static Random Random(this Junk junk)
        {
            return Junk.CreateRandom();
        }

        private static Random Require(Random random)
        {
            if (random == null)
                throw new ArgumentNullException("random");

            return random;
        }

        private static int RequireLength(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length");

            return length;
        }
    }
}
