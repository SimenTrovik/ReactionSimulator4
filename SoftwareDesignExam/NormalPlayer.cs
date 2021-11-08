using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    internal class NormalPlayer : IPlayer
    {
        public string Name { get; set; }
        public int Score { get; private set; }
        public double ScoreMultiplier { get; set; } = 1.0f;
        private int _timeInMs;

        public int TimeInMs
        {
            get => _timeInMs;
            set
            {
                _timeInMs = value;
                Score = (int)(_timeInMs * ScoreMultiplier);
            }
        }
        public NormalPlayer(string name)
        {
            Name = name;
            Score = 0;
        }
        public PlayerType GetPlayerType()
        {
            return PlayerType.Normal;
        }
    }
}
