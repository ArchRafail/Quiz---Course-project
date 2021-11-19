using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Quiz___Course_project
{
    class AccountForm
    {
        List<User> users;
        User user;
        string users_path = "Users.bin";

        public AccountForm(int number)
        {
            users = new List<User>();
            if (FileCheck())
                users = GetUsers();
            switch (number)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    SignAccount();
                    break;
            }
        }

        bool FileCheck()
        {
            FileInfo file = new FileInfo(users_path);
            if (!file.Exists)
            {
                file.Create();
                Console.WriteLine("File with user's ranking system was not exist.");
                Console.WriteLine("User's ranking with new file provided.\n");
                return false;
            }
            else return true;
        }

        List<User> GetUsers()
        {
            if (new FileInfo(users_path).Length > 0)
                using (FileStream fs = new FileStream(users_path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode))
                    {
                        User user = null;
                        while (br.PeekChar() > -1)
                        {
                            user = new User();
                            user.SetRankingPlace(br.ReadInt32());
                            user.SetName(br.ReadString());
                            user.SetPassword(br.ReadString());
                            user.SetPoints(br.ReadInt32());
                            user.SetAveragePoint(br.ReadDouble());
                            users.Add(user);
                        }
                    }
                }
            return users;
        }

        void CreateAccount()
        {
            string name;
            string password;
            int count = 0;
            do
            {
                count++;
                if (count > 1)
                {
                    Console.WriteLine("This name not correspond to the rules or present in ranking table.");
                    Console.WriteLine("Please, check your input. You can also pick up a nickname.");
                }
                name = SetName();
            } while (!CheckNewName(name));
            Console.WriteLine();
            do
            {
                password = SetPassword();
            } while (!CheckNewPassword(password));
            Console.WriteLine();
            user = new User(name, password);
        }

        void SignAccount()
        {
            string name;
            int qty_attempts = 3;
            bool check;
            do
            {
                name = SetName();
                if (check = !CheckAccount(name))
                {
                    Console.WriteLine($"Dear user, you have {qty_attempts-1} attempt/ts to enter to account");
                    qty_attempts--;
                };
            } while (check && qty_attempts > 0);
            if (qty_attempts == 0)
                user = new User();
        }

        string SetName()
        {
            string name;
            Console.WriteLine("Dear user input your name.");
            Console.WriteLine("The name must starts from the uppercase letter and contains at least 2 letters.");
            Console.Write("Input your name here -> ");
            do
            {
                name = Console.ReadLine();
            } while (name.Length < 1);
            return name;
        }

        bool CheckNewName(string name)
        {
            string name_pattern = @"^([A-Z])([a-zA-Z]+)\S*";
            Regex regex = new Regex(name_pattern);
            if (regex.IsMatch(name))
            {
                if (new FileInfo(users_path).Length > 0 && users != null)
                    foreach (User user in users)
                        if (user.GetName() == name)
                        {
                            Console.WriteLine();
                            return false;
                        }
            }
            else
            {
                Console.WriteLine();
                return false;
            }
            return true;
        }

        string SetPassword()
        {
            string password;
            Console.WriteLine("Dear user input the password.");
            Console.WriteLine("It is must be 4 symbols (letters and/or digits)");
            Console.Write("Input password here -> ");
            do
            {
                password = Console.ReadLine();
            } while (password.Length < 1);
            return password;
        }

        bool CheckNewPassword(string password)
        {
            string password_pattern = @"^([a-zA-Z0-9]){4}$";
            Regex regex = new Regex(password_pattern);
            if (regex.IsMatch(password))
                return true;
            Console.WriteLine();
            return false;
        }

        bool CheckAccount(string name)
        {
            User user1 = new User();
            bool coicidence = false;
            if (new FileInfo(users_path).Length > 0 && users != null)
            {
                foreach (User user in users)
                    if (user.GetName() == name)
                    {
                        user1 = user;
                        coicidence = true;
                        break;
                    }
                Console.WriteLine();
                if (coicidence && user1.GetPassword() == SetPassword())
                {
                    this.user = user1;
                    Console.WriteLine();
                    return true;
                }
            }
            return false;
        }

        public User GetUser() => user;

        public List<User> GetAllUsers() => users;
    }
}
