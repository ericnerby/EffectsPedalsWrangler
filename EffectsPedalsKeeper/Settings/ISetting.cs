using System;

namespace EffectsPedalsKeeper.Settings
{
    public interface ISetting
    {
        public string Label { get; }

        public int MinValue { get; }\
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
