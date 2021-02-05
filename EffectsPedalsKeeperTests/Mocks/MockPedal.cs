using EffectsPedalsKeeper;
using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    class MockPedal : IPedal
    {
        public bool Engaged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Maker => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public List<ISetting> Settings => throw new NotImplementedException();

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
            throw new NotImplementedException();
        }
    }
}
