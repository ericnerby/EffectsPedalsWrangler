using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class Builder
    {
        public static Pedal BuildPedal()
        {
            Pedal newPedal;
            Console.Write("What is the name of the pedal?  ");
            var name = Console.ReadLine();

            Console.Write("What is the maker of the pedal?  ");
            var maker = Console.ReadLine();

            EffectType effectType = GetEffectType();

            newPedal = new Pedal(maker, name, effectType);
            AddPedalSettings(newPedal);

            return newPedal;
        }

        private static EffectType GetEffectType()
        {

            Console.WriteLine("What type of Effect is it?\nChoose a number:");
            var index = 0;
            foreach (EffectType type in Enum.GetValues(typeof(EffectType)))
            {
                Console.WriteLine($"{index + 1}. {type}");
                index++;
            }
            var input = Console.ReadLine();
            int typeIndex;
            if(int.TryParse(input, out typeIndex))
            {
                return (EffectType)(typeIndex - 1);
            }
            Console.Write("Please select a number from the list.");
            return GetEffectType();
        }

        private static void AddPedalSettings(Pedal pedal)
        {
            var settingsOptionsText = new string[]
            {
                "Knob with no numbers.",
                "Numbered Knob.",
                "Fixed number of options (rotary or three-way switch).",
                "On/Off switch",
                "Preset list for pedals with preset options."
            };

            var settingsToAdd = new List<ISetting>();

            while(true)
            {
                if(settingsToAdd.Count > 0)
                {
                    Console.WriteLine("\nSettings you've already added:");
                    foreach(ISetting setting in settingsToAdd)
                    {
                        Console.WriteLine(setting);
                    }
                }
                Console.WriteLine("\nPlease add your settings to the pedal");
                for(var i = 0; i < settingsOptionsText.Length; i++)
                {
                    var option = settingsOptionsText[i];
                    Console.WriteLine($"{i + 1}. {option}");
                }

                Console.Write("Choose a number for the setting type.\n"
                    + "Type 'c' to cancel adding setting:  ");
                var input = Console.ReadLine();
                int typeIndex;
                if(int.TryParse(input, out typeIndex))
                {
                    typeIndex -= 1;
                    if (Enum.IsDefined(typeof(SettingType), typeIndex))
                    {
                        settingsToAdd.Add(CreateSetting((SettingType)typeIndex));
                    }
                    else
                    {
                        Console.WriteLine("Please enter your choice from the numers listed.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter your choice as a number.");
                    continue;
                }

                Console.Write("Would you like to add another setting? [N/y]  ");
                input = Console.ReadLine();
                if(input.ToLower() != "y")
                {
                    break;
                }
            }
        }

        private static ISetting CreateSetting(SettingType type)
        {
            Console.WriteLine("What is the label for the setting?");
            var label = Console.ReadLine();
            if (type == SettingType.KnobSetting)
            {
                return CreateKnobSetting(label);
            }
            if (type == SettingType.NumberedKnobSetting)
            {
                return CreateNumberedKnobSetting(label);
            }
            if (type == SettingType.RotarySetting)
            {
                return CreateRotarySetting(label);
            }
            if (type == SettingType.SwitchSetting)
            {
                return new SwitchSetting(label);
            }
            if (type == SettingType.PresetSetting)
            {
                return CreatePresetSetting(label);
            }

            return null;
        }

        private static KnobSetting CreateKnobSetting(string label)
        {
            throw new NotImplementedException();
        }

        private static NumberedKnobSetting CreateNumberedKnobSetting(string label)
        {
            throw new NotImplementedException();
        }

        private static RotarySetting CreateRotarySetting(string label)
        {
            throw new NotImplementedException();
        }

        private static PresetSetting CreatePresetSetting(string label)
        {
            throw new NotImplementedException();
        }
    }

    public enum SettingType
    {
        KnobSetting,
        NumberedKnobSetting,
        RotarySetting,
        SwitchSetting,
        PresetSetting,
    }
}
