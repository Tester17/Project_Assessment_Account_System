using System;

namespace Project_Assessment_Account_System {
    internal class Menu {
        private readonly List<string> _typesOfMenu = new() {"main", "registration","log in", "congratulation", "Password Generation", "Show list of all accounts" };
        private string _typeOfMenu;
        private int _userSelection;
        private List<string> _menuItems;

        internal Menu(string typeOfMenu) {
            _typeOfMenu = _typesOfMenu.Contains(typeOfMenu) ? typeOfMenu : "main";
            _menuItems=MenuItemsByType();
            Open();
        }
        private List<string> MenuItemsByType() {
            List<string> tempMenuItem = new();
            switch (_typeOfMenu) {
                case"main":
                    tempMenuItem.AddRange(new[] { "Log in", "Create account", "Exit" });
                    break;
                case "registration":
                    tempMenuItem.AddRange(new[] { "Enter password manualy", "Generate password", "Change name" });
                    break;
                case "log in":
                    tempMenuItem.AddRange(new[] { "Continue", "Back" });
                    break;
                case "congratulation":
                    tempMenuItem.AddRange(new[] { "Get congrutilation!!!", "Exit" });
                    break;
                case "Password Generation":
                    tempMenuItem.AddRange(new[] { "Apply current settings","Add/remove capital letters", "Add/remove numbers", "Add/remove symbols","Modify password lenght", "Enter password manualy" });
                    break;
                case "Show list of all accounts":
                    tempMenuItem.AddRange(new[] {"Yes","No"});
                    break;
                default:
                    tempMenuItem.AddRange(new[] { "non-existing menu type, back" });
                    break;
            }
            return tempMenuItem;
        }
        private void Open() {
            Console.WriteLine(_typeOfMenu+" menu\n");
            int row = Console.CursorTop;
            int col = Console.CursorLeft;
            int index = 0;
            while (true) { 
                DrawMenu(_menuItems, row, col, index);
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.DownArrow:
                        if (index < _menuItems.Count - 1)
                            index++;
                        else
                            index = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                            index--;
                        else 
                            index = _menuItems.Count - 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        _userSelection = index;
                        return;
                }
            }
        }
        private static void DrawMenu(List<string> items, int row, int col, int index) {
            Console.SetCursorPosition(col, row);
            for (int i = 0; i < items.Count; i++) {
                if (i == index) {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(items[i]);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        internal int GetUserSelection() {
            return _userSelection;
        }
        internal int GetLastMenuItemNumber() {
            return _menuItems.Count-1 ;
        }

    }
}
