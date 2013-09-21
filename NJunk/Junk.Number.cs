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
                yield return unchecked((byte) (n >>  0));
                yield return unchecked((byte) (n >>  8));
                yield return unchecked((byte) (n >> 16));
                yield return unchecked((byte) (n >> 24));
            }
        }

        public static IEnumerable<byte> Bytes(this Junk junk, byte min, byte max)
        {
            return BytesCore(null, min, max);
        }

        public static IEnumerable<byte> NextBytes(this Random random, byte min, byte max)
        {
            return BytesCore(Require(random), min, max);
        }

        private static IEnumerable<byte> BytesCore(Random random, byte min, byte max)
        {
            if (random == null)
                random = Junk.Random;

            for (; ; ) yield return ByteCore(random, min, max);
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

        public static byte[] Bytes(this Junk junk, int length, byte min, byte max)
        {
            return BytesCore(Junk.Random, length);
        }

        public static byte[] NextBytes(this Random random, int length, byte min, byte max)
        {
            return BytesCore(Require(random), length);
        }

        private static byte[] BytesCore(Random random, int length, byte min, byte max)
        {
            var bytes = new byte[RequireLength(length)];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = ByteCore(random, min, max);
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
            return unchecked((byte) random.Next());
        }

        private static byte Byte(this Junk junk, byte min, byte max)
        {
            return ByteCore(Junk.Random, min, max);
        }

        private static byte NextByte(this Random random, byte min, byte max)
        {
            return ByteCore(Require(random), min, max);
        }

        private static byte ByteCore(this Random random, byte min, byte max)
        {
            return unchecked((byte) random.Next(min, max));
        }

        public static IEnumerable<int> Int32s(this Junk junk)
        {
            return Int32sCore(null);
        }

        public static IEnumerable<int> Int32s(this Random random)
        {
            return Int32sCore(Require(random));
        }

        private static IEnumerable<int> Int32sCore(this Random random)
        {
            if (random == null)
                random = Junk.Random;

            for (;;) yield return random.Next();
        }

        public static IEnumerable<int> Int32s(this Junk junk, int min, int max)
        {
            return Int32sCore(null, min, max);
        }

        public static IEnumerable<int> Int32s(this Random random, int min, int max)
        {
            return Int32sCore(Require(random), min, max);
        }

        private static IEnumerable<int> Int32sCore(this Random random, int min, int max)
        {
            if (random == null)
                random = Junk.Random;

            for (;;) yield return random.Next(min, max);
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
