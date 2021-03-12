using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Utils
{
    public class ValueKeeper<T> where T : IBoundedValue
    {
        public T Item;

        private int _storedValue;
        public int StoredValue
        {
            get { return _storedValue; }
            set
            {
                if (value < Item.MinValue || value > Item.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _storedValue = value;
            }
        }

        public ValueKeeper(T item)
        {
            Item = item;
            StoredValue = item.CurrentValue;
        }
    }
}
