using Xunit;
using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void CopyTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}