using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SoftwareDesignExam.ScoreDB;

namespace SoftwareDesignExam.WPF
{
    #region Delegates
    public delegate void LoadHighScoreEvent(object sender, EventArgs e);
    #endregion

    public partial class ShowHighScorePage
    {
        #region Events
        public event ShowMenuClickEvent ShowMenuClickEvent;
        public event LoadHighScoreEvent LoadHighScoreEvent;
        #endregion

        #region Constructor
        public ShowHighScorePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            ShowMenuClickEvent?.Invoke(this, e);
        }

        // Populates the high score table
        public void DisplayHighScore(List<HighScore> highScoresList)
        {
            LvDataBinding.ItemsSource = highScoresList;
        }

        // Gets the list of high scores from the db
        private void HighScoreListBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadHighScoreEvent?.Invoke(this, e);
        }
        #endregion
    }
}