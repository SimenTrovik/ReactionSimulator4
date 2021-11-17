using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<String> _activePlayersList = new();
        private int _playerNumber;

        private List<Border> _playerBorders = new();
        private List<TextBlock> _playerTextBlocks = new();

        public RegisterPlayerPage()
        {
            InitializeComponent();
            PopulateTextBoxList();
            PopulateBorderList();
        }

        private void StartGame(object sender, EventArgs e)
        {
            _keyList.Clear();
            _playerNumber = 0;
            HidePlayerBoxes();
            StartGameEvent?.Invoke(this, e);
        }

        private void HidePlayerBoxes()
        {
            for (int i = 0; i < _playerBorders.Count; i++)
            {
                _playerBorders[i].Opacity = 0;
            }
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

        public void DisplayPlayers(Dictionary<Key, IPlayer> registeredPlayers)
        {
            int _playerNumber = 0;

            foreach (KeyValuePair<Key, IPlayer> kvp in registeredPlayers)
            {
                _playerTextBlocks[_playerNumber].Text =
                    "Name: " + kvp.Value.Name + "\n" +
                    "Difficulty: " + kvp.Value.GetPlayerType() + "\n" +
                    "Key: " + kvp.Key.ToString() + "\n";
                _playerBorders[_playerNumber].Opacity = 1;

                _playerNumber++;
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
            var name = InputNameTextBox.Text;
            
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
                _activePlayersList.Add(name);
                ResetFields();
            }
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

        void PopulateTextBoxList()
        {
            _playerTextBlocks.Add(Player1Text);
            _playerTextBlocks.Add(Player2Text);
            _playerTextBlocks.Add(Player3Text);
            _playerTextBlocks.Add(Player4Text);
            _playerTextBlocks.Add(Player5Text);
            _playerTextBlocks.Add(Player6Text);
        }

        void PopulateBorderList()
        {
            _playerBorders.Add(Player1Border);
            _playerBorders.Add(Player2Border);
            _playerBorders.Add(Player3Border);
            _playerBorders.Add(Player4Border);
            _playerBorders.Add(Player5Border);
            _playerBorders.Add(Player6Border);
        }
    }
}
