namespace EffectsPedalsKeeper.Settings
{
    public class NumberedKnobSetting : Setting, ICopyable
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

        public object Copy()
        {
            return _InternalCopy<NumberedKnobSetting>();
        }
    }
}
