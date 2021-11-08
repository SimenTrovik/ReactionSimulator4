using NUnit.Framework;
using SoftwareDesignExam;

namespace Tests
{
    public class WpfTest
    {
        [SetUp]
        public void Init()
        {
            MainWindow mainWindow = new();
            mainWindow.InitializeComponent();
        }

        [Test]
        public void ShouldDisplayWindowsWhenClicked()
        {
            
        }
    }
}