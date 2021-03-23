using EffectsPedalsKeeper.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class MainMenu : ListMenuPage<MenuPage>
    {
        public static string GlobalOptionsText = "Type '-q' to quit or '-h' for help";
        public static string WelcomeText =
            $"Welcome to the Effects Pedals Wrangler.\n{GlobalOptionsText}.";

        public MainMenu(IList<MenuPage> items) : base(WelcomeText, "Choose a number from the list",
            items, page => page.InputLoop())
        {
            CallingStatement = () => Program.CheckForQuitOrHelp("-q");
        }
    }
}
