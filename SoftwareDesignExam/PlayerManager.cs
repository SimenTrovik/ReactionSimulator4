using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    internal class PlayerManager
    {
        private readonly ConcretePlayerFactory _playerFactory;
        private readonly Dictionary<Key, IPlayer> _playerDictionary;
        public PlayerManager()
        {
            _playerFactory = new ConcretePlayerFactory();
            _playerDictionary = new Dictionary<Key, IPlayer>();
        }

        // Adds a new player to the dictionary, if the provided keyboard key is valid.
        // Return true/false if the addition was successful or not.
        // TODO Move input validation to IOManager?
        public bool AddPlayer(string name, PlayerFactory.PlayerType type, Key key)
        {
            if (key is < Key.A or > Key.Z) return false;
            var newPlayer = _playerFactory.GetPlayer(name, type);
            return _playerDictionary.TryAdd(key, newPlayer);
        }
        public void ResetPlayers()
        {
            _playerDictionary.Clear();
        }

        public List<IPlayer> GetPlayers()
        {
            return null;
        }

        public void SetScore(Key key, int score)
        {

        }
    }
}
