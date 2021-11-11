using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    public class PlayerEventArgs : EventArgs
    {
        public string Name { get; set; }
        public PlayerType PlayerType { get; set; }
        public Key Key { get; set; }
    }
}