using Xunit;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class PresetSettingTests
    {
        protected List<string> _presets = new List<string> { "Edge", "Slapback", "Etherial", "Reverse", "Spooky" };
        private string _testLabel = "Preset";
        private PresetSetting _presetSetting;

        public PresetSettingTests()
        {
            _presetSetting = new PresetSetting(_testLabel, _presets);
        }

        [Fact()]
        public void ConstructorWithNoListTest()
        {
            var noListPreset = new PresetSetting("Preset");
            Assert.NotNull(noListPreset);
        }

        [Fact()]
        public void StepDownTest()
        {
            var target = _presetSetting;
            target.CurrentValue = 1;

            int expected = _presetSetting.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _presetSetting;
            target.CurrentValue = 1;

            int expected = _presetSetting.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _presetSetting;
            target.CurrentValue = _presetSetting.MaxValue;

            int expected = _presetSetting.MaxValue;

            Assert.Equal(_presetSetting.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _presetSetting;
            target.CurrentValue = _presetSetting.MinValue;

            int expected = _presetSetting.MinValue;

            Assert.Equal(_presetSetting.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTest()
        {
            int targetIndex = 1;
            _presetSetting.CurrentValue = targetIndex;
            var target = _presetSetting.ToString();

            string expectedOption = _presets[targetIndex];
            string expectedLabel = _testLabel;

            Assert.Contains(expectedOption, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void AddPresetTest()
        {
            var target = _presetSetting;
            int startingMaxValue = target.MaxValue;
            int expectedMaxValue = startingMaxValue + 1;
            string newPreset = "New Preset";

            target.AddPreset(newPreset);

            Assert.Equal(expectedMaxValue, target.MaxValue);
            Assert.Contains(newPreset, target.Options);
        }

        [Fact()]
        public void RemovePresetTest()
        {
            var target = _presetSetting;
            int startingMaxValue = target.MaxValue;
            int expectedMaxValue = startingMaxValue - 1;
            int indexToRemove = 1;
            string removedItem = target.Options[indexToRemove];

            target.RemovePreset(indexToRemove);

            Assert.Equal(expectedMaxValue, target.MaxValue);
            Assert.DoesNotContain(removedItem, target.Options);
        }

        [Fact()]
        public void CopyTest()
        {
            PresetSetting copy = _presetSetting.Copy();

            copy.CurrentValue += 1;

            var target = copy.CurrentValue;
            var notExpected = _presetSetting.CurrentValue;

            Assert.NotEqual(notExpected, target);
            Assert.IsAssignableFrom<PresetSetting>(copy);
        }

        [Fact()]
        public void CopyDisplayTest()
        {
            PresetSetting copy = _presetSetting.Copy();

            copy.CurrentValue = copy.MaxValue;

            var target = copy.ToString();
            var expected = _presets[_presets.Count - 1];

            Assert.Contains(expected, target);
        }
    }
}
