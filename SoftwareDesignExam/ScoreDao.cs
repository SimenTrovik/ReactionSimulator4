using System.Collections.Generic;
using System.Linq;
using SoftwareDesignExam.ScoreDB;


namespace SoftwareDesignExam
{
    public class ScoreDao
    {
        private readonly ScoreContext _db;

        public ScoreDao()
        {
            _db = new ScoreContext();
        }

        #region Methods
        // Saves ONE Player with any score into DB
        public void SavePlayer(IPlayer player)
        {
            HighScore highScore = new()
            {
                PlayerName = player.Name,
                Score = player.Score,
                Difficulty = player.GetPlayerType(),
                Time = player.TimeInMs
            };

            _db.HighScores.Add(highScore);
            _db.SaveChanges();
        }

        // Saves List of IPlayers into DB
        public void SaveListOfPlayers(List<IPlayer> playerList)
        {
            foreach (var player in playerList)
            {
                if (player.Score != 0 && player.TimeInMs != 0) SavePlayer(player);
            }
        }

        //Returns All players on DB ordered by highest score to lowest
        public List<HighScore> GetHighScores()
        {
            var playerHighScores = _db.HighScores
                .OrderByDescending(p => p.Score)
                .Select(p => new HighScore
                {
                    Id = p.Id,
                    PlayerName = p.PlayerName,
                    Score = p.Score,
                    Time = p.Time,
                    Difficulty = p.Difficulty
                }).ToList();

            return playerHighScores;
        }
        #endregion
    }
}