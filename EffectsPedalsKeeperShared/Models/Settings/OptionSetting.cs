using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models.Settings
{
    public class OptionSetting : Setting
    {
        public OptionSetting()
        {
            Options = new List<Option>();
        }

        public IList<Option> Options { get; set; }
    }
}
