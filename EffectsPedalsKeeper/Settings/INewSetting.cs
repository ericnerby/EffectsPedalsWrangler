using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings
{
    interface INewSetting
    {
        string Label { get; }
        SettingType SettingType { get; }
        int MinValue { get; }
        int MaxValue => Options.Count - 1;
        List<string> Options { get; }

        string CurrentValueDisplay { get; }
        int CurrentValue { get; set; }
    }
}
