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
        private readonly ConcretePlayerFactory _playerFactory;
        // Stores the IPlayer objects, with the Keyboard Key they chose as Key in the dictionary
        private readonly Dictionary<Key, IPlayer> _playerDictionary;
        public PlayerManager()
        {
            _playerFactory = new ConcretePlayerFactory();
            _playerDictionary = new Dictionary<Key, IPlayer>();
        }

        // Adds a new player to the dictionary
        // Returns true/false if the addition was successful or not.
        public bool AddPlayer(string name, PlayerType type, Key key)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var newPlayer = _playerFactory.GetPlayer(name, type);
            return _playerDictionary.TryAdd(key, newPlayer);
        }
        // Removes all registered players
        public void ResetPlayers()
        {
            _playerDictionary.Clear();
        }
        // Sets the score of all players to 0
        public void ResetScores()
        {
            foreach (var player in _playerDictionary)
            {
                player.Value.Score = 0;
            }
        }
        // Returns a List of all Keys in use
        public List<Key> GetPlayerKeyList()
        {
            return _playerDictionary.Keys.ToList();
        }
        // Returns the player that uses the provided key
        public IPlayer GetPlayerByKey(Key key)
        {
            return _playerDictionary[key];
        }
        // Returns all player objects as a List
        public List<IPlayer> GetPlayerList()
        {
            return _playerDictionary.Values.ToList();
        }
        // Returns the entire
        public Dictionary<Key, IPlayer> GetPlayerDictionary()
        {
            return _playerDictionary;
        }
        // Registers a reaction time for a player. Their score is calculated in the IPlayers Score property
        public void RegisterPlayerReactionTime(Key key, int time)
        {
            _playerDictionary[key].TimeInMs = time;
        }
        // Returns the amount of players registered
        public int GetAmountOfPlayers()
        {
            return _playerDictionary.Count;
        }
        // Returns true if the provided Key is already in use by a player
        public bool IsKeyTaken(Key key)
        {
            return _playerDictionary.ContainsKey(key);
        }

        // Returns the player with the highest score.
        // If all players have a score of 0, it will return a IPlayer object with the name "No one"
        public IPlayer GetWinner()
        {
            Key maxKey = Key.None;
            int maxScore = 0;
            foreach (var keyValuePair1 in _playerDictionary.Where(keyValuePair => keyValuePair.Value.Score > maxScore))
            {
                maxScore = keyValuePair1.Value.Score;
                maxKey = keyValuePair1.Key;
            }
            return maxScore == 0 ? _playerFactory.GetPlayer("No one", PlayerType.Normal) : _playerDictionary[maxKey];
        }
    }
    // This enum keeps track of the available difficulties players can choose
    public enum PlayerType
    {
        Normal,
        Easy
    }
}
