using EffectsPedalsKeeper.Settings;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public interface IPedal : IInteractiveEditable
    {
        public bool Engaged { get; set; }

        public string Maker { get; }
        public string Name { get; }
        public EffectType EffectType { get; }

        public List<Setting> Settings { get; }

        public bool AddSettings(IList<Setting> settings);

        public bool AddSettings(params Setting[] settings);

        public string[] PrintSettingDetails();
    }

    public enum EffectType
    {
        Drive,
        Mod,
        Multi,
        Delay,
        Reverb
    }
}
