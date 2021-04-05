using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models.Settings
{
    public class Option
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }

        public OptionSetting OptionSetting { get; set; }
    }
}
