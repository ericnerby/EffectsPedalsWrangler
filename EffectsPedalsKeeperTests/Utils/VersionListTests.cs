using System;
using System.Collections.Generic;
using Xunit;

namespace EffectsPedalsKeeper.Utils.Tests
{
    public class VersionListTests
    {
        private VersionList<TestObject> _versionList;
        private List<TestObject> _testObjects;
        public static TestObject CopyMethod(TestObject item) => item.MakeCopy();

        public VersionListTests()
        {
            _versionList = new VersionList<TestObject>(CopyMethod);
            _testObjects = new List<TestObject>()
            {
                new TestObject() { Name = "Drive", CurrentValue = 10},
                new TestObject() { Name = "Rotary", CurrentValue = 2},
                new TestObject() { Name = "Delay", CurrentValue = 5},
                new TestObject() { Name = "Volume", CurrentValue = 12}
            };
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
                () => _versionList.CheckOutVersion(_versionList.Count));
        }

        [Fact()]
        public void SaveVersionTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SaveAsVersionTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ListVersionsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void SaveItemsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ClearTest()
        {
            Assert.True(false, "This test needs an implementation");
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