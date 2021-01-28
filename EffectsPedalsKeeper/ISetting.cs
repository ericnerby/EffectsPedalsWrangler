namespace EffectsPedalsKeeper
{
    /// <summary>
    ///  Generic Interface for Pedal Settings
    /// </summary>
    interface ISetting
    {
        string Label { get; }

        int CurrentValue { get; set; }
        /// <summary>
        ///  Minimum value allowed for setting
        /// </summary>
        int MinValue { get; }
        /// <summary>
        ///  Maximum value allowed for setting
        /// </summary>
        int MaxValue { get; }

        /// <summary>
        ///  Positive incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        int StepUp();
        /// <summary>
        ///  Negative incremental adjustment to CurrentValue
        /// </summary>
        /// <returns>New CurrentValue</returns>
        int StepDown();

        /// <summary>
        ///  Generate string/visual representation of Setting
        /// </summary>
        string[] Display();
    }
}
