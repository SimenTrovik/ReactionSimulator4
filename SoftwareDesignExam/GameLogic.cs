using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    internal class GameLogic
    {
        #region Fields
        private readonly MainWindow _mainWindow;
        private readonly MenuPage _menuPage = new();
        private readonly RegisterPlayerPage _registerPlayerPage = new();
        private readonly ShowHighScorePage _showHighScorePage = new();
        private readonly GamePage _gamePage = new();
        private readonly Timer _timer = Timer.GetInstance();
        private readonly ScoreDao _scoreDao = new();
        private readonly PlayerManager _playerManager = new();
        private List<Key> _activePlayerKeys = new();
        private readonly SoundManager _soundManager = new();
        #endregion

        #region Constructor
        public GameLogic()
        {
            _mainWindow = new MainWindow();

            _mainWindow.Show();

            SetupDelegateEvents();

            NavigateToMenuPage();
        }
        #endregion

        #region Methods

        // This is used to reset the registered players,
        // when the user goes from an active game back to the RegisterPlayerPage
        private void ResetPlayers()
        {
            _playerManager.ResetPlayers();
            _registerPlayerPage.ClearActivePlayers();
            _mainWindow.HidePlayerBorders();
        }

        // This is the game loop. It will set up the game environment before each round
        // It will then run the Start method in GamePage, and will then loop until there are no active players left.
        // Then it stops the game, displays the winner, and saves the scores to the db
        private void GameLoop()
        {
            _playerManager.ResetScores();
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
            _activePlayerKeys = _playerManager.GetPlayerKeyList();
            _gamePage.Start();
            Task.Run(() =>
            {
                while (_activePlayerKeys.Count != 0)
                {
                    Thread.Sleep(10);
                }
                SoundManager effectSoundManager = new();
                effectSoundManager.WhistleEffect();
                _timer.TimesUp();
                SaveHighScores();
                _gamePage.DisplayWinner(_playerManager.GetWinner());
            });
        }
        private void SaveHighScores()
        {
            _scoreDao.SaveListOfPlayers(_playerManager.GetPlayerList());
        }
        #endregion

        #region PageNavigation
        // We are using 1 main window, and this window navigates to different pages
        private void NavigateToMenuPage()
        {
            _mainWindow.MainFrame.Navigate(_menuPage);
            _mainWindow.HidePlayerBorders();
            _soundManager.MainMenuMusic();
        }

        private void NavigateToRegisterPlayerPage()
        {
            ResetPlayers();
            _registerPlayerPage.ClearActivePlayers();
            _mainWindow.MainFrame.Navigate(_registerPlayerPage);
            _soundManager.RegisterPlayerMusic();
        }

        private void NavigateToShowHighScorePage()
        {
            _mainWindow.MainFrame.Navigate(_showHighScorePage);
            _soundManager.HighScoreMenuMusic();
        }

        private void NavigateToGamePage()
        {
            _mainWindow.MainFrame.Navigate(_gamePage);
            _soundManager.GamePageMusic();
        }

        private void NavigateToRegisterPlayersPageEventHandler(object sender, EventArgs e)
        {
            NavigateToRegisterPlayerPage();
        }

        private void NavigateToShowHighScorePageEventHandler(object sender, EventArgs e)
        {
            NavigateToShowHighScorePage();
        }

        private void NavigateToMenuPageEventHandler(object sender, EventArgs e)
        {
            NavigateToMenuPage();
        }
        #endregion

        #region EventHandlers
        private void DisplayPlayersEventHandler(object sender, PlayerEventArgs e)
        {
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
        }

        //gets and sends high scores to page
        private void DisplayHighScoresEventHandler(object sender, EventArgs e)
        {
            _showHighScorePage.DisplayHighScore(_scoreDao.GetHighScores());
        }

        private void AddPlayerEventHandler(object sender, PlayerEventArgs e)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (!_playerManager.IsKeyTaken(e.Key)) _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGameEventHandler(object sender, EventArgs e)
        {
            NavigateToGamePage();
            GameLoop();
        }

        private void PlayAgainEventHandler(object sender, EventArgs e)
        {
            GameLoop();
        }

        private void RegisterPlayerClickEventHandler(object sender, KeyEventArgs e)
        {
            if (!_activePlayerKeys.Contains(e.Key)) return;
            _playerManager.RegisterPlayerReactionTime(e.Key, _timer.GetTimeMs());
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
            _activePlayerKeys.Remove(e.Key);
        }
        #endregion

        #region EventSubscription
        private void SetupDelegateEvents()
        {
            _registerPlayerPage.RegisterPlayerEvents += AddPlayerEventHandler;
            _registerPlayerPage.RegisterPlayerEvents += DisplayPlayersEventHandler;
            _registerPlayerPage.StartGameEvent += StartGameEventHandler;

            _gamePage.RegisteredPlayerClickEvent += RegisterPlayerClickEventHandler;
            _gamePage.PlayAgainClickEvent += PlayAgainEventHandler;
            _gamePage.ShowMenuClickEvent += NavigateToMenuPageEventHandler;

            _menuPage.StartNewGameEvent += NavigateToRegisterPlayersPageEventHandler;
            _menuPage.ShowHighScoreEvent += NavigateToShowHighScorePageEventHandler;

            _showHighScorePage.LoadHighScoreEvent += DisplayHighScoresEventHandler;
            _showHighScorePage.ShowMenuClickEvent += NavigateToMenuPageEventHandler;
        }
        #endregion
    }
}