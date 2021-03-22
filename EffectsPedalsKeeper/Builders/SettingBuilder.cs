using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Builders
{
    public static class SettingBuilder
    {

        public static Setting CreateSetting(SettingType type, Action<string> checkHelpQuit, IList<ISetting> existingSettings)
        {
            Console.WriteLine("What is the label for the setting?");
            var label = Console.ReadLine();
            if (existingSettings.Any(setting => setting.Label == label))
            {
                Console.Clear();
                Console.WriteLine($"There's already a setting with the label {label}. Setting labels must be unique per pedal.");
                return CreateSetting(type, checkHelpQuit, existingSettings);
            }
            if (string.IsNullOrEmpty(label))
            {
                Console.Clear();
                Console.WriteLine($"The setting label can't be blank.");
                return CreateSetting(type, checkHelpQuit, existingSettings);
            }

            checkHelpQuit(label);

            if (type == SettingType.Switch) { return Setting.CreateSwitchSetting(label); }
            else if (type == SettingType.Named) { return CreateNamedSetting(label, checkHelpQuit); }
            else if (type == SettingType.ClockFace) { return InteractiveCreateClockFaceSetting(label, checkHelpQuit); }
            else if (type == SettingType.Numbered) { return InteractiveCreateNumberedSetting(label, checkHelpQuit); }

            throw new NotImplementedException($"Missing Implementation for {type}.");
        }

        public static Setting InteractiveCreateClockFaceSetting(string label, Action<string> checkHelpQuit)
        {
            var validator = new Regex(@"(\d+):(\d{2})");
            string formatForDisplay = "'h:mm'";

            var formatValidator = new Criterion(
                $"Must be in the format {formatForDisplay}",
                (string input) => validator.IsMatch(input)
            );

            var inputValidator = new InputValidator(
                $"What is the lowest value possible (in format {formatForDisplay})?",
                new Criterion[] { formatValidator },
                Builder.MenuActions
            );

            var response = inputValidator.LoopGetValidatedInput();
            if (response.MenuStatus == MenuStatus.QuitProgram)
            { checkHelpQuit("-q"); }
            if (response.MenuStatus == MenuStatus.Help)
            {
                checkHelpQuit("-h");
                return InteractiveCreateClockFaceSetting(label, checkHelpQuit);
            }
            var minValue = response.Result;

            Criterion valueValidator = new Criterion(
                "MaxValue must be greater than MinValue",
                (string input) => Builder.ClockFaceIsGreaterThan(minValue, input));

            inputValidator.MenuPrompt = $"What is the highest value possible (in format {formatForDisplay})?";
            inputValidator.Criteria.Add(valueValidator);

            response = inputValidator.LoopGetValidatedInput();
            if (response.MenuStatus == MenuStatus.QuitProgram)
            { checkHelpQuit("-q"); }
            if (response.MenuStatus == MenuStatus.Help)
            {
                checkHelpQuit("-h");
                return InteractiveCreateClockFaceSetting(label, checkHelpQuit);
            }
            var maxValue = response.Result;

            return Setting.CreateClockFaceSetting(label, minValue, maxValue);
        }

        private static Setting InteractiveCreateNumberedSetting(string label, Action<string> checkHelpQuit)
        {
            Regex validator = new Regex(@"(\d+).(\d)");
            string formatForDisplay = "'d.d', eg '1.0'";

            var formatValidator = new Criterion(
                $"Must be in the format {formatForDisplay}",
                (string input) => validator.IsMatch(input)
            );

            var inputValidator = new InputValidator(
                $"What is the lowest value possible (in format {formatForDisplay})?",
                new Criterion[] { formatValidator },
                Builder.MenuActions
            );

            var response = inputValidator.LoopGetValidatedInput();
            if (response.MenuStatus == MenuStatus.QuitProgram)
            { checkHelpQuit("-q"); }
            if (response.MenuStatus == MenuStatus.Help)
            {
                checkHelpQuit("-h");
                return InteractiveCreateNumberedSetting(label, checkHelpQuit);
            }
            var minValue = response.Result;

            Criterion valueValidator = new Criterion(
                    "MaxValue must be greater than MinValue",
                    (string input) => double.Parse(minValue) < double.Parse(input));

            inputValidator.MenuPrompt = $"What is the highest value possible (in format {formatForDisplay})?";
            inputValidator.Criteria.Add(valueValidator);

            response = inputValidator.LoopGetValidatedInput();
            if (response.MenuStatus == MenuStatus.QuitProgram)
            { checkHelpQuit("-q"); }
            if (response.MenuStatus == MenuStatus.Help)
            {
                checkHelpQuit("-h");
                return InteractiveCreateNumberedSetting(label, checkHelpQuit);
            }
            var maxValue = response.Result;

            return Setting.CreateNumberedSetting(label, double.Parse(minValue), double.Parse(maxValue));
        }

        public static Setting CreateNamedSetting(string label, Action<string> checkHelpQuit)
        {
            var options = new List<string>();

            while(true)
            {
                Console.WriteLine("Enter a name for the next option.");
                Console.WriteLine("'-d' when done: ");

                var input = Console.ReadLine();

                checkHelpQuit(input);

                if (input.ToLower() == "-d")
                {
                    if (options.Count < 2)
                    {
                        Console.WriteLine("You must enter at least two options.");
                        continue;
                    }
                    break;
                }

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Name must not be blank.");
                    continue;
                }

                if (options.Contains(input))
                {
                    Console.WriteLine($"There's already an option with the name {input}.");
                }

                options.Add(input);
            }

            return new Setting(label, SettingType.Named, options);
        }
    }
}
