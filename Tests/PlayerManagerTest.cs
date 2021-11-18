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
            _manager.AddPlayer("student1", PlayerType.Normal, Key.D);
            _manager.AddPlayer("Kjell", PlayerType.Easy, Key.A);

            var newPlayer1 = _manager.GetPlayerByKey(Key.D);
            var newPlayer2 = _manager.GetPlayerByKey(Key.A);

            Assert.That(newPlayer1.Name == "student1");
            Assert.That(newPlayer1.GetPlayerType() == PlayerType.Normal);
            Assert.That(newPlayer2.Name == "Kjell");
            Assert.That(newPlayer2.GetPlayerType() == PlayerType.Easy);
        }
        [Test]
        public void ShouldReturnPlayersAsList()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);
            _manager.AddPlayer("student3", PlayerType.Normal, Key.F);
            _manager.AddPlayer("student4", PlayerType.Easy, Key.A);
            _manager.AddPlayer("student5", PlayerType.Easy, Key.O);

            var list = _manager.GetPlayerList();

            Assert.That(list.Exists(item => item.Name == "student1" && item.GetPlayerType() == PlayerType.Normal));
            Assert.That(list.Exists(item => item.Name == "student2" && item.GetPlayerType() == PlayerType.Easy));
            Assert.That(list.Exists(item => item.Name == "student3" && item.GetPlayerType() == PlayerType.Normal));
            Assert.That(list.Exists(item => item.Name == "student4" && item.GetPlayerType() == PlayerType.Easy));
            Assert.That(list.Exists(item => item.Name == "student5" && item.GetPlayerType() == PlayerType.Easy));

            Assert.That(list.Count == 5);
        }
        [Test]
        public void ShouldCalculateScoreCorrectly()
        {
            Assert.That(_manager.AddPlayer("student1", PlayerType.Normal, Key.L));
            Assert.True(_manager.AddPlayer("student2", PlayerType.Easy, Key.M));

            _manager.RegisterPlayerReactionTime(Key.L, 250);
            _manager.RegisterPlayerReactionTime(Key.M, 250);

            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Normal, 250) - 750) < 1);
            Assert.That(Math.Abs(GameConfig.CalculateScore(PlayerType.Easy, 250) - 775) < 1);
        }
        [Test]
        public void ShouldResetPlayers() {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);

            _manager.ResetPlayers();

            Assert.That(_manager.GetAmountOfPlayers() == 0);
        }
        [Test]
        public void ShouldGetWinner() {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);

            _manager.RegisterPlayerReactionTime(Key.L, 300);
            _manager.RegisterPlayerReactionTime(Key.M, 200);

            Assert.AreEqual(_manager.GetPlayerByKey(Key.M),_manager.GetWinner());
        }

        [Test]
        public void ShouldReturnKeyIsTaken()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            Assert.True(_manager.IsKeyTaken(Key.L));
        }

        [Test]
        public void ShouldGetWinnerNoone()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);

            _manager.RegisterPlayerReactionTime(Key.L, 0);
            _manager.RegisterPlayerReactionTime(Key.M, 0);

            //This has to be done to force the scores to be zero,
            //because the score calculation is dependant on an active timer
            _manager.ResetScores();

            var winner = _manager.GetWinner();
            Assert.AreEqual(winner.Name, "No one");
        }

        [Test]
        public void ShouldReturnAmountOfPlayers()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);
            _manager.AddPlayer("student6", PlayerType.Normal, Key.F);
            _manager.AddPlayer("student7", PlayerType.Easy, Key.R);

            Assert.AreEqual(4, _manager.GetAmountOfPlayers());
        }
        [Test]
        public void ShouldReturnPlayerKeysAsList()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);
            _manager.AddPlayer("student6", PlayerType.Normal, Key.F);
            _manager.AddPlayer("student7", PlayerType.Easy, Key.R);

            var list = _manager.GetPlayerKeyList();

            Assert.That(list.Contains(Key.L));
            Assert.That(list.Contains(Key.M));
            Assert.That(list.Contains(Key.F));
            Assert.That(list.Contains(Key.R));
        }
        [Test]
        public void ShouldResetScores()
        {
            _manager.AddPlayer("student1", PlayerType.Normal, Key.L);
            _manager.AddPlayer("student2", PlayerType.Easy, Key.M);
            
            _manager.RegisterPlayerReactionTime(Key.L, 200);
            _manager.RegisterPlayerReactionTime(Key.M, 300);

            _manager.ResetScores();

            Assert.That(_manager.GetPlayerByKey(Key.L).Score == 0);
            Assert.That(_manager.GetPlayerByKey(Key.M).Score == 0);
        }
    }
}