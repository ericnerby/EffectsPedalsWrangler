using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Builders
{
    public class Builder
    {
        public static ClockFaceConverter ClockConverter = new ClockFaceConverter(PrecisionValue.Five);
        public static MenuAction[] MenuActions = new MenuAction[]
        {
            new MenuAction("-b", "'-b' to go back", MenuStatus.Back),
            new MenuAction("-h", "", MenuStatus.Help),
            new MenuAction("-q", "", MenuStatus.QuitProgram)
        };

        public static bool ClockFaceIsGreaterThan(string lowValue, string highValue) => ClockConverter.StringTimeToInt(lowValue) < ClockConverter.StringTimeToInt(highValue);

        public static PedalBoard BuildBoard(List<Pedal> pedalsAvailable, Action<string> checkHelpQuit)
        {
            PedalBoard newBoard;
            Console.Write("What is the name of the Pedal Board?  ");
            var name = Console.ReadLine();

            newBoard = new PedalBoard(name);

            newBoard.InteractiveAddPedals(checkHelpQuit, pedalsAvailable);

            return newBoard;
        }

        public static Pedal BuildPedal(Action<string> checkHelpQuit)
        {
            Pedal newPedal;
            Console.Write("What is the name of the pedal?  ");
            var name = Console.ReadLine();
            checkHelpQuit(name);

            Console.Write("What is the maker of the pedal?  ");
            var maker = Console.ReadLine();
            checkHelpQuit(maker);

            EffectType effectType = GetEffectType(checkHelpQuit);

            newPedal = new Pedal(maker, name, effectType);
            AddPedalSettings(newPedal, checkHelpQuit);

            return newPedal;
        }

        private static EffectType GetEffectType(Action<string> checkHelpQuit)
        {

            Console.WriteLine("What type of Effect is it?\nChoose a number:");
            var index = 0;
            foreach (EffectType type in Enum.GetValues(typeof(EffectType)))
            {
                Console.WriteLine($"{index + 1}. {type}");
                index++;
            }
            var input = Console.ReadLine();

            checkHelpQuit(input);

            int typeIndex;
            if(int.TryParse(input, out typeIndex))
            {
                return (EffectType)(typeIndex - 1);
            }
            Console.Write("Please select a number from the list.");
            return GetEffectType(checkHelpQuit);
        }

        private static void AddPedalSettings(Pedal pedal, Action<string> checkHelpQuit)
        {
            var settingsOptionsText = new string[]
            {
                "Knob with no numbers.",
                "Numbered Knob.",
                "Fixed number of options (rotary or three-way switch).",
                "On/Off switch"
            };

            var settingsToAdd = new List<ISetting>();

            while(true)
            {
                if(settingsToAdd.Count > 0)
                {
                    Console.WriteLine("\nSettings you've already added:");
                    foreach(Setting setting in settingsToAdd)
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

                checkHelpQuit(input);

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
                        settingsToAdd.Add(SettingBuilder.CreateSetting((SettingType)typeIndex, checkHelpQuit));
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
    }
}
