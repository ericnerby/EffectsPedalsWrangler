using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EffectsPedalsKeeper
{
    class Program
    {
        static string _globalOptionsText = "Type '-q' to quit or '-h' for help";
        static string _welcomeText =
            $"Welcome to the Effects Pedals Wrangler.\n{_globalOptionsText}.";

        public static JsonSerializerSettings JsonOptions = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };

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
            //Deserializer fails because Setting is an abstract class

            //if (File.Exists("pedals.json"))
            //{
            //    string fileContents = File.ReadAllText("pedals.json");
            //    List<Pedal> pedalsToAdd = JsonConvert.DeserializeObject<List<Pedal>>(fileContents);
            //    Pedals.AddRange(pedalsToAdd);
            //}
            //if (File.Exists("boards.json"))
            //{
            //    string fileContents = File.ReadAllText("boards.json");
            //    List<PedalBoard> boardsToAdd = JsonConvert.DeserializeObject<List<PedalBoard>>(fileContents);
            //    PedalBoards.AddRange(boardsToAdd);
            //}

            //var demoBuilder = new DemoBuilder();
            //Pedals.AddRange(demoBuilder.DemoPedals);
            //PedalBoards.Add(demoBuilder.BuildDemoBoard());

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
            if(Pedals.Count == 0)
            {
                Console.WriteLine("No pedals have been added yet.");
                return;
            }

            while (true)
            {
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
            foreach (PedalBoard board in PedalBoards)
            {
                Console.WriteLine(board);
            }

            while (true)
            {
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
                }
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
            var newBoard = Builder.BuildBoard(Pedals, CheckForQuitOrHelp);
            PedalBoards.Add(newBoard);
        }

        static void CheckForQuitOrHelp(string input)
        {
            input = input.ToLower();

            if(input == "-q")
            {
                string pedalJsonString = JsonConvert.SerializeObject(Pedals, JsonOptions);
                File.WriteAllTextAsync("pedals.json", pedalJsonString);

                string boardJsonString = JsonConvert.SerializeObject(PedalBoards, JsonOptions);
                File.WriteAllTextAsync("boards.json", boardJsonString);
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
