﻿using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace SoftwareDesignExam
{
    public class PlayerManager
    {
        private readonly ConcretePlayerFactory _playerFactory;
        public readonly Dictionary<Key, IPlayer> _playerDictionary;
        public PlayerManager()
        {
            _playerFactory = new ConcretePlayerFactory();
            _playerDictionary = new Dictionary<Key, IPlayer>();
        }

        // Adds a new player to the dictionary
        // Returns true/false if the addition was successful or not.
        public bool AddPlayer(string name, PlayerType type, Key key)
        {
            var newPlayer = _playerFactory.GetPlayer(name, type);
            return _playerDictionary.TryAdd(key, newPlayer);
        }
        public void ResetPlayers()
        {
            _playerDictionary.Clear();
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

        public Dictionary<Key,IPlayer> GetPlayerDictionary()
        {
            return _playerDictionary;
        }

        public void RegisterPlayerReactionTime(Key key, int time)
        {
            _playerDictionary[key].TimeInMs = time;
        }

        // Remove?
        public bool RemovePlayer(Key key)
        {
            return _playerDictionary.Remove(key);
        }

        public int GetAmountOfPlayers()
        {
            return _playerDictionary.Count;
        }

        public bool IsKeyTaken(Key key)
        {
            return _playerDictionary.ContainsKey(key);
        }
    }
    public enum PlayerType
    {
        Normal,
        Easy
    }
}
