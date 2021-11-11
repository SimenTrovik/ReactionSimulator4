using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    class GameLogic {
        private PlayerManager _playerManager = new();
        private ScoreDao _playerDao = new();
        private MainWindow _mainWindow;
        private Timer _timer;

        public GameLogic() {

            _mainWindow = new MainWindow();

            _timer = Timer.Instance();

            _mainWindow.Show();

            RegisterPlayers();
        }

        private void RegisterPlayers() {
            RegisterPlayerPage registerPlayerPage = new();
            _mainWindow.MainFrame.Navigate(registerPlayerPage);
            registerPlayerPage.registeredPlayerEvents += AddPlayer;
        }

        private void AddPlayer(Object sender, PlayerEventArgs e) {
            _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGame() {
            //
        }

        private void PrintHighscore() {
            //
        }

        private void PrintPlayer(int boxId, IPlayer p) {
            //
        }

        private void PrintTimer() {
            //
        }
    }
}
