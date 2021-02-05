using Xunit;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings.Tests
{
    public class PresetSettingTests
    {
        protected List<string> _presets = new List<string> { "Edge", "Slapback", "Etherial", "Reverse", "Spooky" };
        private string _testLabel = "Preset";
        private PresetSetting _preset;

        public PresetSettingTests()
        {
            _preset = new PresetSetting(_testLabel, _presets);
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
            var target = _preset;
            target.CurrentValue = 1;

            int expected = _preset.CurrentValue - 1;

            Assert.Equal(target.StepDown(), expected);
        }

        [Fact()]
        public void StepUpTest()
        {
            var target = _preset;
            target.CurrentValue = 1;

            int expected = _preset.CurrentValue + 1;

            Assert.Equal(target.StepUp(), expected);
        }

        [Fact()]
        public void StepUpAlreadyAtMaxValueTest()
        {
            var target = _preset;
            target.CurrentValue = _preset.MaxValue;

            int expected = _preset.MaxValue;

            Assert.Equal(_preset.StepUp(), expected);
        }

        [Fact()]
        public void StepDownAlreadyAtMinValueTest()
        {
            var target = _preset;
            target.CurrentValue = _preset.MinValue;

            int expected = _preset.MinValue;

            Assert.Equal(_preset.StepDown(), expected);
        }

        [Fact()]
        public void ToStringTest()
        {
            int targetIndex = 1;
            _preset.CurrentValue = targetIndex;
            var target = _preset.ToString();

            string expectedOption = _presets[targetIndex];
            string expectedLabel = _testLabel;

            Assert.Contains(expectedOption, target);
            Assert.Contains(expectedLabel, target);
        }

        [Fact()]
        public void AddPresetTest()
        {
            var target = _preset;
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
            var target = _preset;
            int startingMaxValue = target.MaxValue;
            int expectedMaxValue = startingMaxValue - 1;
            int indexToRemove = 1;
            string removedItem = target.Options[indexToRemove];

            target.RemovePreset(indexToRemove);

            Assert.Equal(expectedMaxValue, target.MaxValue);
            Assert.DoesNotContain(removedItem, target.Options);

        }
    }
}
