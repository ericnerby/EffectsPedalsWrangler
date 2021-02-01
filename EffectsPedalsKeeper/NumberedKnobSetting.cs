﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    /// <summary>
    ///  A knob rotates with no set positions. It can either
    ///  have numbers on the dial, or the position can be
    ///  indicated by position on a clock face, eg. '3:00'.
    /// </summary>
    public class NumberedKnobSetting : Setting
    {
        public override string CurrentValueDisplay
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public NumberedKnobSetting(string label, int minKnobValue, int maxKnobValue)
            : base(label, 0,
                   (maxKnobValue - minKnobValue) * 10)
        {}

        public override string[] Display()
        {
            throw new NotImplementedException();
        }
    }
}