using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam {
    internal class NormalPlayer : IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public double ScoreMultiplier { get; set; } = 1.0f;

        public NormalPlayer(string name)
        {
            Name = name;
            Score = 0;
        }
    }
}
