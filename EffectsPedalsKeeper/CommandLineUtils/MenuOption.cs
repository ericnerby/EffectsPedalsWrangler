using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class MenuOption
    {
        public ResponseType ResponseType { get; }
        public virtual Action Action { get; }
        public string Command { get; }
        public string Description { get; }

        public MenuOption(ResponseType responseType, Action action, string command, string description)
        {
            ResponseType = responseType;
            Action = action;
            Command = command.ToLower();
            Description = description;
        }
    }
}
