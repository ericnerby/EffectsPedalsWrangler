using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings
{
    public class PresetSetting : Setting, ICopyable
    {
        public override int MaxValue => Options.Count - 1;

        public List<string> Options { get; private set; } = new List<string>();

        public override string CurrentValueDisplay
        {
            get
            {
                if (CurrentValue == -1)
                {
                    return "no preset chosen";
                }
                return Options[CurrentValue];
            }
        }

        public PresetSetting(string label, IList<string> options)
            : base(label, -1, options.Count - 1)
        {
            Options = (List<string>)options;
        }

        public PresetSetting(string label)
            : base(label, -1, -1)
        {}

        public bool AddPreset(string option)
        {
            if(Options.Contains(option))
            {
                return false;
            }
            Options.Add(option);
            return true;
        }

        public void RemovePreset(int index)
        {
            if(index >= Options.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Options.RemoveAt(index);
        }

        public override object Copy()
        {
            return _InternalCopy<PresetSetting>();
        }

        public override void InteractiveChangeSetting(Func<string, bool> checkQuit)
        {
            throw new NotImplementedException();
        }
    }
}
