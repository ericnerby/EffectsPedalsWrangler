using EffectsPedalsKeeperShared.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models
{
    public class SettingPreset
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SettingId { get; set; }
        public int PedalPresetId { get; set; }

        public Setting Setting { get; set; }
        public PedalPreset PedalPreset { get; set; }
    }
}
