using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class PresetSetting : Setting
    {
        public PresetSetting(string label, IList<string> Options)
            : base(label, 0, Options.Count)
        {
        }

        public override string CurrentValueDisplay => throw new NotImplementedException();

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
