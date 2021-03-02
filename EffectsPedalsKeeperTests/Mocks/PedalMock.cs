using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    class PedalMock : IPedal
    {
        private List<string> _mockSettingDetails;
        public bool Engaged { get; set; }

        public string Maker { get; }

        public string Name { get; }

        public List<ISetting> Settings => throw new NotImplementedException();

        public EffectType EffectType { get; }

        public PedalMock(string name, string maker,
            EffectType effectType, IList<string> mockSettingDetails)
        {
            Name = name;
            Maker = maker;
            EffectType = effectType;
            _mockSettingDetails = (List<string>)mockSettingDetails;
        }

        public bool AddSettings(IList<ISetting> settings)
        {
            throw new NotImplementedException();
        }

        public bool AddSettings(params ISetting[] settings)
        {
            throw new NotImplementedException();
        }

        public string[] PrintSettingDetails()
        {
            return _mockSettingDetails.ToArray();
        }

        public void InteractiveViewEdit(Action<string> checkQuit)
        {
            throw new NotImplementedException();
        }
    }
}
