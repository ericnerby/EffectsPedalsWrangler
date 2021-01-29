using System;

namespace EffectsPedalsKeeper
{
    /// <summary>
    ///  Generic Interface for Pedal Settings
    /// </summary>
    public abstract class Setting
    {
        public string Label { get; }

        /// <summary>
        ///  Minimum value allowed for setting
        /// </summary>
        int MinValue { get; }
        /// <summary>
        ///  Maximum value allowed for setting
        /// </summary>
        int MaxValue { get; }

        private int _currentValue;
        public int CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                if (value >= MinValue && value <= MaxValue)
                {
                    _currentValue = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"'{nameof(value)}' must be between {nameof(MinValue)} and {nameof(MaxValue)}.");
                }
            }
        }

        public Setting(string label, int minValue, int maxValue)
        {
            if(maxValue <= minValue)
            {
                throw new ArgumentOutOfRangeException($"{nameof(maxValue)} must be greater than {nameof(minValue)}.");
            }
            Label = label;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        ///  Positive incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        public abstract int StepUp();
        /// <summary>
        ///  Negative incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        public abstract int StepDown();

        /// <summary>
        ///  Generate string/visual representation of Setting
        /// </summary>
        public abstract string[] Display();
    }
}
