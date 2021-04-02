using EffectsPedalsKeeperShared.Models;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeperSharedTests.Mocks
{
    public class SettingMock : Setting
    {
        private string _valueDisplayText;

        public SettingMock(string label, string valueDisplayText)
            : base(label, SettingType.Numbered, new List<string>())
        {
            _valueDisplayText = valueDisplayText;
        }

        public SettingMock(string label, IList<string> options)
            : base(label, SettingType.Numbered, options)
        {
            Options = new List<string>(options);
        }

        public new string CurrentValueDisplay
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

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";
    }
}
