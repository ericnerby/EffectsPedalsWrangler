using System;

namespace EffectsPedalsKeeper.Settings
{
    [Serializable()]
    public class SwitchSetting : RotarySetting
    {
        public SwitchSetting(string label)
            : base(label, new string[] { "Off", "On" })
        { }

        public override object Copy()
        {
            return _InternalCopy<SwitchSetting>();
        }
    }
}
