using System;
using System.Collections.Generic;
using EffectsPedalsKeeper.Builders;
using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Pedals;

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
        public static string PedalsFileName = "pedals.json";
        public static string BoardsFileName = "boards.json";

        static void Main(string[] args)
        {
            try
            {
                if (!ListSerializer.DeserializeList(PedalsFileName, Pedals))
                {
                    Console.WriteLine($"There's no {PedalsFileName} file.");
                    DefaultLoaderPrompt(true);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong importing the Pedal data.");
                Console.WriteLine($"Error Message: {e.Message}");
                DefaultLoaderPrompt(true);
            }

            try
            {
                if (!ListSerializer.DeserializeList(BoardsFileName, PedalBoards))
                {
                    DefaultLoaderPrompt(false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong importing the PedalBoard data.");
                Console.WriteLine($"Error Message: {e.Message}");
                DefaultLoaderPrompt(false);
            }

            Console.WriteLine(_welcomeText);
            InputLoop();
        }

        static void DefaultLoaderPrompt(bool loadPedals)
        {
            var demoBuilder = new DemoBuilder();
            if (loadPedals)
            {
                Console.WriteLine("Would you like to load the default Pedals? [Y/n] ");
            }
            else
            {
                Console.WriteLine("Would you like to load the default Board? [Y/n] ");
            }
            var input = Console.ReadLine();

            CheckForQuitOrHelp(input);

            if (input.ToLower() != "n")
            {
                if (loadPedals)
                {
                    Pedals.AddRange(demoBuilder.DemoPedals);
                }
                else
                {
                    PedalBoards.Add(demoBuilder.BuildDemoBoard());
                }
            }

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
                CheckForQuitOrHelp(input);
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
                        Console.WriteLine("(Hit enter to continue) ");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine(
                        $"Please enter your option as a number\n{_globalOptionsText}.");
                    Console.WriteLine("(Hit enter to continue) ");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                Console.Clear();
            }
        }

        static void ViewExistingPedals()
        {
            if(Pedals.Count == 0)
            {
                Console.WriteLine("No pedals have been added yet.");
                return;
            }

            while (true)
            {
                Console.Clear();
                int indexLabel = 1;
                foreach (Pedal pedal in Pedals)
                {
                    Console.WriteLine($"{indexLabel}. {pedal}");
                    indexLabel += 1;
                }
                Console.WriteLine("To view details, enter a number.");
                Console.WriteLine("'-b' to go back to previous screen: ");
                var input = Console.ReadLine();

                CheckForQuitOrHelp(input);

                if (input.ToLower() == "-b") { return; }

                int pedalIndex;
                if (int.TryParse(input, out pedalIndex))
                {
                    pedalIndex -= 1;
                    if (pedalIndex >= 0 && pedalIndex < Pedals.Count)
                    {
                        Pedals[pedalIndex].InteractiveViewEdit(CheckForQuitOrHelp, null);
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid number from the list. (Hit enter to continue) ");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a valid number from the list. (Hit enter to continue) ");
                    Console.ReadLine();
                }
            }
        }

        static void ViewExistingBoards()
        {
            if (PedalBoards.Count == 0)
            {
                Console.WriteLine("No Pedal Boards have been created yet.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Available Pedal Boards:");
                int indexLabel = 1;
                foreach (PedalBoard board in PedalBoards)
                {
                    Console.WriteLine($"{indexLabel}. {board}");
                    indexLabel += 1;
                }
                Console.WriteLine("To view details, enter a number.");
                Console.WriteLine("'-b' to go back to previous screen: ");
                var input = Console.ReadLine();

                CheckForQuitOrHelp(input);

                if (input.ToLower() == "-b") { return; }

                int boardIndex;
                if (int.TryParse(input, out boardIndex))
                {
                    boardIndex -= 1;
                    if (boardIndex >= 0 && boardIndex < Pedals.Count)
                    {
                        var arguments = new Dictionary<string, object>() { { "availablePedals", Pedals} };
                        PedalBoards[boardIndex].InteractiveViewEdit(CheckForQuitOrHelp, arguments);
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid number from the list. (Hit enter to continue) ");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a valid number from the list. (Hit enter to continue) ");
                    Console.ReadLine();
                }
            }
        }

        public static void AddNewPedals()
        {
            while (true)
            {
                Pedals.Add(Builder.BuildPedal(CheckForQuitOrHelp));
                Console.Write("Would you like to add another pedal? [N/y]  ");
                var input = Console.ReadLine();
                CheckForQuitOrHelp(input);
                if (input.ToLower() != "y")
                {
                    break;
                }
            }
        }

        public static void CreateNewBoard()
        {
            var newBoard = Builder.BuildBoard(Pedals, CheckForQuitOrHelp);
            PedalBoards.Add(newBoard);
        }

        public static void CheckForQuitOrHelp(string input)
        {
            input = input.ToLower();

            if(input == "-q")
            {
                ListSerializer.SerializeList(PedalsFileName, Pedals);
                ListSerializer.SerializeList(BoardsFileName, PedalBoards);
                Environment.Exit(0);
            }
            if(input == "-h")
            {
                DisplayHelp();
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("The Help menu goes here.");
            Console.ReadLine();
        }
    }
}
