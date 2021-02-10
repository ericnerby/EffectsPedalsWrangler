using EffectsPedalsKeeper.Utils;
using System;

namespace EffectsPedalsKeeper
{
    public class PedalBoard : VersionedList<IPedal>
    {
        private static IPedal _CopyMethod(IPedal item) => throw new NotImplementedException();

        public PedalBoard(string name) : base(_CopyMethod)
        {
            throw new NotImplementedException();
        }
    }
}
