using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Settings
{
    public class PresetSetting : Setting, ICopyable
    {
        public override int MaxValue => Options.Count - 1;

        public List<string> Options { get; private set; } = new List<string>();

        public override string CurrentValueDisplay
        {
            get
            {
                if (CurrentValue == -1)
                {
                    return "no preset chosen";
                }
                return Options[CurrentValue];
            }
        }

        public PresetSetting(string label, IList<string> options)
            : base(label, -1, options.Count - 1)
        {
            Options = (List<string>)options;
        }

        public PresetSetting(string label)
            : base(label, -1, -1)
        {}

        public bool AddPreset(string option)
        {
            if(Options.Contains(option))
            {
                return false;
            }
            Options.Add(option);
            return true;
        }

        public void RemovePreset(int index)
        {
            if(index >= Options.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Options.RemoveAt(index);
        }

        public override object Copy()
        {
            return _InternalCopy<PresetSetting>();
        }

        public override void InteractiveChangeSetting(Action<string> checkQuit)
        {
            while(true)
            {
                if(Options.Count == 0)
                {
                    Console.WriteLine("There are no presets for this pedal currently.");
                }
                else
                {
                    Console.WriteLine("Current Pedal Presets:");
                    for (var i = 0; i < Options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Options[i]}");
                    }
                    Console.WriteLine("Enter a number to select a preset from the above list.\n"
                                      + "To delete a preset, enter '-d', a space, and the number. For example: '-d 1'.");
                }
                Console.WriteLine("To add a preset, enter '-a', to go back, enter '-b': ");

                var input = Console.ReadLine().ToLower();

                if (input == "-b") { return; }
                if (input == "-a")
                {
                    InteractiveAddPreset(checkQuit);
                    continue;
                }
                if(InteractiveDeletePreset(input)) { continue; }

                int newValue;
                if(int.TryParse(input, out newValue))
                {
                    newValue -= 1;
                    if (newValue >= MinValue && newValue <= MaxValue)
                    {
                        CurrentValue = newValue;
                        break;
                    }
                    Console.WriteLine("Please enter a number of a preset from the listed items.");
                }
                Console.WriteLine("Your input was not recognized. Please try again.");
            }
        }

        // Return bool indicates whether input equals delete option format, not whether deletion was successful
        public bool InteractiveDeletePreset(string input)
        {
            var delFormat = new Regex(@"-d (\d+)", RegexOptions.IgnoreCase);

            var match = delFormat.Match(input);

            if (match.Success)
            {
                string stringIndex = match.Groups[1].Value;
                int indexToDelete;
                if (int.TryParse(stringIndex, out indexToDelete))
                {
                    indexToDelete -= 1;
                    if (indexToDelete >= MinValue && indexToDelete <= MaxValue)
                    {
                        Options.RemoveAt(indexToDelete);
                        return true;
                    }
                    Console.WriteLine("Please enter a number from the list to delete after typing '-d '.");
                    return true;
                }
            }
            return false;
        }

        public void InteractiveAddPreset(Action<string> checkQuit)
        {
            Console.WriteLine("Add Preset here");
        }
    }
}
