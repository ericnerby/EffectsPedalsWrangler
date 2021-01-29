using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class KnobSetting : Setting
    {

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
        {
        }

        public KnobSetting(string label, int minKnobValue, int maxKnobValue)
        {
        }

        public int MinValue => throw new NotImplementedException();

        public int MaxValue => throw new NotImplementedException();

        public override string[] Display()
        {
            throw new NotImplementedException();
        }

        public override int StepDown()
        {
            throw new NotImplementedException();
        }

        public override int StepUp()
        {
            throw new NotImplementedException();
        }
    }
}
