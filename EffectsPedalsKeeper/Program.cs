using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    class Program
    {
        public static string WelcomeText =
            "Welcome to the Effects Pedals Wrangler.\n"
            + "At any time, type '-q' to quit or '-h' for help.";

        public static string[] MenuItems = 
        {
            "View existing pedals",
            "View existing boards",
            "Add new pedals",
            "Create new board"
        };

        static void Main(string[] args)
        {
            var pedals = new List<Pedal>();
            var pedalBoards = new List<PedalBoard>();

            Console.WriteLine(WelcomeText);
            InputLoop();
        }

        public static void InputLoop()
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
            }
        }

        public static bool CheckForQuitOrHelp(string input)
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
