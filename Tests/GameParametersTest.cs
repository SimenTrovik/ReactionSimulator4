using NUnit.Framework;
using GameParameters = SoftwareDesignExam.GameParameters;
using PlayerType = SoftwareDesignExam.PlayerType;

namespace Tests
{
    public class GameParametersTest
    {
        [Test]
        public void ShouldCalculateScoreWithDifferentMultipliers()
        {
            Assert.That(GameParameters.CalculateScore(PlayerType.Normal, 250) == 75);
            Assert.That(GameParameters.CalculateScore(PlayerType.Easy, 250) == 69);
        }
    }
}
