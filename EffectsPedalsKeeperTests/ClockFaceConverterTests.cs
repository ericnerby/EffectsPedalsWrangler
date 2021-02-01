using Xunit;
using EffectsPedalsKeeper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Tests
{
    public class ClockFaceConverterTests
    {
        private ClockFaceConverter _clockFaceConverter;

        public ClockFaceConverterTests()
        {
            _clockFaceConverter = new ClockFaceConverter();
        }

        [Fact()]
        public void StringTimeToIntTest6OClock()
        {
            var target = _clockFaceConverter.StringTimeToInt("6:00");
            var expected = 144;
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void StringTimeToIntTest12OClock()
        {
            var target = _clockFaceConverter.StringTimeToInt("12:00");
            var expected = 72;
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void IntToTimeStringTest6OClock()
        {
            var target = _clockFaceConverter.IntToTimeString(144);
            var expected = "6:00";
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void IntToTimeStringTest12OClock()
        {
            var target = _clockFaceConverter.IntToTimeString(72);
            var expected = "12:00";
            Assert.Equal(expected, target);
        }
    }
}