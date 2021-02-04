using System;

namespace EffectsPedalsKeeper.Settings
{
    /// <summary>
    ///  A RotarySetting has fixed positions, or 'Options', that are labeled.
    ///  This could also represent a three way switch.
    /// </summary>
    public class RotarySetting : Setting
    {
        public string[] Options { get; private set; }

        public override string CurrentValueDisplay => Options[CurrentValue];

        public RotarySetting(string label, string[] options)
            : base(label, 0, options.Length - 1)
        {
            Options = options;
        }

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
