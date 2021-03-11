﻿using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    class PedalMock : Pedal
    {
        private List<string> _mockSettingDetails;

        public PedalMock(string name, string maker,
            EffectType effectType, IList<string> mockSettingDetails)
            : base(maker, name, effectType)
        {
            _mockSettingDetails = (List<string>)mockSettingDetails;
        }

        public new bool AddSettings(IList<ISetting> settings)
        {
            throw new NotImplementedException();
        }

        public new bool AddSettings(params ISetting[] settings)
        {
            throw new NotImplementedException();
        }
    }
}
