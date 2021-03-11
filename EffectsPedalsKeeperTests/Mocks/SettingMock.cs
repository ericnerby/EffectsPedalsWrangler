using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    public class SettingMock : ISetting
    {
        private string _valueDisplayText;

        public SettingMock(string label, string valueDisplayText)
        {
            Label = label;
            _valueDisplayText = valueDisplayText;
        }

        public SettingMock(string label, IList<string> options)
        {
            Label = label;
            Options = new List<string>(options);
        }

        public string CurrentValueDisplay
        {
            get
            {
                if (!string.IsNullOrEmpty(_valueDisplayText))
                {
                    return _valueDisplayText;
                }
                return Options[CurrentValue];
            }
        }

        public string Label { get; private set; }

        public SettingType SettingType => throw new NotImplementedException();

        public int MinValue => 0;
        public int MaxValue => Options.Count - 1;

        public List<string> Options { get; }

        protected int _currentValue;
        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _currentValue = value;
            }
        }

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        public string ToString(int valueToDisplay)
        {
            throw new NotImplementedException();
        }

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
            return (SettingMock)MemberwiseClone();
        }
    }
}
