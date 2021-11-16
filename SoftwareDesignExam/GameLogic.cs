﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    class GameLogic
    {
        private PlayerManager _playerManager = new();
        private ScoreDao _playerDao = new();
        private MainWindow _mainWindow;
        private Timer _timer = Timer.Instance();
        private List<Key> _activePlayerKeys = new();
        private RegisterPlayerPage _registerPlayerPage = new();
        private MenyPage _menyPage = new();
        private GamePage _gamePage = new();

        public GameLogic()
        {
            _mainWindow = new MainWindow();

            _mainWindow.Show();

            SetupPagesWithDelegateEvents();

            NavigateToMenuPage();
        }

        private void ClearListedPlayers()
        {
            _playerManager.ResetPlayers();
            _registerPlayerPage.ClearDisplayedPlayers();
        }

        private void ActiveGame()
        {
            _activePlayerKeys = _playerManager.GetPlayerKeys();
            _gamePage.Start();
            Task.Run(() => {
                while (_activePlayerKeys.Count != 0)
                {

                }
                _gamePage.Stop();
            });
        }

        private void NavigateToMenuPage() 
        {
            _mainWindow.MainFrame.Navigate(_menyPage);
        }

        private void NavigateToRegisterPlayerPage() 
        {
            ClearListedPlayers();
            _mainWindow.MainFrame.Navigate(_registerPlayerPage);
        }

        private void NavigateToGamePage() 
        {
            _mainWindow.MainFrame.Navigate(_gamePage);
        }

        private void NavigateToRegisterPlayersPageEventHandler(object sender, EventArgs e) 
        {
            NavigateToRegisterPlayerPage();
        }

        private void NavigateToMenuPageEventHandler(object sender, EventArgs e) 
        {
            NavigateToMenuPage();
        }

        private void DisplayPlayersEventHandler(Object sender, PlayerEventArgs e)
        {
            _registerPlayerPage.DisplayPlayer(e);
        }

        private void AddPlayerEventHandler(Object sender, PlayerEventArgs e)
        {
            _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGameEventHandler(object sender, EventArgs e)
        {
            NavigateToGamePage();
            ActiveGame();
        }

        private void PlayAgainEventHandler(object sender, EventArgs e) 
        {
            ActiveGame();
        }

        private void RegisterInputEventHandler(object sender, KeyEventArgs e)
        {
            if (_activePlayerKeys.Contains(e.Key))
            {
                int time = _timer.TimeLeft();
                _playerManager.RegisterTime(e.Key, time);
                _activePlayerKeys.Remove(e.Key);
                _gamePage.DisplayScoreByPlayer(_playerManager.GetPlayerByKey(e.Key));
            }
        }

        private void SetupPagesWithDelegateEvents() 
        {
            _registerPlayerPage.registeredPlayerEvents += AddPlayerEventHandler;
            _registerPlayerPage.registeredPlayerEvents += DisplayPlayersEventHandler;
            _registerPlayerPage.startGameEvent += StartGameEventHandler;

            _gamePage.registeredPlayerClickEvent += RegisterInputEventHandler;
            _gamePage.playAgainClickEvent += PlayAgainEventHandler;
            _gamePage.showMenyClickEvent += NavigateToMenuPageEventHandler;

            _menyPage.startNewGameEvent += NavigateToRegisterPlayersPageEventHandler;
        }

        private void PrintHighscore()
        {
            //
        }

        private void PrintPlayer(int boxId, IPlayer p)
        {
            //
        }

        private void PrintTimer()
        {
            //
        }
    }
}
