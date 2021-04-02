using EffectsPedalsKeeperShared.PedalBoards;
using EffectsPedalsKeeperShared.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeperShared.Settings
{
    public class Setting : ISetting
    {
        protected static Regex _clockFormat = new Regex(@"(\d+):(\d{2})");
        protected static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public string Label { get; }
        public SettingType SettingType { get; }
        public int MinValue { get; } = 0;
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
