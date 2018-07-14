using GildedRose.Console.Utils;
using Xunit;

namespace GildedRose.Tests
{
    public class ExtraMathTests
    {
        [Fact]
        public void ClampMinTest()
        {
            Assert.Equal(ExtraMath.Clamp(0, -10, 50),0);
        }

        [Fact]
        public void ClampMaxTest()
        {
            Assert.Equal(ExtraMath.Clamp(0, 60, 50), 50);
        }

        [Fact]
        public void ClampInRangeTest()
        {
            Assert.Equal(ExtraMath.Clamp(0, 20, 50), 20);
        }
    }
}
