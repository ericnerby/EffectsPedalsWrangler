using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;

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
                    InteractiveNewPreset();
                    continue;
                }
                if (input.ToLower() == "-p")
                {
                    InteractiveEditPedals();
                    continue;
                }
                
            }
        }

        private void InteractiveEditPedals()
        {
            throw new NotImplementedException();
        }

        public void InteractiveNewPreset()
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
