using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectsPedalsKeeperShared.Models.Settings
{
    public class Option
    {
        public int Id { get; set; }
        [Required]
        public string Label { get; set; }
        public int Order { get; set; }
        public int OptionSettingId { get; set; }

        public OptionSetting OptionSetting { get; set; }
    }
}
