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
        private MenyPage _menyPage = new();
        private GamePage _gamePage = new();

        public GameLogic()
        {

            _mainWindow = new MainWindow();

            _mainWindow.Show();

            SetupPagesWithDelegateEvents();

            NavigateToMenyPage();

        }

        private void ClearListedPlayers()
        {
            _playerManager.ResetPlayers();
            _registerPlayerPage.PlayerListBlock.Text = "";
        }

        private void ActiveGame()
        {
            _activePlayerKeys = _playerManager.GetPlayerKeys();
            _gamePage.Start();
            Task.Run(() => {
                while (_activePlayerKeys.Count != 0)
                {

                }
                _gamePage.Stop();
            });
        }

        private void NavigateToMenyPage() 
        {
            _mainWindow.MainFrame.Navigate(_menyPage);
        }

        private void NavigateToRegisterPlayerPage() 
        {
            ClearListedPlayers();
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

        private void NavigateToMenyPageEventHandler(object sender, EventArgs e) 
        {
            NavigateToMenyPage();
        }

        private void DisplayPlayersEventHandler(Object sender, PlayerEventArgs e)
        {
            _registerPlayerPage.PlayerListBlock.Text += 
                $"Name: {e.Name}\n" +
                $"Difficulty: {e.PlayerType}\n" +
                $"Key: {e.Key}\n";
        }

        private void AddPlayerEventHandler(Object sender, PlayerEventArgs e)
        {
            _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGameEventHandler(object sender, EventArgs e)
        {
            NavigateToGamePage();
            ActiveGame();
        }

        private void PlayAgainEventHandler(object sender, EventArgs e) 
        {
            ActiveGame();
        }


        private void RegisterInputEventHandler(object sender, KeyEventArgs e)
        {
            if (_activePlayerKeys.Contains(e.Key))
            {
                int time = _timer.TimeLeft();
                _playerManager.RegisterTime(e.Key, time);
                _activePlayerKeys.Remove(e.Key);

                if (!_timer.FinishedTimer && _timer.TimeLeft() == 0)
                {
                    _gamePage.ScoreText.Text +=
                    $"\n {_playerManager.GetPlayerByKey(e.Key).Name}: EARLY START";
                } else
                { 
                    _gamePage.ScoreText.Text +=
                    $"\n {_playerManager.GetPlayerByKey(e.Key).Name}: {_playerManager.GetPlayerByKey(e.Key).Score}";
                }
            }
        }

        private void SetupPagesWithDelegateEvents() 
        {
            _registerPlayerPage.registeredPlayerEvents += AddPlayerEventHandler;
            _registerPlayerPage.registeredPlayerEvents += DisplayPlayersEventHandler;
            _registerPlayerPage.startGameEvent += StartGameEventHandler;

            _gamePage.registeredPlayerClickEvent += RegisterInputEventHandler;
            _gamePage.playAgainClickEvent += PlayAgainEventHandler;
            _gamePage.showMenyClickEvent += NavigateToMenyPageEventHandler;

            _menyPage.startNewGameEvent += NavigateToRegisterPlayersPageEventHandler;
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
