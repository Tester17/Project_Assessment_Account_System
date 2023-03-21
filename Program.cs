using System;

namespace Project_Assessment_Account_System {
    internal class Program {

        internal static void Main() {
            Menu mainMenu = new Menu("main");
            if (mainMenu.GetUserSelection() == mainMenu.GetLastMenuItemNumber()) {
                Thread.Sleep(2000);
                Account.CloseProgram();
            }
            switch (mainMenu.GetUserSelection()) {
                case 0:
                    LogIn();
                    break;
                case 1:
                    CreateAccount();
                    break;

            }
        }
        static private void LogIn() {
            Account account = new Account();
            account.LogIn();
        }
        static private void CreateAccount() {
            Account account = new Account();
            account.CreateNewAccount();
        }
    }
}