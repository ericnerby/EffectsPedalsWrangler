using Xunit;
using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class NewSettingTests
    {
        private string _clockFaceLabel = "Drive";
        private string _clockFaceLowerLimit = "6:30";
        private string _clockFaceUpperLimit = "5:30";
        private NewSetting _clockFaceSetting;

        public NewSettingTests()
        {
            _clockFaceSetting = new NewSetting(_clockFaceLabel, SettingType.ClockFace,
                _clockFaceLowerLimit, _clockFaceUpperLimit);

        }
        [Fact()]
        public void ClockFaceConstructorTest()
        {
            var label = "Drive";
            var lowerLimit = "6:30";
            var upperLimit = "5:30";
            var type = SettingType.ClockFace;
            var testSetting = new NewSetting(label, type, lowerLimit, upperLimit);

            var expected = lowerLimit;
            var target = testSetting.Options[0];

            Assert.Equal(expected, target);
            Assert.Equal(type, testSetting.SettingType);
        }

        [Fact()]
        public void CopyTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}