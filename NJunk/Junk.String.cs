using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;

namespace NJunk
{
    partial class JunkExtensions
    {
        public static string UnicodeString(this Junk junk, int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var random = Junk.Random;
            var chars  = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = UnicodeChar(random);

            return new string(chars);
        }

        public static string AsciiString(this Junk junk, int length)
        {
            if (length < 0)
                throw new ArgumentNullException("length");

            var random = Junk.Random;
            var chars  = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = AsciiChar(random);

            return new string(chars);
        }

        public static char UnicodeChar(this Junk junk)
        {
            return UnicodeChar(Junk.Random);
        }

        private static char AsciiChar(Random random)
        {
            return (char) random.Next(0x20, 0x80);
        }

        public static char AsciiChar(this Junk junk)
        {
            return AsciiChar(Junk.Random);
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
    }
}
