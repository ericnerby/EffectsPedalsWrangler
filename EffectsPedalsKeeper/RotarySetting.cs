using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class RotarySetting : ISetting
    {
        public string[] Options { get; private set; }
        public string Label { get; }

        public int MinValue { get; } = 0;
        public int MaxValue { get; }

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

        public string CurrentOption => Options[CurrentValue];

        public RotarySetting(string label, string[] options)
        {
            Label = label;
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

        public override string ToString()
        {
            return $"{Label}: {CurrentOption}";
        }
    }
}
