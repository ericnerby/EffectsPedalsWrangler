using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class ListMenuPage<T> : MenuPage
    {
        public string NumberedListDescription { get; }
        public IList<T> Items { get; }
        public Action<T> ItemAction { get; }

        public ListMenuPage(string startingText, string numberedListDescription,
            IList<T> items, Action<T> itemAction, MenuOption[] menuOptions = null)
            : base(startingText, menuOptions)
        {
            NumberedListDescription = numberedListDescription;
            Items = items;
            ItemAction = itemAction;
        }
        public override void InputLoop(Action callingStatement = null)
        {
            if (callingStatement != null)
            {
                CallingStatement = callingStatement;
            }

            OpeningDisplay();

            InputResponse input = NewInputValidator.ParseInput(Console.ReadLine());
            if (ProcessInput(input, callingStatement))
            {
                return;
            }

            if (input.ResponseType == ResponseType.Int)
            {
                if (TakeNumberedOption(input.Value))
                {
                    return;
                }
            }

            Console.WriteLine("Input not recognized. Hit enter to continue. ");
            Console.ReadLine();
            InputLoop(callingStatement);
        }

        protected override void OpeningDisplay()
        {
            Console.Clear();
            Console.WriteLine(StartingText);
            DisplayNumberedOptions();
            DisplayMenuOptions();
            Console.Write(": ");
        }

        protected void DisplayNumberedOptions()
        {
            var index = 1;
            foreach (object item in Items)
            {
                Console.WriteLine($"{index}. {item}");
                index++;
            }
            Console.WriteLine(NumberedListDescription);
        }

        protected bool TakeNumberedOption(string input)
        {
            var index = int.Parse(input);
            index -= 1;
            if (index >= 0 && index < Items.Count)
            {
                var item = Items[index];
                ItemAction(item);
                return true;
            }
            return false;
        }
    }
}
