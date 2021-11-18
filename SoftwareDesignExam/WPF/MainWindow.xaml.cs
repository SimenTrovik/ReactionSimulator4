using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    public partial class MainWindow
    {
        #region Fields
        private List<Border> _playerBorders = new();
        private List<TextBlock> _playerTextBlocks = new();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            PopulateTextBlocksList();
            PopulateBorderList();
        }
        #endregion

        #region Methods
        public void DisplayPlayers(Dictionary<Key, IPlayer> registeredPlayers)
        {
            var playerNumber = 0;

            foreach (var (key, player) in registeredPlayers)
            {
                var boxContent =
                    "Name: " + player.Name + "\n" +
                    "Mode: " + player.GetPlayerType() + "\n" +
                    "Key: " + key + "\n";

                boxContent += !Timer.GetInstance().WaitingToStart ? "Score: " + player.Score : "Score: 0";

                _playerTextBlocks[playerNumber].Text = boxContent;

                _playerBorders[playerNumber].Opacity = 1;

                playerNumber++;
            }
        }

        public void HidePlayerBorders()
        {
            foreach (var border in _playerBorders)
            {
                border.Opacity = 0;
            }
        }

        private void PopulateTextBlocksList()
        {
            _playerTextBlocks.Add(Player1Text);
            _playerTextBlocks.Add(Player2Text);
            _playerTextBlocks.Add(Player3Text);
            _playerTextBlocks.Add(Player4Text);
            _playerTextBlocks.Add(Player5Text);
            _playerTextBlocks.Add(Player6Text);
        }

        private void PopulateBorderList()
        {
            _playerBorders.Add(Player1Border);
            _playerBorders.Add(Player2Border);
            _playerBorders.Add(Player3Border);
            _playerBorders.Add(Player4Border);
            _playerBorders.Add(Player5Border);
            _playerBorders.Add(Player6Border);
        }
    }
    #endregion
}