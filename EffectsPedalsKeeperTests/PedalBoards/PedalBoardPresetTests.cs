using Xunit;
using System.Collections.Generic;
using System.Linq;
using EffectsPedalsKeeper.Tests.Mocks;
using EffectsPedalsKeeper.Pedals;

namespace EffectsPedalsKeeper.PedalBoards.Tests
{
    public class PedalBoardPresetTests
    {
        private string[] _settingsOptions = new string[]
            {"6:30", "8:30", "10:30", "12:30", "2:30", "4:30", "5:30"};

        private SettingMock[] _pedalOneSettings;
        private string[] _pedalOneSettingsLabels = new string[]
            {"Gain", "Treble", "Output"};

        private SettingMock[] _pedalTwoSettings;
        private string[] _pedalTwoSettingsLabels = new string[]
            {"Level", "Tone", "Gain"};

        private PedalMock _testPedalOne;
        private string _pedalOneName = "Centaur";
        private string _pedalOneMaker = "Klon";
        private EffectType _pedalOneType = EffectType.Drive;

        private PedalMock _testPedalTwo;
        private string _pedalTwoName = "Blues Driver";
        private string _pedalTwoMaker = "Boss";
        private EffectType _pedalTwoType = EffectType.Drive;

        private int _startingValue = 3;

        private List<IPedal> _pedals;
        private PedalBoardPreset _preset;

        public PedalBoardPresetTests()
        {
            _pedalOneSettings = new SettingMock[]
            {
                new SettingMock(_pedalOneSettingsLabels[0], _settingsOptions),
                new SettingMock(_pedalOneSettingsLabels[1], _settingsOptions),
                new SettingMock(_pedalOneSettingsLabels[2], _settingsOptions)
            };

            _pedalTwoSettings = new SettingMock[]
            {
                new SettingMock(_pedalTwoSettingsLabels[0], _settingsOptions),
                new SettingMock(_pedalTwoSettingsLabels[1], _settingsOptions),
                new SettingMock(_pedalTwoSettingsLabels[2], _settingsOptions)
            };

            _testPedalOne = new PedalMock(_pedalOneName, _pedalOneMaker, _pedalOneType, _pedalOneSettings);
            _testPedalTwo = new PedalMock(_pedalTwoName, _pedalTwoMaker, _pedalTwoType, _pedalTwoSettings);

            _testPedalOne.Settings.ForEach(setting => setting.CurrentValue = _startingValue);
            _testPedalTwo.Settings.ForEach(setting => setting.CurrentValue = _startingValue);

            _pedals = new List<IPedal>(2) { _testPedalOne, _testPedalTwo };
            _preset = new PedalBoardPreset("Testing Presets", _pedals);
        }

        [Fact()]
        public void InitializerSettingsCountTest()
        {
            var expected = _pedals.Aggregate(0,
                (total, next) => total + next.Settings.Count);

            var target = _preset.SettingValues.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void InitializerEngagedCountTest()
        {
            var expected = _pedals.Count;
            var target = _preset.EngagedList.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void TwoSettingsWithSameLabelButDifferentPedalTest()
        {
            var testingPedal = _pedals[0];
            var testingSetting = testingPedal.Settings[0];
            testingSetting.CurrentValue += 1;

            var preset = new PedalBoardPreset("Testing Preset Two", _pedals);

            var testingValue = preset.SettingValues.Where(settingValue => settingValue.Item == testingSetting).ToArray();

            Assert.Single(testingValue);

            int expected = testingSetting.CurrentValue;
            int target = testingValue[0].StoredValue;

            Assert.Equal(expected, target);
        }
    }
}