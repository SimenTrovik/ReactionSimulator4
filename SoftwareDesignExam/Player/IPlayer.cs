namespace SoftwareDesignExam
{
    public interface IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int TimeInMs { get; set; }

        public PlayerType GetPlayerType();

        // Overloads the < and > operators to compare IPlayers based on their score
        public static bool operator <(IPlayer player1, IPlayer player2)
        {
            return player1.Score < player2.Score;
        }
        public static bool operator >(IPlayer player1, IPlayer player2)
        {
            return player1.Score > player2.Score;
        }
    }
}