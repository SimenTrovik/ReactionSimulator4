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
        private RegisteredPlayerEvent _event;

        public GameLogic() {
            _event = new(AddPlayer);

            _mainWindow = new MainWindow();
            
            _timer = Timer.Instance();

            _mainWindow.Show();

            RegisterPlayers();
        }

        private void RegisterPlayers() {
            RegisterPlayerPage registerPlayerPage = new();
            _mainWindow.MainFrame.Navigate(registerPlayerPage);
            //registerPlayerPage.registeredPlayerEvents += _event;
        }

        private void AddPlayer(string name, PlayerType playerType, Key key) {
            _playerManager.AddPlayer(name, playerType, key);
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
