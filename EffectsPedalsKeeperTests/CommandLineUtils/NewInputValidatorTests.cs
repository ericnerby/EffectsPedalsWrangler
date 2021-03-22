using Xunit;
using EffectsPedalsKeeper.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils.Tests
{
    public class NewInputValidatorTests
    {
        [Fact()]
        public void ParseInputEmptyTest()
        {
            var testString = "";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Empty, response.ResponseType);
            Assert.Null(response.Value);
        }

        [Fact()]
        public void ParseInputDashTest()
        {
            var testString = "-B";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.DashOption, response.ResponseType);
            Assert.Equal(testString.ToLower(), response.Value);
        }

        [Fact()]
        public void ParseInputIntTest()
        {
            var testString = "5";
            var target = "5";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Int, response.ResponseType);
            Assert.Equal(target, response.Value);
        }

        [Fact()]
        public void ParseInputDoubleTest()
        {
            var testString = "5.4";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Double, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputClockTest()
        {
            var testString = "12:35";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.TwelveClock, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputMinutesAboveSixtyTest()
        {
            var testString = "12:75";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Misc, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputZeroHourTest()
        {
            var testString = "0:35";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Misc, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputMilitaryTimeTest()
        {
            var testString = "14:05";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Misc, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputClockMinTest()
        {

            var testString = "1:00";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.TwelveClock, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputClockMaxTest()
        {

            var testString = "12:59";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.TwelveClock, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }

        [Fact()]
        public void ParseInputMiscTest()
        {
            var testString = "Something-Different 15.3";
            var response = NewInputValidator.ParseInput(testString);

            Assert.Equal(ResponseType.Misc, response.ResponseType);
            Assert.Equal(testString, response.Value);
        }
    }
}