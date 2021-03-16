using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;

namespace EffectsPedalsKeeper.Pedals
{
    public class Pedal : IPedal
    {
        public bool Engaged { get; set; }

        public string Maker { get; }
        public string Name { get; }
        public EffectType EffectType { get; }

        public List<ISetting> Settings { get; private set; }

        public int UniqueID { get; }

        public Pedal(string maker, string name, EffectType effectType)
        {
            Maker = maker;
            Name = name;
            EffectType = effectType;
            Settings = new List<ISetting>();
            UniqueID = IDGenerator.GenerateID();
        }

        [JsonConstructor]
        public Pedal(string maker, string name, EffectType effectType, int uniqueID)
        {
            Maker = maker;
            Name = name;
            EffectType = effectType;
            Settings = new List<ISetting>();
            UniqueID = IDGenerator.PassThroughID(uniqueID);
        }

        public bool AddSettings(IList<ISetting> settings)
        {
            var startingCount = Settings.Count;
            Settings.AddRange(settings);
            if(startingCount + settings.Count == Settings.Count)
            {
                return true;
            }
            return false;
        }

        public bool AddSettings(params ISetting[] settings)
        {
            var startingCount = Settings.Count;
            Settings.AddRange(settings);
            if (startingCount + settings.Length == Settings.Count)
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

        public override string ToString()
        {
            return $"{Name} by {Maker} ({EffectType})";
        }

        public object Copy()
        {
            var newPedal = new Pedal(Maker, Name, EffectType, UniqueID);
            newPedal.Engaged = Engaged;

            var newSettings = new ISetting[Settings.Count];
            for(var i = 0; i < Settings.Count; i++)
            {
                var oldSetting = Settings[i];
                newSettings[i] = (ISetting)oldSetting.Copy();
            }
            newPedal.AddSettings(newSettings);

            return newPedal;
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            PedalBoardPreset preset = null;
            if(additionalArgs.ContainsKey("preset"))
            {
                preset = (PedalBoardPreset)additionalArgs["preset"];
            }
            while(true)
            {
                Console.WriteLine(this);
                bool engaged;
                if (preset != null) { engaged = preset.EngagedList[this]; }
                else { engaged = Engaged; }
                Console.WriteLine(engaged ? "Engaged" : "Not Engaged");
                Console.WriteLine("Settings:");

                var index = 1;
                foreach (ISetting setting in Settings)
                {
                    string settingString;
                    if (preset != null)
                    {
                        int value = preset.SettingValues.Where(value => value.Item == setting).First().StoredValue;
                        settingString = setting.ToString(value);
                    }
                    else
                    {
                        settingString = setting.ToString();
                    }
                    Console.WriteLine($"{index}. {settingString}");
                    index++;
                }

                Console.WriteLine("To edit a setting on this pedal, select a number from the above list.");
                Console.WriteLine("'-e' to toggle engaged status | '-b' to go back to previous screen: ");

                var input = Console.ReadLine();

                checkQuit(input);
                if(input.ToLower() == "-b") { return; }

                if(input.ToLower() == "-e")
                {
                    if (preset != null)
                    {
                        if (preset.EngagedList[this]) { preset.EngagedList[this] = false; }
                        else { preset.EngagedList[this] = true; }
                        continue;
                    }
                    else
                    {
                        if (Engaged) { Engaged = false; }
                        else { Engaged = true; }
                        continue;

                    }
                }

                int settingIndex;
                if(int.TryParse(input, out settingIndex))
                {
                    settingIndex -= 1;
                    if(settingIndex >= 0 && settingIndex < Settings.Count)
                    {
                        Settings[settingIndex].InteractiveViewEdit(checkQuit, additionalArgs);
                        continue;
                    }
                }

                Console.WriteLine("Input not recognized.");
            }
        }
    }
}
