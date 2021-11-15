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

    public delegate void RegisteredPlayerClickEvent(Object sender, KeyEventArgs e);
    public delegate void PlayAgainClickEvent(Object sender, EventArgs e);
    public delegate void ShowMenyClickEvent(Object sender, EventArgs e);

    public partial class GamePage : Page
    {
        public event RegisteredPlayerClickEvent registeredPlayerClickEvent;
        public event PlayAgainClickEvent playAgainClickEvent;
        public event ShowMenyClickEvent showMenyClickEvent;

        public void RegisterPlayerClick_KeyDown(object sender, KeyEventArgs e)
        {
            registeredPlayerClickEvent.Invoke(this, e);
        }

        private void PlayAgain(object sender, EventArgs e) {
            playAgainClickEvent.Invoke(this, e);
            ScoreText.Text = "Scores:";
            TimerText.Text = "Get ready...";
            TrafficLight.Fill = Colors.Yellow;
        }

        private void ShowMeny(object sender, EventArgs e) {
            showMenyClickEvent.Invoke(this, e);        
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += RegisterPlayerClick_KeyDown;
        }

        private Timer timer = Timer.Instance();
        public GamePage()
        {
            InitializeComponent();

            /* 



             */
        }

        public void Start()
        {
            Task.Run(() =>
            {
                Timer timer = Timer.Instance();
                timer.StartTimer();
                while (timer.GetTimeMs() == 0) { Thread.Sleep(10); }


                Dispatcher.Invoke(() => { TrafficLight.Fill = Colors.Green; });
                while (timer.TimeLeft() > 0)
                {
                    Dispatcher.Invoke(() => { TimerText.Text = timer.TimeLeft().ToString(); });
                    Thread.Sleep(10);
                }

                Dispatcher.Invoke(() =>
                {
                    TimerText.Text = "Time's up!";
                    TrafficLight.Fill = Colors.Red;
                });
            });
        }

        public void Stop()
        {
            Timer timer = Timer.Instance();
            timer.TimesUp();

        }
    }

    public static class Colors
    {
        public static SolidColorBrush Green { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("Green");
        public static SolidColorBrush Red { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkRed");
        public static SolidColorBrush Yellow { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("Yellow");
    }
}
