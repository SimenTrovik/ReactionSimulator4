namespace SoftwareDesignExam {
    public interface IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        int TimeInMs { get; set; }

        public PlayerType GetPlayerType();
        
        public static bool operator <(IPlayer player1, IPlayer player2)
        {
            if (player1.Score < player2.Score) return true;
            return false;
        }
        public static bool operator >(IPlayer player1, IPlayer player2)
        {
            if (player1.Score > player2.Score) return true;
            return false;
        }
    }
    
}
