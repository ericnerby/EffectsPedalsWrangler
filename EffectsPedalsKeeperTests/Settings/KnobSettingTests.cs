using Xunit;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class KnobSettingTests
    {
        private KnobSetting _knobSetting;
        private string _knobSettingLabel = "Treble";

        public KnobSettingTests()
        {
            _knobSetting = new KnobSetting(_knobSettingLabel, "6:30", "5:30");

        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _knobSetting;
            target.CurrentValue = 15;

            int expected = target.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _knobSetting;
            target.CurrentValue = 15;

            int expected = _knobSetting.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _knobSetting;
            target.CurrentValue = _knobSetting.MaxValue;

            int expected = _knobSetting.MaxValue;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _knobSetting;
            target.CurrentValue = _knobSetting.MinValue;

            int expected = _knobSetting.MinValue;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTestMiddleClockFace()
        {
            int targetValue = 72;
            _knobSetting.CurrentValue = targetValue;
            var target = _knobSetting.ToString();

            string expectedDisplayValue = "12:00";
            string expectedLabel = _knobSettingLabel;

            Assert.Contains(expectedDisplayValue, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void CopyTest()
        {
            KnobSetting copy = _knobSetting.Copy();

            copy.CurrentValue += 5;

            var target = copy.CurrentValue;
            var notExpected = _knobSetting.CurrentValue;

            Assert.NotEqual(notExpected, target);
            Assert.IsAssignableFrom<KnobSetting>(copy);
        }
    }
}