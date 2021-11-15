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

namespace SoftwareDesignExam.WPF {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    /// 

    public delegate void RegisteredPlayerClickEvent(Object sender, KeyEventArgs e);

    public partial class GamePage : Page {
        public event RegisteredPlayerClickEvent registeredPlayerClickEvent;
       

        public void RegisterPlayerClick_KeyDown(object sender, KeyEventArgs e) {
            registeredPlayerClickEvent.Invoke(this, e);
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e) {
            var window = Window.GetWindow(this);
            window.KeyDown += RegisterPlayerClick_KeyDown;
        }

        private Timer timer = Timer.Instance();
        public GamePage() {
            InitializeComponent();

            //Task.Run(() => {
            //    timer.StartTimer();

            //    while(timer.GetTimeMs() == 0) { Thread.Sleep(10); }

            //    Dispatcher.Invoke(() => { TrafficLight.Fill = Colors.Green; });

            //    while (timer.TimeLeft() > 0) {  
            //        Dispatcher.Invoke(() => { TimerText.Text = timer.TimeLeft().ToString(); });
            //        Thread.Sleep(10);
            //    }

            //    Dispatcher.Invoke(() => {
            //        TimerText.Text = "Time's up!";
            //        TrafficLight.Fill = Colors.Red;
            //    });
            //});
        }
    }

    public static class Colors {
        public static SolidColorBrush Green { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("Green");
        public static SolidColorBrush Red { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkRed");
    }
}
