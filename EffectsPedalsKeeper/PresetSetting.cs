using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class PresetSetting : Setting
    {
        public List<string> Presets { get; private set; }

        public PresetSetting(string label, IList<string> presets)
            : base(label, 0, presets.Count)
        {
            Presets = (List<string>)presets;
        }

        public PresetSetting(string label)
            : base(label, 0, 1)
        {
            Presets = new List<string>();
        }

        public void AddPreset(string preset)
        {
            throw new NotImplementedException();
        }

        public void RemovePreset(int index)
        {
            throw new NotImplementedException();
        }

        public override string CurrentValueDisplay => throw new NotImplementedException();

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
