using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeperShared.Models
{
    public class Preset
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PedalBoard PedalBoard { get; set; }
        public ICollection<PedalPreset> PedalPresets { get; set; }
    }
}
