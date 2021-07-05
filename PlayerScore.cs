using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    class PlayerScore
    {
        public int Lives { get; private set; }
        public int GameTime { get; private set; }
        public string Name { get; private set; }
        public int Points { get; private set; }
        public string Id { get; private set; }

        public PlayerScore(string name, int lives, int gameTime, int points, string id)
        {
            Name = name;
            Lives = lives;
            GameTime = gameTime;
            Points = points;
            Id = id;
        }

        public PlayerScore(string name, int lives, int gameTime, int points)
        {
            Name = name;
            Lives = lives;
            GameTime = gameTime;
            Points = points;
            Id = CreateId();
        }

        private string CreateId()
        {
            return Guid.NewGuid().ToString();
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
