using System;

namespace EffectsPedalsKeeper.Settings
{
    /// <summary>
    ///  Generic Interface for Pedal Settings
    /// </summary>
    public abstract class Setting : ISetting
    {
        public string Label { get; }

        /// <summary>
        ///  Minimum value allowed for setting
        /// </summary>
        public int MinValue { get; protected set; }
        /// <summary>
        ///  Maximum value allowed for setting
        /// </summary>
        public virtual int MaxValue { get; protected set; }

        /// <summary>
        ///  CurrentValue converted to the appropriate string for the particular Setting type.
        /// </summary>
        public abstract string CurrentValueDisplay { get; }


        private int _currentValue;
        /// <summary>
        ///  Current Value of setting. Can be set directly as long
        ///  as given value is between MinValue and MaxValue.
        /// </summary>
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
                    throw new ArgumentOutOfRangeException(
                        $"'{nameof(value)}' must be between {nameof(MinValue)} and {nameof(MaxValue)}.");
                }
            }
        }

        public Setting(string label, int minValue, int maxValue)
        {
            if(maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(maxValue)} must be greater than or equal to {nameof(minValue)}.");
            }
            Label = label;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        ///  Positive incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        public int StepUp()
        {
            if (CurrentValue >= MaxValue)
            {
                return CurrentValue;
            }
            else
            {
                return ++CurrentValue;
            }
        }

        /// <summary>
        ///  Negative incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        public int StepDown()
        {
            if (CurrentValue <= MinValue)
            {
                return CurrentValue;
            }
            else
            {
                return --CurrentValue;
            }
        }

        /// <summary>
        ///  Generate string/visual representation of Setting
        /// </summary>
        public abstract string[] Display();

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";
    }
}
