using EffectsPedalsKeeper.Utils;
using System;

namespace EffectsPedalsKeeper.Settings
{
    /// <summary>
    ///  A knob rotates with no set positions. The position is
    ///  indicated by position on a clock face, eg. '3:00'.
    /// </summary>
    public class KnobSetting : Setting
    {
        private static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public override string CurrentValueDisplay => _clockFaceConverter.IntToTimeString(CurrentValue);

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, _clockFaceConverter.StringTimeToInt(minClockPosition), _clockFaceConverter.StringTimeToInt(maxClockPosition))
        {}
    }
}
