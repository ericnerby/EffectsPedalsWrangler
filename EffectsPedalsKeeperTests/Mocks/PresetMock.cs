using System;
using System.Collections;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Tests.Mocks
{
    class PresetMock : IPreset
    {
        private List<IPedal> _pedals;
        public string Name { get; }
        public string Description { get; set; }

        public PresetMock(string name, IList<IPedal> pedals)
        {
            Name = name;
            _pedals = (List<IPedal>)pedals;
        }

        public IPedal this[int index]
        {
            get
            {
                return _pedals[index];
            }
            set
            {
                _pedals[index] = value;
            }
        }

        public int Count => _pedals.Count;

        public bool IsReadOnly => false;

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
            _pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator() => _pedals.GetEnumerator();

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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
