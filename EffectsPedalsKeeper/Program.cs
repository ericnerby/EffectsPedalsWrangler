using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    class Program
    {
        static string WelcomeText =
            "Welcome to the Effects Pedals Wrangler.\n"
            + "At any time, type '-q' to quit or '-h' for help.";

        static string[] MenuItems = 
        {
            "View existing pedals",
            "View existing boards",
            "Add new pedals",
            "Create new board"
        };

        static Action[] MenuActions =
        {
            () => ViewExistingPedals(),
            () => ViewExistingBoards(),
            () => AddNewPedals(),
            () => CreateNewBoard()
        };

        static void ViewExistingPedals()
        {
            Console.WriteLine("Existing Pedals");
        }

        static void ViewExistingBoards()
        {
            Console.WriteLine("Existing Boards");
        }

        static void AddNewPedals()
        {
            while(true)
            {
                Pedals.Add(Builder.BuildPedal());
                Console.Write("Would you like to add another pedal? [N/y]  ");
                var input = Console.ReadLine();
                if(input.ToLower() != "y")
                {
                    break;
                }
            }
        }

        static void CreateNewBoard()
        {
            Console.WriteLine("New Board");
        }

        public static List<Pedal> Pedals = new List<Pedal>();
        public static List<PedalBoard> PedalBoards = new List<PedalBoard>();

        static void Main(string[] args)
        {
            Console.WriteLine(WelcomeText);
            InputLoop();
        }

        static void InputLoop()
        {
            while(true)
            {
                Console.WriteLine("Choose from the following options:");
                for(var i = 0; i < MenuItems.Length; i++)
                {
                    Console.WriteLine($"{(i + 1)}. {MenuItems[i]}");
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
                    if(option >= 0 && option < MenuActions.Length)
                    {
                        MenuActions[option]();
                    }
                    else
                    {
                        Console.WriteLine(
                            "Please choose a number from the list\n"
                            + "Type '-q' to quit or '-h' for help");
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Please enter your option as a number\n"
                        + "Type '-q' to quit or '-h' for help");
                }
            }
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
