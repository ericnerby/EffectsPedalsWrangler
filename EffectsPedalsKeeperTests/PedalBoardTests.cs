using EffectsPedalsKeeper;
using Xunit;
using EffectsPedalsKeeper.Tests.Mocks;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests
{
    public class PedalBoardTests
    {
        private PedalBoard _pedalBoard;
        private string _boardName = "Tour the Test";
        private IPreset _presetMock;
        private List<IPedal> _pedalMocks;
        private List<List<string>> _pedalNames = new List<List<string>>() {
            new List<string>() {"TubeScreamer", "Ibanez"},
            new List<string>() {"Bad Stone", "Electro-Harmonix"},
            new List<string>() {"Carbon Copy", "MXR"},
        };
        private List<EffectType> _pedalEffectTypes = new List<EffectType>() {
            EffectType.Drive,
            EffectType.Mod,
            EffectType.Delay
        };
        private List<List<string>> _pedalSettings = new List<List<string>>()
        {
            new List<string>() {"Drive: 11:30", "Tone: 1:00", "Level: 8:00"},
            new List<string>() {"Rate: 3:00", "Feedback: 7:30", "Manual Shift: 12:00"},
            new List<string>() {"Regen: 10:00", "Mix: 12:00", "Delay: 9:00"},
        };

        public PedalBoardTests()
        {
            _pedalMocks = new List<IPedal>();
            for (var i = 0; i < _pedalNames.Count; i++)
            {
                _pedalMocks.Add(new PedalMock(
                    _pedalNames[i][0], _pedalNames[i][1],
                    _pedalEffectTypes[i], _pedalSettings[i]));
            }
            _presetMock = new PresetMock(_boardName, _pedalMocks);
            _pedalBoard = new PedalBoard();
        }

        [Fact()]
        public void CheckOutPresetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SavePresetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SaveAsPresetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void NewPresetTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddPedalsTest()
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
        public void RemoveAtTest()
        {
            Assert.True(false, "This test needs an implementation");
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
        public void RemoveTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SaveCurrentPedalsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}