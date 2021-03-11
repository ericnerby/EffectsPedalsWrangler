using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Utils
{
    public class ValueKeeper<T> where T : IBoundedValue
    {
        public T Item;

        public int StoredValue
        {
            get { return StoredValue; }
            set
            {
                if (value < Item.MinValue || value > Item.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                StoredValue = value;
            }
        }

        public ValueKeeper(T item)
        {
            Item = item;
            StoredValue = item.CurrentValue;
        }
    }
}
