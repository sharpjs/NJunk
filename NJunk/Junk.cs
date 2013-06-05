using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

namespace NJunk
{
    // Code written by Jeff Sharp as part of a future NJunk library
    //
    public static class Junk
    {
        // Thread-safe dispenser for random number generators
        // See http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx
        //
        private static class Core
        {
            private static readonly RandomNumberGenerator
                SeedGenerator = RandomNumberGenerator.Create();

            private static readonly object
                SeedLock = new object();

            [ThreadStatic]
            private static Random random;

            internal static Random Random
            {
                get { return random ?? (random = CreateRandom()); }
            }

            internal static Random CreateRandom()
            {
                var buffer = new byte[sizeof(int)];
                lock (SeedLock) SeedGenerator.GetBytes(buffer);
                var seed = BitConverter.ToInt32(buffer, 0);
                return new Random(seed);
            }
        }

        public static Random Generator
        {
            get { return Core.Random; }
        }

        public static Random CreateRandom()
        {
            return Core.CreateRandom();
        }

        public static int Int32(int min, int max)
        {
            return Core.Random.Next(min, max);
        }

        public static long Int64(long min, long max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("min, max");

            unchecked
            {
                return min + (long) (ulong)
                (
                    (ulong) (max - min) * Core.Random.NextDouble()
                );
            }
        }

        public static byte[] Bytes(int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var bytes = new byte[length];

            Core.Random.NextBytes(bytes);

            return bytes;
        }

        public static string UnicodeString(int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var random = Core.Random;
            var chars  = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = UnicodeChar(random);

            return new string(chars);
        }

        public static string AsciiString(int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var random = Core.Random;
            var chars  = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = AsciiChar(random);

            return new string(chars);
        }

        public static char UnicodeChar()
        {
            return UnicodeChar(Core.Random);
        }

        private static char AsciiChar(Random random)
        {
            return (char) random.Next(0x20, 0x80);
        }

        public static char AsciiChar()
        {
            return AsciiChar(Core.Random);
        }

        private static char UnicodeChar(Random random)
        {
            for (;;)
            {
                var c = (char) random.Next(0x0020, 0xD800);
                if (0 != (DesirableUnicodeCategories & 1 << (int) char.GetUnicodeCategory(c)))
                    return c;
            }
        }

        private const int DesirableUnicodeCategories = 0
            | 1 << (int) UnicodeCategory.UppercaseLetter
            | 1 << (int) UnicodeCategory.LowercaseLetter
            | 1 << (int) UnicodeCategory.TitlecaseLetter
            | 1 << (int) UnicodeCategory.OtherLetter
            | 1 << (int) UnicodeCategory.DecimalDigitNumber
            | 1 << (int) UnicodeCategory.LetterNumber
            | 1 << (int) UnicodeCategory.OtherNumber
            | 1 << (int) UnicodeCategory.SpaceSeparator
            | 1 << (int) UnicodeCategory.ConnectorPunctuation
            | 1 << (int) UnicodeCategory.DashPunctuation
            | 1 << (int) UnicodeCategory.OpenPunctuation
            | 1 << (int) UnicodeCategory.ClosePunctuation
            | 1 << (int) UnicodeCategory.InitialQuotePunctuation
            | 1 << (int) UnicodeCategory.FinalQuotePunctuation
            | 1 << (int) UnicodeCategory.OtherPunctuation
            | 1 << (int) UnicodeCategory.MathSymbol
            | 1 << (int) UnicodeCategory.CurrencySymbol
            | 1 << (int) UnicodeCategory.OtherSymbol;

        [Obsolete("Use System.Object.ReferenceEquals() instead.", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool ReferenceEquals(object a, object b)
        {
            return object.ReferenceEquals(a, b);
        }

        [Obsolete("Use System.Object.ReferenceEquals() instead.", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new static bool Equals(object a, object b)
        {
            return object.Equals(a, b);
        }
    }
}
