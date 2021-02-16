using EffectsPedalsKeeper.Utils;

namespace EffectsPedalsKeeper.Settings
{
    public class KnobSetting : Setting, ICopyable
    {
        private static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public override string CurrentValueDisplay => _clockFaceConverter.IntToTimeString(CurrentValue);

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, _clockFaceConverter.StringTimeToInt(minClockPosition), _clockFaceConverter.StringTimeToInt(maxClockPosition))
        {}

        public object Copy()
        {
            return _InternalCopy<KnobSetting>();
        }
    }
}
