using EffectsPedalsKeeper.PedalBoards;
using EffectsPedalsKeeper.Pedals;
using EffectsPedalsKeeper.Settings;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public class DemoBuilder
    {
        public List<Pedal> DemoPedals = new List<Pedal>() { 
            new Pedal("Ibanez", "TubeScreamer", EffectType.Drive),
            new Pedal("EHX", "Memory Boy", EffectType.Delay),
            new Pedal("Boss", "CS-2 Compressor Sustainer", EffectType.Drive),
            new Pedal("EHX", "Electric Mistress", EffectType.Mod),
            new Pedal("Boss", "DS-1 Distortion", EffectType.Drive),
            new Pedal("Line 6", "DL4", EffectType.Delay),
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

            DemoPedals[2].AddSettings(
                Setting.CreateClockFaceSetting("Level", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Attack", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Sustain", "6:30", "5:30")
            );

            DemoPedals[3].AddSettings(
                Setting.CreateClockFaceSetting("Rate", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Range", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Color", "6:30", "5:30")
            );

            DemoPedals[4].AddSettings(
                Setting.CreateClockFaceSetting("Tone", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Level", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Dist", "6:30", "5:30")
            );

            DemoPedals[5].AddSettings(
                new Setting(
                    "Model Selector", SettingType.Named,
                    new string[]
                    {
                        "Loop Sampler",
                        "Tube Echo",
                        "Tape Echo",
                        "Multi-head",
                        "Sweep Echo",
                        "Analog Echo",
                        "Analog w/ Mod",
                        "Lo Res Delay",
                        "Digital Delay",
                        "Digital w/ Mod",
                        "Rhythmic Delay",
                        "Stereo Delays",
                        "Ping Pong",
                        "Reverse",
                        "Dynamic Delay",
                        "Auto Volume Echo"
                    }
                ),
                Setting.CreateClockFaceSetting("Delay Time", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Repeats", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Tweak", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Tweez", "6:30", "5:30"),
                Setting.CreateClockFaceSetting("Mix", "6:30", "5:30")
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
