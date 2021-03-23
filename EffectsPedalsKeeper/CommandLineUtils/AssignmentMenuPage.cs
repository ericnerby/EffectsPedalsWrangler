using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class AssignmentMenuPage<T> : MenuPage
    {
        public string AssignmentDescription { get; }
        public Func<T, bool> ValueValidator { get; }
        public Func<string, T> ValueConverter { get; }
        public ResponseType ResponseType { get; }
        public bool Repeating { get; }
        public AssignmentMenuPage(string startingText, string assignmentDesription, bool repeating,
            ResponseType responseType, Func<T, bool> valueValidator, Func<string, T> valueConverter,
            MenuOption[] menuOptions = null)
            : base(startingText, menuOptions)
        {
            AssignmentDescription = assignmentDesription;
            ValueValidator = valueValidator;
            ValueConverter = valueConverter;
            ResponseType = responseType;
            Repeating = repeating;
        }

        public void InputLoop(Action callingStatement, ref T destination)
        {
            while (true)
            {
                CallingStatement = callingStatement;

                OpeningDisplay();

                InputResponse input = NewInputValidator.ParseInput(Console.ReadLine());

                if (ProcessInput(input, callingStatement))
                {
                    return;
                }

                if (input.ResponseType == ResponseType)
                {
                    if (AssignValue(ref destination, input.Value))
                    {
                        if (Repeating)
                        {
                            Console.WriteLine("Value updated. Hit enter to continue. ");
                            Console.ReadLine();
                            continue;
                        }
                        else { return; }
                    }
                }

                Console.WriteLine("Input not recognized. Hit enter to continue. ");
                Console.ReadLine();
            }
        }

        public bool AssignValue(ref T destination, string value)
        {
            T convertedValue = ValueConverter(value);
            if (ValueValidator(convertedValue))
            {
                destination = convertedValue;
                return true;
            }
            return false;
        }

        protected override void OpeningDisplay()
        {
            Console.Clear();
            Console.WriteLine(StartingText);
            Console.Write(AssignmentDescription);
            DisplayMenuOptions();
            Console.Write(": ");
        }
    }
}
