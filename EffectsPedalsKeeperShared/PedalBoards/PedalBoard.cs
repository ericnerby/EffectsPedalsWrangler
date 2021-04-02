using EffectsPedalsKeeperShared.Pedals;
using EffectsPedalsKeeperShared.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EffectsPedalsKeeperShared.PedalBoards
{
    public class PedalBoard : IList<IPedal>
    {
        public string Name { get; set; }
        public List<PedalBoardPreset> Presets { get; private set; }

        public PedalBoard(string name)
        {
            Name = name;
            _pedals = new List<IPedal>();
            Presets = new List<PedalBoardPreset>();
        }

        public PedalBoard(string name, IList<IPedal> pedals)
        {
            Name = name;
            if(pedals.Count > 0) { _pedals = new List<IPedal>(pedals); }
            else { _pedals = new List<IPedal>(); }
            Presets = new List<PedalBoardPreset>();
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

        private void AddPresetOptions(IPedal pedal)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                preset.AddPedals(new IPedal[] { pedal });
            }
        }

        private void MovePresetOptions(int currentIndex, int newIndex)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                preset.MovePedal(currentIndex, newIndex);
            }
        }

        private void InsertPresetOptions(IPedal pedal, int position)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                preset.InsertPedal(pedal, position);
            }
        }

        private void RemovePresetOptions(int position)
        {
            foreach (PedalBoardPreset preset in Presets)
            {
                preset.RemovePedal(position);
            }
        }

        //IList Implementation
        protected List<IPedal> _pedals;

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
            AddPresetOptions(item);
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
            AddPresetOptions(item);
            _pedals.Insert(index, item);
        }

        public bool Remove(IPedal item)
        {
            var index = _pedals.IndexOf(item);
            if (index == -1)
            {
                return false;
            }
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _pedals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            RemovePresetOptions(index);
            _pedals.RemoveAt(index);
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
            MovePresetOptions(currentIndex, newIndex);
        }
        public void AddRange(List<IPedal> pedalsToAdd)
        {
            foreach (IPedal pedal in pedalsToAdd)
            {
                Add(pedal);
            }
        }
    }
}
