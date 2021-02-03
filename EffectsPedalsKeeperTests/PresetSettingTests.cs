using Xunit;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests
{
    public class PresetSettingTests
    {
        protected List<string> _options = new List<string> { "Edge", "Slapback", "Etherial", "Reverse", "Spooky" };
        private string _testLabel = "Preset";
        private PresetSetting _preset;

        public PresetSettingTests()
        {
            _preset = new PresetSetting(_testLabel, _options);
        }

        [Fact()]
        public void DisplayTest()
        {
            for (int index = 0; index < _options.Count; index++)
            {
                _preset.CurrentValue = index;
                string expected = _options[index];
                string[] target = _preset.Display();
                Assert.Contains(target, item => item.Contains(expected));
            }
        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _preset;
            target.CurrentValue = 1;

            int expected = _preset.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _preset;
            target.CurrentValue = 1;

            int expected = _preset.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _preset;
            target.CurrentValue = _preset.MaxValue;

            int expected = _preset.MaxValue;

            Assert.Equal(_preset.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _preset;
            target.CurrentValue = _preset.MinValue;

            int expected = _preset.MinValue;

            Assert.Equal(_preset.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTest()
        {
            int targetIndex = 1;
            _preset.CurrentValue = targetIndex;
            var target = _preset.ToString();

            string expectedOption = _options[targetIndex];
            string expectedLabel = _testLabel;

            Assert.Contains(expectedOption, target);
            Assert.Contains(expectedLabel, target);
        }
    }
}
