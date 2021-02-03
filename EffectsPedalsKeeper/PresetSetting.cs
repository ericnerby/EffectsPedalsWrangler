using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class PresetSetting : Setting
    {
        public List<string> Options { get; private set; }

        public PresetSetting(string label, IList<string> options)
            : base(label, 0, options.Count)
        {
            Options = (List<string>)options;
        }

        public override string CurrentValueDisplay => throw new NotImplementedException();

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
