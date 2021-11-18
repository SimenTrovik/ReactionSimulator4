using System;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    // Custom EventArgs that can set and get Name, PlayerType and Key
    public class PlayerEventArgs : EventArgs
    {
        public string Name { get; set; }
        public PlayerType PlayerType { get; set; }
        public Key Key { get; set; }
    }
}