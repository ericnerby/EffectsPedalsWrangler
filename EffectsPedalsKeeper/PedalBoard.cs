using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

                int presetIndex;
                if(int.TryParse(input, out presetIndex)
                    && presetIndex >= 1 && presetIndex <= ListVersions().Count)
                {
                    presetIndex -= 1;
                    InteractiveViewEditPreset(checkQuit, presetIndex);
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
            while(true)
            {
                Console.WriteLine("What should the new preset be called? ('-b' to go back) ");
                var input = Console.ReadLine();

                checkQuit(input);
                if(input.ToLower() == "-b") { return; }

                if(string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter a name for the preset.");
                    continue;
                }

                if (SaveAsVersion(input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"There's already a preset with the name '{input}'.\nPlease select a different name.");
                    continue;
                }
            }

            InteractiveViewEditPreset(checkQuit, CheckedOutVersionIndex);
        }

        private void InteractiveViewEditPreset(Action<string> checkQuit, int presetIndex)
        {
            if(presetIndex < 0 || presetIndex >= ListVersions().Count)
            {
                throw new IndexOutOfRangeException();
            }

            CheckOutVersion(presetIndex);

            while(true)
            {
                Console.WriteLine(CheckedOutVersionName);
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));
                Console.WriteLine("Guitar ->");
                int index = 1;
                foreach (Pedal pedal in this)
                {
                    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));
                    Console.WriteLine($"{index}. {pedal}");
                    Console.WriteLine($"Pedal {(pedal.Engaged ? "engaged" : "not engaged")}");
                    Console.WriteLine(string.Concat(Enumerable.Repeat(".", 10)));
                    foreach (NewSetting setting in pedal.Settings)
                    {
                        Console.WriteLine(setting);
                    }
                    Console.WriteLine(string.Concat(Enumerable.Repeat(".", 10)));
                    index++;
                }
                Console.WriteLine("-> Amp");
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));

                Console.WriteLine("Enter pedal number to adjust settings.");
                Console.WriteLine($"'-b' to go back without saving | '-s' to save changes to {CheckedOutVersionName}: ");

                var input = Console.ReadLine();

                checkQuit(input);
                if(input.ToLower() == "-b") { return; }

                if(input.ToLower() == "-s")
                {
                    SaveVersion();
                    return;
                }

                int pedalIndex;
                if(int.TryParse(input, out pedalIndex)
                    && pedalIndex >= 1 && pedalIndex <= Count)
                {
                    pedalIndex -= 1;
                    this[pedalIndex].InteractiveViewEdit(checkQuit, null);
                    continue;
                }
                Console.WriteLine("Input not recognized.");
            }
        }

        public PedalBoard(string name) : base(_CopyMethod)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
