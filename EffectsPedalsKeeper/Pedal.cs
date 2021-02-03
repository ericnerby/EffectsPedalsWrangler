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
            var startingCount = Settings.Count;
            Settings.AddRange(settings);
            if(startingCount + settings.Count == Settings.Count)
            {
                return true;
            }
            return false;
        }

        public string[] PrintSettingDetails()
        {
            var output = new string[Settings.Count];
            for(var i = 0; i < Settings.Count; i++)
            {
                output[i] = Settings[i].ToString();
            }
            return output;
        }
    }
}
