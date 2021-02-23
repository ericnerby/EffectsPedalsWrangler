using System;

namespace EffectsPedalsKeeper
{
    interface IInteractiveEditable
    {
        void InteractiveChangeSetting(Func<string, bool> checkQuit);
    }
}
