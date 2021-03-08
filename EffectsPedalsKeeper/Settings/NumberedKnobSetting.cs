using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Settings
{
    [Serializable()]
    public class NumberedKnobSetting : Setting, ICopyable
    {
        public int MinValueDisplay { get; }
        public int MaxValueDisplay => DisplayToValue(MaxValue);

        public override string CurrentValueDisplay
        {
            get
            {
                double value = ValueToDisplay(CurrentValue);
                return value.ToString("0.0");
            }
        }

        public NumberedKnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {
            MinValueDisplay = minKnobValue;
        }

        public override object Copy()
        {
            return _InternalCopy<NumberedKnobSetting>();
        }

        private double ValueToDisplay(int value) => ((double)value / 10) + MinValueDisplay;
        private int DisplayToValue(double displayValue) => (int)((displayValue - MinValueDisplay) * 10);

        public override void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            Console.WriteLine(this);
            Console.WriteLine($"Minimum Value: {MinValueDisplay}");
            Console.WriteLine($"Maximum Value: {(MaxValue / 10) + MinValueDisplay}");

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
                    int newIntValue = DisplayToValue(newValue);

                    if (newIntValue >= MinValue && newIntValue <= MaxValue)
                    {
                        CurrentValue = newIntValue;
                        break;
                    }
                    Console.WriteLine($"New knob position must be between {MinValueDisplay} and {MaxValueDisplay}.");
                }
                else
                {
                    Console.WriteLine("New knob position must be entered as a decimal or whole number, eg '9.5'.");
                }
            }
        }
    }
}
