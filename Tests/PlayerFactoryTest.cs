using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            var player = _factory.GetPlayer("Simen", ConcretePlayerFactory.PlayerType.Normal);

            Assert.That(player.Name == "Simen");
            Assert.That(player.ScoreMultiplier == 1.0f);
            Assert.That(player.Score == 0);

            /*
            How do??
            Assert.That(player.GetType() == typeof(NormalPlayer);
            */
        }
        [Test]
        public void ShouldCreateEasyPlayer()
        {
            var player = _factory.GetPlayer("Kjell", ConcretePlayerFactory.PlayerType.Easy);

            Assert.That(player.Name == "Kjell");
            Assert.That(player.ScoreMultiplier == 0.8f);
            Assert.That(player.Score == 0);

        }
    }
}
