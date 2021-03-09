using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    public class SettingMock : Settings.Setting
    {
        private string _valueDisplayText;

        public SettingMock(string label, int minValue, int maxValue, string valueDisplayText)
            : base(label, minValue, maxValue)
        {
            _valueDisplayText = valueDisplayText;
        }

        public override string CurrentValueDisplay => _valueDisplayText;

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public override void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }
    }
}
