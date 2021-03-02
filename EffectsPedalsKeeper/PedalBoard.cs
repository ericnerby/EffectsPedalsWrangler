using EffectsPedalsKeeper.Utils;
using System;

namespace EffectsPedalsKeeper
{
    public class PedalBoard : VersionedList<Pedal>, IInteractiveEditable
    {
        public string Name { get; set; }
        private static Pedal _CopyMethod(Pedal item) => (Pedal)item.Copy();

        public void InteractiveViewEdit(Action<string> checkQuit)
        {
            throw new NotImplementedException();
        }

        public PedalBoard(string name) : base(_CopyMethod)
        {
            Name = name;
        }
    }
}
