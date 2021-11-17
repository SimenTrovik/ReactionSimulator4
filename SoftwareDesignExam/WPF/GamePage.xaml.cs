using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private int _playerNumber;

        private Timer timer = Timer.Instance();

        public GamePage()
        {
            InitializeComponent();
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

        private void ShowMeny(object sender, EventArgs e)
        {
            showMenuClickEvent.Invoke(this, e);
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += RegisterPlayerClick_KeyDown;
        }

        public void DisplayScoreByPlayer(IPlayer player)
        {
            ScoreText.Text +=
                $"\n {player.Name}: {player.Score}";
        }

        private void ReadyStyling()
        {
            Dispatcher.Invoke(() =>
            {
                ScoreText.Text = "Scores:";
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
            string name = "";
            string difficulty = "";
            string key = "";

            foreach (KeyValuePair<Key, IPlayer> kvp in registeredPlayers)
            {
                name = "Name: " + kvp.Value.Name;
                difficulty = "Difficulty: " + kvp.Value.GetPlayerType().ToString();
                key = "Key: " + kvp.Key.ToString();
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
                        Box4Key.Text = key;
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

            

        }
    }
}
