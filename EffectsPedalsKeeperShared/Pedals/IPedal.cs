using EffectsPedalsKeeperShared.PedalBoards;
using EffectsPedalsKeeperShared.Settings;
using System.Collections.Generic;

namespace EffectsPedalsKeeperShared.Pedals
{
    public interface IPedal
    {
        public bool Engaged { get; set; }

        public string Maker { get; }
        public string Name { get; }
        public EffectType EffectType { get; }

        public List<ISetting> Settings { get; }

        public bool AddSettings(IList<ISetting> settings);

        public bool AddSettings(params ISetting[] settings);

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
