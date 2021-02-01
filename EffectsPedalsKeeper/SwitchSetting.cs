using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class SwitchSetting : RotarySetting
    {
        public SwitchSetting(string label)
            : base(label, new string[] { "Off", "On" })
        {}
    }
}
