using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoftwareDesignExam;
using SoftwareDesignExam.ScoreDB;

namespace Tests
{
	public class ScoreDaoTest
	{

		private ConcretePlayerFactory _factory;

		[SetUp]
		public void EmptyHighScoreTable()
		{
			using ScoreContext db = new();

			//db.Database.Migrate(); lager Databasen når koden blir kjørt akkurat som "update-database" (tror jeg)

			db.HighScores.RemoveRange(db.HighScores.ToList());

			db.SaveChanges();

			_factory = new ConcretePlayerFactory();


		}


		[Test]
		public void ShouldUpdateScore()
		{

			var player = _factory.GetPlayer("Mario", PlayerFactory.PlayerType.Easy);
			player.Score = 64;
			ScoreDao.SaveScoreAndPlayer(player);

			Assert.AreEqual(64, player.Score);

			player.Score = 1;
			ScoreDao.SaveScore(player);

			int? mariosUpdatedScore = ScoreDao.GetScore(player);

			Assert.AreEqual(1, mariosUpdatedScore);
		}


		[Test]
		public void ShouldSaveScoreAndPlayer()
		{
			var player = _factory.GetPlayer("Halla", PlayerFactory.PlayerType.Normal);
			var player2 = _factory.GetPlayer("Balla", PlayerFactory.PlayerType.Normal);


			ScoreDao.SaveScoreAndPlayer(player);
			ScoreDao.SaveScoreAndPlayer(player2);

			using ScoreContext db = new();

			List<string> playerNames = db.HighScores
				.Select(c => c.PlayerName)
				.ToList();

			Assert.That(playerNames.Contains("Halla"));
			Assert.That(playerNames.Contains("Balla"));
		}


		[Test]
		public void ShouldGivePlayerScore()
		{
			var player = _factory.GetPlayer("Doge", PlayerFactory.PlayerType.Normal);
			player.Score = 420;

			ScoreDao.SaveScoreAndPlayer(player);

			Assert.AreEqual(420, ScoreDao.GetScore(player));
		}


		[Test]
		public void ShouldGiveListBasedOnHighestScore()
		{
			var player1 = _factory.GetPlayer("Forsen", PlayerFactory.PlayerType.Easy);
			player1.Score = 3;

			var player2 = _factory.GetPlayer("Asmongold", PlayerFactory.PlayerType.Normal);
			player2.Score = 5;
			
			var player3 = _factory.GetPlayer("Tyler1", PlayerFactory.PlayerType.Normal);
			player3.Score = 1;

			ScoreDao.SaveScoreAndPlayer(player1);
			ScoreDao.SaveScoreAndPlayer(player2);
			ScoreDao.SaveScoreAndPlayer(player3);

			Assert.AreEqual("Asmongold", ScoreDao.GetHighScores()[0]);
			Assert.AreEqual("Tyler1", ScoreDao.GetHighScores()[2]);
		}


		[Test]
		public void ShouldGivePlayerDifficulty()
		{

			var player = _factory.GetPlayer("Pepe", PlayerFactory.PlayerType.Easy);

			ScoreDao.SaveScoreAndPlayer(player);

			PlayerFactory.PlayerType playerDifficulty = ScoreDao.GetPlayerDifficulty(player);

			Assert.AreEqual(PlayerFactory.PlayerType.Easy, playerDifficulty);
		}


		[Test]
		public void ShouldUpdateAndGetPlayerTime()
		{
			var player = _factory.GetPlayer("TimeWizard", PlayerFactory.PlayerType.Normal);

			ScoreDao.SaveScoreAndPlayer(player);

			ScoreDao.SavePlayerTime(player, 777);

			Assert.AreEqual(777, ScoreDao.GetPlayerTime(player));


		}

	}
}