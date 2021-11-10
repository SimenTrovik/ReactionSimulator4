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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareDesignExam.WPF {
    /// <summary>
    /// Interaction logic for RegisterPlayerPage.xaml
    /// </summary>

    public delegate void RegisteredPlayerEvent(string name, PlayerType playerType, Key key);

    public partial class RegisterPlayerPage : Page {
        private bool isListeningForKeys = false;
        private Key currentKey;
        public RegisteredPlayerEvent[] registeredPlayerEvents;

        public RegisterPlayerPage() {
            InitializeComponent();
            KeyPressGrid.Opacity = 0;
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e) {
            KeyPressGrid.Opacity = 1;
            isListeningForKeys = true;
        }

        private void RegisterPlayerP_KeyDown(object sender, KeyEventArgs e) {
            if (isListeningForKeys && e.Key is >= Key.A and <= Key.Z) {
                SetCurrentKey(e.Key);
            }
        }

        private void SetCurrentKey(Key key) {
            currentKey = key;
            CurrKey.Text = "Your chosen key: " + currentKey;
        }

        private void ConfirmPlayerButton_Click(object sender, RoutedEventArgs e) {
            string name = InputNameTextBox.Text;
            PlayerType playerType = PlayerType.Normal;
            if (NormalRadio.IsChecked != null && NormalRadio.IsChecked.Value) {
                playerType = PlayerType.Normal;
            } else if (EasyRadio.IsChecked != null && EasyRadio.IsChecked.Value) {
                playerType = PlayerType.Easy;
            }

            Key key = currentKey;
            foreach (var registeredPlayerEvent in registeredPlayerEvents) {
                registeredPlayerEvent.Invoke(name, playerType, key);
            }

            ResetFields();
        }

        private void ResetFields() {
            NormalRadio.IsChecked = true;
            InputNameTextBox.Text = "";
            currentKey = new();
        }
    }
}
