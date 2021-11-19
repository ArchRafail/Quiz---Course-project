using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace Quiz___Course_project
{
    class Game
    {
        List<Quiz> quizzes;
        List<User> users;
        User user;
        Random random = new Random();
        int countDown;
        bool answerSelected;
        string quizzes_path = "Quiz And Answers.txt";
        string users_path = "Users.bin";

        public Game(User user, List<User> users)
        {
            quizzes = new List<Quiz>();
            this.user = user;
            this.users = users;

            using (StreamReader sr = new StreamReader(quizzes_path, System.Text.Encoding.Default))
            {
                string line;
                int i = 0;
                Quiz quiz = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i == 0)
                        quiz = new Quiz();
                    switch (i)
                    {
                        case 0:
                            quiz.SetQuestion(line);
                            break;
                        case 1:
                            quiz.SetAnswer1(line);
                            break;
                        case 2:
                            quiz.SetAnswer2(line);
                            break;
                        case 3:
                            quiz.SetRightAnswer(int.Parse(line));
                            break;
                        case 4:
                            quiz.SetRightString(line);
                            break;
                    }
                    i++;
                    if (i == 5)
                    {
                        quizzes.Add(quiz);
                        i = 0;
                    }
                }
            }

            for (int i = 0; i < quizzes.Count; i++)
            {
                Quiz tmp = quizzes[i];
                quizzes.RemoveAt(i);
                quizzes.Insert(random.Next(quizzes.Count), tmp);
            }

            countDown = 0;
            answerSelected = false;
        }

        bool CheckLength(char[] answer) => answer.Length == 1;

        bool CheckChar(char[] answer) => ((answer[0] > 48 && answer[0] < 53) || answer[0] == 120 || answer[0] == 88);

        int Menu()
        {
            char[] answer_char = Console.ReadLine().ToCharArray();
            answerSelected = true;
            int answer = 0;
            if (CheckLength(answer_char) && CheckChar(answer_char))
                if (answer_char[0] == 120 || answer_char[0] == 88)
                    answer = 0;
                else
                    answer = Convert.ToInt32(answer_char[0] - 48);
            Console.WriteLine();
            return answer;
        }

        public void SaveUsers(List<User> users)
        {
            using (FileStream fs = new FileStream(users_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode))
                {
                    foreach(User user in users)
                    {
                        bw.Write(user.GetRankingPlace());
                        bw.Write(user.GetName());
                        bw.Write(user.GetPassword());
                        bw.Write(user.GetPoints());
                        bw.Write(user.GetAveragePoints());
                    }
                }
            }
            Console.WriteLine("File with user's ranking system was updated.\n");
        }

        void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (answerSelected == false)
            {
                if (countDown-- <= 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Sorry, but time is out.");
                    Console.WriteLine("Please, push Enter to look at the correct answer.");
                    return;
                }
                else
                {
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine(countDown + " seconds");
                    Console.SetCursorPosition(25, 4);
                }
            }
        }

        public void StartGame(int difficult)
        {
            int answer = 0, points = 0, total_question = 1, coefficient = 2;
            if (difficult == 1)
                coefficient = 3;
            else if (difficult == 3)
                coefficient = 1;
            countDown = 60 * coefficient / 3;
            Timer aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            foreach (Quiz quiz in quizzes)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(quiz);
                Console.WriteLine("x / X - STOP the game.");
                Console.Write("Give the answer here -> ");
                aTimer.Enabled = true;
                answer = Menu();
                aTimer.Stop();
                if (answerSelected && countDown > 0)
                {
                    if (answer == 0)
                        break;
                    else if (answer == quiz.GetRightAnswer())
                        points += 10;
                }
                Console.SetCursorPosition(0, 6);
                Console.WriteLine($"Right answer: {quiz.GetRightString()}");
                Console.ReadKey();
                total_question++;
                if (total_question == difficult * 10 + 1)
                    break;
                countDown = 60 * coefficient / 3;
                answerSelected = false;
            }
            user.SetPoints(points);
            double averagepoint = difficult * ((total_question-1) * 10 * 0.6 + points * 0.4) / (difficult * 10);
            user.SetAveragePoint(averagepoint);

            bool checkinsert = false;
            if (users.Count != 0)
            {
                foreach (User user in users)
                {
                    if (this.user.GetName() == user.GetName())
                    {
                        user.SetPoints(this.user.GetPoints());
                        user.SetAveragePoint(this.user.GetAveragePoints());
                        checkinsert = true;
                        break;
                    }
                }
            }
            if (!checkinsert)
                users.Add(this.user);
            users.Sort((a,b)=>-a.GetAveragePoints().CompareTo(b.GetAveragePoints()));
            int j = 1;
            foreach (User user in users)
                user.SetRankingPlace(j++);
            Console.Clear();
            SaveUsers(this.users);
        }
    }
}