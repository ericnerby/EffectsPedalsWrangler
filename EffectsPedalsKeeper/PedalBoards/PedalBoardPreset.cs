using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.PedalBoards
{
    public class PedalBoardPreset
    {
        public string Name { get; set; }
        public List<ValueKeeper<ISetting>> SettingValues;
        public Dictionary<IPedal, bool> EngagedList;

        public PedalBoardPreset(string name, IList<IPedal> pedals)
        {
            Name = name;
            EngagedList = new Dictionary<IPedal, bool>();
            SettingValues = new List<ValueKeeper<ISetting>>();

            foreach(IPedal pedal in pedals)
            {
                EngagedList.Add(pedal, pedal.Engaged);
                var valueKeepersToAdd = new List<ValueKeeper<ISetting>>(pedal.Settings.Count);
                foreach (ISetting setting in pedal.Settings)
                {
                    valueKeepersToAdd.Add(new ValueKeeper<ISetting>(setting));
                }
                SettingValues.AddRange(valueKeepersToAdd);
            }
        }
    }
}
