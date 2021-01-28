using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class RotarySetting : ISetting
    {
        public RotarySetting(string[] options)
        {

        }

        public int CurrentValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int MinValue => throw new NotImplementedException();

        public int MaxValue => throw new NotImplementedException();

        public string[] Display()
        {
            throw new NotImplementedException();
        }

        public int StepDown()
        {
            throw new NotImplementedException();
        }

        public int StepUp()
        {
            throw new NotImplementedException();
        }
    }
}
