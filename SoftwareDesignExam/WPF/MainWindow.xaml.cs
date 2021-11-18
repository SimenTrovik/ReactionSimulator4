using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private List<Border> _playerBorders = new();
        private List<TextBlock> _playerTextBlocks = new();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            PopulateTextBoxList();
            PopulateBorderList();
        }
        #endregion

        #region Methods
        public void DisplayPlayers(Dictionary<Key, IPlayer> registeredPlayers)
        {
            int _playerNumber = 0;

            foreach (KeyValuePair<Key, IPlayer> kvp in registeredPlayers)
            {
                string boxContent =
                    "Name: " + kvp.Value.Name + "\n" +
                    "Mode: " + kvp.Value.GetPlayerType() + "\n" +
                    "Key: " + kvp.Key.ToString() + "\n";

                if (!Timer.GetInstance().WaitingToStart)
                {
                    boxContent += "Score: " + kvp.Value.Score;
                } else {
                    boxContent += "Score: 0";
                }

                _playerTextBlocks[_playerNumber].Text = boxContent;

                _playerBorders[_playerNumber].Opacity = 1;

                _playerNumber++;
            }
        }

        public void HidePlayerBoxes()
        {
            for (int i = 0; i < _playerBorders.Count; i++)
            {
                _playerBorders[i].Opacity = 0;
            }
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
        #endregion
}