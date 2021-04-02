using EffectsPedalsKeeperShared.Utils;
using System;

namespace EffectsPedalsKeeperSharedTests.Mocks
{
    public class BoundedValueMock : IBoundedValue
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        private int _currentValue;
        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(CurrentValue)} must be between {nameof(MinValue)} and {nameof(MaxValue)}");
                }
                _currentValue = value;
            }
        }

        public BoundedValueMock(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
