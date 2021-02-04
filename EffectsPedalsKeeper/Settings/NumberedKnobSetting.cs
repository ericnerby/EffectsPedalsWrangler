using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Settings
{
    /// <summary>
    ///  A knob rotates with no set positions. It can either
    ///  have numbers on the dial, or the position can be
    ///  indicated by position on a clock face, eg. '3:00'.
    /// </summary>
    public class NumberedKnobSetting : Setting
    {
        private int _minKnobValue;

        public override string CurrentValueDisplay
        {
            get
            {
                double value = ((double)CurrentValue / 10) + _minKnobValue;
                return value.ToString("0.0");
            }
        }

        public NumberedKnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {
            _minKnobValue = minKnobValue;
        }

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
