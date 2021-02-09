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
            _pedals.Add(item);
        }

        public void Clear()
        {
            _pedals.Clear();
        }

        public bool Contains(IPedal item)
        {
            return _pedals.Contains(item);
        }

        public void CopyTo(IPedal[] array, int arrayIndex)
        {
            _pedals.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IPedal> GetEnumerator() => _pedals.GetEnumerator();

        public int IndexOf(IPedal item)
        {
            return _pedals.IndexOf(item);
        }

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
    }
}
