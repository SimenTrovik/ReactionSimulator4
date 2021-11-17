namespace SoftwareDesignExam
{
    internal class NormalPlayer : IPlayer
    {
        public string Name { get; set; }
        public int Score { get; private set; }
        public double ScoreMultiplier { get; set; }
        private int _timeInMs;

        public int TimeInMs
        {
            get => _timeInMs;
            set
            {
                _timeInMs = value;
                Score = (int)GameConfig.CalculateScore(GetPlayerType(), _timeInMs); ;
            }
        }
        public NormalPlayer(string name, double multiplier)
        {
            Name = name;
            Score = 0;
            ScoreMultiplier = multiplier;
        }
        public PlayerType GetPlayerType()
        {
            return PlayerType.Normal;
        }
    }
}
