using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper
{
    public interface IInteractiveEditable
    {
        void InteractiveViewEdit(Action<string> checkQuit, Dictionary<string, object> additionalArgs);
    }
}
