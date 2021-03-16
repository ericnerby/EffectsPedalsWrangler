using Xunit;
using System;
using EffectsPedalsKeeperTests.Mocks;

namespace EffectsPedalsKeeper.Utils.Tests
{
    public class ValueKeeperTests
    {
        private ValueKeeper _valueKeeper;
        private BoundedValueMock _boundedValueMock;
        private int _minValue = 0;
        private int _maxValue = 5;
        private int _startingValue = 3;

        public ValueKeeperTests()
        {
            _boundedValueMock = new BoundedValueMock(_minValue, _maxValue);
            _boundedValueMock.CurrentValue = _startingValue;
            _valueKeeper = new ValueKeeper(_boundedValueMock);
        }

        [Fact()]
        public void ValueKeeperTest()
        {
            var target = _valueKeeper.StoredValue;
            var expected = _boundedValueMock.CurrentValue;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void StoredValueOutOfRangeTest()
        {
            var outOfRangeValue = _boundedValueMock.MaxValue + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _valueKeeper.StoredValue = outOfRangeValue);
        }
    }
}