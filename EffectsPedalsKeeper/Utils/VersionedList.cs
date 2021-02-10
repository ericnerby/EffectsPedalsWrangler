﻿using System;
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
            var count = _versions.Count;
            var output = new Dictionary<int, string>(count);
            for (var i = 0; i < count; i++)
            {
                output.Add(i, _versions[i].Name);
            }
            return output;
        }

        // IList implementation
        public T this[int index]
        {
            get => _checkedOutList[index];
            set => throw new NotImplementedException();
        }

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
            _checkedOutList.Clear();
            _versions.Clear();
        }

        public bool Contains(T item) => _checkedOutList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _checkedOutList.CopyTo(array, arrayIndex);

        public void AddRange(IEnumerable<T> collection)
        {
            foreach(var item in collection)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => _checkedOutList.GetEnumerator();

        public int IndexOf(T item) => _checkedOutList.IndexOf(item);

        public void Insert(int index, T item)
        {
            _checkedOutList.Insert(index, item);
            foreach(var version in _versions)
            {
                version.Items.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1) return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            _checkedOutList.RemoveAt(index);
            foreach(var version in _versions)
            {
                version.Items.RemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
