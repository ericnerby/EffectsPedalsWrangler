using System;

namespace EffectsPedalsKeeper
{
    public interface IInteractiveEditable
    {
        void InteractiveChangeSetting(Action<string> checkQuit);
    }
}
