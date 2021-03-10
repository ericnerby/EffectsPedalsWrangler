using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Utils
{
    public class InputValidator
    {
        public List<Criterion> Criteria { get; set; }
        public List<MenuAction> MenuActions { get; set; }
        public string MenuPrompt { get; set; }

        public InputValidator(string menuPrompt, IList<Criterion> criteria, IList<MenuAction> menuActions)
        {
            MenuPrompt = menuPrompt;
            Criteria = new List<Criterion>(criteria);
            MenuActions = new List<MenuAction>(menuActions);
        }

        public ValidatorResponse LoopGetValidatedInput()
        {
            while(true)
            {
                Console.WriteLine(MenuPrompt);
                foreach(MenuAction menuAction in MenuActions)
                {
                    if (menuAction.Instructions != "")
                    {
                        Console.WriteLine(menuAction.Instructions);
                    }
                }
                Console.WriteLine(": ");
                var input = Console.ReadLine();

                foreach(MenuAction menuAction in MenuActions)
                {
                    if (input.ToLower() == menuAction.MenuCommand)
                    {
                        return new ValidatorResponse(menuAction.MenuStatus, null);
                    }
                }

                foreach(Criterion criterion in Criteria)
                {
                    if (!criterion.CheckFunction(input))
                    {
                        Console.WriteLine(criterion.MessageIfNotFollowed);
                        continue;
                    }
                }

                return new ValidatorResponse(MenuStatus.InputReceived, input);
            }
        }
    }

    public class ValidatorResponse
    {
        public MenuStatus MenuStatus { get; }
        public string Result { get; }

        public ValidatorResponse(MenuStatus menuStatus, string result)
        {
            MenuStatus = menuStatus;
            Result = result;
        }
    }

    public class Criterion
    {
        public string MessageIfNotFollowed { get; set; }
        public Func<string, bool> CheckFunction { get; set; }

        public Criterion(string messageIfNotFollowed, Func<string, bool> checkFunction)
        {
            MessageIfNotFollowed = messageIfNotFollowed;
            CheckFunction = checkFunction;
        }
    }

    public class MenuAction
    {
        private string _menuCommand;
        public string MenuCommand
        {
            get
            {
                return _menuCommand;
            }
            set
            {
                _menuCommand = value.ToLower();
            }
        }

        public string Instructions { get; set; }

        public MenuStatus MenuStatus { get; set; }

        public MenuAction(string menuCommand, string instructions, MenuStatus menuStatus)
        {
            MenuCommand = menuCommand;
            Instructions = instructions;
            MenuStatus = menuStatus;
        }
    }

    public enum MenuStatus
    {
        Back,
        QuitProgram,
        Help,
        InputReceived
    }
}
