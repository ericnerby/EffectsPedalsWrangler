using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Utils
{
    public class ValueKeeper<T> where T : IBoundedValue
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

        public ValueKeeper(T item)
        {
            MinValue = item.MinValue;
            MaxValue = item.MaxValue;
            StoredValue = item.CurrentValue;
        }
    }
}
