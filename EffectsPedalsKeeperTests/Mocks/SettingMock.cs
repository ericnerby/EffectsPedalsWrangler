using System;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    public class SettingMock : Settings.ISetting
    {
        private string _valueDisplayText;

        public SettingMock(string label, int minValue, int maxValue, string valueDisplayText)
        {
            Label = label;
            MinValue = minValue;
            MaxValue = maxValue;
            _valueDisplayText = valueDisplayText;
        }

        public string CurrentValueDisplay => _valueDisplayText;

        public string Label { get; }

        public int MinValue { get; }

        public int MaxValue { get; }

        public int CurrentValue => throw new NotImplementedException();

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public int StepDown()
        {
            throw new NotImplementedException();
        }

        public int StepUp()
        {
            throw new NotImplementedException();
        }

        public void InteractiveChangeSetting(Action<string> checkQuit)
        {
            throw new NotImplementedException();
        }
    }
}
