using EffectsPedalsKeeperShared.Models;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeperSharedTests.Mocks
{
    class PedalMock : Pedal
    {
        public PedalMock(string name, string maker,
            EffectType effectType, IList<Setting> settings)
            : base(maker, name, effectType)
        {
            AddSettings(settings);
        }
    }
}
