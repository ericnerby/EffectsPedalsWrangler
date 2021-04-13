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
        public int PresetId { get; set; }
        public int PedalId { get; set; }

        public PedalPreset()
        {
            SettingPresets = new List<SettingPreset>();
        }

        public Preset Preset { get; set; }
        public Pedal Pedal { get; set; }
        public ICollection<SettingPreset> SettingPresets { get; set; }
    }
}
