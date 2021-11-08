using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoftwareDesignExam
{
    /// <summary>
    /// Interaction logic for RegisterKeyWindow.xaml
    /// </summary>
    public partial class RegisterKeyWindow : Window
    {
        public RegisterKeyWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MainWindow mainWindow = new();
            PlayerManager playerManager = new();
            playerManager.AddPlayer(mainWindow.RegisteredPlayers, PlayerType.Normal, e.Key);
        }
    }
}