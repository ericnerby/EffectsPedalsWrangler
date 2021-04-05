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

        protected int _value;
        public int Value { get; set; }

        public Setting Setting { get; set; }
    }
}
