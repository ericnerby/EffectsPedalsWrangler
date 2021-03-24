using EffectsPedalsKeeper.CommandLineUtils;
using EffectsPedalsKeeper.Pedals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class MainMenu : ListMenuPage<MenuPage>
    {
        public MenuPage[] MenuPages;
        public static string GlobalOptionsText = "Type '-q' to quit or '-h' for help";
        public static string WelcomeText =
            $"Welcome to the Effects Pedals Wrangler.\n{GlobalOptionsText}.";

        public MainMenu(IList<MenuPage> items) : base(WelcomeText, "Choose a number from the list",
            items, page => page.InputLoop())
        {
            CallingStatement = () => Program.CheckForQuitOrHelp("-q");
            MenuPages = new MenuPage[]
            {
                new ListMenuPage<Pedal>(
                    "Available Pedals",
                    "Choose a pedal from the list",
                    Program.Pedals,
                    pedal => pedal.PedalEditor.InputLoop(() => this.InputLoop()))
            };
        }
    }
}
