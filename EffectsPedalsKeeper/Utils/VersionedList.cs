using System;
using System.Collections;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Utils
{
    /// <summary>
    /// A List of reference type objects with version control.
    /// The same set of objects are saved across all versions,
    /// but their properties can be set differently.
    /// </summary>
    /// <typeparam name="T">Type T must be a reference type</typeparam>
    public class VersionedList<T> : IList<T> where T : class
    {
        private List<T> _checkedOutList;
        private List<Version<T>> _versions;
        private Func<T, T> _InternalCopy;

        public VersionedList(Func<T, T> copyMethod)
        {
            _InternalCopy = copyMethod;
            _checkedOutList = new List<T>();
            _versions = new List<Version<T>>();
        }

        public void CheckOutVersion(int index)
        {
            if (index >= _versions.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            _checkedOutList.Clear();
            foreach(T item in _versions[index].Items)
            {
                _checkedOutList.Add(_InternalCopy(item));
            }
        }

        public bool SaveVersion()
        {
            throw new NotImplementedException();
        }

        public bool SaveAsVersion(string name)
        {

            var startingCount = _versions.Count;
            var newVersion = new Version<T>(name);
            foreach(var item in _checkedOutList)
            {
                newVersion.Items.Add(_InternalCopy(item));
            }
            _versions.Add(newVersion);
            return _versions.Count > startingCount;
        }

        public Dictionary<int, string> ListVersions()
        {
            throw new NotImplementedException();
        }

        // IList implementation
        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => _checkedOutList.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _checkedOutList.Add(item);
            foreach(var version in _versions)
            {
                version.Items.Add(_InternalCopy(item));
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach(var item in collection)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
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
    }

    public class Version<T>
    {
        public Version(string name)
        {
            Name = name;
            Items = new List<T>();
        }
        public string Name { get; set; }
        public List<T> Items { get; set; }
    }

}
