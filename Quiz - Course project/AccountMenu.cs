using System;
using System.Collections.Generic;
using System.IO;

namespace Quiz___Course_project
{
    class AccountMenu
    {
        List<User> users;
        User user;
        Game game;

        public AccountMenu(User user, List<User> users)
        {
            this.user = user;
            this.users = users;
            game = null;
        }

        public int showMenu()
        {
            if (user.GetName() == null)
                return 4;
            int answer;
            bool check = false;
            Console.WriteLine("What do you like to do?");
            Console.WriteLine("1 - Start a new game.");
            Console.WriteLine("2 - Look at the ranking table.");
            Console.WriteLine("3 - Change the account's password.");
            Console.WriteLine("4 - Exit from the game.");
            answer = MainScreen.Menu(4);
            switch (answer)
            {
                case 1:
                    game = new Game(user, users);
                    Difficult();
                    game.StartGame(MainScreen.Menu(3));
                    break;
                case 2:
                    ShowRanking();
                    break;
                case 3:
                    Console.WriteLine("Dear user, please input the old password.");
                    if (user.GetPassword() == SetPassword())
                    {
                        game = new Game(user, users);
                        Console.WriteLine("\nInput the new password.");
                        user.SetPassword(SetPassword());
                        foreach (User user in users)
                        {
                            if (user.GetName() == this.user.GetName())
                            {
                                user.SetPassword(this.user.GetPassword());
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            game.SaveUsers(this.users);
                            Console.WriteLine("Password is already updated!\n");
                        }
                        else
                        {
                            Console.WriteLine("\nUser is not present in ranking table.");
                            Console.WriteLine("Please, try the game firts, please.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nSorry, but password is incorrect.");
                        Console.WriteLine("Password can not be changed and updated.\n");
                    }
                    break;
                case 4:
                    break;
            }
            return answer;
        }

        public void Difficult()
        {
            Console.WriteLine("Dear user, pick up the game's difficult.");
            Console.WriteLine("1st level (digit 1) - 10 questions with 60 sec. per each.");
            Console.WriteLine("2nd level (digit 2)- 20 questions with 40 sec. per each.");
            Console.WriteLine("3nd level (digit 3) - 30 questions with 20 sec. per each.");
            Console.WriteLine("Any time you can left the game, but the points quantity you will receive less.");
        }

        void ShowRanking()
        {
            foreach (User user in users)
                Console.WriteLine(user);
            Console.WriteLine();
        }

        string SetPassword()
        {
            string password;
            Console.WriteLine("It is must be 4 symbols (letters and/or digits)");
            Console.Write("Input the password here -> ");
            do
            {
                password = Console.ReadLine();
            } while (password.Length < 1);
            return password;
        }
    }
}
