﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    /// <summary>
    ///  A knob rotates with no set positions. It can either
    ///  have numbers on the dial, or the position can be
    ///  indicated by position on a clock face, eg. '3:00'.
    /// </summary>
    public class KnobSetting : Setting
    {
        public bool Numbered { get; }

        public string CurrentSettingDisplay
        {
            get
            {
                if (Numbered)
                {
                    return "filler";
                }
                else
                {
                    return IntToTimeString(CurrentValue);
                }
            }
        }

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, StringTimeToInt(minClockPosition), StringTimeToInt(maxClockPosition))
        {
            Numbered = false;
        }

        public KnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0, 
                   (maxKnobValue - minKnobValue) * 10)
        {
            Numbered = true;
        }

        public override string[] Display()
        {
            throw new NotImplementedException();
        }

        public override int StepDown()
        {
            throw new NotImplementedException();
        }

        public override int StepUp()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Label}: {CurrentSettingDisplay}";
        }

        /// <summary>
        ///  Converts value between 1 and 144 to clock face position.
        /// </summary>
        /// <param name="value">int between 1 and 144 to convert</param>
        /// <returns>int[] with hours and minutes</returns>
        private static int[] ConvertToClockDigits(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 1 and 144.");
            }
            var result = new int[2];
            int minutes = value * 5;
            int hours = (int)(minutes / 60);

            // Since 0 would be 6:00, add 6 hours
            hours += 6;
            if (hours > 12) { hours -= 12; }

            minutes = minutes % 60;
            result[0] = hours;
            result[1] = minutes;
            return result;
        }

        private static int ConvertFromClockDigits(int hours, int minutes)
        {
            if(hours <= 0 || hours > 12)
            {
                throw new ArgumentOutOfRangeException($"{nameof(hours)} must be between 1-12");
            }
            if (minutes < 0 || minutes > 60)
            {
                throw new ArgumentOutOfRangeException($"{nameof(minutes)} must be between 0-60");
            }
            int result = hours * 60;
            result += minutes;

            // Since 6:00 would be 0, subtract 6hr * 60min
            result -= 360;

            result = (int)(result / 5);
            if (result < 0) { result = 144 + result; }
            return result;
        }

        private static string IntToTimeString(int value)
        {
            int[] timeDigits = ConvertToClockDigits(value);
            if (timeDigits[1] == 0)
            {
                return $"{timeDigits[0]}:00";
            }
            return $"{timeDigits[0]}:{timeDigits[1]}";
        }

        private static int StringTimeToInt(string timeString)
        {
            string[] time = timeString.Split(':');
            int[] timeDigits = new int[2];
            if (!int.TryParse(time[0], out timeDigits[0]) || !int.TryParse(time[1], out timeDigits[1]))
            {
                throw new ArgumentException($"{nameof(timeString)} must be in the format '6:55'");
            }
            return ConvertFromClockDigits(timeDigits[0], timeDigits[1]);
        }
    }
}
