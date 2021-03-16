using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using EffectsPedalsKeeper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeper.PedalBoards
{
    [JsonObject]
    public class PedalBoard : IList<IPedal>, IInteractiveEditable, IUniqueID
    {
        public string Name { get; set; }
        public List<PedalBoardPreset> Presets { get; private set; }
        [JsonProperty]
        public int UniqueID { get; }

        public PedalBoard(string name)
        {
            Name = name;
            _pedals = new List<IPedal>();
            Presets = new List<PedalBoardPreset>();
            UniqueID = IDGenerator.GenerateID();
        }

        public PedalBoard(string name, IList<IPedal> pedals)
        {
            Name = name;
            if(pedals.Count > 0) { _pedals = new List<IPedal>(pedals); }
            else { _pedals = new List<IPedal>(); }
            Presets = new List<PedalBoardPreset>();
            UniqueID = IDGenerator.GenerateID();
        }

        [JsonConstructor]
        public PedalBoard(string name, IList<IPedal> pedals, IList<PedalBoardPreset> presets, int uniqueID)
        {
            Name = name;
            if (pedals.Count > 0) { _pedals = new List<IPedal>(pedals); }
            else { _pedals = new List<IPedal>(); }
            Presets = new List<PedalBoardPreset>(presets);
            UniqueID = IDGenerator.PassThroughID(uniqueID);
        }

        public override string ToString() => $"{Name} | Number of Pedals: {Count}";

        public bool PresetAdd(string name)
        {
            if (Presets.Any(preset => preset.Name == name))
            {
                return false;
            }
            Presets.Add(new PedalBoardPreset(name, _pedals));
            return true;
        }

        public bool PresetRemove(PedalBoardPreset preset) => Presets.Remove(preset);

        public void PresetRemoveAt(int index) => Presets.RemoveAt(index);

        private void ExpandPresetOptions(IPedal pedal)
        {

            foreach (PedalBoardPreset preset in Presets)
            {
                var valueKeepers = new List<ValueKeeper<ISetting>>(pedal.Settings.Count);
                foreach (ISetting setting in pedal.Settings)
                {
                    valueKeepers.Add(new ValueKeeper<ISetting>(setting));
                }
                preset.SettingValues.AddRange(valueKeepers);
                preset.EngagedList.Add(pedal, pedal.Engaged);
            }
        }

        private void RemovePresetOptions(IPedal pedal)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                foreach (ISetting setting in pedal.Settings)
                {
                    preset.SettingValues.RemoveAll(value => value.Item == setting);
                }
            }
        }

        //IList Implementation
        private List<IPedal> _pedals;

        public IPedal this[int index]
        {
            get => _pedals[index];
            set
            {
                if (value == this[index])
                {
                    this[index] = value;
                }
                else
                {
                    RemoveAt(index);
                    Insert(index, value);
                }
            }
        }

        public int Count => _pedals.Count;

        public bool IsReadOnly => false;

        public void Add(IPedal item)
        {
            ExpandPresetOptions(item);
            _pedals.Add(item);
        }

        public void Clear()
        {
            _pedals.Clear();
            Presets.Clear();
        }

        public bool Contains(IPedal item) => _pedals.Contains(item);

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            _pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator() => _pedals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(IPedal item) => _pedals.IndexOf(item);

        public void Insert(int index, IPedal item)
        {
            ExpandPresetOptions(item);
            _pedals.Insert(index, item);
        }

        public bool Remove(IPedal item)
        {
            if (_pedals.Remove(item))
            {
                RemovePresetOptions(item);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _pedals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var pedal = _pedals[index];
            RemovePresetOptions(pedal);
            _pedals.RemoveAt(index);
        }

        //Interactive Editing
        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            if (!additionalArgs.ContainsKey("availablePedals"))
            {
                throw new ArgumentNullException($"The {nameof(additionalArgs)} argument in {nameof(PedalBoard.InteractiveViewEdit)} must define the key-value pair 'availablePedals'.");
            }
            var availablePedals = (List<Pedal>)additionalArgs["availablePedals"];

            Console.WriteLine(Name);
            if (Count > 0)
            {
                Console.Write("Signal Chain:\nGuitar -> ");
                foreach (Pedal pedal in this)
                {
                    Console.Write($"{pedal} -> ");
                }
                Console.Write("Amp\n");
            }
            else
            {
                Console.WriteLine("No pedals assigned currently.");
            }

            while (true)
            {
                if (Presets.Count > 0)
                {
                    Console.WriteLine("Presets:");
                    for (var i = 0; i < Presets.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Presets[i]}");
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
                if (int.TryParse(input, out presetIndex)
                    && presetIndex >= 1 && presetIndex <= Presets.Count)
                {
                    presetIndex -= 1;
                    var preset = Presets[presetIndex];
                    InteractiveViewEditPreset(checkQuit, preset);
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
                //TODO: edit pedals from here

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
                        if (int.TryParse(Console.ReadLine(), out destinationIndex)
                            && destinationIndex >= 1 && destinationIndex <= Count)
                        {
                            destinationIndex -= 1;
                            MovePedal(index, destinationIndex);
                            Console.WriteLine("Pedal moved.");
                            continue;
                        }
                        Console.WriteLine("Please select a valid number from the list.");
                        continue;
                    }
                }
            }
        }

        public void MovePedal(int currentIndex, int newIndex)
        {
            if (currentIndex < 0 || newIndex < 0
                || currentIndex >= Count || newIndex >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var pedalToMove = this[currentIndex];
            _pedals.RemoveAt(currentIndex);
            _pedals.Insert(newIndex, pedalToMove);
        }

        public void InteractiveAddPedals(Action<string> checkQuit, List<Pedal> availablePedals)
        {
            var pedalsToAdd = new List<IPedal>();
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

        public void AddRange(List<IPedal> pedalsToAdd)
        {
            foreach (IPedal pedal in pedalsToAdd)
            {
                Add(pedal);
            }
        }

        public void InteractiveNewPreset(Action<string> checkQuit)
        {
            while (true)
            {
                Console.WriteLine("What should the new preset be called? ('-b' to go back) ");
                var input = Console.ReadLine();

                checkQuit(input);
                if (input.ToLower() == "-b") { return; }

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("You must enter a name for the preset.");
                    continue;
                }

                if (PresetAdd(input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"There's already a preset with the name '{input}'.\nPlease select a different name.");
                    continue;
                }
            }

            InteractiveViewEditPreset(checkQuit, Presets[Presets.Count - 1]);
        }

        private void InteractiveViewEditPreset(Action<string> checkQuit, PedalBoardPreset preset)
        {
            while (true)
            {
                Console.WriteLine(preset.Name);
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));
                Console.WriteLine("Guitar ->");
                int index = 1;
                foreach (Pedal pedal in this)
                {
                    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));
                    Console.WriteLine($"{index}. {pedal}");
                    Console.WriteLine($"Pedal {(preset.EngagedList[pedal] ? "engaged" : "not engaged")}");
                    Console.WriteLine(string.Concat(Enumerable.Repeat(".", 10)));
                    foreach (Setting setting in pedal.Settings)
                    {
                        int value = preset.SettingValues.Where(value => value.Item == setting).First().StoredValue;
                        Console.WriteLine(setting.ToString(value));
                    }
                    Console.WriteLine(string.Concat(Enumerable.Repeat(".", 10)));
                    index++;
                }
                Console.WriteLine("-> Amp");
                Console.WriteLine(string.Concat(Enumerable.Repeat("-", 10)));

                Console.WriteLine("Enter pedal number to adjust settings.");
                Console.WriteLine($"'-b' to go back: ");

                var input = Console.ReadLine();

                checkQuit(input);
                if (input.ToLower() == "-b") { return; }

                int pedalIndex;
                if (int.TryParse(input, out pedalIndex)
                    && pedalIndex >= 1 && pedalIndex <= Count)
                {
                    pedalIndex -= 1;
                    var args = new Dictionary<string, object>()
                    {
                        { "preset", preset} 
                    };
                    this[pedalIndex].InteractiveViewEdit(checkQuit, args);
                    continue;
                }
                Console.WriteLine("Input not recognized.");
            }
        }
    }
}
