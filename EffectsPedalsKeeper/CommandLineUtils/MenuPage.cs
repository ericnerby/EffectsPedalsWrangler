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

        public MenuPage(string startingText, MenuOption[] menuOptions = null)
        {
            StartingText = startingText;
            GlobalOptions = new MenuOption[]
            {
                new MenuOption(ResponseType.DashOption, () => Program.CheckForQuitOrHelp("-q"), "-q", null),
                new MenuOption(ResponseType.DashOption, () => Program.CheckForQuitOrHelp("-h"), "-h", null),
                new MenuOption(ResponseType.DashOption, CallingStatement, "-b", "'-b' to go back to previous screen'")
            };
            if (menuOptions != null)
            {
                MenuOptions = new List<MenuOption>(menuOptions);
            }
            else
            {
                MenuOptions = new List<MenuOption>();
            }
        }

        public virtual void InputLoop(Action callingStatement = null)
        {
            if (callingStatement != null)
            {
                CallingStatement = callingStatement;
            }

            while (true)
            {
                OpeningDisplay();

                InputResponse input = NewInputValidator.ParseInput(Console.ReadLine());
                if (ProcessInput(input, CallingStatement))
                {
                    return;
                }

                Console.WriteLine("Input not recognized. Hit enter to continue. ");
                Console.ReadLine();
            }
        }

        protected virtual void OpeningDisplay()
        {
            Console.Clear();
            Console.WriteLine(StartingText);
            DisplayMenuOptions();
            Console.Write(": ");
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

        protected bool ProcessInput(InputResponse input, Action callingStatement)
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
            }
            return false;
        }
    }

}
