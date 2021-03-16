using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.Interfaces
{
    public interface IInteractiveEditable
    {
        void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs);
    }
}
