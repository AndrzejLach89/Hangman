using System;

namespace Hangman
{
    class PlayerScore : IComparable<PlayerScore>
    {
        public int Lives { get; private set; }
        public int GameTime { get; private set; }
        public string Name { get; private set; }
        public int Points { get; private set; }
        public string Id { get; private set; }
        public int Rounds { get; private set; }

        public PlayerScore(string name, int lives, int gameTime, int points, int rounds, string id)
        {
            Name = name;
            Lives = lives;
            GameTime = gameTime;
            Points = points;
            Id = id;
            Rounds = rounds;
        }

        public PlayerScore(string name, int lives, int gameTime, int points, int rounds)
        {
            Name = name;
            Lives = lives;
            GameTime = gameTime;
            Points = points;
            Rounds = rounds;
            Id = CreateId();
        }

        public override string ToString()
        {
            string output = Name + " | " + Lives.ToString() + " | " + GameTime.ToString() + " | " + Points.ToString() + " | " + Rounds.ToString() + " | " + Id;
            return output;
        }

        private string CreateId()
        {
            var time = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            return time;//Guid.NewGuid().ToString();
        }

        public int CompareTo(PlayerScore comparedScore)
        {
            if (comparedScore == null)
            {
                return 1;
            }
            else
            {
                return this.Points.CompareTo(comparedScore.Points);
            }
        }
    }
}
