using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class AssignmentMenuPage<T> : MenuPage
    {
        public AssignmentMenuOption<T> AssignmentMenuOption { get; set; }
        public bool Repeating { get; set; }
        public AssignmentMenuPage(MenuOption[] menuOptions, string startingText, AssignmentMenuOption<T> assignmentMenuOption, bool repeating)
            : base(menuOptions, startingText)
        {
            AssignmentMenuOption = assignmentMenuOption;
            Repeating = repeating;
        }

        public void InputLoop(Action callingStatement, ref T destination)
        {
            CallingStatement = callingStatement;

            OpeningDisplay<object>(null);

            InputResponse input = NewInputValidator.ParseInput(Console.ReadLine());

            if (ProcessInput<object>(input, callingStatement))
            {
                return;
            }

            if (input.ResponseType == AssignmentMenuOption.ResponseType)
            {
                if (AssignmentMenuOption.AssignValue(ref destination, input.Value))
                {
                    if (Repeating)
                    {
                        Console.WriteLine("Value updated. Hit enter to continue. ");
                        Console.ReadLine();
                        InputLoop(callingStatement);
                    }
                    else { return; }
                }
            }

            Console.WriteLine("Input not recognized. Hit enter to continue. ");
            Console.ReadLine();
            InputLoop(callingStatement);
        }

        protected void OpeningDisplay()
        {
            Console.Clear();
            Console.WriteLine(StartingText);
            Console.WriteLine(AssignmentMenuOption.Description);
            DisplayMenuOptions();
            Console.Write(": ");
        }
    }
}
