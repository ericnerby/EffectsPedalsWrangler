using EffectsPedalsKeeper.Tests.Mocks;
using Xunit;

namespace EffectsPedalsKeeper.Tests
{
    public class PedalTests
    {
        private Pedal _pedal;
        private string _maker = "Ibanez";
        private string _name = "Tubescreamer";
        private string[][] _settingStrings = new string[3][]
        {
            new string[] {"Drive", "6:00"},
            new string[] {"Tone", "3:00"},
            new string[] {"Level", "9:00"}
        };
        private SettingMock[] _mockSettings;

        public PedalTests()
        {
            _pedal = new Pedal(_maker, _name, EffectType.Drive);
            _mockSettings = new SettingMock[_settingStrings.Length];
            for(var i = 0; i < _settingStrings.Length; i++)
            {
                _mockSettings[i] = new SettingMock(_settingStrings[i][0],
                    12, 132, _settingStrings[i][1]);
            }
        }

        [Fact()]
        public void ToStringTest()
        {
            string target = _pedal.ToString();

            string expectedMaker = _maker;
            string expectedName = _name;
            string expectedType = "Drive";

            Assert.Contains(expectedMaker, target);
            Assert.Contains(expectedName, target);
            Assert.Contains(expectedType, target);
        }

        [Fact()]
        public void PrintSettingDetailsTest()
        {
            var target = _pedal;
            target.AddSettings(_mockSettings);

            foreach(string[] settingStringSet in _settingStrings)
            {
                string expected = $"{settingStringSet[0]}: {settingStringSet[1]}";
                Assert.Contains(expected, target.PrintSettingDetails());
            }
        }

        [Fact()]
        public void AddSettingsTest()
        {
            var target = _pedal;
            target.AddSettings(_mockSettings);

            int expected = _mockSettings.Length;

            Assert.Equal(expected, target.Settings.Count);
        }

        [Fact()]
        public void AddSettingsParamsTest()
        {
            var target = _pedal;
            target.AddSettings(
                new SettingMock("Level", 0, 100, "5.5"),
                new SettingMock("Gain", 0, 100, "9.0")
            );

            int expected = 2;

            Assert.Equal(expected, target.Settings.Count);
        }

        [Fact()]
        public void CopyTest()
        {
            _pedal.AddSettings(_mockSettings);
            Pedal copy = _pedal.Copy();

            copy.Settings[2] = new SettingMock("Level", 12, 132, "12:00");

            var notExpected = _pedal.Settings[2];
            var target = copy.Settings[2];

            Assert.NotEqual(notExpected, target);
            Assert.IsAssignableFrom<Pedal>(copy);
        }
    }
}