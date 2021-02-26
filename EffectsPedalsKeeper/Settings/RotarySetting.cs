using System;

namespace EffectsPedalsKeeper.Settings
{
    public class RotarySetting : Setting, ICopyable
    {
        public string[] Options { get; private set; }

        public override string CurrentValueDisplay => Options[CurrentValue];

        public RotarySetting(string label, string[] options)
            : base(label, 0, options.Length - 1)
        {
            Options = options;
        }

        public override object Copy()
        {
            return _InternalCopy<RotarySetting>();
        }

        public override void InteractiveChangeSetting(Action<string> checkQuit)
        {
            while(true)
            {
                Console.WriteLine("Options:");
                for(var i = 0; i < Options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Options[i]}");
                }
                Console.WriteLine("Please select an option by number from the above list\n"
                                  + "Or enter '-b' to go back to previous page:  ");

                var input = Console.ReadLine();

                if(input.ToLower() == "-b") { return; }

                int newValue;
                if(int.TryParse(input, out newValue))
                {
                    newValue -= 1;
                    if (newValue >= MinValue && newValue <= MaxValue)
                    {
                        CurrentValue = newValue;
                        break;
                    }
                    Console.WriteLine("Please choose a number from the displayed list.");
                }
                else
                {
                    Console.WriteLine("Please enter your selection as a number.");
                }
            }
        }
    }
}
