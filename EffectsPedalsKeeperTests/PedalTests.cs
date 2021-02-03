using Xunit;
using EffectsPedalsKeeper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Tests
{
    public class PedalTests
    {
        private Pedal _pedal;
        private string _maker = "Ibanez";
        private string _name = "Tubescreamer";

        public PedalTests()
        {
            _pedal = new Pedal(_maker, _name);
        }

        [Fact()]
        public void PrintSettingDetailsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DisplayPedalTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddSettingsTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}