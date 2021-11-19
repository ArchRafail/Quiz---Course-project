using System;
using System.Collections.Generic;

namespace Quiz___Course_project
{
    class MainScreen
    {
        public static int Screen()
        {
            int choose;
            Console.WriteLine("Welcome!");
            Console.WriteLine("It is the first million game, but without money :).");
            Console.WriteLine("Dear user, would you like to create new account or sign in to the game?");
            Console.WriteLine("1 - Create new account.");
            Console.WriteLine("2 - Sign in to the game.");
            Console.WriteLine("3 - Exit.");
            choose = Menu(3);
            return choose;
        }

        static bool CheckLength(char[] answer) => answer.Length == 1;

        static bool CheckChar(char[] answer, int qty_props) => (answer[0] > 48 && answer[0] < (48 + qty_props + 1));

        public static int Menu(int qty_props)
        {
            Console.Write("Input the digit here -> ");
            char[] answer_char = Console.ReadLine().ToCharArray();
            int answer = 0;
            if (CheckLength(answer_char) && CheckChar(answer_char,qty_props))
                answer = Convert.ToInt32(answer_char[0] - 48);
            Console.WriteLine();
            return answer;
        }
    }
}
