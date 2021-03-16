using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Settings;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Pedals
{
    public interface IPedal : IInteractiveEditable, ICopyable, IUniqueID
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
