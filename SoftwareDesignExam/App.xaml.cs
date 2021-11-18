using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareDesignExam
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    // This class instantiates GameLogic class, which works as our WPF MainWindow class
    public partial class App : Application
    {
        public void Application_Startup(object sender, StartupEventArgs e) {
            GameLogic gameLogic = new();
        }
    }
}