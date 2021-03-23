using EffectsPedalsKeeper.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace EffectsPedalsKeeper
{
    public class MainMenu : MenuPage
    {
        public static string GlobalOptionsText = "Type '-q' to quit or '-h' for help";
        public static string WelcomeText =
            $"Welcome to the Effects Pedals Wrangler.\n{GlobalOptionsText}.";
        static List<MenuPage> _mainMenuItems = new List<MenuPage>()
        {
            new MenuPage("View Existing Pedals"),
            new MenuPage("View Existing Boards"),
            new MenuPage("Add new pedals"),
            new MenuPage("Create new board")
        };
        public NumberedMenuOption<MenuPage> MainMenuNav { get; }

        public MainMenu() : base(WelcomeText)
        {
            MainMenuNav = new NumberedMenuOption<MenuPage>(
                _mainMenuItems,
                "Choose a number from the list",
                page => page.InputLoop(
                    () => this.InputLoop(
                        () => Program.CheckForQuitOrHelp("-q"))));
        }
    }
}
