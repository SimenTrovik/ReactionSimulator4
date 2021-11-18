using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    public class PlayerManager
    {
        #region Fields
        private readonly ConcretePlayerFactory _playerFactory;
        private readonly Dictionary<Key, IPlayer> _playerDictionary;
        public PlayerManager()
        {
            _playerFactory = new ConcretePlayerFactory();
            _playerDictionary = new Dictionary<Key, IPlayer>();
        }
        #endregion

        #region Constructor
        // Adds a new player to the dictionary
        // Returns true/false if the addition was successful or not.
        public bool AddPlayer(string name, PlayerType type, Key key)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var newPlayer = _playerFactory.GetPlayer(name, type);
            return _playerDictionary.TryAdd(key, newPlayer);
        }
        #endregion

        #region Methods
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
        #endregion

        #region Getters/Setters
        public bool IsKeyTaken(Key key)
        {
            return _playerDictionary.ContainsKey(key);
        }

        public int GetAmountOfPlayers()
        {
            return _playerDictionary.Count;
        }

        public IPlayer GetWinner()
        {
            Key maxKey = Key.None;
            int maxScore = -1; 
            foreach (var (key, value) in _playerDictionary.Where(keyValuePair => keyValuePair.Value.Score > maxScore))
            {
                maxScore= value.Score;
                maxKey = key;
            }
            return maxScore == 0 ? _playerFactory.GetPlayer("No one", PlayerType.Normal) : _playerDictionary[maxKey];
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

        public Dictionary<Key, IPlayer> GetPlayerDictionary()
        {
            return _playerDictionary;
        }
        #endregion

    }
    public enum PlayerType
    {
        Normal,
        Easy
    }
}