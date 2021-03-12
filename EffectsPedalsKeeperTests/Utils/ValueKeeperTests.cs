using Xunit;
using System;

namespace EffectsPedalsKeeper.Utils.Tests
{
    public class ValueKeeperTests
    {
        private TestBoundedValueObject _testBoundedValueObject;
        private ValueKeeper<TestBoundedValueObject> _valueKeeper;
        private int _minValue = 0;
        private int _maxValue = 5;
        private int _startingValue = 3;

        public ValueKeeperTests()
        {
            _testBoundedValueObject = new TestBoundedValueObject(_minValue, _maxValue);
            _testBoundedValueObject.CurrentValue = _startingValue;
            _valueKeeper = new ValueKeeper<TestBoundedValueObject>(_testBoundedValueObject);
        }

        [Fact()]
        public void ValueKeeperTest()
        {
            var target = _valueKeeper.StoredValue;
            var expected = _testBoundedValueObject.CurrentValue;
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void CurrentValueAssignmentOutOfBoundsTest()
        {
            var outOfBoundsValue = _testBoundedValueObject.MaxValue + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _valueKeeper.StoredValue = outOfBoundsValue);
        }
    }

    public class TestBoundedValueObject : IBoundedValue
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        private int _currentValue;
        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _currentValue = value;
            }
        }

        public TestBoundedValueObject(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}