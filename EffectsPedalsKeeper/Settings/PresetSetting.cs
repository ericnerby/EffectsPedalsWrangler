using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class PresetSetting : Setting
    {
        public override int MaxValue => Options.Count - 1;

        public List<string> Options { get; private set; }

        public PresetSetting(string label, IList<string> options)
            : base(label, 0, options.Count)
        {
            Options = (List<string>)options;
        }

        public PresetSetting(string label)
            : base(label, 0, 1)
        {
            Options = new List<string>();
        }

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

        public override string CurrentValueDisplay => throw new NotImplementedException();

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
