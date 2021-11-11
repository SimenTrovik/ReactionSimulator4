using System;
using System.Collections.Generic;
using System.Linq;
using SoftwareDesignExam.ScoreDB;


namespace SoftwareDesignExam {
	public class ScoreDao {

		/*
		 * MÅ GJØRES:
		 * 
		 * Sjekke canvas via tomas og finne metode for å kjøre "update-database" automatisk
		 * Fjerne unødvendige migrations
		 * Legge til / teste om auto update-database funker
		 * 
		 */


		// Saves ONE Player with anyscore into DB
		public static void SavePlayer(IPlayer player)
		{

			using ScoreContext db = new();

			HighScore highScore = new()
			{
				PlayerName = player.Name,
				Score = player.Score,
				Difficulty = player.GetPlayerType(),
				Time = player.TimeInMs
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
					SavePlayer(player);
				}
			}

		}



		//Returns All players on DB ordered by highest score to lowest
		public static List<HighScore> GetHighScores()
		{
			using ScoreContext db = new();

			var playerHighScores = db.HighScores
				.OrderByDescending(p => p.Score)
				.Select(p => new HighScore
				{
					Id = p.Id,
					PlayerName = p.PlayerName,
					Score = p.Score,
					Time = p.Time,
					Difficulty = p.Difficulty
				}).ToList();

			return playerHighScores;
		}

	}
}
