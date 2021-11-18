using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    /*  
     * This class is used for keeping track of information about the players participating in the game.
     * It uses a ConcretePlayerFactory to create IPlayer 
     */ 
    public class PlayerManager
    {
        #region Fields
        private readonly ConcretePlayerFactory _playerFactory;
        // Stores the IPlayer objects, with the Keyboard Key they chose as Key in the dictionary
        private readonly Dictionary<Key, IPlayer> _playerDictionary;
        #endregion

        #region Constructor
        public PlayerManager()
        {
            _playerFactory = new ConcretePlayerFactory();
            _playerDictionary = new Dictionary<Key, IPlayer>();
        }
        #endregion

        #region Methods
        // Adds a new player to the dictionary
        // Returns true/false if the addition was successful or not.
        public bool AddPlayer(string name, PlayerType type, Key key)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var newPlayer = _playerFactory.GetPlayer(name, type);
            return _playerDictionary.TryAdd(key, newPlayer);
        }

        public IPlayer GetWinner()
        {
            IPlayer leadingPlayer = _playerFactory.GetPlayer("No one", PlayerType.Normal);
            foreach ((Key key, IPlayer player) in _playerDictionary)
            {
                if (player > leadingPlayer)
                {
                    leadingPlayer = player;
                }
            }
            return leadingPlayer;
        }

        public void ResetPlayers()
        {
            _playerDictionary.Clear();
        }

        public void ResetScores()
        {
            foreach (var player in _playerDictionary)
            {
                player.Value.Score = 0;
            }
        }

        public void RegisterPlayerReactionTime(Key key, int time)
        {
            _playerDictionary[key].TimeInMs = time;
        }

        public bool IsKeyTaken(Key key)
        {
            return _playerDictionary.ContainsKey(key);
        }

        public int GetAmountOfPlayers()
        {
            return _playerDictionary.Count;
        }

        public List<Key> GetPlayerKeyList()
        {
            return _playerDictionary.Keys.ToList();
        }

        public IPlayer GetPlayerByKey(Key key)
        {
            return _playerDictionary[key];
        }

        public List<IPlayer> GetPlayerList()
        {
            return _playerDictionary.Values.ToList();
        }

        // Returns the entire dictionary
        public Dictionary<Key, IPlayer> GetPlayerDictionary()
        {
            return _playerDictionary;
        }
        #endregion
    }
    // This enum keeps track of the available difficulties players can choose
    public enum PlayerType
    {
        Normal,
        Easy
    }
}