using EffectsPedalsKeeperShared.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models.ViewModels
{
    public class PedalDetailsViewModel
    {
        public Pedal Pedal { get; set; }
        public IList<Setting> PedalSettings { get; set; }

        public PedalDetailsViewModel(Pedal pedal)
        {
            Pedal = pedal;
            PedalSettings = new List<Setting>(pedal.Settings);
        }
    }
}
