using System;

namespace EffectsPedalsKeeper.Utils
{
    public class ClockFaceConverter
    {
        private int _conversionValue;
        private int _offset = 0;

        public int MaxIntRange { get; protected set; }
        public string StartingPoint => IntToTimeString(0);

        public ClockFaceConverter(PrecisionValue precisionValue)
        {
            _conversionValue = (int)precisionValue;
            MaxIntRange = 720 / _conversionValue;
        }

        public ClockFaceConverter(PrecisionValue precisionValue, string minValue)
        {
            _conversionValue = (int)precisionValue;
            MaxIntRange = 720 / _conversionValue;
            _offset = StringTimeToInt(minValue);
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
            var output = $"{timeDigits[0]}:";
            if (timeDigits[1] == 0) { output += "00";
            }
            else if (timeDigits[1] < 10)
            {
                output += $"0{timeDigits[1]}";
            }
            else
            {
                output += timeDigits[1].ToString();
            }
            return output;
        }

        private int[] _ConvertToClockDigits(int value)
        {
            if (value < 0 || value > MaxIntRange)
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and {nameof(MaxIntRange)}: {MaxIntRange}.");
            }

            value += _offset;

            var result = new int[2];
            int minutes = value * _conversionValue;
            int hours = minutes / 60;

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
            result -= _offset;

            result = (int)(result / _conversionValue);
            if (result < 0)
            {
                result = MaxIntRange + result;
            }
            else if (result == 0)
            {
                result = MaxIntRange;
            }
            return result;
        }
    }

    public enum PrecisionValue : int
    {
        One = 1,
        Five = 5,
        Fifteen = 15,
        Thirty = 30,
        Sixty = 60
    }
}
