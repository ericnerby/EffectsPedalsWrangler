using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings
{
    public interface ISetting : ICopyable, IInteractiveEditable, IBoundedValue, IUniqueID
    {
        string Label { get; }
        SettingType SettingType { get; }
        List<string> Options { get; }

        string CurrentValueDisplay { get; }

        string ToString(int valueToDisplay);
    }

    public enum SettingType
    {
        ClockFace,
        Numbered,
        Named,
        Switch
    }
}
