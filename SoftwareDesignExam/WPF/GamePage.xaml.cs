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

namespace SoftwareDesignExam.WPF {
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page {
        public GamePage() {
            InitializeComponent();

            StartGame();
        }

        private void StartGame() {
            Timer timer = Timer.Instance();
            timer.StartTimer();
            while (timer.GetTimeMs() == 0) {
                //
            }
            
            while (timer.GetTimeMs() > 0 && !timer.FinishedTimer) {
                TimerText.Text = timer.GetTimeMs().ToString();
            }
        }
    }
}
