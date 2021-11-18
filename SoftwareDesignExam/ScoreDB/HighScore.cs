namespace SoftwareDesignExam.ScoreDB
{
    public class HighScore
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public PlayerType Difficulty { get; set; }
    }
}