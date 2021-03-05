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

        /// <summary>
        /// Index of CheckedOutVersion. -1 if no version saved.
        /// </summary>
        public int CheckedOutVersionIndex { get; private set; }

        public string CheckedOutVersionName
        {
            get
            {
                if (_versions.Count == 0)
                {
                    return null;
                }
                return _versions[CheckedOutVersionIndex].Name;
            }
        }

        public VersionedList(Func<T, T> copyMethod)
        {
            _InternalCopy = copyMethod;
            _checkedOutList = new List<T>();
            _versions = new List<Version<T>>();
            CheckedOutVersionIndex = -1;
        }

        public void CheckOutVersion(int index)
        {
            if (index >= _versions.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            CheckedOutVersionIndex = index;
            _checkedOutList.Clear();

            foreach(T item in _versions[index].Items)
            {
                _checkedOutList.Add(_InternalCopy(item));
            }
        }

        public bool SaveVersion()
        {
            if (_versions.Count == 0) return false;
            var destination = _versions[CheckedOutVersionIndex].Items;
            for(var i = 0; i < _checkedOutList.Count; i++)
            {
                destination[i] = _InternalCopy(_checkedOutList[i]);
            }
            return true;
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
            CheckedOutVersionIndex = _versions.IndexOf(newVersion);
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

        public void MoveItem(int currentIndex, int newIndex)
        {
            if(currentIndex < 0 || newIndex < 0
                || currentIndex > Count || newIndex > Count)
            {
                throw new IndexOutOfRangeException();
            }

            var itemToMove = _checkedOutList[currentIndex];
            _checkedOutList.RemoveAt(currentIndex);
            _checkedOutList.Insert(newIndex, itemToMove);

            foreach (var version in _versions)
            {
                var versionedItemToMove = version.Items[currentIndex];
                version.Items.RemoveAt(currentIndex);
                version.Items.Insert(newIndex, versionedItemToMove);
            }
        }

        // IList implementation
        public T this[int index]
        {
            get
            {
                return _checkedOutList[index];
            }
            set
            {
                _checkedOutList[index] = _InternalCopy(value);
                foreach(var version in _versions)
                {
                    version.Items[index] = _InternalCopy(value);
                }
            }
        }

        public int Count => _checkedOutList.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _checkedOutList.Add(_InternalCopy(item));
            foreach(var version in _versions)
            {
                version.Items.Add(_InternalCopy(item));
            }
        }

        public void Clear()
        {
            _checkedOutList.Clear();
            _versions.Clear();
            CheckedOutVersionIndex = -1;
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
