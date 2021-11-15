using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    class GameLogic
    {
        private PlayerManager _playerManager = new();
        private ScoreDao _playerDao = new();
        private MainWindow _mainWindow;
        private Timer _timer = Timer.Instance();
        private List<Key> _activePlayerKeys = new();
        private RegisterPlayerPage _registerPlayerPage = new();
        private GamePage _gamePage = new();

        public GameLogic()
        {

            _mainWindow = new MainWindow();

            _mainWindow.Show();

            RegisterPlayers();
        }

        private void RegisterPlayers()
        {
            //RegisterPlayerPage _registerPlayerPage = new();
            _mainWindow.MainFrame.Navigate(_registerPlayerPage);
            _registerPlayerPage.registeredPlayerEvents += AddPlayer;
            _registerPlayerPage.registeredPlayerEvents += DisplayPlayers;
            _registerPlayerPage.startGameEvent += StartGame;
        }

        private void DisplayPlayers(Object sender, PlayerEventArgs e)
        {
            _registerPlayerPage.PlayerListBlock.Text += $"Name: {e.Name}\nDifficulty: {e.PlayerType}\nKey: {e.Key}\n";
        }

        private void AddPlayer(Object sender, PlayerEventArgs e)
        {
            _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGame(object sender, EventArgs e)
        {

            _mainWindow.MainFrame.Navigate(_gamePage);
            _gamePage.registeredPlayerClickEvent += RegisterInput;
            ActiveGame();
        }

        private void RegisterInput(object sender, KeyEventArgs e)
        {
            if (_activePlayerKeys.Contains(e.Key))
            {
                int time = _timer.TimeLeft();
                _playerManager.RegisterTime(e.Key, time);
                _activePlayerKeys.Remove(e.Key);
                _gamePage.ScoreText.Text +=
                    $"\n {_playerManager.GetPlayerByKey(e.Key).Name}: {_playerManager.GetPlayerByKey(e.Key).Score}";
            }

        }

        private void ActiveGame()
        {
            _activePlayerKeys = _playerManager.GetPlayerKeys();
            _gamePage.Start();
            Task.Run(() =>
            {
                while (_activePlayerKeys.Count != 0)
                {

                }
                _gamePage.Stop();
            });
        }

        private void PrintHighscore()
        {
            //
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
