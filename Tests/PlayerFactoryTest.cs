using System;
using NUnit.Framework;
using SoftwareDesignExam;

namespace Tests
{
    internal class PlayerFactoryTest
    {
        private ConcretePlayerFactory _factory;

        [SetUp]
        public void Init()
        {
            _factory = new ConcretePlayerFactory();
        }

        [Test]
        public void ShouldCreateNormalPlayer()
        {
            var player = _factory.GetPlayer("Simen", PlayerType.Normal);

            Assert.That(player.Name == "Simen");
            Assert.That(player.ScoreMultiplier == 1.0f);
            Assert.That(player.Score == 0);
            Assert.That(player.GetPlayerType() == PlayerType.Normal);
        }
        [Test]
        public void ShouldCreateEasyPlayer()
        {
            var player = _factory.GetPlayer("Kjell", PlayerType.Easy);

            Assert.That(player.Name == "Kjell");
            Assert.That(player.ScoreMultiplier == 1.2f);
            Assert.That(player.Score == 0);
            Assert.That(player.GetPlayerType() == PlayerType.Easy);
        }
        [Test]
        public void ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(()=> _factory.GetPlayer("", PlayerType.Easy));
        }
    }
}
