using EffectsPedalsKeeper.Interfaces;
using EffectsPedalsKeeper.Pedals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.PedalBoards
{
    public class NewPedalBoard : IList<IPedal>, IInteractiveEditable
    {
        public List<PedalBoardPreset> Presets;

        //IList Implementation
        public IPedal this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(IPedal item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IPedal item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            throw new NotImplementedException();
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
