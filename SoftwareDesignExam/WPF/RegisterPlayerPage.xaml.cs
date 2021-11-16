using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace SoftwareDesignExam.WPF {
    /// <summary>
    /// Interaction logic for RegisterPlayerPage.xaml
    /// </summary>

    public delegate void RegisteredPlayerEvent(object sender, PlayerEventArgs e);
    public delegate void StartGameEvent(object sender, EventArgs e);

    public partial class RegisterPlayerPage : Page {
        private bool isListeningForKeys = false;
        private Key currentKey;
        public event RegisteredPlayerEvent registeredPlayerEvents;
        public event StartGameEvent startGameEvent;

        public RegisterPlayerPage() {
            InitializeComponent();
            KeyPressGrid.Opacity = 0;
        }

        private void StartGame(object sender, EventArgs e) {
            startGameEvent.Invoke(this, e);
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e) {
            KeyPressGrid.Opacity = 1;
            isListeningForKeys = true;
        }

        private void RegisterPlayerP_KeyDown(object sender, KeyEventArgs e) {
            if (isListeningForKeys && e.Key is >= Key.A and <= Key.Z) {
                SetCurrentKey(e.Key);
            }
        }

        public void DisplayPlayer(PlayerEventArgs e)
        {
            PlayerListBlock.Text +=
                $"Name: {e.Name}\n" +
                $"Difficulty: {e.PlayerType}\n" +
                $"Key: {e.Key}\n";
        }

        private void SetCurrentKey(Key key) {
            currentKey = key;
            CurrKey.Text = "Your chosen key: " + currentKey;
        }

        private void ConfirmPlayerButton_Click(object sender, RoutedEventArgs e) {
            string name = InputNameTextBox.Text;
            PlayerType playerType = PlayerType.Normal;
            if (NormalRadio.IsChecked != null && NormalRadio.IsChecked.Value) {
                playerType = PlayerType.Normal;
            } else if (EasyRadio.IsChecked != null && EasyRadio.IsChecked.Value) {
                playerType = PlayerType.Easy;
            }

            Key key = currentKey;
            var data = new PlayerEventArgs();
            data.Name = name;
            data.PlayerType = playerType;
            data.Key = key;

            registeredPlayerEvents.Invoke(this, data);

            ResetFields();
        }

        public void ClearDisplayedPlayers()
        {
            PlayerListBlock.Text = "";
        }

        private void ResetFields() {
            NormalRadio.IsChecked = true;
            InputNameTextBox.Text = "";
            currentKey = new();
            KeyPressGrid.Opacity = 0;
        }
    }
}
