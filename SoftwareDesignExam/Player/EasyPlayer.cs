using System;
using System.Collections.Generic;
namespace SoftwareDesignExam
{
    internal class EasyPlayer : IPlayer
    {
        public string Name { get; set; }
        public int Score { get;  set; }
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
        public EasyPlayer(string name, double multiplier)
        {
            Name = name;
            Score = 0;
            ScoreMultiplier = multiplier;
        }
        public PlayerType GetPlayerType()
        {
            return PlayerType.Easy;
        }
    }
}
