using EffectsPedalsKeeper.Utils;
using System;

namespace EffectsPedalsKeeper.Settings
{
    public class KnobSetting : Setting, ICopyable
    {
        private static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public override string CurrentValueDisplay => _clockFaceConverter.IntToTimeString(CurrentValue);

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, _clockFaceConverter.StringTimeToInt(minClockPosition), _clockFaceConverter.StringTimeToInt(maxClockPosition))
        {}

        public override object Copy()
        {
            return _InternalCopy<KnobSetting>();
        }

        public override void InteractiveChangeSetting(Func<string, bool> checkQuit)
        {
            throw new NotImplementedException();
        }
    }
}
