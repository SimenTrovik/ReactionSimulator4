using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareDesignExam.WPF
{
    #region Delegates
    public delegate void RegisterPlayerEvent(object sender, PlayerEventArgs e);
    public delegate void StartGameEvent(object sender, EventArgs e);
    #endregion

    public partial class RegisterPlayerPage
    {
        #region Events
        public event RegisterPlayerEvent RegisterPlayerEvents;
        public event StartGameEvent StartGameEvent;
        #endregion

        #region Fields
        private bool _isListeningForKeys;
        private Key _currentKey = Key.A;
        private readonly List<Key> _keyList = new();
        private readonly List<string> _activePlayersList = new();
        #endregion

        #region Constructor
        public RegisterPlayerPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        // Starts the game, if there is at least one player registered
        private void StartGame(object sender, EventArgs e)
        {
            if (_keyList.Count <= 0) return;
            _keyList.Clear();
            StartGameEvent?.Invoke(this, e);
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPressGrid.Visibility = Visibility.Visible;
            _isListeningForKeys = true;
        }

        // Checks if the pressed key is a Key in the english alphabet
        private void RegisterPlayerP_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isListeningForKeys && e.Key is >= Key.A and <= Key.Z)
            {
                SetCurrentKey(e.Key);
            }
        }

        // Checks if the pressed key is in use, and gives user feedback
        private void SetCurrentKey(Key key)
        {
            _currentKey = key;
            CurrKey.Text = "Your chosen key: " + _currentKey;
            CurrKey.Foreground = _keyList.Contains(_currentKey) ? Colors.Red : Colors.Green;
        }

        private void ConfirmPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            var name = InputNameTextBox.Text;

            // Checks if chosen key or name is taken
            if (_keyList.Contains(_currentKey))
            {
                MessageBox.Show("That key is taken!");
            }
            else if (_activePlayersList.Contains(name))
            {
                MessageBox.Show("That name is taken!");
            }
            else
            {
                // If input is valid, the new player is added to the game
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

                // This is a cap we chose. If cap is to be increased,
                // additional boxes need to be added to the WPF
                if (_keyList.Count < 6)
                {
                    RegisterPlayerEvents?.Invoke(this, data);
                    _keyList.Add(_currentKey);
                    _activePlayersList.Add(name);
                }
                ResetFields();
            }
        }

        public void ClearActivePlayers()
        {
            _activePlayersList.Clear();
        }

        // Resets input fields between registering players
        private void ResetFields()
        {
            NormalRadio.IsChecked = true;
            InputNameTextBox.Text = "";
            CurrKey.Text = "Your chosen key: ";
            KeyPressGrid.Visibility = Visibility.Hidden;
            _isListeningForKeys = false;
        }
        #endregion
    }
}