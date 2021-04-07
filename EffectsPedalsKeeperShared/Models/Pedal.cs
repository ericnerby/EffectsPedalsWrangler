using EffectsPedalsKeeperShared.Models.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EffectsPedalsKeeperShared.Models
{
    public class Pedal
    {
        public int Id { get; set; }
        [Required,StringLength(30, MinimumLength = 3)]
        public string Maker { get; set; }
        [Required,StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, DisplayName("Effect Type")]
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
