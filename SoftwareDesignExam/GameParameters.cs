﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public static class GameParameters
    {
        public static int TimesUpTime { get; } = 1000;      // Milliseconds before timer stops and a game is over
        public static int LeastRandomVal { get; } = 2000;   // Least    amount of milliseconds before the light turns green
        public static int MaxRandomVal { get; } = 5000;     // Maximum  amount of milliseconds before the light turns green

        // Multipliers for calculating player scores
        public static Dictionary<PlayerType, double> multipliers = new() {
            { PlayerType.Easy, 1.1 },
            { PlayerType.Normal, 1 }
        };

        public static double CalculateScore(PlayerType playerType, int timeInMs)
        {
            return 1000 - timeInMs / multipliers[playerType];
        }
    }
}