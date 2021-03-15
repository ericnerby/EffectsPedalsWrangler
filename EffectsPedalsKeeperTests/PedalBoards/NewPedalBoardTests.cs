using Xunit;
using EffectsPedalsKeeper.PedalBoards;
using System;
using System.Collections.Generic;
using System.Text;
using EffectsPedalsKeeper.Tests.Mocks;
using EffectsPedalsKeeper.Pedals;

namespace EffectsPedalsKeeper.PedalBoards.Tests
{
    public class NewPedalBoardTests
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
        private NewPedalBoard _pedalBoard;

        public NewPedalBoardTests()
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
            _pedalBoard = new NewPedalBoard(_boardName, new IPedal[] { _testPedalOne });
        }

        [Fact()]
        public void NoPedalsConstructorTest()
        {
            var emptyPedalBoard = new NewPedalBoard("Boring Board");

            Assert.Empty(emptyPedalBoard);
        }

        [Fact()]
        public void AddTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ClearTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ContainsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void CopyToTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void IndexOfTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void InsertTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RemoveTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RemoveAtTest()
        {
            Assert.True(false, "This test needs an implementation");
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
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void PresetRemoveAtTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void InteractiveViewEditTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}