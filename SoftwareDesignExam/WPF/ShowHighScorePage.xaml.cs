using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SoftwareDesignExam.ScoreDB;

namespace SoftwareDesignExam.WPF {
	/// <summary>
	/// Interaction logic for ShowHighScorePage.xaml
	/// </summary>
	///

	public delegate void ShowMenyClickEvent2(object sender, EventArgs e);

	public delegate void LoadHighScoreEvent(object sender, EventArgs e);

	public partial class ShowHighScorePage : Page
	{
		public event ShowMenyClickEvent2 showMenyClickEvent;
		public event LoadHighScoreEvent loadHighScoreEvent;

        public ShowHighScorePage()
		{
			InitializeComponent();
        }

		private void BackToMeny(object sender, RoutedEventArgs e)
		{
			showMenyClickEvent.Invoke(this, e);
		}

		public void DisplayHighScore(List<HighScore> highScoresList)
        {
            LvDataBinding.ItemsSource = highScoresList;

            //highScoreListBlock.Text = "";

            //int counter = 0;
            //string wow = "Rank | Name | Score | Time | Difficulty \n";

            //foreach (HighScore playerHighScore in highScoresList)
            //{
            //	counter++;
            //	wow +=
            //		counter +
            //		$" {playerHighScore.PlayerName}, " +
            //		$"{playerHighScore.Score}, " +
            //		$"{playerHighScore.Time}, " +
            //		$"{playerHighScore.Difficulty.ToString()}\n";
            //}

            //highScoreListBlock.Text = wow;


        }

		private void HighScoreListBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            loadHighScoreEvent.Invoke(this, e);
		}
	}
}
