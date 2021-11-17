using System;
using NUnit.Framework;
using SoftwareDesignExam;
using PlayerType = SoftwareDesignExam.PlayerType;

namespace Tests
{
    public class GameConfigTest
    {
        [Test]
        public void ShouldCalculateScoreWithDifferentMultipliers()
        { 
            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Normal, 250) - 750) < 1);
            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Easy, 250) - 775) < 1);
        }
    }
}
