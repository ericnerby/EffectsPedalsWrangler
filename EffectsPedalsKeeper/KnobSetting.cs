using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    /// <summary>
    ///  A knob rotates with no set positions. It can either
    ///  have numbers on the dial, or the position can be
    ///  indicated by position on a clock face, eg. '3:00'.
    /// </summary>
    public class KnobSetting : Setting
    {

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, 10, 110)
        {
        }

        public KnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 10, 110)
        {
        }

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
