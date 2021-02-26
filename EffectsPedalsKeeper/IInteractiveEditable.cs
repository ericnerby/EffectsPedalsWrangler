using System;

namespace EffectsPedalsKeeper
{
    public interface IInteractiveEditable
    {
        void InteractiveChangeSetting(Func<string, bool> checkQuit);
    }
}
