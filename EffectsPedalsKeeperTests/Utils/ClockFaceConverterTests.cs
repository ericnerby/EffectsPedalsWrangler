﻿using Xunit;

namespace EffectsPedalsKeeper.Utils.Tests
{
    public class ClockFaceConverterTests
    {
        private ClockFaceConverter _clockFaceConverter;

        public ClockFaceConverterTests()
        {
            _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);
        }

        [Fact()]
        public void ConstructorWithMinValTest()
        {
            var minValue = "6:30";
            var clockFaceConverterWithMin = new ClockFaceConverter(PrecisionValue.Five, minValue);

            var target = clockFaceConverterWithMin.IntToTimeString(0);
            var expected = minValue;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void ConstructorWithMinValMaxRangeTest()
        {
            var minValue = "6:30";
            var clockFaceConverterWithMin = new ClockFaceConverter(PrecisionValue.Five, minValue);

            int maxValue = clockFaceConverterWithMin.MaxIntRange;

            var target = clockFaceConverterWithMin.IntToTimeString(maxValue);
            var expected = minValue;

            Assert.Equal(expected, target);
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