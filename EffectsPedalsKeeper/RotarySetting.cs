using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class RotarySetting : Setting
    {
        public string[] Options { get; private set; }

        public string CurrentOption => Options[CurrentValue];

        public RotarySetting(string label, string[] options)
            : base(label, 0, options.Length - 1)
        {
            Options = options;
        }

        public override string[] Display()
        {
            throw new NotImplementedException();
        }

        public override int StepDown()
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

        public override int StepUp()
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
