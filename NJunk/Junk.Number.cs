using System;
using System.Collections.Generic;

namespace NJunk
{
    partial class JunkExtensions
    {
        public static IEnumerable<byte> Bytes(this Junk junk)
        {
            return BytesCore(null);
        }

        public static IEnumerable<byte> NextBytes(this Random random)
        {
            return BytesCore(Require(random));
        }

        private static IEnumerable<byte> BytesCore(Random random)
        {
            if (random == null)
                random = Junk.Random;

            for (;;)
            {
                var n = random.Next();
                yield return (byte) (n >>  0 & 0xFF);
                yield return (byte) (n >>  8 & 0xFF);
                yield return (byte) (n >> 16 & 0xFF);
                yield return (byte) (n >> 24 & 0xFF);
            }
        }

        public static byte[] Bytes(this Junk junk, int length)
        {
            return BytesCore(Junk.Random, length);
        }

        public static byte[] NextBytes(this Random random, int length)
        {
            return BytesCore(Require(random), length);
        }

        private static byte[] BytesCore(Random random, int length)
        {
            var bytes = new byte[RequireLength(length)];
            random.NextBytes(bytes);
            return bytes;
        }

        private static byte Byte(this Junk junk)
        {
            return ByteCore(Junk.Random);
        }

        private static byte NextByte(this Random random)
        {
            return ByteCore(Require(random));
        }

        private static byte ByteCore(this Random random)
        {
            unchecked { return (byte) random.Next(); }
        }

        public static IEnumerable<int> Int32s(this Junk junk, int min, int max)
        {
            var random = Junk.Random;
            for (;;) { yield return random.Next(min, max); }
        }

        public static int Int32(this Junk junk, int min, int max)
        {
            return Junk.Random.Next(min, max);
        }

        public static long Int64(this Junk junk, long min, long max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException("min, max");

            unchecked
            {
                return min + (long) (ulong)
                (
                    (ulong) (max - min) * Junk.Random.NextDouble()
                );
            }
        }
    }
}
