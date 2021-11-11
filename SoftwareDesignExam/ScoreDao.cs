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
		 * 
		 * Nytt Score/highScore object som skal lagres i en liste som blir sendt ut 
		 * via getHighScore metoden
		 * 
		 * Sjekke canvas via tomas og finne metode for å kjøre "update-database" automatisk
		 * 
		 */


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

		// Saves List of IPlayers into DB
		public static void SaveListOfPlayers(List<IPlayer> playerList)
		{
			//sjekk hvis score er 0 så skal den ikke lagres
			
			foreach (IPlayer player in playerList)
			{
				if(player.Score != 0)
				{
					SaveScoreAndPlayer(player);
				}
			}


		}


		//NOT NEEDED
		//Updates score of player already inside DB
		public static void SaveScore(IPlayer player)
		{
			using ScoreContext db = new();

			var name = db.HighScores.First(c => c.PlayerName == player.Name);

			name.Score = player.Score;

			db.SaveChanges();
		}


		//NOT NEEDED
		//Returns score saved in DB to x player
		public static int? GetScore(IPlayer player)
		{

			using ScoreContext db = new();

			int? sum = db.HighScores.First(c => c.PlayerName == player.Name).Score;

			int sumNeverNull = sum ?? 0;

			return sumNeverNull;
		}


		//NOT NEEDED
		// Returns players difficulty
		public static PlayerType GetPlayerDifficulty(IPlayer player)
		{
			using ScoreContext db = new();

			var playerType = db.HighScores.First(c => c.PlayerName == player.Name).Difficulty;

			return playerType;

		}


		//NOT NEEDED
		//Updates score of player already inside DB
		public static void SavePlayerTime(IPlayer player, int Time)
		{
			using ScoreContext db = new();

			var name = db.HighScores.First(c => c.PlayerName == player.Name);

			name.Time = Time;

			db.SaveChanges();
		}



	}

}
