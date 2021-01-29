using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class KnobSetting : ISetting
    {
        public string Label { get; }

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
        {
        }

        public KnobSetting(string label, int minKnobValue, int maxKnobValue)
        {
        }

        public int CurrentValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int MinValue => throw new NotImplementedException();

        public int MaxValue => throw new NotImplementedException();

        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public int StepDown()
        {
            throw new NotImplementedException();
        }

        public int StepUp()
        {
            throw new NotImplementedException();
        }
    }
}
