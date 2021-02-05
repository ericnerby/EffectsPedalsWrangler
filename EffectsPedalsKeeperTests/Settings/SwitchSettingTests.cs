using Xunit;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class SwitchSettingTests
    {
        private SwitchSetting _switch;
        private string _testLabel = "Bright";

        public SwitchSettingTests()
        {
            _switch = new SwitchSetting(_testLabel);
        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _switch;
            target.CurrentValue = 1;

            int expected = _switch.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _switch;
            target.CurrentValue = 0;

            int expected = _switch.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _switch;
            target.CurrentValue = target.MaxValue;

            int expected = target.MaxValue;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _switch;
            target.CurrentValue = target.MinValue;

            int expected = target.MinValue;

            Assert.Equal(_switch.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTest()
        {
            _switch.CurrentValue = 1;
            var target = _switch.ToString();

            string expectedOption = "On";
            string expectedLabel = _testLabel;

            Assert.Contains(expectedOption, target);
            Assert.Contains(expectedLabel, target);
        }
    }
}