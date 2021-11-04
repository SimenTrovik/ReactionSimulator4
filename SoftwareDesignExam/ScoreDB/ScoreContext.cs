using Microsoft.EntityFrameworkCore;
using SoftwareDesignExam.ScoreDB;

namespace SoftwareDesignExam.ScoreDB
{
	public class ScoreContext : DbContext
	{
		public DbSet<HighScore> HighScores { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; " +
			                            "Database = ScoreDB; " +
			                            "Trusted_Connection = True;");
		}
	}
}