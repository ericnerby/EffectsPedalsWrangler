using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Utils
{
    public interface IBoundedValue
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        public int CurrentValue { get; set; }
    }
}
