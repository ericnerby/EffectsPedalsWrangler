using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models
{
    public class PedalPreset
    {
        public int Id { get; set; }
        public bool Engaged { get; set; }

        public Preset Preset { get; set; }
        public Pedal Pedal { get; set; }
        public ICollection<SettingPreset> SettingPresets { get; set; }
    }
}
