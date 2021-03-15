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
        public List<PedalBoardPreset> Presets;
        [JsonProperty]
        protected List<IPedal> Pedals;

        //IList Implementation
        public IPedal this[int index] { get => Pedals[index]; set => throw new NotImplementedException(); }

        public int Count => Pedals.Count;

        public bool IsReadOnly => false;

        public void Add(IPedal item)
        {
            throw new NotImplementedException();
        }

        public void Clear() => Pedals.Clear();

        public bool Contains(IPedal item) => Pedals.Contains(item);

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            Pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IPedal item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IPedal item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IPedal item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //Interactive Editing
        public void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs)
        {
            throw new NotImplementedException();
        }
    }
}
