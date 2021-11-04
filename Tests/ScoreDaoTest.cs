using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SoftwareDesignExam;
using SoftwareDesignExam.ScoreDB;

namespace Tests
{
	public class ScoreDaoTest
	{
		[SetUp]
		public void EmptyHighScoreTable()
		{
			using ScoreContext db = new();

			db.HighScores.RemoveRange(db.HighScores.ToList());

			db.SaveChanges();
		}

		[Test]
		public void ShouldUpdateScore()
		{
			ScoreDao.SaveScoreAndPlayer("Mario" , 64);
			ScoreDao.SaveScore("Mario", 1);

			int? mariosUpdatedScore = ScoreDao.GetScore("Mario");

			Assert.AreEqual(1, mariosUpdatedScore);
		}

		[Test]
		public void ShouldSaveScoreAndPlayer()
		{
			ScoreDao.SaveScoreAndPlayer("Halla", 1);
			ScoreDao.SaveScoreAndPlayer("Balla", 5);

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
			ScoreDao.SaveScoreAndPlayer("Doge", 420);

			Assert.AreEqual(420, ScoreDao.GetScore("Doge"));
		}

		[Test]
		public void ShouldGiveListBasedOnHighestScore()
		{
			ScoreDao.SaveScoreAndPlayer("Forsen" , 3);
			ScoreDao.SaveScoreAndPlayer("Asmongold" , 5);
			ScoreDao.SaveScoreAndPlayer("Tyler1" , 1);

			Assert.AreEqual("Asmongold", ScoreDao.GetHighScores()[0]);
			Assert.AreEqual("Tyler1", ScoreDao.GetHighScores()[2]);
		}


	}
}