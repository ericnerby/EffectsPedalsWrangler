namespace EffectsPedalsKeeper.Settings
{
    public class SwitchSetting : RotarySetting
    {
        public SwitchSetting(string label)
            : base(label, new string[] { "Off", "On" })
        {}
    }
}
