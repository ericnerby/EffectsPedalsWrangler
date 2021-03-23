using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class MenuPage
    {
        public MenuOption[] GlobalOptions { get; }
        public Action CallingStatement { get; set; }
        public List<MenuOption> MenuOptions { get; protected set; }

        public MenuPage(Action callingStatement, MenuOption[] menuOptions)
        {
            CallingStatement = callingStatement;
            GlobalOptions = new MenuOption[]
            {
                new MenuOption(ResponseType.DashOption, () => Program.CheckForQuitOrHelp("-q"), "-q", null),
                new MenuOption(ResponseType.DashOption, CallingStatement, "-b", "'-b' to go back to previous screen'")
            };
            MenuOptions = new List<MenuOption>(menuOptions);
        }
    }
}
