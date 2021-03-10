using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Builders
{
    public static class SettingBuilder
    {

        public static NewSetting CreateSetting(SettingType type, Action<string> checkHelpQuit)
        {
            Console.WriteLine("What is the label for the setting?");
            var label = Console.ReadLine();

            checkHelpQuit(label);

            if (type == SettingType.Switch) { return NewSetting.CreateSwitchSetting(label); }
            else if (type == SettingType.Named) { return CreateNamedSetting(label); }
            else if (type == SettingType.ClockFace) { return InteractiveCreateClockFaceSetting(label, checkHelpQuit); }
            else if (type == SettingType.Numbered) { return InteractiveCreateNumberedSetting(label, checkHelpQuit); }

            throw new NotImplementedException($"Missing Implementation for {type}.");
        }

        public static NewSetting InteractiveCreateClockFaceSetting(string label, Action<string> checkHelpQuit)
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

            return NewSetting.CreateClockFaceSetting(label, minValue, maxValue);
        }

        private static NewSetting InteractiveCreateNumberedSetting(string label, Action<string> checkHelpQuit)
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

            return NewSetting.CreateNumberedSetting(label, double.Parse(minValue), double.Parse(maxValue));
        }

        public static NewSetting CreateNamedSetting(string label)
        {
            throw new NotImplementedException();
        }
    }
}
