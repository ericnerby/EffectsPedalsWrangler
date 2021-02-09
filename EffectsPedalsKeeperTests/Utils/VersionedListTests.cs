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
        public void CheckOutVersionTest()
        {
            Assert.True(false, "This test needs an implementation");
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
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void CopyToTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetEnumeratorTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void IndexOfTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void InsertTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RemoveTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void RemoveAtTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddRangeTest()
        {
            _versionedList.AddRange(_testObjects.ToArray());

            var expected = _testObjects.Count;
            var target = _versionedList.Count;

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
    }
}