using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareDesignExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<String> _registeredPlayers = new();
        string _registeredPlayer = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterPlayerClick(object sender, RoutedEventArgs e)
        {
             
            _registeredPlayer = registeredPlayer.Text;
            RegisterKeyWindow registerKeyWindow = new();
            registerKeyWindow.Show();
        }

        public String RegisteredPlayers => _registeredPlayer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new();
            gameWindow.Show();
        }
    }
}
