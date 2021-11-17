using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    class GameLogic
    {
        private MainWindow _mainWindow;
        private MenuPage _menuPage = new();
        private RegisterPlayerPage _registerPlayerPage = new();
        private GamePage _gamePage = new();
        private Timer _timer = Timer.Instance();
        private ScoreDao _scoreDao = new();
        private PlayerManager _playerManager = new();
        private List<Key> _activePlayerKeys = new();

        public GameLogic()
        {
            _mainWindow = new MainWindow();

            _mainWindow.Show();

            SetupDelegateEvents();

            NavigateToMenuPage();
        }

        private void ResetPlayers()
        {
            _playerManager.ResetPlayers();
            _registerPlayerPage.ClearDisplayedPlayers();
        }

        private void GameLoop()
        {
            _activePlayerKeys = _playerManager.GetPlayerKeyList();
            _gamePage.Start();
            Task.Run(() =>
            {
                while (_activePlayerKeys.Count != 0)
                {
                    Thread.Sleep(10);
                }
                _gamePage.Stop();
                SaveHighScores();

            });
        }


        private void NavigateToMenuPage()
        {
            _mainWindow.MainFrame.Navigate(_menuPage);
        }

        private void NavigateToRegisterPlayerPage()
        {
            ResetPlayers();
            _mainWindow.MainFrame.Navigate(_registerPlayerPage);
        }

        private void NavigateToGamePage()
        {
            _mainWindow.MainFrame.Navigate(_gamePage);
        }

        private void NavigateToRegisterPlayersPageEventHandler(object sender, EventArgs e)
        {
            NavigateToRegisterPlayerPage();
        }

        private void NavigateToMenuPageEventHandler(object sender, EventArgs e)
        {
            NavigateToMenuPage();
        }

        private void DisplayPlayersEventHandler(Object sender, PlayerEventArgs e)
        {
            _registerPlayerPage.DisplayPlayer(_playerManager.GetPlayerDictionary());
        }

        private void AddPlayerEventHandler(Object sender, PlayerEventArgs e)
        {
            if (!_playerManager.IsKeyTaken(e.Key))
            {
                _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
            }
            else
            {
                // Display "Key taken"
            }

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
            _activePlayerKeys.Remove(e.Key);
            _gamePage.DisplayScoreByPlayer(_playerManager.GetPlayerByKey(e.Key));
        }

        private void SetupDelegateEvents()
        {
            _registerPlayerPage.RegisterPlayerEvents += AddPlayerEventHandler;
            _registerPlayerPage.RegisterPlayerEvents += DisplayPlayersEventHandler;
            _registerPlayerPage.StartGameEvent += StartGameEventHandler;

            _gamePage.registeredPlayerClickEvent += RegisterPlayerClickEventHandler;
            _gamePage.playAgainClickEvent += PlayAgainEventHandler;
            _gamePage.showMenyClickEvent += NavigateToMenuPageEventHandler;

            _menuPage.StartNewGameEvent += NavigateToRegisterPlayersPageEventHandler;
        }
        private void SaveHighScores()
        {
            _scoreDao.SaveListOfPlayers(_playerManager.GetPlayerList());
        }

        private void PrintHighscore()
        {

        }

        private void PrintPlayer(int boxId, IPlayer p)
        {
            //
        }

        private void PrintTimer()
        {
            //
        }
    }
}
