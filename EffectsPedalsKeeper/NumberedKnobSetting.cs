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
    public class NumberedKnobSetting : KnobSetting
    {
        public override string CurrentValueDisplay => _IntToDoubleString(CurrentValue);

        public NumberedKnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {}

        private static string _IntToDoubleString(int value)
        {
            throw new NotImplementedException();
        }
    }
}
