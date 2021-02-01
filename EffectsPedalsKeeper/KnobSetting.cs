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
        private static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter();

        public bool Numbered { get; }

        public override string CurrentValueDisplay => _clockFaceConverter.IntToTimeString(CurrentValue);

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, _clockFaceConverter.StringTimeToInt(minClockPosition), _clockFaceConverter.StringTimeToInt(maxClockPosition))
        {}

        public KnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {}

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
