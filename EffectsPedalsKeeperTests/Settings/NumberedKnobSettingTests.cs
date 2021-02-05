using Xunit;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class NumberedKnobSettingTests
    {
        private NumberedKnobSetting _numberedKnobSetting;
        private string _numberedKnobSettingLabel = "Gain";

        public NumberedKnobSettingTests()
        {
            _numberedKnobSetting = new NumberedKnobSetting(_numberedKnobSettingLabel, 1, 10);
        }

        [Fact()]
        public void DisplayTest()
        {
            int testValue = 0;
            _numberedKnobSetting.CurrentValue = testValue;
            string expected = "1.0";
            var target = _numberedKnobSetting.Display();
            Assert.Contains(target, item => item.Contains(expected));
        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _numberedKnobSetting;
            target.CurrentValue = 15;

            int expected = target.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _numberedKnobSetting;
            target.CurrentValue = 15;

            int expected = _numberedKnobSetting.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _numberedKnobSetting;
            target.CurrentValue = _numberedKnobSetting.MaxValue;

            int expected = _numberedKnobSetting.MaxValue;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _numberedKnobSetting;
            target.CurrentValue = _numberedKnobSetting.MinValue;

            int expected = _numberedKnobSetting.MinValue;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTestMiddle()
        {
            int targetValue = _numberedKnobSetting.MaxValue / 2;
            _numberedKnobSetting.CurrentValue = targetValue;
            var target = _numberedKnobSetting.ToString();

            string expectedDisplayValue = "5.5";
            string expectedLabel = _numberedKnobSettingLabel;

            Assert.Contains(expectedDisplayValue, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void ToStringTestMax()
        {
            int targetValue = _numberedKnobSetting.MaxValue;
            _numberedKnobSetting.CurrentValue = targetValue;
            var target = _numberedKnobSetting.ToString();

            string expectedDisplayValue = "10.0";
            string expectedLabel = _numberedKnobSettingLabel;

            Assert.Contains(expectedDisplayValue, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void ToStringTestMin()
        {
            int targetValue = _numberedKnobSetting.MinValue;
            _numberedKnobSetting.CurrentValue = targetValue;
            var target = _numberedKnobSetting.ToString();

            string expectedDisplayValue = "1.0";
            string expectedLabel = _numberedKnobSettingLabel;

            Assert.Contains(expectedDisplayValue, target);
            Assert.Contains(expectedLabel, target);
        }
    }
}