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

        public Pedal(string maker, string name, EffectType effectType)
        {
            Maker = maker;
            Name = name;
            EffectType = effectType;
            Settings = new List<ISetting>();
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
            var newPedal = new Pedal(Maker, Name, EffectType);
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
            int pedalIndex = -1;
            if (additionalArgs.ContainsKey("preset"))
            {
                preset = (PedalBoardPreset)additionalArgs["preset"];
                if (!additionalArgs.ContainsKey("pedalIndex"))
                {
                    throw new ArgumentException($"'pedalIndex' must be provided in {nameof(additionalArgs)} along with 'preset'");
                }
                pedalIndex = (int)additionalArgs["pedalIndex"];
            }
            while(true)
            {
                Console.WriteLine(this);
                bool engaged;
                if (preset != null) { engaged = preset.EngagedList[pedalIndex]; }
                else { engaged = Engaged; }
                Console.WriteLine(engaged ? "Engaged" : "Not Engaged");
                Console.WriteLine("Settings:");

                var settingIndex = 0;
                foreach (ISetting setting in Settings)
                {
                    string settingString;
                    if (preset != null)
                    {
                        int value = preset.PedalKeepers[pedalIndex][settingIndex].StoredValue;
                        settingString = setting.ToString(value);
                    }
                    else
                    {
                        settingString = setting.ToString();
                    }
                    Console.WriteLine($"{settingIndex + 1}. {settingString}");
                    settingIndex++;
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
                        if (preset.EngagedList[pedalIndex]) { preset.EngagedList[pedalIndex] = false; }
                        else { preset.EngagedList[pedalIndex] = true; }
                        continue;
                    }
                    else
                    {
                        if (Engaged) { Engaged = false; }
                        else { Engaged = true; }
                        continue;

                    }
                }

                int settingIndexInput;
                if(int.TryParse(input, out settingIndexInput))
                {
                    settingIndexInput -= 1;
                    if(settingIndexInput >= 0 && settingIndexInput < Settings.Count)
                    {
                        additionalArgs["settingIndex"] = settingIndexInput;
                        Settings[settingIndexInput].InteractiveViewEdit(checkQuit, additionalArgs);
                        continue;
                    }
                }

                Console.WriteLine("Input not recognized.");
            }
        }
    }
}
