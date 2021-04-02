using EffectsPedalsKeeperShared.Utils;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeperShared.Settings
{
    public interface ISetting : IBoundedValue
    {
        string Label { get; }
        SettingType SettingType { get; }
        List<string> Options { get; }

        string CurrentValueDisplay { get; }

        string ToString(int valueToDisplay);

        object Copy();
    }

    public enum SettingType
    {
        ClockFace,
        Numbered,
        Named,
        Switch
    }
}
