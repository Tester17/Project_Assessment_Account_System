using System;


namespace Project_Assessment_Account_System {
    internal class Account {
        private Dictionary<string, string> _accounts = new Dictionary<string, string>();
        private const int _minPasswordlenght=6;
        internal Account() {
            GetAccountsFromFile();
        }
        private void GetAccountsFromFile() {
            List<string> accounts = File.ReadAllLines("accounts.txt").ToList();
            foreach (string account in accounts)      
            {
                string[] temp = account.Split(' ');
                _accounts.Add(temp[0], temp[1]);     
            }                                           

        }
        internal void LogIn() {
            const int maxNumberOfTry = 5;
            int numberOftry = 0;
            string username = GetInputFromUser("name");
            while (_accounts.ContainsKey(username) == false) {
                numberOftry++;
                if (numberOftry == maxNumberOfTry) {
                    Console.WriteLine("This name doesn't exist!!! And you don't have any more login attempts!!!");
                    CloseProgram();
                }
                Console.WriteLine("This name doesn't exist!!!" + ((maxNumberOfTry - numberOftry > 1) ? $"You have {maxNumberOfTry - numberOftry} attempts left" : "This is the last attempt!!!"));
                GetInputFromUser("name");

            }
            Console.Clear();
            string password = GetInputFromUser("password");
            numberOftry = 1;
            while (_accounts[username].ToString() != password)
            {
                numberOftry++;
                if (numberOftry == maxNumberOfTry)
                {
                    Console.WriteLine("Wrong password!!! And you don't have any more login attempts!!!");
                    CloseProgram();
                }
                Console.WriteLine("Wrong password, try again!!!" + ((maxNumberOfTry-numberOftry > 1 )?$"You have {maxNumberOfTry - numberOftry} attempts left":"This is the last attempt!!!"));
                password = GetInputFromUser("password");
            }
            Console.WriteLine($"You are log in account {username}!!!");
            if (File.ReadAllLines("admins.txt").ToList().Contains(username)){
                Menu showAccounts = new Menu("Show list of all accounts");
                switch (showAccounts.GetUserSelection()) {
                    case 0:
                        ShowListOfAccounts();
                        break;
                    default:
                        break;
                }

            }
            Congratulations();

        }
        public static void CloseProgram()
        {
            Environment.Exit(0);
        }
        private void Congratulations()
        { for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                Console.WriteLine("You are log in!!!");
                Thread.Sleep(50);
            }
            Menu congratulation = new Menu("congratulation");
            if (congratulation.GetUserSelection() == congratulation.GetLastMenuItemNumber())
            {
                Program.Main();
            }
            switch (congratulation.GetUserSelection())
            {
                case 0:
                    Congratulations();
                    break;

            }
        }
        private bool CheckUserInput(string userInput) {
            List<char> charsOfInput = userInput.ToList();
            if (charsOfInput.Contains(' ') || charsOfInput == null) {
                return false;
            }
            return true;

        }
        private string GetInputFromUser(string type) {
            Console.Write($"Enter {type}: ");
            string userInput = Console.ReadLine();
            while (CheckUserInput(userInput) == false) {
                Console.Write($"The {type} cannot contains \" \". Try again !!!\nEnter username: ");
                userInput = Console.ReadLine();
            }
            Console.Clear();
            return userInput;
            
        }
        private void ShowListOfAccounts() {
            foreach(KeyValuePair<string, string> account in _accounts) {
                if (File.ReadAllLines("admins.txt").ToList().Contains(account.Key)) {
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"Username: {account.Key} Password: {account.Value}");
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to close this list");
            Console.ReadKey();
        }
        internal void CreateNewAccount()
        {
            string newUsername = GetInputFromUser("new name");
            while (_accounts.ContainsKey(newUsername))
            {
                Console.WriteLine("This name already exist, try again!!!");
                newUsername=GetInputFromUser("new name");
            }
            Console.WriteLine($"\nYou have entered {newUsername} as a new username ");
            Menu registration = new Menu("registration");
            if (registration.GetUserSelection() == registration.GetLastMenuItemNumber())
            {
                CreateNewAccount();
            }
            switch (registration.GetUserSelection())
            {
                case 0:
                    _accounts[newUsername] = ManuallyPasswordCreation();
                    break;
                case 1:
                    _accounts[newUsername]=SetPasswordSettings();
                    break;
            }
            Console.Clear();
            File.AppendAllText("accounts.txt",newUsername+" " + _accounts[newUsername]);
            Console.WriteLine("New account has been successfully created!!!\n" +
                              "\tUsername: {0}\n"+ 
                              "\tPassword: {1}\n", newUsername, _accounts[newUsername]);

            Program.Main();
                


        }
        private string ManuallyPasswordCreation() {
            string inputPassword="";
            while (inputPassword.Length < _minPasswordlenght) {
                Console.WriteLine("Enter password, the password must be at least 6 characters long");
                inputPassword= Console.ReadLine();
             
                if (inputPassword.ToArray().ToList().Contains(' ')) {
                    Console.WriteLine("Password can't contains \" \"!!! Try again!!! ");
                }
            }
            Console.Clear();
            return inputPassword;
        }
        private string SetPasswordSettings(bool capital=true, bool numbers = true, bool symbols = true,int passwordLenght=10) {
            Console.WriteLine($"Current password generator settings:\n" +
                              $"\tPassword will contains capital letters: {capital}\n" +
                              $"\tPassword will contains numbers: {numbers}\n" +
                              $"\tPassword will contains special symbols: {symbols}\n" +
                              $"\tCurrent password lenght: {passwordLenght}");
            Menu passwordGenerationMenu = new Menu("Password Generation");
           
            if (passwordGenerationMenu.GetUserSelection() == passwordGenerationMenu.GetLastMenuItemNumber()) {
                return ManuallyPasswordCreation();
            }
            switch (passwordGenerationMenu.GetUserSelection()) {
                case 0:
                    return GeneratePassword(capital, numbers, symbols,passwordLenght);
                case 1:
                    return SetPasswordSettings(!capital, numbers, symbols, passwordLenght);
                case 2:
                    return SetPasswordSettings(capital, !numbers, symbols, passwordLenght);
                case 3:
                    return SetPasswordSettings(capital, numbers, !symbols, passwordLenght);
                default:
                    return SetPasswordSettings(capital, numbers, symbols, ChangePasswordLenght());

            }
            
        }
        private int ChangePasswordLenght() {
            int passwordLenght=0;
            while (passwordLenght < _minPasswordlenght) {
                Console.WriteLine("Enter the password length, the minimum length is {0}:",_minPasswordlenght);
                while(Int32.TryParse(Console.ReadLine(), out passwordLenght)==false) {
                    Console.WriteLine("The password length must be a number!!! Try again!!!");
                }
            }
            Console.Clear();
            return passwordLenght;
        }
        private string GeneratePassword(bool сapitalLetters, bool numbers, bool symbols, int passwordLenght) {

            string password="";
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string symbolChars = "!@#$%^&*()_+-=[]{}\\|;':\",./<>?";
            string allowedCharacters = alphabet;
            if (сapitalLetters) {
                allowedCharacters += alphabet.ToUpper();
            }
            if (numbers) {
                allowedCharacters += numberChars;
            }
            if(symbols) {
                allowedCharacters += symbolChars;
            }
            for(int i =0; i < passwordLenght; i++) {
                password += allowedCharacters[rnd.Next(allowedCharacters.Length)];
            }


            return password;
        }
    }

}
