using EffectsPedalsKeeperShared.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeperShared.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public SettingType SettingType { get; set; }
        public virtual int MinValue { get; set; }
        public virtual int MaxValue { get; set; }

        public Pedal Pedal { get; set; }
    }

    public enum SettingType
    {
        Knob,
        Numbered,
        Slider,
        Button,
        Switch,
        Rotary,
        Other
    }
}
