using EffectsPedalsKeeper.Settings;
using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class DemoBuilder
    {
        public List<Pedal> DemoPedals = new List<Pedal>() { 
            new Pedal("Ibanez", "TubeScreamer", EffectType.Drive),
            new Pedal("EHX", "Memory Boy", EffectType.Delay),
        };

        public DemoBuilder()
        {
            DemoPedals[0].AddSettings(
                new KnobSetting("Drive", "6:30", "5:30"),
                new KnobSetting("Tone", "6:30", "5:30"),
                new KnobSetting("Level", "6:30", "5:30")
            );

            DemoPedals[1].AddSettings(
                new KnobSetting("Blend", "6:30", "5:30"),
                new KnobSetting("Gain", "6:30", "5:30"),
                new KnobSetting("Rate", "6:30", "5:30"),
                new KnobSetting("Depth", "6:30", "5:30"),
                new KnobSetting("Drive", "6:30", "5:30"),
                new KnobSetting("Feedback", "6:30", "5:30"),
                new KnobSetting("Delay", "6:30", "5:30"),
                new RotarySetting(
                    "Tap Divide",
                    new string[]
                    {
                        "Dotted Eighth",
                        "Quarter Triplet",
                        "Eighth",
                        "Eighth Triplet",
                        "Sixteenth"
                    }
                ),
                new RotarySetting(
                    "Exp. Mode",
                    new string[]
                    {
                        "Rate",
                        "Depth",
                        "Feedback",
                        "Delay",
                    }
                )
            );
        }

        public PedalBoard BuildDemoBoard()
        {
            var board = new PedalBoard("Demo Board");

            board.AddRange(DemoPedals);

            return board;
        }
    }
}
