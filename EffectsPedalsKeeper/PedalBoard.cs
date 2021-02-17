using EffectsPedalsKeeper.Utils;

namespace EffectsPedalsKeeper
{
    public class PedalBoard : VersionedList<Pedal>
    {
        public string Name { get; set; }
        private static Pedal _CopyMethod(Pedal item) => (Pedal)item.Copy();

        public PedalBoard(string name) : base(_CopyMethod)
        {
            Name = name;
        }
    }
}
