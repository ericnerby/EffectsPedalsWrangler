using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Pedals;
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
                Setting.CreateClockFaceSetting("Drive", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Tone", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Level", "6:30", "5:30")
            );

            DemoPedals[1].AddSettings(
                Setting.CreateClockFaceSetting("Blend", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Gain", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Rate", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Depth", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Drive", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Feedback", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Delay", "6:30", "5:30"),
                new Setting(
                    "Tap Divide", SettingType.Named,
                    new string[]
                    {
                        "Dotted Eighth",
                        "Quarter Triplet",
                        "Eighth",
                        "Eighth Triplet",
                        "Sixteenth"
                    }
                ),
                new Setting(
                    "Exp. Mode", SettingType.Named,
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

            DemoPedals.ForEach(pedal => board.Add(pedal));

            return board;
        }
    }
}
