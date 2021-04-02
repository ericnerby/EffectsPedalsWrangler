using Xunit;
using System.Collections.Generic;
using System.Linq;
using EffectsPedalsKeeperSharedTests.Mocks;

namespace EffectsPedalsKeeperShared.Models.Tests
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

        private List<Pedal> _pedals;
        private string _presetName = "Testing Presets";
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

            _pedals = new List<Pedal>(2) { _testPedalOne, _testPedalTwo };
            _preset = new PedalBoardPreset(_presetName, _pedals);
        }

        [Fact()]
        public void InitializerSettingsCountTest()
        {
            var expected = _pedals.Aggregate(0,
                (total, next) => total + next.Settings.Count);

            var target = _preset.PedalKeepers.Aggregate(0,
                (total, keeper) => total + keeper.Count());

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
        public void PedalsEngagedTest()
        {
            _preset.EngagedList[0] = true;
            Assert.Equal(1, _preset.PedalsEngaged);
        }

        [Fact()]
        public void ToStringTest()
        {
            var target = _preset.ToString();
            var expected = _presetName;

            Assert.Contains(expected, target);
        }
    }
}