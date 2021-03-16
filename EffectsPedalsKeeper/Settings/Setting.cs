using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Settings
{
    public class Setting : ISetting
    {
        protected static Regex _clockFormat = new Regex(@"(\d+):(\d{2})");
        protected static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public string Label { get; }
        public SettingType SettingType { get; }
        public int UniqueID { get; }
        [JsonIgnore]
        public int MinValue { get; } = 0;
        [JsonIgnore]
        public int MaxValue => Options.Count - 1;
        public List<string> Options { get; protected set; }

        protected int _currentValue;
        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _currentValue = value;
            }
        }

        public string CurrentValueDisplay => Options[CurrentValue];

        public Setting(string label, SettingType settingType, IList<string> options)
        {
            Label = label;
            SettingType = settingType;
            Options = new List<string>(options);
            UniqueID = IDGenerator.GenerateID();
        }

        [JsonConstructor]
        public Setting(string label, SettingType settingType, IList<string> options, int uniqueID)
        {
            Label = label;
            SettingType = settingType;
            Options = new List<string>(options);
            UniqueID = IDGenerator.PassThroughID(uniqueID);
        }

        public override string ToString() => $"{Label}: {CurrentValueDisplay}";

        public string ToString(int valueToDisplay)
        {
            if (valueToDisplay < MinValue || valueToDisplay > MaxValue)
            {
                throw new IndexOutOfRangeException();
            }
            return $"{Label}: {Options[valueToDisplay]}";
        }

        public object Copy()
        {
            return (Setting)MemberwiseClone();
        }

        // IInteractiveEditable Implementations
        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            if(SettingType == SettingType.Named || SettingType == SettingType.Switch)
            { NamedInteractiveViewEdit(checkQuit, additionalArgs); }
            else if(SettingType == SettingType.ClockFace || SettingType == SettingType.Numbered)
            { RangedInteractiveViewEdit(checkQuit, additionalArgs); }
        }

        private void RangedInteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            PedalBoardPreset preset = null;
            if (additionalArgs.ContainsKey("preset"))
            {
                preset = (PedalBoardPreset)additionalArgs["preset"];
            }
            Regex validator;
            string formatForDisplay;
            if (SettingType == SettingType.ClockFace)
            {
                validator = _clockFormat;
                formatForDisplay = "'h:mm'";
            }
            else
            {
                validator = new Regex(@"\d+.\d");
                formatForDisplay = "'d.d', eg '1.0'";
            }

            if (preset != null)
            {
                int value = preset.SettingValues.Where(value => value.Item == this).First().StoredValue;
                Console.WriteLine(ToString(value));
            }
            else
            {
                Console.WriteLine(this);
            }
            Console.WriteLine($"Minimum Value: {Options[MinValue]}");
            Console.WriteLine($"Maximum Value: {Options[MaxValue]}");

            while (true)
            {
                Console.WriteLine($"Please enter a new position in the format {formatForDisplay}\n"
                                  + "Or enter '-b' to go back to previous screen:  ");
                var input = Console.ReadLine();

                checkQuit(input);

                if (input.ToLower() == "-b") { return; }

                var match = validator.Match(input);
                if (match.Success)
                {
                    var newValue = Options.IndexOf(match.Value);
                    if (newValue == -1)
                    {
                        Console.WriteLine("Position must be between"
                                          + $" {Options[MinValue]} and {Options[MaxValue]}");
                        continue;
                    }
                    if (preset != null)
                    {
                        preset.SettingValues.Where(value => value.Item == this).First().StoredValue = newValue;
                    }
                    else
                    {
                        CurrentValue = newValue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine($"Position must be in format {formatForDisplay}.");
                }
            }
        }

        private void NamedInteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            PedalBoardPreset preset = null;
            if (additionalArgs.ContainsKey("preset"))
            {
                preset = (PedalBoardPreset)additionalArgs["preset"];
            }

            if (preset != null)
            {
                int value = preset.SettingValues.Where(value => value.Item == this).First().StoredValue;
                Console.WriteLine(ToString(value));
            }
            else
            {
                Console.WriteLine(this);
            }

            while (true)
            {
                Console.WriteLine("Options:");
                for (var i = 0; i < Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Options[i]}");
                }
                Console.WriteLine("Please select an option by number from the above list\n"
                                  + "Or enter '-b' to go back to previous page:  ");

                var input = Console.ReadLine();

                if (input.ToLower() == "-b") { return; }

                int newValue;
                if (int.TryParse(input, out newValue))
                {
                    newValue -= 1;
                    if (newValue >= MinValue && newValue <= MaxValue)
                    {
                        if (preset != null)
                        {
                            preset.SettingValues.Where(value => value.Item == this).First().StoredValue = newValue;
                        }
                        else
                        {
                            CurrentValue = newValue;
                        }
                    }
                    Console.WriteLine("Please choose a number from the displayed list.");
                }
                else
                {
                    Console.WriteLine("Please enter your selection as a number.");
                }
            }
        }

        // Static Constructors
        public static Setting CreateClockFaceSetting(string label, string minValue, string maxValue)
        {
            var minMatch = _clockFormat.Match(minValue);
            var maxMatch = _clockFormat.Match(maxValue);

            if(!minMatch.Success || !maxMatch.Success)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(minValue)} and {nameof(maxValue)} must both be string times in the format 'h:mm'.");
            }

            var minHour = int.Parse(minMatch.Groups[1].Value);
            var minMinute = int.Parse(minMatch.Groups[2].Value);
            var maxHour = int.Parse(maxMatch.Groups[1].Value);
            var maxMinute = int.Parse(maxMatch.Groups[2].Value);

            if(minHour > 12 || minHour < 1 || minMinute > 60 || minMinute < 0
                || maxHour > 12 || maxHour < 1 || maxMinute > 60 || maxMinute < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(minValue)} and {nameof(maxValue)} must both be string times in the format 'h:mm'.");
            }

            var minValueInt = _clockFaceConverter.StringTimeToInt(minValue);
            var maxValueInt = _clockFaceConverter.StringTimeToInt(maxValue);

            if(minValueInt >= maxValueInt)
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(minValue)} must be less than {nameof(maxValue)} on the clockface (6:00 is the starting position).");
            }

            var options = new List<string>();

            for(var i = minValueInt; i <= maxValueInt; i++)
            {
                options.Add(_clockFaceConverter.IntToTimeString(i));
            }

            return new Setting(label, SettingType.ClockFace, options);
        }

        public static Setting CreateNumberedSetting(string label, double minVal, double maxVal)
        {
            var options = new List<string>();

            do
            {
                options.Add(minVal.ToString("0.0"));
                minVal += 0.1;
            }
            while (minVal < maxVal);

            return new Setting(label, SettingType.Numbered, options);
        }

        public static Setting CreateSwitchSetting(string label)
        {
            return new Setting(label, SettingType.Switch, new string[] { "Off", "On" });
        }
    }
}
