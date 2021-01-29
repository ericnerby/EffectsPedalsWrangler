using Xunit;
using EffectsPedalsKeeper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Tests
{
    public class KnobSettingTests
    {
        private KnobSetting _knobSetting;
        private KnobSetting _knobNumbered;
        private string _knobSettingLabel = "Treble";
        private string _knobNumberedLabel = "Gain";

        public KnobSettingTests()
        {
            _knobSetting = new KnobSetting(_knobSettingLabel, "6:30", "5:30");
            _knobNumbered = new KnobSetting(_knobNumberedLabel, 1, 10);

        }

        [Fact()]
        public void DisplayTestNumbered()
        {
            int testValue = 108;
            _knobNumbered.CurrentValue = testValue;
            string expected = "7.";
            var target = _knobNumbered.Display();
            Assert.Contains(target, item => item.Contains(expected));
        }

        [Fact()]
        public void DisplayTestClockFace()
        {
            int testValue = 36;
            _knobSetting.CurrentValue = testValue;
            string expected = "9:00";
            var target = _knobSetting.Display();
            Assert.Contains(target, item => item.Contains(expected));
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
        public void ToStringTestMiddleNumbered()
        {
            int targetValue = 72;
            _knobNumbered.CurrentValue = targetValue;
            var target = _knobNumbered.ToString();

            string expectedDisplayValue = "5.5";
            string expectedLabel = _knobNumberedLabel;

            Assert.Contains(expectedDisplayValue, target);
            Assert.Contains(expectedLabel, target);
        }
    }
}