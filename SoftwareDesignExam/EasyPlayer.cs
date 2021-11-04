using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    internal class EasyPlayer : IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public double ScoreMultiplier { get; set; } = 0.8f;

        public EasyPlayer(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
