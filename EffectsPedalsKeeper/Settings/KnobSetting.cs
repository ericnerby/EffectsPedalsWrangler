using EffectsPedalsKeeper.Utils;
using System;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.Settings
{
    public class KnobSetting : Setting, ICopyable
    {
        private static ClockFaceConverter _clockFaceConverter = new ClockFaceConverter(PrecisionValue.Five);

        public override string CurrentValueDisplay => _clockFaceConverter.IntToTimeString(CurrentValue);

        public KnobSetting(string label, string minClockPosition, string maxClockPosition)
            : base(label, _clockFaceConverter.StringTimeToInt(minClockPosition), _clockFaceConverter.StringTimeToInt(maxClockPosition))
        {}

        public override object Copy()
        {
            return _InternalCopy<KnobSetting>();
        }

        public override void InteractiveViewEdit(Action<string> checkQuit)
        {
            var timeValidation = new Regex(@"\d+:\d{2}");

            Console.WriteLine(this);
            Console.WriteLine($"Minimum Value: {_clockFaceConverter.IntToTimeString(MinValue)}");
            Console.WriteLine($"Maximum Value: {_clockFaceConverter.IntToTimeString(MaxValue)}");

            while (true)
            {
                Console.WriteLine("Please enter a new ClockFace position in the format 'h:mm'\n"
                                  + "Or enter '-b' to go back to previous screen:  ");
                var input = Console.ReadLine();

                checkQuit(input);

                if (input.ToLower() == "-b") { return; }

                var match = timeValidation.Match(input);
                if (match.Success)
                {
                    int hours;
                    if (int.TryParse(match.Groups[1].Value, out hours)
                        && hours > 0 && hours <= 12)
                    {
                        var newValue = _clockFaceConverter.StringTimeToInt(input);
                        if(newValue >= MinValue && newValue <= MaxValue)
                        {
                            CurrentValue = newValue;
                            break;
                        }
                    }
                    Console.WriteLine("ClockFace position must be between"
                                      + $" {_clockFaceConverter.IntToTimeString(MinValue)}" 
                                      + $" and {_clockFaceConverter.IntToTimeString(MaxValue)}");
                }
                else
                {
                    Console.WriteLine("ClockFace position must be in format 'h:mm'.");
                }
            }
        }
    }
}
