using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper
{
    public class PedalBoard : VersionedList<Pedal>, IInteractiveEditable
    {
        public string Name { get; set; }
        private static Pedal _CopyMethod(Pedal item) => (Pedal)item.Copy();


        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            if(!additionalArgs.ContainsKey("availablePedals"))
            {
                throw new ArgumentNullException($"The {nameof(additionalArgs)} argument in {nameof(PedalBoard.InteractiveViewEdit)} must define the key-value pair 'availablePedals'.");
            }
            var availablePedals = (List<Pedal>)additionalArgs["availablePedals"];

            Console.WriteLine(Name);
            if(Count > 0)
            {
                Console.Write("Signal Chain:\nGuitar -> ");
                foreach(Pedal pedal in this)
                {
                    Console.Write($"{pedal} -> ");
                }
                Console.Write("Amp\n");
            }
            else
            {
                Console.WriteLine("No pedals assigned currently.");
            }

            while(true)
            {
                if (ListVersions().Count > 0)
                {
                    Console.WriteLine("Presets:");
                    foreach (KeyValuePair<int, string> keyValue in ListVersions())
                    {
                        Console.WriteLine($"{keyValue.Key + 1}. {keyValue.Value}");
                    }

                    Console.WriteLine("To view or edit a preset, select a number from the list.");
                }

                Console.WriteLine("To add, delete, or reorder pedals, enter '-p'");
                Console.WriteLine("'-a' to add a preset | '-b' to go back to previous screen: ");

                var input = Console.ReadLine();
                checkQuit(input);

                if (input.ToLower() == "-b") { return; }
                if (input.ToLower() == "-a")
                { 
                    InteractiveNewPreset(checkQuit);
                    continue;
                }
                if (input.ToLower() == "-p")
                {
                    InteractiveEditPedals(checkQuit, availablePedals);
                    continue;
                }
            }
        }

        private void InteractiveEditPedals(Action<string> checkQuit, List<Pedal> availablePedals)
        {
            var selectorFormat = new Regex(@"([a-zA-Z])(\d+)");

            while (true)
            {
                if (Count == 0)
                {
                    Console.WriteLine("No pedals assigned currently.");
                }
                else if (Count > 0)
                {
                    Console.WriteLine("Signal Chain:\nGuitar-> ");
                    var pedalIndex = 1;
                    foreach (Pedal pedal in this)
                    {
                        Console.WriteLine($"{pedalIndex}. {pedal}");
                        pedalIndex++;
                    }
                    Console.WriteLine("->Amp\n");
                    Console.WriteLine("To remove a pedal, enter 'd', followed by the number in the above list, eg. 'd3'.");
                    Console.WriteLine("To move a pedal, enter 'm', followed by the number in the above list.");
                }
                Console.WriteLine("'-a' to add pedals | '-b' to go back:  ");

                var input = Console.ReadLine();

                checkQuit(input);

                if (input.ToLower() == "-b") { return; }

                if (input.ToLower() == "-a")
                {
                    InteractiveAddPedals(checkQuit, availablePedals);
                    continue;
                }

                var match = selectorFormat.Match(input);
                if (match.Success)
                {
                    string option = match.Groups[1].Value.ToLower();
                    int index = int.Parse(match.Groups[2].Value);
                    index -= 1;
                    if (index < 0 || index >= Count)
                    {
                        Console.WriteLine("Please select a valid number from the list.");
                        continue;
                    }

                    if (option == "d")
                    {
                        RemoveAt(index);
                        continue;
                    }
                    else if (option == "m")
                    {
                        Console.WriteLine("Which slot should the pedal be in? (starting with 1): ");
                        int destinationIndex;
                        if(int.TryParse(Console.ReadLine(), out destinationIndex)
                            && destinationIndex >= 1 && destinationIndex <= Count)
                        {
                            destinationIndex -= 1;
                            MoveItem(index, destinationIndex);
                            Console.WriteLine("Pedal moved.");
                            continue;
                        }
                        Console.WriteLine("Please select a valid number from the list.");
                        continue;
                    }
                }
            }
        }

        public void InteractiveAddPedals(Action<string> checkQuit, List<Pedal> availablePedals)
        {
            var pedalsToAdd = new List<Pedal>();
            if (availablePedals.Count == 0)
            {
                Console.WriteLine("There are currently no pedals to add.\n"
                                  + "Please add pedals from the main menu and then "
                                  + "come back and add them to the Board.");
                return;
            }
            while (true)
            {
                Console.WriteLine("Existing Pedals:");
                for (var i = 0; i < availablePedals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availablePedals[i]}");
                }
                Console.WriteLine("Please type a number from the list in the order you want to add to the board.\n"
                                  + "Type '-s' to stop adding pedals.");
                var input = Console.ReadLine();

                checkQuit(input);

                if (input.ToLower() == "-s")
                {
                    break;
                }
                int pedalIndex;
                if (int.TryParse(input, out pedalIndex))
                {
                    pedalIndex -= 1;
                    if (pedalIndex >= 0 && pedalIndex < availablePedals.Count)
                    {
                        pedalsToAdd.Add(availablePedals[pedalIndex]);
                        continue;
                    }
                }
                Console.WriteLine("Please select a number in the list of pedals");
            }

            AddRange(pedalsToAdd);
            Console.WriteLine($"{pedalsToAdd.Count} pedals added to {this}");
        }

        public void InteractiveNewPreset(Action<string> checkQuit)
        {
            throw new NotImplementedException();
        }

        public PedalBoard(string name) : base(_CopyMethod)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
