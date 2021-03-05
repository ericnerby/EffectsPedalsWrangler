using EffectsPedalsKeeper.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace EffectsPedalsKeeper.Utils.Tests
{
    public class VersionedListTests
    {
        private VersionedList<TestObject> _versionedList;
        private List<TestObject> _testObjects;
        public static TestObject CopyMethod(TestObject item) => item.MakeCopy();

        public VersionedListTests()
        {
            _versionedList = new VersionedList<TestObject>(CopyMethod);
            _testObjects = new List<TestObject>
            {
                new TestObject() { Name = "Drive", CurrentValue = 10},
                new TestObject() { Name = "Rotary", CurrentValue = 2},
                new TestObject() { Name = "Delay", CurrentValue = 5},
                new TestObject() { Name = "Volume", CurrentValue = 12}
            };
        }

        private static void _AddTestObjects(
            VersionedList<TestObject> versionList,
            List<TestObject> testObjects)
        {
            versionList.AddRange(testObjects.ToArray());
        }

        [Fact()]
        public void CheckOutVersionOutsideRangeTest()
        {
            Assert.Throws<IndexOutOfRangeException>(
                () => _versionedList.CheckOutVersion(_versionedList.Count));
        }

        [Fact()]
        public void SaveVersionTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var firstVersionName = "first version test";
            var secondVersionName = "second version test";
            var testDifference = 5;
            var targetIndex = 1;
            var expected = _testObjects[targetIndex].CurrentValue;

            _versionedList.SaveAsVersion(firstVersionName);
            _versionedList.SaveAsVersion(secondVersionName);

            _versionedList[1].CurrentValue += testDifference;

            Assert.True(_versionedList.SaveVersion());

            _versionedList.CheckOutVersion(0);
            var target = _versionedList[targetIndex].CurrentValue;

            Assert.Equal(expected, target);

            _versionedList.CheckOutVersion(1);
            target = _versionedList[targetIndex].CurrentValue;
            expected += testDifference;
            Assert.Equal(expected, target);
        }

        [Fact()]
        public void SaveAsVersionTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            Assert.True(_versionedList.SaveAsVersion("first version test"));
            Assert.NotEmpty(_versionedList.ListVersions());
        }

        [Fact()]
        public void ListVersionsTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var firstVersionName = "first version test";
            var secondVersionName = "second version test";

            _versionedList.SaveAsVersion(firstVersionName);

            _versionedList[2].CurrentValue += 2;
            _versionedList.SaveAsVersion(secondVersionName);

            var target = _versionedList.ListVersions().Values;

            Assert.Contains(firstVersionName, target);
            Assert.Contains(secondVersionName, target);
        }

        [Fact()]
        public void AddTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var startingValue = _versionedList.Count;

            _versionedList.SaveAsVersion("first version test");
            _versionedList.SaveAsVersion("second version test");

            _versionedList.Add(new TestObject()
            { Name = "Reverb", CurrentValue = 1 });

            var expected = startingValue += 1;

            _versionedList.CheckOutVersion(0);

            Assert.Equal(expected, _versionedList.Count);

            _versionedList.CheckOutVersion(1);

            Assert.Equal(expected, _versionedList.Count);
        }

        [Fact()]
        public void ClearTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            _versionedList.Clear();

            Assert.Empty(_versionedList);
        }

        [Fact()]
        public void ContainsTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            Assert.Contains(_testObjects[0], _versionedList);
        }

        [Fact()]
        public void CopyToTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var target = new TestObject[_versionedList.Count];

            _versionedList.CopyTo(target, 0);

            Assert.All(target, item => Assert.NotNull(item));
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            Assert.IsAssignableFrom<IEnumerator<TestObject>>(_versionedList.GetEnumerator());
        }

        [Fact()]
        public void IndexOfTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var testIndex = 1;
            var testObject = _testObjects[testIndex];

            Assert.Equal(testIndex, _versionedList.IndexOf(testObject));
        }

        [Fact()]
        public void InsertTest()
        {
            _versionedList.Add(_testObjects[0]);
            _versionedList.Add(_testObjects[2]);
            _versionedList.SaveAsVersion("first test version");

            _versionedList.Insert(1, _testObjects[1]);

            _versionedList.SaveAsVersion("second test version");

            var expected = _testObjects[1];

            _versionedList.CheckOutVersion(0);
            var target = _versionedList[1];

            Assert.Equal(expected, target);

            _versionedList.CheckOutVersion(1);
            target = _versionedList[1];

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void RemoveTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var itemToRemove = _testObjects[1];

            _versionedList.SaveAsVersion("first test version");

            _versionedList.Remove(itemToRemove);

            _versionedList.SaveAsVersion("second test version");

            _versionedList.CheckOutVersion(0);

            Assert.DoesNotContain(itemToRemove, _versionedList);

            _versionedList.CheckOutVersion(1);

            Assert.DoesNotContain(itemToRemove, _versionedList);
        }

        [Fact()]
        public void RemoveAtTest()
        {
            _AddTestObjects(_versionedList, _testObjects);

            var indexToRemove = 1;

            var itemToRemove = _testObjects[indexToRemove];

            _versionedList.SaveAsVersion("first test version");

            _versionedList.RemoveAt(indexToRemove);

            _versionedList.SaveAsVersion("second test version");

            _versionedList.CheckOutVersion(0);

            Assert.DoesNotContain(itemToRemove, _versionedList);

            _versionedList.CheckOutVersion(1);

            Assert.DoesNotContain(itemToRemove, _versionedList);
        }

        [Fact()]
        public void AddRangeTest()
        {
            _versionedList.AddRange(_testObjects.ToArray());

            var expected = _testObjects.Count;
            var target = _versionedList.Count;

            Assert.Equal(expected, target);
        }

        [Fact()]
        public void MoveItemTest()
        {
            _versionedList.AddRange(_testObjects.ToArray());

            var startingIndex = 2;
            var targetIndex = 1;
            var expectedLength = _testObjects.Count;

            _versionedList.MoveItem(startingIndex, targetIndex);

            var expected = _testObjects[startingIndex];
            var target = _versionedList[targetIndex];

            var targetLength = _versionedList.Count;

            Assert.Equal(expectedLength, targetLength);
            Assert.Equal(expected, target);
        }
    }

    public class TestObject
    {
        public string Name { get; set; }
        public int CurrentValue { get; set; }

        public TestObject MakeCopy()
        {
            return (TestObject)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TestObject))
            {
                return false;
            }
            var that = (TestObject)obj;
            return that.Name == Name && that.CurrentValue == CurrentValue;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + CurrentValue;
        }
    }
}