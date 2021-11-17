using System.Collections.Generic;

namespace SoftwareDesignExam
{
    public static class GameParameters
    {
        public static int ReactionDeadline { get; } = 1000;     // Milliseconds before timer stops and a game is over
        public static int StartTimeMinimum { get; } = 2000;     // Minimum amount of milliseconds before the light turns green
        public static int StartTimeMaximum { get; } = 5000;     // Maximum amount of milliseconds before the light turns green
        readonly private static double EasyPlayerMultiplyer = 1.1;
        readonly private static double NormalPlayerMultiplyer = 1;

        // Multipliers for calculating player scores
        public static Dictionary<PlayerType, double> multipliers = new() {
            { PlayerType.Easy, EasyPlayerMultiplyer },
            { PlayerType.Normal, NormalPlayerMultiplyer }
        };

        public static double CalculateScore(PlayerType playerType, int timeInMs)
        {
            // Calculations for player scores
            if (Timer.Instance().WaitingToStart) return 0;
            return 1000 - timeInMs / multipliers[playerType];
        }
    }
}
