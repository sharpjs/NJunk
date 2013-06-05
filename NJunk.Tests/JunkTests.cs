using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NJunk.Tests
{
    [TestFixture]
    public class JunkTests
    {
        [Test]
        public void Random()
        {
            var a = Junk.Random;
            var b = Junk.Random;

            Assert.That(a, Is.Not.Null & Is.SameAs(b));
        }

        [Test]
        public void CreateRandom()
        {
            var a = Junk.Get.Random();
            var b = Junk.Get.Random();

            Assert.That(a, Is.Not.Null);
            Assert.That(b, Is.Not.Null & Is.Not.SameAs(a));
        }

        [Test]
        public void Int32()
        {
            var numbers = new HashSet<int>();

            for (var i = 0; i < 1000; i++)
                numbers.Add(Junk.Get.Int32(-2000000, 4000000));

            Assert.That(numbers, Has.Count.EqualTo(1000).Within(3)
                & Has.All.AtLeast(-2000000).And.LessThan(4000000));
        }

        [Test]
        public void Int32_MinToMax()
        {
            var numbers = new HashSet<int>();

            for (var i = 0; i < 1000; i++)
                numbers.Add(Junk.Get.Int32(int.MinValue, int.MaxValue));

            Assert.That(numbers, Has.Count.EqualTo(1000).Within(3)
                & Has.Some.LessThan   (-2000000)
                & Has.Some.GreaterThan( 4000000));
        }

        [Test]
        public void Int64()
        {
            var numbers = new HashSet<long>();

            for (var i = 0; i < 1000; i++)
                numbers.Add(Junk.Get.Int64(-2000000000000000000, 4000000000000000000));

            Assert.That(numbers, Has.Count.EqualTo(1000).Within(3)
                & Has.All.AtLeast(-2000000000000000000).And.LessThan(4000000000000000000));
        }

        [Test]
        public void Int64_MinToMax()
        {
            var numbers = new HashSet<long>();

            for (var i = 0; i < 1000; i++)
                numbers.Add(Junk.Get.Int64(long.MinValue, long.MaxValue));

            Assert.That(numbers, Has.Count.EqualTo(1000).Within(3)
                & Has.Some.LessThan   (-2000000000000000000)
                & Has.Some.GreaterThan( 4000000000000000000));
        }
    }
}
