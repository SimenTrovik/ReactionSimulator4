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
            var player = _factory.GetPlayer("student1", PlayerType.Normal);

            Assert.That(player.Name == "student1");
            //Assert.That(player.ScoreMultiplier == 10);
            Assert.That(player.Score == 0);
            Assert.That(player.GetPlayerType() == PlayerType.Normal);
        }
        [Test]
        public void ShouldCreateEasyPlayer()
        {
            var player = _factory.GetPlayer("student2", PlayerType.Easy);

            Assert.That(player.Name == "student2");
           // Assert.That(player.ScoreMultiplier == 8);
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
