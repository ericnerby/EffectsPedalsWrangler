using System;

namespace EffectsPedalsKeeperTests.Mocks
{
    public class SettingMock : EffectsPedalsKeeper.Setting
    {
        private string _valueDisplayText;

        public SettingMock(string label, int minValue, int maxValue, string valueDisplayText)
            : base(label, minValue, maxValue)
        {
            _valueDisplayText = valueDisplayText;
        }

        public override string CurrentValueDisplay => _valueDisplayText;

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}
