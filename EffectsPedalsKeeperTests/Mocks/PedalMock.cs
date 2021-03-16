using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    class PedalMock : IPedal
    {
        public PedalMock(string name, string maker,
            EffectType effectType, IList<ISetting> settings)
        {
            Maker = maker;
            Name = name;
            EffectType = effectType;
            Settings = new List<ISetting>(settings);
        }

        public bool Engaged { get; set; }

        public string Maker { get; }

        public string Name { get; }

        public EffectType EffectType { get; }

        public List<ISetting> Settings { get; }

        public int UniqueID => throw new NotImplementedException();

        public bool AddSettings(IList<ISetting> settings)
        {
            throw new NotImplementedException();
        }

        public bool AddSettings(params ISetting[] settings)
        {
            throw new NotImplementedException();
        }

        public object Copy()
        {
            throw new NotImplementedException();
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }

        public string[] PrintSettingDetails()
        {
            throw new NotImplementedException();
        }
    }
}
