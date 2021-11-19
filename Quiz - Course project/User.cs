using System;

namespace Quiz___Course_project
{
    class User
    {
        int ranking_place;
        string name;
        string password;
        int points;
        double average_points;

        public User()
        {
            ranking_place = 0;
            name = null;
            password = null;
            points = 0;
            average_points = 0;
        }

        public User(string name, string password)
        {
            ranking_place = 0;
            this.name = name;
            this.password = password;
            points = 0;
            average_points = 0;
        }

        public void SetRankingPlace(int number) { ranking_place = number; }
        public void SetName(string text) { name = text; }
        public void SetPassword(string text) { password = text; }
        public void SetPoints(int number) { points = number; }
        public void SetAveragePoint(double number) { average_points = number; }
        public int GetRankingPlace() => ranking_place;
        public string GetPassword() => password;
        public string GetName() => name;
        public int GetPoints() => points;
        public double GetAveragePoints() => average_points;
        public override string ToString()
        {
            return $"Ranking place: {ranking_place}\tUser name: {name}\tTotal points: {points}\tAverage points: {average_points}\n";
        }

    }
}
