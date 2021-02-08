using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class PedalBoard
    {
        public IPreset WorkingPreset { get; }
        public List<IPreset> PresetList { get; }

        public void CheckOutPreset(int index)
        {
            throw new NotImplementedException();
        }

        public bool SavePreset()
        {
            throw new NotImplementedException();
        }

        public void NewPreset(string newPresetName)
        {
            throw new NotImplementedException();
        }

        public bool AddPedals(params IPedal[] pedals)
        {
            throw new NotImplementedException();
        }
    }
}
