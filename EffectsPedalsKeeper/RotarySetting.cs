using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class RotarySetting : ISetting
    {
        public string[] Options { get; private set; }

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
                    throw new ArgumentOutOfRangeException($"'{nameof(value)}' must be between {nameof(MinValue)} and {nameof(MaxValue)}");
                }
            }
        }

        public int MinValue { get; } = 0;

        public int MaxValue { get; }

        public RotarySetting(string[] options)
        {
            MaxValue = options.Length - 1;
            Options = options;
        }
        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public int StepDown()
        {
            if(CurrentValue <= 0)
            {
                return CurrentValue;
            }
            else
            {
                return --CurrentValue;
            }
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
    }
}
