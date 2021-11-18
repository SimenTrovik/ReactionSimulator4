﻿namespace SoftwareDesignExam {
    public interface IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        int TimeInMs { get; set; }

        public PlayerType GetPlayerType();
    }
}
