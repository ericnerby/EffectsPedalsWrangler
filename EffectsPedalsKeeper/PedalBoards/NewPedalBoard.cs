using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Pedals;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.PedalBoards
{
    public class NewPedalBoard : IList<IPedal>, IInteractiveEditable
    {
        public string Name { get; set; }
        public List<PedalBoardPreset> Presets { get; private set; }

        public NewPedalBoard(string name, IList<IPedal> pedals)
        {
            Name = name;
            _pedals = new List<IPedal>(pedals);
        }

        //IList Implementation
        [JsonProperty]
        private List<IPedal> _pedals;

        public IPedal this[int index] { get => _pedals[index]; set => throw new NotImplementedException(); }

        public int Count => _pedals.Count;

        public bool IsReadOnly => false;

        public void Add(IPedal item)
        {

        }

        public void Clear() => _pedals.Clear();

        public bool Contains(IPedal item) => _pedals.Contains(item);

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            _pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator() => _pedals.GetEnumerator();

        public int IndexOf(IPedal item) => _pedals.IndexOf(item);

        public void Insert(int index, IPedal item)
        {
            _pedals.Insert(index, item);
        }

        public bool Remove(IPedal item)
        {
            return _pedals.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _pedals.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //Interactive Editing
        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }
    }
}
