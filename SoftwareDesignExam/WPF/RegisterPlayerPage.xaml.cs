using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;


namespace SoftwareDesignExam.WPF
{
    /// <summary>
    /// Interaction logic for RegisterPlayerPage.xaml
    /// </summary>

    public delegate void RegisterPlayerEvent(object sender, PlayerEventArgs e);
    public delegate void StartGameEvent(object sender, EventArgs e);

    public partial class RegisterPlayerPage : Page
    {
        public event RegisterPlayerEvent RegisterPlayerEvents;
        public event StartGameEvent StartGameEvent;

        private bool _isListeningForKeys;
        private Key _currentKey = Key.A;
        private List<Key> _keyList = new();

        public RegisterPlayerPage()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, EventArgs e)
        {
            StartGameEvent?.Invoke(this, e);
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPressGrid.Opacity = 1;
            _isListeningForKeys = true;
        }

        private void RegisterPlayerP_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isListeningForKeys && e.Key is >= Key.A and <= Key.Z)
            {
                SetCurrentKey(e.Key);
            }
        }

        public void DisplayPlayer(Dictionary<Key, IPlayer> registeredPlayers)
        {
            PlayerListBlock.Text = "";

            foreach (KeyValuePair<Key, IPlayer> entry in registeredPlayers)
            {
                PlayerListBlock.Text +=
                    $"Name: {entry.Value.Name}\n" +
                    $"Difficulty: {entry.Value.GetPlayerType().ToString()}\n" +
                    $"Key: {entry.Key}\n";
            }

        }

        private void SetCurrentKey(Key key)
        {
            _currentKey = key;
            CurrKey.Text = "Your chosen key: " + _currentKey;
            if (_keyList.Contains(_currentKey)) {
                CurrKey.Foreground = Colors.Red;
            } else CurrKey.Foreground = Colors.Green;
        }

        private void ConfirmPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_keyList.Contains(_currentKey))
            {
                var name = InputNameTextBox.Text;
                var playerType = PlayerType.Normal;
                if (NormalRadio.IsChecked != null && NormalRadio.IsChecked.Value)
                {
                    playerType = PlayerType.Normal;
                }
                else if (EasyRadio.IsChecked != null && EasyRadio.IsChecked.Value)
                {
                    playerType = PlayerType.Easy;
                }

                var key = _currentKey;
                var data = new PlayerEventArgs
                {
                    Name = name,
                    PlayerType = playerType,
                    Key = key
                };

                RegisterPlayerEvents?.Invoke(this, data);
                _keyList.Add(_currentKey);

                ResetFields();
            } else
            {
                MessageBox.Show("That key is taken!");
            }
        }

        public void ClearDisplayedPlayers()
        {
            PlayerListBlock.Text = "";
        }

        private void ResetFields()
        {
            NormalRadio.IsChecked = true;
            InputNameTextBox.Text = "";
            _currentKey = Key.A;
            KeyPressGrid.Opacity = 0;
            _isListeningForKeys = false;
        }
    }
}
