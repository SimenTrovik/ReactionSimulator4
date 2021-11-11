using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoftwareDesignExam;
using SoftwareDesignExam.ScoreDB;
using System.Windows.Input;
using Key = System.Windows.Input.Key;

namespace Tests
{
	public class ScoreDaoTest
	{
		private PlayerManager _manager;


		[SetUp]
		public void EmptyHighScoreTable()
		{
			using ScoreContext db = new();

			//db.Database.Migrate(); lager Databasen når koden blir kjørt akkurat som "update-database" (tror jeg)

			db.HighScores.RemoveRange(db.HighScores.ToList());

			db.SaveChanges();

			_manager = new PlayerManager();

		}


		[Test]
		public void ShouldSaveScoreAndPlayer()
		{
			_manager.AddPlayer("Halla", PlayerType.Normal, Key.D);
			var player6 = _manager.GetPlayerByKey(Key.D);

			_manager.AddPlayer("Hei", PlayerType.Easy, Key.A);
			var player5 = _manager.GetPlayerByKey(Key.A);

			
			ScoreDao.SaveScoreAndPlayer(player5);
			ScoreDao.SaveScoreAndPlayer(player6);

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

			_manager.RegisterTime(Key.D, 1100);

			_manager.RegisterTime(Key.A, 1200);

			_manager.RegisterTime(Key.B, 1000);

			ScoreDao.SaveScoreAndPlayer(player1);
			ScoreDao.SaveScoreAndPlayer(player2);
			ScoreDao.SaveScoreAndPlayer(player3);

			Assert.AreEqual("Asmongold", ScoreDao.GetHighScores()[0].PlayerName);
			Assert.AreEqual("Tyler1", ScoreDao.GetHighScores()[2].PlayerName);
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

			_manager.RegisterTime(Key.L, 1000);

			var list = _manager.GetPlayersAsList();

			ScoreDao.SaveListOfPlayers(list);

			var list2 = ScoreDao.GetHighScores();

			Assert.AreEqual(1, list2.Count);

		}
	

	}
}