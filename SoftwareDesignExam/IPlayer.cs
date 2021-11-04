using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam {
    public interface IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        double ScoreMultiplier { get; set; }
    }
    
}
