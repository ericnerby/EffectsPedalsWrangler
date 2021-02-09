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

        public VersionedList(Func<T, T> copyMethod)
        {
            _checkedOutList = new List<T>();
        }

        public void CheckOutVersion(int index)
        {
            throw new NotImplementedException();
        }

        public bool SaveVersion()
        {
            throw new NotImplementedException();
        }

        public bool SaveAsVersion(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> ListVersions()
        {
            throw new NotImplementedException();
        }

        // IList implementation
        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => _checkedOutList.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotImplementedException();
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
            _checkedOutList.AddRange(collection);
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
}