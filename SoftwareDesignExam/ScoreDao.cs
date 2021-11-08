using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SoftwareDesignExam.ScoreDB;


namespace SoftwareDesignExam {
	public class ScoreDao {

		/*
		 * MÅ GJØRES:
		 * GetScore / GetHighScores?
		 *
		 *	+Metodene får inn IPlayer
		 *	+GetHighScores skal sende de 10 beste
		 *
		 */

		/*
		 *	DONE:
		 *	+Time column i DB som har tiden
		 *	+Difficulty column i DB
		 *	+Lagre string verdi av difficulty NB! Bare lagret det som enum siden 
		 *	PlayerFactory lager enumene der :thumbsup:
		 *	Lagre spillere uten score? SVAR: Nei, de lages etter de har spilt en runde (kan endres ofc)
		 *
		 */


		
		//Updates score of player already inside DB
		public static void SaveScore(IPlayer player)
		{
			using ScoreContext db = new();

			var name = db.HighScores.First(c => c.PlayerName == player.Name);

			name.Score = player.Score;

			db.SaveChanges();
		}


		// Saves Player with anyscore into DB
		public static void SaveScoreAndPlayer(IPlayer player)
		{

			using ScoreContext db = new();

			//TODO
			// need a getter to grab players difficulty instead of this way
			//Veldig dårlig det her assa, men vil ikke endre på filer som andre jobber på uten å snakke med dem først
			

			HighScore highScore = new()
			{
				PlayerName = player.Name,
				Score = player.Score,
				Difficulty = player.GetPlayerType()
				
			};

			db.HighScores.Add(highScore);
			db.SaveChanges();

		}


		//Returns score saved in DB to x player
		public static int? GetScore(IPlayer player)
		{

			using ScoreContext db = new();

			int? sum = db.HighScores.First(c => c.PlayerName == player.Name).Score;

			int sumNeverNull = sum ?? 0;

			return sumNeverNull;
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


		// Returns players difficulty
		public static PlayerType GetPlayerDifficulty(IPlayer player)
		{
			using ScoreContext db = new();

			var playerType = db.HighScores.First(c => c.PlayerName == player.Name).Difficulty;

			return playerType;

		}


		//Updates score of player already inside DB
		public static void SavePlayerTime(IPlayer player, int Time)
		{
			using ScoreContext db = new();

			var name = db.HighScores.First(c => c.PlayerName == player.Name);

			name.Time = Time;

			db.SaveChanges();
		}


		// Returns player time
		public static int? GetPlayerTime(IPlayer player)
		{

			using ScoreContext db = new();

			int? playerTime = db.HighScores.First(c => c.PlayerName == player.Name).Time;

			int playerTimeNeverNull = playerTime ?? 0;

			return playerTimeNeverNull;

		}


	}

}
