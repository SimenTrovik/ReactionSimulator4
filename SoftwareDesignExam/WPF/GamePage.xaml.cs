using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SoftwareDesignExam.WPF
{
    #region Delegates
    public delegate void RegisteredPlayerClickEvent(object sender, KeyEventArgs e);
    public delegate void PlayAgainClickEvent(object sender, EventArgs e);
    public delegate void ShowMenuClickEvent(object sender, EventArgs e);
    #endregion

    public partial class GamePage
    {
        #region Fields
        public event RegisteredPlayerClickEvent RegisteredPlayerClickEvent;
        public event PlayAgainClickEvent PlayAgainClickEvent;
        public event ShowMenuClickEvent ShowMenuClickEvent;

        private readonly Timer _timer = Timer.GetInstance();
        #endregion

        #region Constructor
        public GamePage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        // This is called at the start of a new game. Handles everything the WPF needs to do during a game.
        public void Start()
        {
            // This is threaded because the graphics on the WPF is not updated if this is on the main thread
            Task.Run(() =>
            {
                // The timer starts(Pre-Timer)
                _timer.StartTimer();
                // Changes what is shown in the WPF
                ReadyStyling();
                HideOptions();
                // Waits until the Pre-Timer is done
                WaitForGoSignal();
                // Changes what is shown in the WPF
                GameOnStyling();
                UpdateTimerTextWithCountdown();
                // Changes the WPF when the time is over
                TimesUpStyling();
                // Makes the option buttons visible
                ShowOptions();
            });
        }

        // Pauses for the duration of the Pre-Timer
        private void WaitForGoSignal()
        {
            while (_timer.GetTimeMs() == 0) { Thread.Sleep(10); }
        }

        // Updates the timer on screen
        private void UpdateTimerTextWithCountdown()
        {
            while (_timer.TimeLeft() > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    TimerText.Text = _timer.TimeLeft().ToString();
                });
                Thread.Sleep(10);
            }
        }

        public void ShowOptions()
        {
            Dispatcher.Invoke(() =>
            {
                PlayAgainButton.Opacity = 1;
                MenuButton.Opacity = 1;
            });
        }

        public void HideOptions()
        {
            Dispatcher.Invoke(() =>
            {
                PlayAgainButton.Opacity = 0;
                MenuButton.Opacity = 0;
            });
        }

        // Displays the winner of a game on screen
        public void DisplayWinner(IPlayer winner)
        {
            Dispatcher.Invoke(() =>
            {
                HeaderText.Text = $"{winner.Name} won! Score: {winner.Score}";
            });
        }

        #endregion

        #region EventInvokers
        public void RegisterPlayerClick_KeyDown(object sender, KeyEventArgs e)
        {
            RegisteredPlayerClickEvent.Invoke(this, e);
        }

        private void PlayAgain(object sender, EventArgs e)
        {
            PlayAgainClickEvent.Invoke(this, e);
        }

        private void ShowMenu(object sender, EventArgs e)
        {
            ShowMenuClickEvent.Invoke(this, e);
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += RegisterPlayerClick_KeyDown;
        }
        #endregion

        #region Styling
        private void ReadyStyling()
        {
            Dispatcher.Invoke(() =>
            {
                HeaderText.Text = "Press your key when the light turns green!";
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
        #endregion
    }
}
