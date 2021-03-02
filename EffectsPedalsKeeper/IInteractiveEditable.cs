using System;

namespace EffectsPedalsKeeper
{
    public interface IInteractiveEditable
    {
        void InteractiveViewEdit(Action<string> checkQuit);
    }
}
