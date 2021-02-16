using Xunit;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class RotarySettingTests
    {
        protected string[] _options;
        private RotarySetting _rotary;
        private string _testLabel;

        public RotarySettingTests()
        {
            _testLabel = "Type";
            _options = new string[] { "Tape", "Digital", "Echo", "Fade", "Mod" };
            _rotary = new RotarySetting(_testLabel, _options);
        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _rotary;
            target.CurrentValue = 1;

            int expected = _rotary.CurrentValue -1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _rotary;
            target.CurrentValue = 1;

            int expected = _rotary.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _rotary;
            target.CurrentValue = _options.Length - 1;

            int expected = _options.Length - 1;

            Assert.Equal(_rotary.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _rotary;
            target.CurrentValue = 0;

            int expected = 0;

            Assert.Equal(_rotary.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTest()
        {
            int targetIndex = 1;
            _rotary.CurrentValue = targetIndex;
            var target = _rotary.ToString();

            string expectedOption = _options[targetIndex];
            string expectedLabel = _testLabel;

            Assert.Contains(expectedOption, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void CopyTest()
        {
            RotarySetting copy = (RotarySetting)_rotary.Copy();

            copy.CurrentValue += 1;

            var target = copy.CurrentValue;
            var notExpected = _rotary.CurrentValue;

            Assert.NotEqual(notExpected, target);
            Assert.IsAssignableFrom<RotarySetting>(copy);
        }

        [Fact()]
        public void CopyDisplayTest()
        {
            RotarySetting copy = (RotarySetting)_rotary.Copy();

            copy.CurrentValue = copy.MaxValue;

            var target = copy.ToString();
            var expected = _options[_options.Length - 1];

            Assert.Contains(expected, target);
        }
    }
}