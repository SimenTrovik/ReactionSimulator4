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
    }
}
