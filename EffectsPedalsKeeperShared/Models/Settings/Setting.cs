using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EffectsPedalsKeeperShared.Models.Settings
{
    public class Setting
    {
        public int Id { get; set; }
        [Required, StringLength(30, MinimumLength = 3)]
        public string Label { get; set; }
        [DisplayName("Setting Type")]
        public SettingType SettingType { get; set; }
        public virtual int MinValue { get; set; }
        public virtual int MaxValue { get; set; }

        public int PedalId { get; set; }

        public Pedal Pedal { get; set; }

        public Setting()
        {}

        public Setting(SettingType settingType)
        {
            SettingType = settingType;
            if (settingType == SettingType.Knob
                || settingType == SettingType.Numbered)
            {
                MinValue = 1;
                MaxValue = 360;
            }
            else if (settingType == SettingType.Slider)
            {
                MinValue = 0;
                MaxValue = 100;
            }
            else if (settingType == SettingType.Button
                     || settingType == SettingType.Switch)
            {
                MinValue = 0;
                MaxValue = 1;
            }
        }
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
