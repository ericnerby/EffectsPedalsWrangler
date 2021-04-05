using EffectsPedalsKeeperShared.Models.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeperShared.Models
{
    public class Pedal
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Name { get; }
        public EffectType EffectType { get; set; }

        public Pedal()
        {
            Settings = new List<Setting>();
            OptionSettings = new List<OptionSetting>();
        }

        public IList<Setting> Settings { get; set; }
        public IList<OptionSetting> OptionSettings { get; set; }

        public override string ToString() => $"{Name} by {Maker} ({EffectType})";
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
