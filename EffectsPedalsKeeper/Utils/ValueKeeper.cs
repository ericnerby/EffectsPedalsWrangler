using Newtonsoft.Json;
using System;

namespace EffectsPedalsKeeper.Utils
{
    public class ValueKeeper
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        private int _storedValue;
        public int StoredValue
        {
            get { return _storedValue; }
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _storedValue = value;
            }
        }

        public ValueKeeper(IBoundedValue item)
        {
            MinValue = item.MinValue;
            MaxValue = item.MaxValue;
            StoredValue = item.CurrentValue;
        }

        [JsonConstructor]
        public ValueKeeper(int minValue, int maxValue, int currentValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            StoredValue = currentValue;
        }
    }
}
