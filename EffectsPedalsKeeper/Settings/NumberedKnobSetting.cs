using System;

namespace EffectsPedalsKeeper.Settings
{
    public class NumberedKnobSetting : Setting, ICopyable
    {
        private int _minKnobValue;

        public override string CurrentValueDisplay
        {
            get
            {
                double value = ((double)CurrentValue / 10) + _minKnobValue;
                return value.ToString("0.0");
            }
        }

        public NumberedKnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {
            _minKnobValue = minKnobValue;
        }

        public override object Copy()
        {
            return _InternalCopy<NumberedKnobSetting>();
        }

        public override void InteractiveChangeSetting(Action<string> checkQuit)
        {
            while (true)
            {
                Console.WriteLine("Please enter a new knob position as a decimal or whole number\n"
                                  + "Or enter '-b' to go back to previous screen:  ");
                var input = Console.ReadLine();

                checkQuit(input);

                if (input.ToLower() == "-b") { return; }

                double newValue;
                if(double.TryParse(input, out newValue))
                {
                    var newIntValue = (int)(newValue - _minKnobValue) * 10;

                    if (newIntValue >= MinValue && newIntValue <= MaxValue)
                    {
                        CurrentValue = newIntValue;
                        break;
                    }
                    Console.WriteLine($"New knob position must be between {_minKnobValue} and {(MaxValue / 10) + _minKnobValue}.");
                }
                else
                {
                    Console.WriteLine("New knob position must be entered as a decimal or whole number, eg '9.5'.");
                }
            }
        }
    }
}
