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
                NewSetting.CreateClockFaceSetting("Drive", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Tone", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Level", "6:30", "5:30")
            );

            DemoPedals[1].AddSettings(
                NewSetting.CreateClockFaceSetting("Blend", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Gain", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Rate", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Depth", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Drive", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Feedback", "6:30", "5:30"),
                NewSetting.CreateClockFaceSetting("Delay", "6:30", "5:30"),
                new NewSetting(
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
                new NewSetting(
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

            board.AddRange(DemoPedals);

            return board;
        }
    }
}
