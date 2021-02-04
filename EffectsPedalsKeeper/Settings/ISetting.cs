using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.Settings
{
    public interface ISetting
    {
        public string Label { get; }

        /// <summary>
        ///  Minimum value allowed for setting
        /// </summary>
        public int MinValue { get; }
        /// <summary>
        ///  Maximum value allowed for setting
        /// </summary>
        public int MaxValue { get; }

        /// <summary>
        ///  CurrentValue converted to the appropriate string for the particular Setting type.
        /// </summary>
        public abstract string CurrentValueDisplay { get; }

        public int CurrentValue { get; }

        public int StepUp();

        public int StepDown();
    }
}
