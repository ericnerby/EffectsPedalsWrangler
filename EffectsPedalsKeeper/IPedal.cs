using EffectsPedalsKeeper.Settings;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public interface IPedal
    {
        public bool Engaged { get; set; }

        public string Maker { get; }
        public string Name { get; }

        public List<ISetting> Settings { get; }

        public bool AddSettings(IList<ISetting> settings);

        public bool AddSettings(params ISetting[] settings);

        public string[] PrintSettingDetails();
    }
}
