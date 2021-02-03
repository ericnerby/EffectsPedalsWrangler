using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class Pedal
    {
        public bool Engaged { get; set; }

        public string Maker { get; }
        public string Name { get; }

        public List<Setting> Settings { get; private set; }

        public Pedal(string maker, string name)
        { }

        public bool AddSettings(IList<Setting> settings)
        {
            throw new NotImplementedException();
        }

        public string[] PrintSettingDetails()
        {
            throw new NotImplementedException();
        }

        public string[] DisplayPedal()
        {
            throw new NotImplementedException();
        }

    }
}
