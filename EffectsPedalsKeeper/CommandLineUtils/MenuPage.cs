using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class MenuPage
    {
        public string StartingText { get; set; }
        public MenuOption[] GlobalOptions { get; }
        public Action CallingStatement { get; set; }
        public List<MenuOption> MenuOptions { get; protected set; }

        public MenuPage(MenuOption[] menuOptions, string startingText)
        {
            StartingText = startingText;
            GlobalOptions = new MenuOption[]
            {
                new MenuOption(ResponseType.DashOption, () => Program.CheckForQuitOrHelp("-q"), "-q", null),
                new MenuOption(ResponseType.DashOption, () => Program.CheckForQuitOrHelp("-h"), "-h", null),
                new MenuOption(ResponseType.DashOption, CallingStatement, "-b", "'-b' to go back to previous screen'")
            };
            MenuOptions = new List<MenuOption>(menuOptions);
        }

        public virtual void InputLoop(Action callingStatement)
        {
            InputLoop<object>(callingStatement);
        }

        public virtual void InputLoop<T>(Action callingStatement, NumberedMenuOption<T> numberedMenuOption = null)
        {
            CallingStatement = callingStatement;

            OpeningDisplay(numberedMenuOption);

            InputResponse input = NewInputValidator.ParseInput(Console.ReadLine());
            if (ProcessInput(input, callingStatement, numberedMenuOption))
            {
                return;
            }

            Console.WriteLine("Input not recognized. Hit enter to continue. ");
            Console.ReadLine();
            InputLoop(callingStatement, numberedMenuOption);
        }

        protected virtual void OpeningDisplay<T>(NumberedMenuOption<T> numberedMenuOption)
        {
            Console.Clear();
            Console.WriteLine(StartingText);
            if (numberedMenuOption != null)
            {
                DisplayNumberedOptions(numberedMenuOption);
            }
            DisplayMenuOptions();
            Console.Write(": ");
        }

        protected void DisplayNumberedOptions<T>(NumberedMenuOption<T> numberedMenuOption)
        {
            var index = 1;
            foreach (object item in numberedMenuOption.Items)
            {
                Console.WriteLine($"{index}. {item}");
                index++;
            }
            Console.WriteLine(numberedMenuOption.Description);
        }

        protected void DisplayMenuOptions()
        {
            foreach (var menuOption in GlobalOptions)
            {
                if (string.IsNullOrEmpty(menuOption.Description))
                {
                    Console.Write("\n");
                    Console.Write(menuOption.Description);
                }
            }
            foreach (var menuOption in MenuOptions)
            {
                if (string.IsNullOrEmpty(menuOption.Description))
                {
                    Console.Write("\n");
                    Console.Write(menuOption.Description);
                }
            }
        }

        protected bool CheckNumberedOption<T>(string input, NumberedMenuOption<T> numberedMenuOption)
        {
            if (numberedMenuOption.ActOnItem(int.Parse(input)))
            {
                return true;
            }
            return false;
        }

        protected bool ProcessInput<T>(InputResponse input, Action callingStatement, NumberedMenuOption<T> numberedMenuOption = null)
        {
            List<MenuOption> optionsOfMatchingType = new List<MenuOption>(GlobalOptions.Where(option => option.ResponseType == input.ResponseType));
            optionsOfMatchingType.AddRange(MenuOptions.Where(option => option.ResponseType == input.ResponseType));
            if (optionsOfMatchingType.Count > 0)
            {
                foreach (var option in optionsOfMatchingType)
                {
                    if (input.Value == option.Command)
                    {
                        option.Action();
                        return true;
                    }
                }

                if (input.ResponseType == ResponseType.Int
                    && numberedMenuOption != null)
                {
                    if (CheckNumberedOption(input.Value, numberedMenuOption))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
