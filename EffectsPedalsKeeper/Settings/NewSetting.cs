using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Settings
{
    public class NewSetting : ICopyable
    {
        protected static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public string Label { get; }
        public SettingType SettingType { get; }
        public List<string> Options { get; protected set; }

        public string CurrentValueDisplay => throw new NotImplementedException();

        public int CurrentValue => throw new NotImplementedException();

        public NewSetting(string label, SettingType settingType, string lowerLimit, string upperLimit)
        {

        }

        public object Copy()
        {
            throw new NotImplementedException();
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }
    }

    public enum SettingType
    {
        ClockFace,
        Numbered,
        Named,
        Switch
    }
}
