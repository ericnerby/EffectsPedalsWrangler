using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    class Program
    {
        static string _globalOptionsText = "Type '-q' to quit or '-h' for help";
        static string _welcomeText =
            $"Welcome to the Effects Pedals Wrangler.\n{_globalOptionsText}.";

        static string[] _menuItems = 
        {
            "View existing pedals",
            "View existing boards",
            "Add new pedals",
            "Create new board"
        };

        static Action[] _menuActions =
        {
            () => ViewExistingPedals(),
            () => ViewExistingBoards(),
            () => AddNewPedals(),
            () => CreateNewBoard()
        };

        public static List<Pedal> Pedals = new List<Pedal>();
        public static List<PedalBoard> PedalBoards = new List<PedalBoard>();

        static void Main(string[] args)
        {
            Console.WriteLine(_welcomeText);
            InputLoop();
        }

        static void InputLoop()
        {
            while(true)
            {
                Console.WriteLine("Choose from the following options:");
                for(var i = 0; i < _menuItems.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {_menuItems[i]}");
                }
                var input = Console.ReadLine();
                if(CheckForQuitOrHelp(input))
                {
                    break;
                }
                int option;
                if(int.TryParse(input, out option))
                {
                    option -= 1;
                    if(option >= 0 && option < _menuActions.Length)
                    {
                        _menuActions[option]();
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Please choose a number from the list\n{_globalOptionsText}.");
                    }
                }
                else
                {
                    Console.WriteLine(
                        $"Please enter your option as a number\n{_globalOptionsText}.");
                }
            }
        }

        static void ViewExistingPedals()
        {
            if(Pedals.Count > 0)
            {
                foreach (Pedal pedal in Pedals)
                {
                    Console.WriteLine($"{pedal}");
                }
            }
            else
            {
                Console.WriteLine("No pedals have been added yet.");
            }
        }

        static void ViewExistingBoards()
        {
            if (PedalBoards.Count > 0)
            {
                foreach (PedalBoard board in PedalBoards)
                {
                    Console.WriteLine(board);
                }
            }
            else
            {
                Console.WriteLine("No Pedal Boards have been created yet.");
            }
        }

        static void AddNewPedals()
        {
            while (true)
            {
                Pedals.Add(Builder.BuildPedal());
                Console.Write("Would you like to add another pedal? [N/y]  ");
                var input = Console.ReadLine();
                if (input.ToLower() != "y")
                {
                    break;
                }
            }
        }

        static void CreateNewBoard()
        {
            Builder.BuildBoard(Pedals);
        }

        static bool CheckForQuitOrHelp(string input)
        {
            input = input.ToUpper();

            if(input == "-Q")
            {
                return true;
            }
            if(input == "-H")
            {
                DisplayHelp();
            }
            return false;
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("The Help menu goes here.");
            Console.ReadLine();
        }
    }
}
