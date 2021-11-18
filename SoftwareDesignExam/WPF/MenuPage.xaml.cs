using System;
using SoftwareDesignExam.ScoreDB;

namespace SoftwareDesignExam.WPF {
    #region Delegates
    public delegate void StartNewGameEvent(object sender, EventArgs e);
    public delegate void ShowHighScoreEvent(object sender, EventArgs e);
    #endregion

    public partial class MenuPage{
        #region Events
        public event StartNewGameEvent StartNewGameEvent;
        public event ShowHighScoreEvent ShowHighScoreEvent;
        #endregion

        #region Constructor
        public MenuPage() {
            InitializeComponent();
            using ScoreContext db = new();
            db.Database.EnsureCreated();
        }
        #endregion

        #region Methods
        private void NewGame(object sender, EventArgs e)
        {
            StartNewGameEvent?.Invoke(this, e);
        }

        private void ShowHighScores(object sender, EventArgs e)
        {
            ShowHighScoreEvent?.Invoke(this, e);
        }
        #endregion
    };
}