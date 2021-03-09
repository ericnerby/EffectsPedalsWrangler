using Xunit;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class NewSettingTests
    {
        private string _namedSettingLabel = "Delay Type";
        private List<string> _namedSettingOptions = new List<string> {
            "Modulated",
            "Slapback",
            "Dotted Eighth",
            "Tape"
        };
        private NewSetting _namedSetting;

        public NewSettingTests()
        {
            _namedSetting = new NewSetting(_namedSettingLabel,
                SettingType.Named, _namedSettingOptions);
        }

        [Fact()]
        public void SwitchConstructorTest()
        {
            var label = "Mid Boost";
            var switchSetting = NewSetting.CreateSwitchSetting(label);

            var expected = label;
            var target = switchSetting.Label;

            Assert.Equal(expected, target);
            Assert.Equal(2, switchSetting.Options.Count);
        }

        [Fact()]
        public void ClockFaceConstructorTest()
        {
            var label = "Drive";
            var lowerLimit = "6:30";
            var upperLimit = "5:30";
            var type = SettingType.ClockFace;
            NewSetting testSetting = NewSetting.CreateClockFaceSetting(label, lowerLimit, upperLimit);

            var expected = lowerLimit;
            var target = testSetting.Options[0];

            Assert.Equal(expected, target);
            Assert.Equal(type, testSetting.SettingType);
        }

        [Fact()]
        public void CurrentValueDisplayTest()
        {
            var target = _namedSetting.CurrentValueDisplay;
            var expected = _namedSettingOptions[0];

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void CurrentValueSetTest()
        {
            var testIndex = 1;

            _namedSetting.CurrentValue = testIndex;

            var target = _namedSetting.CurrentValueDisplay;
            var expected = _namedSettingOptions[testIndex];

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void CurrentValueSetOutOfRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _namedSetting.CurrentValue = _namedSetting.Options.Count);
        }

        [Fact()]
        public void ToStringTest()
        {
            var target = _namedSetting.ToString();

            var expectedLabel = _namedSettingLabel;
            var expectedValue = _namedSettingOptions[0];

            Assert.Contains(expectedLabel, target);
            Assert.Contains(expectedValue, target);
        }

        [Fact()]
        public void CopyTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}