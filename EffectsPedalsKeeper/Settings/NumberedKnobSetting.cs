using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Settings
{
    /// <summary>
    ///  A knob rotates with no set positions.
    ///  A NumberedKnob's value is indicated by
    ///  a string version of a double.
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
    }
}
