using System;
using System.Globalization;

namespace NJunk
{
    partial class JunkExtensions
    {
        public static string UnicodeString(this Junk junk, int length)
        {
            return UnicodeStringCore(Junk.Random, length);
        }

        public static string UnicodeString(this Random random, int length)
        {
            return UnicodeStringCore(Require(random), length);
        }

        private static string UnicodeStringCore(Random random, int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var chars = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = UnicodeCharCore(random);

            return new string(chars);
        }

        public static string AsciiString(this Junk junk, int length)
        {
            return AsciiStringCore(Junk.Random, length);
        }

        public static string NextAsciiString(this Random random, int length)
        {
            return AsciiStringCore(Require(random), length);
        }

        private static string AsciiStringCore(Random random, int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var chars = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = AsciiCharCore(random);

            return new string(chars);
        }

        public static char UnicodeChar(this Junk junk)
        {
            return UnicodeCharCore(Junk.Random);
        }

        public static char NextUnicodeChar(this Random random)
        {
            return UnicodeCharCore(Require(random));
        }

        private static char UnicodeCharCore(Random random)
        {
            for (;;)
            {
                var c = (char) random.Next(0x0020, 0xD800);
                if (0 != (DesirableUnicodeCategories & 1 << (int) char.GetUnicodeCategory(c)))
                    return c;
            }
        }

        public static char AsciiChar(this Junk junk)
        {
            return AsciiCharCore(Junk.Random);
        }

        public static char NextAsciiChar(this Random random)
        {
            return AsciiCharCore(Require(random));
        }

        private static char AsciiCharCore(Random random)
        {
            return (char) random.Next(0x20, 0x80);
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
    }
}
