using Xunit;
using EffectsPedalsKeeper.PedalBoards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EffectsPedalsKeeper.Tests.Mocks;
using EffectsPedalsKeeper.Pedals;

namespace EffectsPedalsKeeper.PedalBoards.Tests
{
    public class PedalBoardTests
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
        private string _boardName = "My Awesome PedalBoard";
        private PedalBoard _pedalBoard;

        public PedalBoardTests()
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
            _pedalBoard = new PedalBoard(_boardName, new IPedal[] { _testPedalOne });
        }

        [Fact()]
        public void NoPedalsConstructorTest()
        {
            var emptyPedalBoard = new PedalBoard("Boring Board");

            Assert.Empty(emptyPedalBoard);
        }

        [Fact()]
        public void AddTest()
        {
            _pedalBoard.PresetAdd("My Awesome Preset");

            _pedalBoard.Add(_testPedalTwo);

            Assert.Equal(2, _pedalBoard.Count);

            var expected = _pedalBoard.Aggregate(0, (total, pedal) => total + pedal.Settings.Count);
            var target = _pedalBoard.Presets[0].SettingValues.Count;

            Assert.Equal(expected, target);

            expected = _pedalBoard.Count;
            target = _pedalBoard.Presets[0].EngagedList.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void ClearTest()
        {
            _pedalBoard.PresetAdd("My Awesome Preset");

            _pedalBoard.Clear();

            Assert.Empty(_pedalBoard);
            Assert.Empty(_pedalBoard.Presets);
        }

        [Fact()]
        public void ContainsTest()
        {
            Assert.Contains(_testPedalOne, _pedalBoard);
        }

        [Fact()]
        public void CopyToTest()
        {
            _pedalBoard.Add(_testPedalTwo);

            var newList = new IPedal[_pedalBoard.Count];
            _pedalBoard.CopyTo(newList, 0);

            var target = newList.Count();
            var expected = _pedalBoard.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            Assert.IsAssignableFrom<IEnumerator<IPedal>>(_pedalBoard.GetEnumerator());
        }

        [Fact()]
        public void IndexOfTest()
        {
            var expected = 0;
            var target = _pedalBoard.IndexOf(_testPedalOne);

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void InsertTest()
        {
            var testIndex = 0;
            _pedalBoard.Insert(testIndex, _testPedalTwo);

            Assert.Equal(testIndex, _pedalBoard.IndexOf(_testPedalTwo));
        }

        [Fact()]
        public void RemoveTest()
        {
            _pedalBoard.Add(_testPedalTwo);
            _pedalBoard.PresetAdd("My Awesome Preset");

            _pedalBoard.Remove(_testPedalOne);

            var pedalOneSettings = _testPedalOne.Settings;
            var target = _pedalBoard.Presets[0].SettingValues.Where(value => pedalOneSettings.Contains(value.Item));

            Assert.Empty(target);
            Assert.Equal(_testPedalTwo.Settings.Count, _pedalBoard.Presets[0].SettingValues.Count);
        }

        [Fact()]
        public void RemoveAtTest()
        {
            _pedalBoard.Add(_testPedalTwo);
            _pedalBoard.PresetAdd("My Awesome Preset");

            _pedalBoard.RemoveAt(0);

            var pedalOneSettings = _testPedalOne.Settings;
            var target = _pedalBoard.Presets[0].SettingValues.Where(value => pedalOneSettings.Contains(value.Item));

            Assert.Empty(target);
        }

        [Fact()]
        public void PresetAddTest()
        {
            _pedalBoard.PresetAdd("Awesome Preset");

            var expected = _testPedalOne.Settings.Count;
            var target = _pedalBoard.Presets[0].SettingValues.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void PresetRemoveTest()
        {
            _pedalBoard.PresetAdd("Awesome Preset");

            var presetToRemove = _pedalBoard.Presets[0];

            _pedalBoard.PresetRemove(presetToRemove);

            Assert.Empty(_pedalBoard.Presets);
        }

        [Fact()]
        public void PresetRemoveAtTest()
        {
            _pedalBoard.PresetAdd("Awesome Preset");

            _pedalBoard.PresetRemoveAt(0);

            Assert.Empty(_pedalBoard.Presets);
        }
    }
}