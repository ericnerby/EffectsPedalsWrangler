using EffectsPedalsKeeper.Tests.Mocks;
using System.Collections.Generic;
using Xunit;

namespace EffectsPedalsKeeper.Tests
{
    public class PresetTests
    {
        private Preset _preset;
        private string _presetName = "Test the System";
        private List<IPedal> _pedals;
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

        public PresetTests()
        {
            _preset = new Preset(_presetName);
            _pedals = new List<IPedal>();
            for (var i = 0; i < _pedalNames.Count; i++)
            {
                _pedals.Add(new MockPedal(
                    _pedalNames[i][0], _pedalNames[i][1],
                    _pedalEffectTypes[i], _pedalSettings[i]));
            }
        }

        private void _AddPedals(Preset preset, List<IPedal> pedals)
        {
            foreach (IPedal pedal in pedals)
            {
                preset.Add(pedal);
            }
        }

        [Fact()]
        public void AddTest()
        {
            _preset.Add(_pedals[0]);
            Assert.NotEmpty(_preset);
        }

        [Fact()]
        public void ClearTest()
        {
            _preset.Add(_pedals[0]);
            _preset.Clear();
            Assert.Empty(_preset);
        }

        [Fact()]
        public void ContainsTest()
        {
            _preset.Add(_pedals[0]);
            _preset.Add(_pedals[1]);
            Assert.Contains(_pedals[0], _preset);
        }

        [Fact()]
        public void CopyToTest()
        {
            _AddPedals(_preset, _pedals);

            var testArray = new IPedal[_preset.Count];
            _preset.CopyTo(testArray, 0);
            Assert.All(testArray, pedal => Assert.NotNull(pedal));
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            _AddPedals(_preset, _pedals);

            Assert.IsAssignableFrom<IEnumerator<IPedal>>(_preset.GetEnumerator());
        }

        [Fact()]
        public void IndexOfTest()
        {
            _AddPedals(_preset, _pedals);

            var testIndex = 1;
            var expected = testIndex;
            var target = _preset.IndexOf(_pedals[testIndex]);
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void InsertTest()
        {
            _preset.Add(_pedals[0]);
            _preset.Add(_pedals[2]);
            _preset.Insert(1, _pedals[1]);

            var expected = _pedals[1];
            var target = _preset[1];

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void RemoveTest()
        {
            _AddPedals(_preset, _pedals);

            var pedalToRemove = _pedals[1];

            _preset.Remove(pedalToRemove);

            Assert.DoesNotContain(pedalToRemove, _preset);
        }

        [Fact()]
        public void RemoveAtTest()
        {
            _AddPedals(_preset, _pedals);

            var indexToRemove = 1;
            var pedalToRemove = _pedals[indexToRemove];

            _preset.RemoveAt(indexToRemove);

            Assert.DoesNotContain(pedalToRemove, _preset);
        }

        [Fact()]
        public void ToStringTest()
        {
            var expectedName = _presetName;
            var target = _preset.ToString();

            Assert.Contains(expectedName, target);
        }
    }
}