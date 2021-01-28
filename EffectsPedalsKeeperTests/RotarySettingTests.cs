using Xunit;

namespace EffectsPedalsKeeper.Tests
{
    public class RotarySettingTests
    {
        static string[] _options = new string[] { "Tape", "Digital", "Echo", "Fade", "Mod"};
        static RotarySetting _rotary = new RotarySetting(_options);

        [Fact()]
        public void DisplayTest()
        {
            string[] target = _rotary.Display();

            for (int i = 0; i < _options.Length; i++)
            {
                Assert.Contains(_options[i], target[i]);
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

            Assert.Equal(_rotary.StepUp(), expected);
        }
    }
}