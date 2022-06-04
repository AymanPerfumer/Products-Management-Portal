using Domain.ValueObjects;
using System;
using Xunit;

namespace Test.Domain
{
    public class TitleTest
    {
        [Fact]
        public void Title_Cannot_Be_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Title(null));
        }

        [Fact]
        public void Title_Cannot_Be_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => new Title(""));
        }

        [Fact]
        public void Title_Length_Cannot_Be_More_Than_100()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Title(
                @"Value object pattern is not unique to DDD, but it probably became most popular within the
                DDD community.It probably happened due to such characteristics of value objects as
                expressiveness and strong encapsulation.Fundamentally, value objects allow declaring
                entity properties with explicit types that use Ubiquitous Language.Besides, such objects
                can explicitly define how they can be created and what operations can be performed within
                and between them. It is a perfect example of making implicit, explicit."));
        }
    }
}