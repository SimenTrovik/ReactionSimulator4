using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SoftwareDesignExam.WPF
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    /// 

    public delegate void RegisteredPlayerClickEvent(object sender, KeyEventArgs e);
    public delegate void PlayAgainClickEvent(object sender, EventArgs e);
    public delegate void ShowMenuClickEvent(object sender, EventArgs e);

    public partial class GamePage : Page
    {
        public event RegisteredPlayerClickEvent registeredPlayerClickEvent;
        public event PlayAgainClickEvent playAgainClickEvent;
        public event ShowMenuClickEvent showMenuClickEvent;

        private Timer timer = Timer.GetInstance();

        private List<Border> _playerBorders = new();
        private List<TextBlock> _playerTextBoxes = new();

        public GamePage()
        {
            InitializeComponent();
            PopulateTextBoxList();
            PopulateBorderList();
        }

        public void Start()
        {
            Task.Run(() =>
            {
                timer.StartTimer();

                ReadyStyling();

                HideOptions();

                WaitForGoSignal();

                GameOnStyling();

                UpdateTimerTextWithCountdown();

                TimesUpStyling();

                ShowOptions();
            });
        }

        private void WaitForGoSignal()
        {
            while (timer.GetTimeMs() == 0) { Thread.Sleep(10); }
        }

        private void UpdateTimerTextWithCountdown()
        {
            while (timer.TimeLeft() > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    TimerText.Text = timer.TimeLeft().ToString();
                });
                Thread.Sleep(10);
            }
        }

        public void ShowOptions()
        {
            Dispatcher.Invoke(() =>
            {
                PlayAgainButton.Opacity = 1;
                MenyButton.Opacity = 1;
            });
        }

        public void HideOptions()
        {
            Dispatcher.Invoke(() =>
            {
                PlayAgainButton.Opacity = 0;
                MenyButton.Opacity = 0;
            });
        }

        public void Stop()
        {
            timer.TimesUp();
        }

        public void RegisterPlayerClick_KeyDown(object sender, KeyEventArgs e)
        {
            registeredPlayerClickEvent.Invoke(this, e);
        }

        private void PlayAgain(object sender, EventArgs e)
        {
            playAgainClickEvent.Invoke(this, e);
        }

        private void ShowMenu(object sender, EventArgs e)
        {
            HidePlayerBoxes();
            showMenuClickEvent.Invoke(this, e);
        }

        private void HidePlayerBoxes()
        {
            for (int i = 0; i < _playerBorders.Count; i++)
            {
                _playerBorders[i].Opacity = 0;
            }
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += RegisterPlayerClick_KeyDown;
        }

        private void ReadyStyling()
        {
            Dispatcher.Invoke(() =>
            {
                TimerText.Text = "Get ready...";
                TrafficLight.Fill = Colors.Yellow;
            });
        }

        private void GameOnStyling()
        {
            Dispatcher.Invoke(() =>
            {
                TrafficLight.Fill = Colors.Green;
            });
        }

        private void TimesUpStyling()
        {
            Dispatcher.Invoke(() =>
            {
                TimerText.Text = "Done!";
                TrafficLight.Fill = Colors.Red;
            });
        }

        public void DisplayPlayers(Dictionary<Key, IPlayer> registeredPlayers)
        {
            int _playerNumber = 0;

            foreach (KeyValuePair<Key, IPlayer> kvp in registeredPlayers)
            {
                _playerTextBoxes[_playerNumber].Text =
                    "Name: " + kvp.Value.Name + "\n" +
                    "Difficulty: " + kvp.Value.GetPlayerType() + "\n" +
                    "Key: " + kvp.Key.ToString() + "\n" +
                    "Score: " + kvp.Value.Score;
                _playerBorders[_playerNumber].Opacity = 1;

                _playerNumber++;
            }
        }

        void PopulateTextBoxList()
        {
            _playerTextBoxes.Add(Player1Text);
            _playerTextBoxes.Add(Player2Text);
            _playerTextBoxes.Add(Player3Text);
            _playerTextBoxes.Add(Player4Text);
            _playerTextBoxes.Add(Player5Text);
            _playerTextBoxes.Add(Player6Text);
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
