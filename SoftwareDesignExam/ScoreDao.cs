using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftwareDesignExam.ScoreDB;


namespace SoftwareDesignExam {
	public class ScoreDao {

		/*
		 * Lagre spillere uten score?
		 * GetScore / GetHighScores?
		 *
		 *	+Metodene får inn IPlayer
		 *	+Time column i DB som har tiden
		 *	+Difficulty column i DB
		 *	+Lagre string verdi av difficulty
		 *	+GetHighScores skal sende de 10 beste
		 *
		 */


		//Updates score of player already inside DB
		public static void SaveScore(String PlayerName, int Score)
		{
			using ScoreContext db = new();

			var name = db.HighScores.First(c => c.PlayerName == PlayerName);

			name.Score = Score;

			db.SaveChanges();
		}


		// Saves Player with anyscore into DB
		public static void SaveScoreAndPlayer(string PlayerName, int Score)
		{

			using ScoreContext db = new();

			HighScore highScore = new()
			{
				PlayerName = PlayerName,
				Score = Score
			};

			db.HighScores.Add(highScore);
			db.SaveChanges();

		}


		//Returns score saved in DB to x player
		public static int? GetScore(string PlayerName)
		{
			using ScoreContext db = new();

			int? sum = db.HighScores.First(c => c.PlayerName == PlayerName).Score;

			if (sum == null)
			{
				return 0;
			}

			return sum;
		}


		//Returns All players on DB ordered by highest score to lowest
		public static List<string> GetHighScores()
		{
			using ScoreContext db = new();

			List<string> playerNames = db.HighScores
				.OrderByDescending(c => c.Score)
				.Select(c => c.PlayerName)
				.ToList();

			return playerNames;
		}

	}

}
