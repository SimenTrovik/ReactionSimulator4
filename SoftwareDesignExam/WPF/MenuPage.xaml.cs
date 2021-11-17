using System;

using System.Windows;
using System.Windows.Controls;


namespace SoftwareDesignExam.WPF {

    public delegate void StartNewGameEvent(object sender, EventArgs e);

    public delegate void ShowHighScoreEvent(object sender, EventArgs e);

    public partial class MenuPage : Page {
        public event StartNewGameEvent StartNewGameEvent;
        public event ShowHighScoreEvent ShowHighScoreEvent;

        public MenuPage() {
            InitializeComponent();
        }

        private void NewGame(object sender, EventArgs e)
        {
            StartNewGameEvent?.Invoke(this, e);
        }

        private void ShowHighscores(object sender, EventArgs e)
        {
            ShowHighScoreEvent?.Invoke(this, e);
	        //MessageBox.Show("HIGHSCORES!");
        }
    };
}
