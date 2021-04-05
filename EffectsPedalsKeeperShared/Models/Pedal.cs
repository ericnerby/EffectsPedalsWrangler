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

        public IList<Setting> Settings { get; set; }

        public override string ToString()
        {
            return $"{Name} by {Maker} ({EffectType})";
        }
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
