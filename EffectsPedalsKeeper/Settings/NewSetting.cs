﻿using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Settings
{
    public class NewSetting : ICopyable
    {
        protected static Regex _clockFormat = new Regex(@"(\d+):(\d{2})");
        protected static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public string Label { get; }
        public SettingType SettingType { get; }
        public int MinValue { get; } = 0;
        public int MaxValue => Options.Count - 1;
        public List<string> Options { get; protected set; }

        public string CurrentValueDisplay => Options[CurrentValue];

        protected int _currentValue;
        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if(value < MinValue || value > MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _currentValue = value;
            }
        }

        public NewSetting(string label, SettingType settingType, IList<string> options)
        {
            Label = label;
            SettingType = settingType;
            Options = new List<string>(options);
        }

        public object Copy()
        {
            throw new NotImplementedException();
        }

        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }

        // Static Constructors
        public static NewSetting CreateClockFaceSetting(string label, string minValue, string maxValue)
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

            return new NewSetting(label, SettingType.ClockFace, options);
        }
    }

    public enum SettingType
    {
        ClockFace,
        Numbered,
        Named,
        Switch
    }
}
