using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper
{
    public class Builder
    {
        public static ClockFaceConverter ClockConverter = new ClockFaceConverter(PrecisionValue.Five);

        public static PedalBoard BuildBoard(List<Pedal> pedalsAvailable, Action<string> checkHelpQuit)
        {
            PedalBoard newBoard;
            Console.Write("What is the name of the Pedal Board?  ");
            var name = Console.ReadLine();

            newBoard = new PedalBoard(name);

            newBoard.InteractiveAddPedals(checkHelpQuit, pedalsAvailable);

            return newBoard;
        }

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
                if(input.ToLower() == "c")
                {
                    break;
                }

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

            if(settingsToAdd.Count > 0)
            {
                pedal.AddSettings(settingsToAdd);
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

            throw new NotImplementedException($"Missing Implementation for {type}.");
        }

        private static KnobSetting CreateKnobSetting(string label)
        {
            string minClock;
            int minValue;
            string maxClock;

            Regex clockFormat = new Regex(@"(\d+):(\d{2})");

            while (true)
            {
                Console.WriteLine("What is the lowest clockface value possible on the knob ('7:00')? ");
                var input = Console.ReadLine();
                var match = clockFormat.Match(input);
                if (match.Success)
                {
                    int hours;
                    if(int.TryParse(match.Groups[1].Value, out hours)
                        && hours > 0 && hours <= 12)
                    {
                        minClock = input;
                        minValue = ClockConverter.StringTimeToInt(input);
                        break;
                    }
                }
                Console.WriteLine("Please enter a clockface position in the format 'h:mm'.");
            }

            while (true)
            {
                Console.WriteLine("What is the highest clockface value possible on the knob ('5:00')? ");
                var input = Console.ReadLine();
                var match = clockFormat.Match(input);
                if (match.Success)
                {
                    int hours;
                    if (int.TryParse(match.Groups[1].Value, out hours)
                        && hours > 0 && hours <= 12
                        && ClockConverter.StringTimeToInt(match.Groups[0].Value) > minValue)
                    {
                        maxClock = input;
                        break;
                    }
                }
                Console.WriteLine("Please enter a clockface position in the format 'h:mm'.\n"
                                  + "Value must be greater than min provided.");
            }
            return new KnobSetting(label, minClock, maxClock);
        }

        private static NumberedKnobSetting CreateNumberedKnobSetting(string label)
        {
            int minValue;
            int maxValue;

            while(true)
            {
                Console.WriteLine("What is the lowest number possible on the knob? ");
                var input = Console.ReadLine();
                if(int.TryParse(input, out minValue))
                {
                    break;
                }
                Console.WriteLine("Please enter an integer.");
            }

            while (true)
            {
                Console.WriteLine("What is the highest number possible on the knob? ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out maxValue) && maxValue > minValue)
                {
                    break;
                }
                Console.WriteLine("Please enter an integer greater than the min value given.");
            }
            return new NumberedKnobSetting(label, minValue, maxValue);
        }

        private static RotarySetting CreateRotarySetting(string label)
        {
            List<string> newOptions = new List<string>();
            while(true)
            {
                Console.WriteLine("Please provide the next option\n('-d' when done adding options): ");
                var input = Console.ReadLine();
                if (input.ToLower() == "-d")
                {
                    if(newOptions.Count < 1)
                    {
                        Console.WriteLine("Please provide at least one option.");
                        continue;
                    }
                    break;
                }
                if(!string.IsNullOrEmpty(input))
                {
                    newOptions.Add(input);
                }
            }

            return new RotarySetting(label, newOptions.ToArray());
        }

        private static PresetSetting CreatePresetSetting(string label)
        {
            List<string> presets = new List<string>();
            while(true)
            {
                Console.WriteLine("Please provide the next preset name\n('-d' when done adding presets): ");
                var input = Console.ReadLine();
                if (input.ToLower() == "-d")
                {
                    if (presets.Count < 1)
                    {
                        return new PresetSetting(label);
                    }
                    return new PresetSetting(label, presets);
                }
                if (!string.IsNullOrEmpty(input))
                {
                    presets.Add(input);
                }
            }
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
