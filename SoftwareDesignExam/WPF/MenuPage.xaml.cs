using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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

    public delegate void StartNewGameEvent(object sender, EventArgs e);

    public partial class MenyPage : Page {
        public event StartNewGameEvent startNewGameEvent;

        public MenyPage() {
            InitializeComponent();
        }

        private void NewGame(object sender, EventArgs e) {
            startNewGameEvent.Invoke(this, e);
        }

        private void ShowHighscores(object sender, EventArgs e) {
            MessageBox.Show("HIGHSCORES!");
        }

    };
}
