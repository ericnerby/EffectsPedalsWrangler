using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EffectsPedalsKeeper
{
    public class Preset : IList<IPedal>
    {
        private List<IPedal> _pedals;

        public string Name { get; }

        public IPedal this[int index] {
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
        public Preset(string name)
        {
            Name = name;
            _pedals = new List<IPedal>();
        }

        public override string ToString()
        {
            string output = Name;
            if (_pedals.Count > 0)
            {
                int engagedPedals = _pedals.Where(p => p.Engaged).Count();
                int totalPedals = _pedals.Count;
                output += $"{engagedPedals}/{totalPedals} pedals engaged";
            }
            return output;
        }

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

        public IEnumerator<IPedal> GetEnumerator()
        {
            return _pedals.GetEnumerator();
        }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
