using System;

namespace Quiz___Course_project
{
    class Program
    {
        static void Main(string[] args)
        {
            int choose = MainScreen.Screen();
            if (choose == 3 || choose == 0)
                goto Finish;
            AccountForm account = new AccountForm(choose);
            AccountMenu menu = new AccountMenu(account.GetUser(), account.GetAllUsers());
            do
            {
                choose = menu.showMenu();
            } while (choose != 4);
Finish:
            Console.WriteLine("The Quiz-game was finished.");
            Console.WriteLine("Thank's that took participation in testing.");
        }
    }
}