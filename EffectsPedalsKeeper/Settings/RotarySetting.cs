using System;

namespace EffectsPedalsKeeper.Settings
{
    public class RotarySetting : Setting, ICopyable
    {
        public string[] Options { get; private set; }

        public override string CurrentValueDisplay => Options[CurrentValue];

        public RotarySetting(string label, string[] options)
            : base(label, 0, options.Length - 1)
        {
            Options = options;
        }

        public object Copy()
        {
            return _InternalCopy<RotarySetting>();
        }
    }
}
