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

        public void InteractiveViewEdit(Action<string> checkQuit)
        {
            Console.WriteLine(Name);
            if(Count > 0)
            {
                Console.Write("Signal Chain:\nGuitar-> ");
                foreach(Pedal pedal in this)
                {
                    Console.Write($"{pedal} ");
                }
                Console.Write("->Amp\n");
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
                    InteractiveEditPedals(checkQuit);
                    continue;
                }
            }
        }

        private void InteractiveEditPedals(Action<string> checkQuit)
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
                        Console.Write($"{pedalIndex}. {pedal} ");
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
                        Console.WriteLine("Which pedal should it be before? (0 for first) ");
                        int destinationIndex;
                        if(int.TryParse(Console.ReadLine(), out destinationIndex)
                            && destinationIndex >= 0 && destinationIndex < Count)
                        {
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
