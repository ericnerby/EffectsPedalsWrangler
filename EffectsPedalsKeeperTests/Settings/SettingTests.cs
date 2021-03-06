﻿using Xunit;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class SettingTests
    {
        private string _namedSettingLabel = "Delay Type";
        private List<string> _namedSettingOptions = new List<string> {
            "Modulated",
            "Slapback",
            "Dotted Eighth",
            "Tape"
        };
        private Setting _namedSetting;

        public SettingTests()
        {
            _namedSetting = new Setting(_namedSettingLabel,
                SettingType.Named, _namedSettingOptions);
        }

        [Fact()]
        public void SwitchConstructorTest()
        {
            var label = "Mid Boost";
            var switchSetting = Setting.CreateSwitchSetting(label);

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
            Setting testSetting = Setting.CreateClockFaceSetting(label, lowerLimit, upperLimit);

            var expected = lowerLimit;
            var target = testSetting.Options[0];

            Assert.Equal(expected, target);
            Assert.Equal(type, testSetting.SettingType);
        }

        [Fact()]
        public void NumberedConstructorTest()
        {
            var label = "Tone";
            var lowerLimit = 1;
            var upperLimit = 10;
            var type = SettingType.Numbered;
            Setting numberedSetting = Setting.CreateNumberedSetting(label, lowerLimit, upperLimit);

            Assert.Equal(type, numberedSetting.SettingType);

            string minDisplay = "1.0";

            Assert.Contains(minDisplay, numberedSetting.CurrentValueDisplay);

            string maxDisplay = "10.0";
            numberedSetting.CurrentValue = numberedSetting.MaxValue;

            Assert.Contains(maxDisplay, numberedSetting.CurrentValueDisplay);
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
        public void ToStringWithValueArgumentTest()
        {
            var testValue = 2;

            _namedSetting.CurrentValue = testValue - 1;

            var target = _namedSetting.ToString(testValue);
            var expected = _namedSettingOptions[testValue];

            Assert.Contains(expected, target);
        }

        [Fact()]
        public void ToStringWithValueOutOfRangeTest()
        {
            var outOfRangeValue = _namedSetting.Options.Count;
            Assert.Throws<IndexOutOfRangeException>(() => _namedSetting.ToString(outOfRangeValue));
        }

        [Fact()]
        public void CopyTest()
        {
            var original = _namedSetting;
            Setting copy = (Setting)_namedSetting.Copy();

            copy.CurrentValue += 1;

            Assert.NotEqual(copy.CurrentValue, original.CurrentValue);
            Assert.Equal(original.Options.Count, copy.Options.Count);
        }
    }
}