using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class ClockFaceConverter
    {
        private int _maxIntRange;
        public int MaxIntRange {
            get
            {
                return _maxIntRange;
            } set
            {
                if (value > 0)
                {
                    _maxIntRange = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be greater than 0.");
                }
            }
        }

        /// <summary>
        ///  Create a ClockFaceConverter to convert between clockface
        ///  and integer values ranging from 1 to the given range.
        /// </summary>
        /// <param name="maxIntRange"></param>
        public ClockFaceConverter()
        {
            _maxIntRange = 144;
        }

        /// <summary>
        ///  Converts value between 1 and 144 to clock face position.
        /// </summary>
        /// <param name="value">int between 1 and 'MaxIntRange' to convert</param>
        /// <returns>int[] with hours and minutes</returns>
        private int[] _ConvertToClockDigits(int value)
        {
            if (value <= 0 || value > MaxIntRange)
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 1 and {nameof(MaxIntRange)}: {MaxIntRange}.");
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

        private int _ConvertFromClockDigits(int hours, int minutes)
        {
            if (hours <= 0 || hours > 12)
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
            if (result < 0) { result = MaxIntRange + result; }
            return result;
        }

        public int StringTimeToInt(string timeString)
        {
            string[] time = timeString.Split(':');
            int[] timeDigits = new int[2];
            if (!int.TryParse(time[0], out timeDigits[0]) || !int.TryParse(time[1], out timeDigits[1]))
            {
                throw new ArgumentException($"{nameof(timeString)} must be in the format '6:55'");
            }
            return _ConvertFromClockDigits(timeDigits[0], timeDigits[1]);
        }

        public string IntToTimeString(int value)
        {
            int[] timeDigits = _ConvertToClockDigits(value);
            return $"{timeDigits[0]}:"
                + ((timeDigits[1] == 0) ? "00" : timeDigits[1].ToString());
        }
    }
}
