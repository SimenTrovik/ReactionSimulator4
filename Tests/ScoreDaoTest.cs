using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SoftwareDesignExam;
using SoftwareDesignExam.ScoreDB;
using Key = System.Windows.Input.Key;

namespace Tests
{
	public class ScoreDaoTest
	{
		private PlayerManager _manager;
        private ScoreDao _scoreDao; 

		[SetUp]
		public void EmptyHighScoreTable()
		{
			using ScoreContext db = new();

			// Deletes and creates the DB
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();

			/*
			 * for å tømme db kjør linjen under:
			 * db.HighScores.RemoveRange(db.HighScores.ToList());
			 *
			 */

			_manager = new PlayerManager();
            _scoreDao = new();
		}


		[Test]
		public void ShouldSaveScoreAndPlayer()
        {
            ScoreDao _scoreDao = new();
			_manager.AddPlayer("Halla", PlayerType.Normal, Key.D);
			var player1 = _manager.GetPlayerByKey(Key.D);

			_manager.AddPlayer("Hei", PlayerType.Easy, Key.A);
			var player2 = _manager.GetPlayerByKey(Key.A);

			
			_scoreDao.SavePlayer(player1);
			_scoreDao.SavePlayer(player2);

			using ScoreContext db = new();

			List<string> playerNames = db.HighScores
				.Select(c => c.PlayerName)
				.ToList();

			Assert.That(playerNames.Contains("Halla"));
			Assert.That(playerNames.Contains("Hei"));

		}


		[Test]
		public void ShouldGiveListBasedOnHighestScore()
		{

			_manager.AddPlayer("Forsen", PlayerType.Normal, Key.D);
			var player1 = _manager.GetPlayerByKey(Key.D);

			_manager.AddPlayer("Asmongold", PlayerType.Normal, Key.A);
			var player2 = _manager.GetPlayerByKey(Key.A);

			_manager.AddPlayer("Tyler1", PlayerType.Normal, Key.B);
			var player3 = _manager.GetPlayerByKey(Key.B);

			_manager.RegisterPlayerReactionTime(Key.D, 1100);

			_manager.RegisterPlayerReactionTime(Key.A, 1200);

			_manager.RegisterPlayerReactionTime(Key.B, 1000);

            _scoreDao.SavePlayer(player1);
            _scoreDao.SavePlayer(player2);
            _scoreDao.SavePlayer(player3);

			Assert.AreEqual("Asmongold", _scoreDao.GetHighScores()[0].PlayerName);
			Assert.AreEqual("Tyler1", _scoreDao.GetHighScores()[2].PlayerName);
		}


		//Lidl test pls improve D:
		[Test]
		public void ShouldSaveListOfPlayersWithScore()
		{
			_manager.AddPlayer("Simen", PlayerType.Normal, Key.L);
			_manager.AddPlayer("Martin", PlayerType.Easy, Key.M);
			_manager.AddPlayer("Steffan", PlayerType.Normal, Key.F);
			_manager.AddPlayer("Torstein", PlayerType.Easy, Key.A);
			_manager.AddPlayer("Ruben", PlayerType.Easy, Key.O);

			_manager.RegisterPlayerReactionTime(Key.L, 1000);

			var list = _manager.GetPlayerList();

            _scoreDao.SaveListOfPlayers(list);

			var list2 = _scoreDao.GetHighScores();

			Assert.AreEqual(1, list2.Count);

		}
	

	}
}