using System;

namespace EffectsPedalsKeeper.Settings
{
    public abstract class Setting : ISetting, ICopyable, IInteractiveEditable
    {
        public string Label { get; }

        public int MinValue { get; protected set; }
        public virtual int MaxValue { get; protected set; }

        public abstract string CurrentValueDisplay { get; }


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
            CurrentValue = MinValue;
        }

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

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        protected T _InternalCopy<T>()
        {
            return (T)this.MemberwiseClone();
        }

        public virtual object Copy()
        {
            return _InternalCopy<Setting>();
        }

        abstract public void InteractiveChangeSetting(Func<string, bool> checkQuit);
    }
}
