using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;

namespace NJunk
{
    // Thread-safe dispenser for random number generators
    // See http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx
    // See http://msmvps.com/blogs/jon_skeet/archive/2009/11/04/revisiting-randomness.aspx
    //
    public sealed class Junk
    {
        private Junk() { }

        private static readonly RandomNumberGenerator
            SeedGenerator = RandomNumberGenerator.Create();

        private static readonly object
            SeedLock = new object();

#if NET35
        [ThreadStatic]
        private static Random random;

        internal static Random Random
        {
            get { return random ?? (random = CreateRandom()); }
        }
#else
        private static readonly ThreadLocal<Random>
            ThreadRandom = new ThreadLocal<Random>(CreateRandom);

        internal static Random Random
        {
            get { return ThreadRandom.Value; }
        }
#endif

        internal static Random CreateRandom()
        {
            var buffer = new byte[sizeof(int)];
            lock (SeedLock) SeedGenerator.GetBytes(buffer);
            var seed = BitConverter.ToInt32(buffer, 0);
            return new Random(seed);
        }

        public static Junk Get
        {
            get { return null; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool ReferenceEquals(object a, object b)
        {
            return object.ReferenceEquals(a, b);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object a, object b)
        {
            return object.Equals(a, b);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
