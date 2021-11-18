using System;
using System.Collections.Generic;
using System.Xaml.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoftwareDesignExam;
using NUnit.Framework;
using System.Windows.Input;
using System.Xml.Serialization;
using Key = System.Windows.Input.Key;


namespace Tests
{
    public class PlayerManagerTest
    {
        private PlayerManager _manager;
        [SetUp]
        public void Init()
        {
            _manager = new PlayerManager();
        }

        [Test]
        public void ShouldAddPlayers()
        {
            _manager.AddPlayer("Simen", PlayerType.Normal, Key.D);
            _manager.AddPlayer("Kjell", PlayerType.Easy, Key.A);

            var newPlayer1 = _manager.GetPlayerByKey(Key.D);
            var newPlayer2 = _manager.GetPlayerByKey(Key.A);

            Assert.That(newPlayer1.Name == "Simen");
            Assert.That(newPlayer1.GetPlayerType() == PlayerType.Normal);
            Assert.That(newPlayer2.Name == "Kjell");
            Assert.That(newPlayer2.GetPlayerType() == PlayerType.Easy);
        }
        [Test]
        public void ShouldReturnPlayersAsList()
        {
            _manager.AddPlayer("Simen", PlayerType.Normal, Key.L);
            _manager.AddPlayer("Martin", PlayerType.Easy, Key.M);
            _manager.AddPlayer("Steffan", PlayerType.Normal, Key.F);
            _manager.AddPlayer("Torstein", PlayerType.Easy, Key.A);
            _manager.AddPlayer("Ruben", PlayerType.Easy, Key.O);

            var list = _manager.GetPlayerList();

            Assert.That(list.Exists(item => item.Name == "Simen" && item.GetPlayerType() == PlayerType.Normal));
            Assert.That(list.Exists(item => item.Name == "Martin" && item.GetPlayerType() == PlayerType.Easy));
            Assert.That(list.Exists(item => item.Name == "Steffan" && item.GetPlayerType() == PlayerType.Normal));
            Assert.That(list.Exists(item => item.Name == "Torstein" && item.GetPlayerType() == PlayerType.Easy));
            Assert.That(list.Exists(item => item.Name == "Ruben" && item.GetPlayerType() == PlayerType.Easy));

            Assert.That(list.Count == 5);
        }
        [Test]
        public void ShouldCalculateScoreCorrectly()
        {
            Assert.That(_manager.AddPlayer("Simen", PlayerType.Normal, Key.L));
            Assert.True(_manager.AddPlayer("Martin", PlayerType.Easy, Key.M));

            _manager.RegisterPlayerReactionTime(Key.L, 250);
            _manager.RegisterPlayerReactionTime(Key.M, 250);

            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Normal, 250) - 750) < 1);
            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Easy, 250) - 775) < 1);
        }
        [Test]
        public void ShouldResetPlayers() {
            _manager.AddPlayer("Simen", PlayerType.Normal, Key.L);
            _manager.AddPlayer("Martin", PlayerType.Easy, Key.M);

            _manager.ResetPlayers();

            Assert.That(_manager.GetAmountOfPlayers() == 0);
        }
        [Test]
        public void ShouldGetWinner() {
            _manager.AddPlayer("Simen", PlayerType.Normal, Key.L);
            _manager.AddPlayer("Martin", PlayerType.Easy, Key.M);

            _manager.RegisterPlayerReactionTime(Key.L, 300);
            _manager.RegisterPlayerReactionTime(Key.M, 200);

            Assert.AreEqual(_manager.GetPlayerByKey(Key.M),_manager.GetWinner());
        }

        [Test]
        public void ShouldReturnKeyIsTaken()
        {
            _manager.AddPlayer("Simen", PlayerType.Normal, Key.L);
            Assert.True(_manager.IsKeyTaken(Key.L));
        }
        [Test]
        public void s()
        {

        }

    }
}