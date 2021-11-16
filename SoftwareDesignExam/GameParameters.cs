using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public static class GameParameters
    {
        public static Dictionary<PlayerType, int> multipliers = new() {
            { PlayerType.Easy, 8 },
            { PlayerType.Normal, 10 }
        };

        public static int CalculateScore(PlayerType playerType, int timeInMs)
        {
            return 100 - timeInMs / multipliers[playerType];
        }
    }
}
