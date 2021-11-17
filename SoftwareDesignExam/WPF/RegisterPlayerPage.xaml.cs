using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private int _playerNumber;

        public RegisterPlayerPage()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, EventArgs e)
        {
            _keyList.Clear();
            _playerNumber = 0;
            Player1Box.Opacity = 0;
            Player2Box.Opacity = 0;
            Player3Box.Opacity = 0;
            Player4Box.Opacity = 0;
            Player5Box.Opacity = 0;
            Player6Box.Opacity = 0;
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

        public void DisplayPlayer(PlayerEventArgs e)
        {

            string name = "Name: " + e.Name;
            string difficulty = "Difficulty: " + e.PlayerType;
            string key = "Key: " + e.Key;
            _playerNumber++;

                switch (_playerNumber)
                {
                    case 1:
                        Player1Box.Opacity = 1;
                        Box1Name.Text = name;
                        Box1Difficulty.Text = difficulty;
                        Box1Key.Text = key;
                        break;
                    case 2:
                        Player2Box.Opacity = 1;
                        Box2Name.Text = name;
                        Box2Difficulty.Text = difficulty;
                        Box2Key.Text = key;
                        break;
                    case 3:
                        Player3Box.Opacity = 1;
                        Box3Name.Text = name;
                        Box3Difficulty.Text = difficulty;
                        Box3Key.Text = key;
                        break;
                    case 4:
                        Player4Box.Opacity = 1;
                        Box4Name.Text = name;
                        Box4Difficulty.Text = difficulty;
                        Box4Key.Text += key;
                        break;
                    case 5:
                        Player5Box.Opacity = 1;
                        Box5Name.Text = name;
                        Box5Difficulty.Text = difficulty;
                        Box5Key.Text = key;
                        break;
                    case 6:
                        Player6Box.Opacity = 1;
                        Box6Name.Text = name;
                        Box6Difficulty.Text = difficulty;
                        Box6Key.Text = key;
                        break;
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
            Player1BoxText.Text = "";
        }

        private void ResetFields()
        {
            NormalRadio.IsChecked = true;
            InputNameTextBox.Text = "";
            CurrKey.Text = "Your chosen key: ";
            KeyPressGrid.Opacity = 0;
            _isListeningForKeys = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
