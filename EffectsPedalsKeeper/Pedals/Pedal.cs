using System;
using System.Collections.Generic;
using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Settings;

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
                if (Settings[i] is ICopyable)
                {
                    var oldSetting = (ICopyable)Settings[i];
                    newSettings[i] = (ISetting)oldSetting.Copy();
                }
            }
            newPedal.AddSettings(newSettings);

            return newPedal;
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            while(true)
            {
                Console.WriteLine(this);
                Console.WriteLine(Engaged ? "Engaged" : "Not Engaged");
                Console.WriteLine("Settings:");

                var index = 1;
                foreach (ISetting setting in Settings)
                {
                    Console.WriteLine($"{index}. {setting}");
                    index++;
                }

                Console.WriteLine("To edit a setting on this pedal, select a number from the above list.");
                Console.WriteLine("'-e' to toggle engaged status | '-b' to go back to previous screen: ");

                var input = Console.ReadLine();

                checkQuit(input);
                if(input.ToLower() == "-b") { return; }

                if(input.ToLower() == "-e")
                {
                    if(Engaged) { Engaged = false; }
                    else { Engaged = true; }
                    continue;
                }

                int settingIndex;
                if(int.TryParse(input, out settingIndex))
                {
                    settingIndex -= 1;
                    if(settingIndex >= 0 && settingIndex < Settings.Count)
                    {
                        Settings[settingIndex].InteractiveViewEdit(checkQuit, null);
                        continue;
                    }
                }

                Console.WriteLine("Input not recognized.");
            }
        }
    }
}
