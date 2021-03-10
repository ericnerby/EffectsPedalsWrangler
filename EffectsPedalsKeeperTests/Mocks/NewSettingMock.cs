using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    public class NewSettingMock : ISetting
    {
        private string _valueDisplayText;

        public NewSettingMock(string label, string valueDisplayText)
        {
            Label = label;
            _valueDisplayText = valueDisplayText;
        }

        public string CurrentValueDisplay => _valueDisplayText;

        public string Label { get; private set; }

        public SettingType SettingType => throw new NotImplementedException();

        public int MinValue => throw new NotImplementedException();

        public List<string> Options => throw new NotImplementedException();

        public int CurrentValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }

        public object Copy()
        {
            return (NewSettingMock)MemberwiseClone();
        }
    }
}
