using Xunit;

namespace EffectsPedalsKeeper.Tests
{
    public class RotarySettingTests
    {
        private string[] _options;
        private RotarySetting _rotary;

        public RotarySettingTests()
        {
            _options = new string[] { "Tape", "Digital", "Echo", "Fade", "Mod" };
            _rotary = new RotarySetting(_options);
        }

        [Fact()]
        public void DisplayTest()
        {
            for(int index = 0; index < _options.Length; index++)
            {
                _rotary.CurrentValue = index;
                string expected = _options[index];
                string[] target = _rotary.Display();
                Assert.Contains(target, item => item.Contains(expected));
            }
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
        public void StepUpAlreadyAtMaxValue()
        {
            var target = _rotary;
            target.CurrentValue = _options.Length - 1;

            int expected = _options.Length - 1;

            Assert.Equal(_rotary.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValue()
        {
            var target = _rotary;
            target.CurrentValue = 0;

            int expected = 0;

            Assert.Equal(_rotary.StepDown(), expected);
        }
    }
}