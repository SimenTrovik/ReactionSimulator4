using System.Collections.Generic;

namespace SoftwareDesignExam
{
    public static class GameConfig
    {
        public static int ReactionDeadline { get; } = 30000;     // Milliseconds before timer stops and a game is over
        public static int StartTimeMinimum { get; } = 2000;     // Minimum amount of milliseconds before the light turns green
        public static int StartTimeMaximum { get; } = 5000;     // Maximum amount of milliseconds before the light turns green
        private static readonly double EasyPlayerMultiplier = 0.9;
        private static readonly double NormalPlayerMultiplier = 1;

        // Multipliers for calculating player scores
        public static Dictionary<PlayerType, double> multipliers = new() {
            { PlayerType.Easy, EasyPlayerMultiplier },
            { PlayerType.Normal, NormalPlayerMultiplier }
        };

        public static double CalculateScore(PlayerType playerType, int timeInMs)
        {
            // Calculations for player scores
            if (Timer.GetInstance().WaitingToStart) return 0;
            double score = 1000 - timeInMs * multipliers[playerType];
            if (score < 0) return 0;
            return score;
        }
    }
}